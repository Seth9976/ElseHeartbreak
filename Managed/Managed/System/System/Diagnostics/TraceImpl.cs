using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x0200025D RID: 605
	internal class TraceImpl
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x00037B50 File Offset: 0x00035D50
		private TraceImpl()
		{
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00037B74 File Offset: 0x00035D74
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x00037B80 File Offset: 0x00035D80
		public static bool AutoFlush
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.autoFlush;
			}
			set
			{
				TraceImpl.InitOnce();
				TraceImpl.autoFlush = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00037B90 File Offset: 0x00035D90
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x00037B9C File Offset: 0x00035D9C
		public static int IndentLevel
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.indentLevel;
			}
			set
			{
				object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
				lock (listenersSyncRoot)
				{
					TraceImpl.indentLevel = value;
					foreach (object obj in TraceImpl.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj;
						traceListener.IndentLevel = TraceImpl.indentLevel;
					}
				}
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x00037C44 File Offset: 0x00035E44
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x00037C50 File Offset: 0x00035E50
		public static int IndentSize
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.indentSize;
			}
			set
			{
				object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
				lock (listenersSyncRoot)
				{
					TraceImpl.indentSize = value;
					foreach (object obj in TraceImpl.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj;
						traceListener.IndentSize = TraceImpl.indentSize;
					}
				}
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00037CF8 File Offset: 0x00035EF8
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.listeners;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00037D04 File Offset: 0x00035F04
		private static object ListenersSyncRoot
		{
			get
			{
				return ((ICollection)TraceImpl.Listeners).SyncRoot;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00037D10 File Offset: 0x00035F10
		public static CorrelationManager CorrelationManager
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.correlation_manager;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00037D1C File Offset: 0x00035F1C
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00037D28 File Offset: 0x00035F28
		[global::System.MonoLimitation("the property exists but it does nothing.")]
		public static bool UseGlobalLock
		{
			get
			{
				TraceImpl.InitOnce();
				return TraceImpl.use_global_lock;
			}
			set
			{
				TraceImpl.InitOnce();
				TraceImpl.use_global_lock = value;
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00037D38 File Offset: 0x00035F38
		private static void InitOnce()
		{
			if (TraceImpl.initLock != null)
			{
				object obj = TraceImpl.initLock;
				lock (obj)
				{
					if (TraceImpl.listeners == null)
					{
						IDictionary settings = DiagnosticsConfiguration.Settings;
						TraceImplSettings traceImplSettings = (TraceImplSettings)settings[".__TraceInfoSettingsKey__."];
						settings.Remove(".__TraceInfoSettingsKey__.");
						TraceImpl.autoFlush = traceImplSettings.AutoFlush;
						TraceImpl.indentLevel = traceImplSettings.IndentLevel;
						TraceImpl.indentSize = traceImplSettings.IndentSize;
						TraceImpl.listeners = traceImplSettings.Listeners;
					}
				}
				TraceImpl.initLock = null;
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00037DE4 File Offset: 0x00035FE4
		[global::System.MonoTODO]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				TraceImpl.Fail(new StackTrace(true).ToString());
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00037DFC File Offset: 0x00035FFC
		[global::System.MonoTODO]
		public static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				TraceImpl.Fail(message);
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00037E0C File Offset: 0x0003600C
		[global::System.MonoTODO]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (!condition)
			{
				TraceImpl.Fail(message, detailMessage);
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00037E1C File Offset: 0x0003601C
		public static void Close()
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Close();
				}
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00037EBC File Offset: 0x000360BC
		[global::System.MonoTODO]
		public static void Fail(string message)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Fail(message);
				}
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00037F5C File Offset: 0x0003615C
		[global::System.MonoTODO]
		public static void Fail(string message, string detailMessage)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Fail(message, detailMessage);
				}
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00037FFC File Offset: 0x000361FC
		public static void Flush()
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Flush();
				}
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0003809C File Offset: 0x0003629C
		public static void Indent()
		{
			TraceImpl.IndentLevel++;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000380AC File Offset: 0x000362AC
		public static void Unindent()
		{
			TraceImpl.IndentLevel--;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000380BC File Offset: 0x000362BC
		public static void Write(object value)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Write(value);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0003816C File Offset: 0x0003636C
		public static void Write(string message)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Write(message);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0003821C File Offset: 0x0003641C
		public static void Write(object value, string category)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Write(value, category);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000382CC File Offset: 0x000364CC
		public static void Write(string message, string category)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Write(message, category);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0003837C File Offset: 0x0003657C
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				TraceImpl.Write(value);
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0003838C File Offset: 0x0003658C
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				TraceImpl.Write(message);
			}
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0003839C File Offset: 0x0003659C
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceImpl.Write(value, category);
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000383AC File Offset: 0x000365AC
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceImpl.Write(message, category);
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000383BC File Offset: 0x000365BC
		public static void WriteLine(object value)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.WriteLine(value);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0003846C File Offset: 0x0003666C
		public static void WriteLine(string message)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.WriteLine(message);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003851C File Offset: 0x0003671C
		public static void WriteLine(object value, string category)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.WriteLine(value, category);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000385CC File Offset: 0x000367CC
		public static void WriteLine(string message, string category)
		{
			object listenersSyncRoot = TraceImpl.ListenersSyncRoot;
			lock (listenersSyncRoot)
			{
				foreach (object obj in TraceImpl.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.WriteLine(message, category);
					if (TraceImpl.AutoFlush)
					{
						traceListener.Flush();
					}
				}
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0003867C File Offset: 0x0003687C
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				TraceImpl.WriteLine(value);
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003868C File Offset: 0x0003688C
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				TraceImpl.WriteLine(message);
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003869C File Offset: 0x0003689C
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceImpl.WriteLine(value, category);
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000386AC File Offset: 0x000368AC
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceImpl.WriteLine(message, category);
			}
		}

		// Token: 0x04000694 RID: 1684
		private static object initLock = new object();

		// Token: 0x04000695 RID: 1685
		private static bool autoFlush;

		// Token: 0x04000696 RID: 1686
		[ThreadStatic]
		private static int indentLevel = 0;

		// Token: 0x04000697 RID: 1687
		[ThreadStatic]
		private static int indentSize;

		// Token: 0x04000698 RID: 1688
		private static TraceListenerCollection listeners;

		// Token: 0x04000699 RID: 1689
		private static bool use_global_lock;

		// Token: 0x0400069A RID: 1690
		private static CorrelationManager correlation_manager = new CorrelationManager();
	}
}
