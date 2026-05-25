using System;
using System.Diagnostics;
using System.Threading;

namespace GameTypes
{
	// Token: 0x02000009 RID: 9
	public static class D
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002E7C File Offset: 0x0000107C
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static event D.LogHandler onDLog
		{
			add
			{
				D.LogHandler logHandler = D.onDLog;
				D.LogHandler logHandler2;
				do
				{
					logHandler2 = logHandler;
					logHandler = Interlocked.CompareExchange<D.LogHandler>(ref D.onDLog, (D.LogHandler)Delegate.Combine(logHandler2, value), logHandler);
				}
				while (logHandler != logHandler2);
			}
			remove
			{
				D.LogHandler logHandler = D.onDLog;
				D.LogHandler logHandler2;
				do
				{
					logHandler2 = logHandler;
					logHandler = Interlocked.CompareExchange<D.LogHandler>(ref D.onDLog, (D.LogHandler)Delegate.Remove(logHandler2, value), logHandler);
				}
				while (logHandler != logHandler2);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002EF4 File Offset: 0x000010F4
		public static void isNull(object pObject)
		{
			if (pObject == null)
			{
				StackFrame stackFrame = new StackFrame(1, true);
				string text = string.Format("Line: {0}\r\nColumn: {1}\r\nWhere:{2}", stackFrame.GetFileLineNumber(), stackFrame.GetFileColumnNumber(), stackFrame.GetMethod().Name);
				throw new NullReferenceException(text);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F44 File Offset: 0x00001144
		public static void isNull(object pObject, string pMessage)
		{
			if (pObject == null)
			{
				throw new NullReferenceException(pMessage);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F54 File Offset: 0x00001154
		public static void assert(bool pCondition)
		{
			if (!pCondition)
			{
				StackFrame stackFrame = new StackFrame(1, true);
				string text = string.Format("Line: {0}\r\nColumn: {1}\r\nWhere:{2}", stackFrame.GetFileLineNumber(), stackFrame.GetFileColumnNumber(), stackFrame.GetMethod().Name);
				throw new Exception(text);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FA4 File Offset: 0x000011A4
		public static void assert(bool pCondition, string pMessage)
		{
			if (!pCondition)
			{
				throw new Exception(pMessage);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002FB4 File Offset: 0x000011B4
		public static void Log(string pMessage)
		{
			if (D.onDLog != null)
			{
				D.onDLog(pMessage);
				return;
			}
			throw new Exception(pMessage + " (to log this message instead of receiving an exception, listen to D.onDLog)");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static void LogError(string pMessage)
		{
			throw new Exception(pMessage);
		}

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000047 RID: 71
		public delegate void LogHandler(string pMessage);
	}
}
