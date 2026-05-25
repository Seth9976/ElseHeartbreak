using System;

namespace System.Threading
{
	// Token: 0x020006A3 RID: 1699
	internal class LockQueue
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x000DF37C File Offset: 0x000DD57C
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000DF38C File Offset: 0x000DD58C
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool flag2;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					flag2 = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return flag2;
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x000DF434 File Offset: 0x000DD634
		public bool IsEmpty
		{
			get
			{
				bool flag;
				lock (this)
				{
					flag = this.lockCount == 0;
				}
				return flag;
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000DF484 File Offset: 0x000DD684
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000DF4C8 File Offset: 0x000DD6C8
		public void PulseAll()
		{
			lock (this)
			{
				Monitor.PulseAll(this);
			}
		}

		// Token: 0x04001BB7 RID: 7095
		private ReaderWriterLock rwlock;

		// Token: 0x04001BB8 RID: 7096
		private int lockCount;
	}
}
