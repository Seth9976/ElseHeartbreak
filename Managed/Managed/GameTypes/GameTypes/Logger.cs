using System;
using System.Threading;

namespace GameTypes
{
	// Token: 0x0200000B RID: 11
	public class Logger
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x00003030 File Offset: 0x00001230
		private event D.LogHandler onLog
		{
			add
			{
				D.LogHandler logHandler = this.onLog;
				D.LogHandler logHandler2;
				do
				{
					logHandler2 = logHandler;
					logHandler = Interlocked.CompareExchange<D.LogHandler>(ref this.onLog, (D.LogHandler)Delegate.Combine(logHandler2, value), logHandler);
				}
				while (logHandler != logHandler2);
			}
			remove
			{
				D.LogHandler logHandler = this.onLog;
				D.LogHandler logHandler2;
				do
				{
					logHandler2 = logHandler;
					logHandler = Interlocked.CompareExchange<D.LogHandler>(ref this.onLog, (D.LogHandler)Delegate.Remove(logHandler2, value), logHandler);
				}
				while (logHandler != logHandler2);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000306C File Offset: 0x0000126C
		public void Log(string pMessage)
		{
			if (this.onLog != null)
			{
				this.onLog(pMessage);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003088 File Offset: 0x00001288
		public void AddListener(D.LogHandler pOnLog)
		{
			this.onLog += pOnLog;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003094 File Offset: 0x00001294
		public void RemoveListener(D.LogHandler pOnLog)
		{
			this.onLog -= pOnLog;
		}
	}
}
