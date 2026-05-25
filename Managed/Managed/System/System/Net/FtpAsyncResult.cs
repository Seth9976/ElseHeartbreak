using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000307 RID: 775
	internal class FtpAsyncResult : IAsyncResult
	{
		// Token: 0x06001AAC RID: 6828 RVA: 0x0004B2F8 File Offset: 0x000494F8
		public FtpAsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0004B31C File Offset: 0x0004951C
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0004B324 File Offset: 0x00049524
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.waitHandle == null)
					{
						this.waitHandle = new ManualResetEvent(false);
					}
				}
				return this.waitHandle;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0004B384 File Offset: 0x00049584
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0004B38C File Offset: 0x0004958C
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag;
				lock (obj)
				{
					flag = this.completed;
				}
				return flag;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0004B3DC File Offset: 0x000495DC
		internal bool GotException
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0004B3EC File Offset: 0x000495EC
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0004B3F4 File Offset: 0x000495F4
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0004B3FC File Offset: 0x000495FC
		internal FtpWebResponse Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0004B408 File Offset: 0x00049608
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0004B410 File Offset: 0x00049610
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0004B41C File Offset: 0x0004961C
		internal void WaitUntilComplete()
		{
			if (this.IsCompleted)
			{
				return;
			}
			this.AsyncWaitHandle.WaitOne();
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0004B438 File Offset: 0x00049638
		internal bool WaitUntilComplete(int timeout, bool exitContext)
		{
			return this.IsCompleted || this.AsyncWaitHandle.WaitOne(timeout, exitContext);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0004B460 File Offset: 0x00049660
		internal void SetCompleted(bool synch, Exception exc, FtpWebResponse response)
		{
			this.synch = synch;
			this.exception = exc;
			this.response = response;
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.waitHandle != null)
				{
					this.waitHandle.Set();
				}
			}
			this.DoCallback();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0004B4DC File Offset: 0x000496DC
		internal void SetCompleted(bool synch, FtpWebResponse response)
		{
			this.SetCompleted(synch, null, response);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0004B4E8 File Offset: 0x000496E8
		internal void SetCompleted(bool synch, Exception exc)
		{
			this.SetCompleted(synch, exc, null);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0004B4F4 File Offset: 0x000496F4
		internal void DoCallback()
		{
			if (this.callback != null)
			{
				try
				{
					this.callback(this);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0004B540 File Offset: 0x00049740
		internal void Reset()
		{
			this.exception = null;
			this.synch = false;
			this.response = null;
			this.state = null;
			object obj = this.locker;
			lock (obj)
			{
				this.completed = false;
				if (this.waitHandle != null)
				{
					this.waitHandle.Reset();
				}
			}
		}

		// Token: 0x0400106D RID: 4205
		private FtpWebResponse response;

		// Token: 0x0400106E RID: 4206
		private ManualResetEvent waitHandle;

		// Token: 0x0400106F RID: 4207
		private Exception exception;

		// Token: 0x04001070 RID: 4208
		private AsyncCallback callback;

		// Token: 0x04001071 RID: 4209
		private Stream stream;

		// Token: 0x04001072 RID: 4210
		private object state;

		// Token: 0x04001073 RID: 4211
		private bool completed;

		// Token: 0x04001074 RID: 4212
		private bool synch;

		// Token: 0x04001075 RID: 4213
		private object locker = new object();
	}
}
