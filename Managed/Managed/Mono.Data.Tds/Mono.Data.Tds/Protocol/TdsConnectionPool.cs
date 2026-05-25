using System;
using System.Collections;
using System.Threading;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000017 RID: 23
	public class TdsConnectionPool
	{
		// Token: 0x06000144 RID: 324 RVA: 0x0000D144 File Offset: 0x0000B344
		public TdsConnectionPool(TdsConnectionPoolManager manager, TdsConnectionInfo info)
		{
			this.info = info;
			this.manager = manager;
			this.conns = new ArrayList(info.PoolMaxSize);
			this.available = new Queue(info.PoolMaxSize);
			this.InitializePool();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000D190 File Offset: 0x0000B390
		private void InitializePool()
		{
			for (int i = this.conns.Count; i < this.info.PoolMinSize; i++)
			{
				try
				{
					Tds tds = this.manager.CreateConnection(this.info);
					this.conns.Add(tds);
					this.available.Enqueue(tds);
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000D218 File Offset: 0x0000B418
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000D224 File Offset: 0x0000B424
		public bool Pooling
		{
			get
			{
				return !this.no_pooling;
			}
			set
			{
				this.no_pooling = !value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000D230 File Offset: 0x0000B430
		public Tds GetConnection()
		{
			if (this.no_pooling)
			{
				return this.manager.CreateConnection(this.info);
			}
			Tds tds = null;
			int num = this.info.PoolMaxSize * 2;
			Exception ex;
			do
			{
				while (tds == null)
				{
					bool flag = false;
					Queue queue = this.available;
					lock (queue)
					{
						if (this.available.Count > 0)
						{
							tds = (Tds)this.available.Dequeue();
							break;
						}
						lock (this.conns)
						{
							if (this.conns.Count >= this.info.PoolMaxSize - this.in_progress)
							{
								Monitor.Exit(this.conns);
								if (!Monitor.Wait(this.available, this.info.Timeout * 1000))
								{
									throw new InvalidOperationException("Timeout expired. The timeout period elapsed before a connection could be obtained. A possible explanation is that all the connections in the pool are in use, and the maximum pool size is reached.");
								}
								if (this.available.Count > 0)
								{
									tds = (Tds)this.available.Dequeue();
									break;
								}
								continue;
							}
							else
							{
								flag = true;
								this.in_progress++;
							}
						}
					}
					if (flag)
					{
						try
						{
							tds = this.manager.CreateConnection(this.info);
							ArrayList arrayList = this.conns;
							lock (arrayList)
							{
								this.conns.Add(tds);
							}
							return tds;
						}
						finally
						{
							Queue queue2 = this.available;
							lock (queue2)
							{
								this.in_progress--;
							}
						}
					}
				}
				bool flag2 = true;
				ex = null;
				try
				{
					flag2 = !tds.IsConnected || !tds.Reset();
				}
				catch (Exception ex2)
				{
					flag2 = true;
					ex = ex2;
				}
				if (!flag2)
				{
					return tds;
				}
				ArrayList arrayList2 = this.conns;
				lock (arrayList2)
				{
					this.conns.Remove(tds);
				}
				tds.Disconnect();
				num--;
			}
			while (num != 0);
			throw ex;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000D510 File Offset: 0x0000B710
		public void ReleaseConnection(Tds connection)
		{
			if (connection == null)
			{
				return;
			}
			if (this.no_pooling)
			{
				connection.Disconnect();
				return;
			}
			if (connection.poolStatus == 2)
			{
				ArrayList arrayList = this.conns;
				lock (arrayList)
				{
					this.conns.Remove(connection);
				}
				connection.Disconnect();
				connection = null;
			}
			Queue queue = this.available;
			lock (queue)
			{
				if (connection != null)
				{
					this.available.Enqueue(connection);
				}
				Monitor.Pulse(this.available);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public void ResetConnectionPool()
		{
			Queue queue = this.available;
			lock (queue)
			{
				ArrayList arrayList = this.conns;
				lock (arrayList)
				{
					for (int i = this.conns.Count - 1; i >= 0; i--)
					{
						Tds tds = (Tds)this.conns[i];
						tds.poolStatus = 2;
					}
					for (int i = this.available.Count - 1; i >= 0; i--)
					{
						Tds tds = (Tds)this.available.Dequeue();
						tds.Disconnect();
						this.conns.Remove(tds);
					}
					this.available.Clear();
					this.InitializePool();
				}
				Monitor.PulseAll(this.available);
			}
		}

		// Token: 0x040000D3 RID: 211
		private TdsConnectionInfo info;

		// Token: 0x040000D4 RID: 212
		private bool no_pooling;

		// Token: 0x040000D5 RID: 213
		private TdsConnectionPoolManager manager;

		// Token: 0x040000D6 RID: 214
		private Queue available;

		// Token: 0x040000D7 RID: 215
		private ArrayList conns;

		// Token: 0x040000D8 RID: 216
		private int in_progress;
	}
}
