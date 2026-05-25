using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200015D RID: 349
	public sealed class Debug
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0001EC14 File Offset: 0x0001CE14
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool flag = true;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, flag);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0001EC30 File Offset: 0x0001CE30
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, num, flag);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0001EC54 File Offset: 0x0001CE54
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref white, num, flag);
		}

		// Token: 0x06000F18 RID: 3864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 start, ref Vector3 end, ref Color color, float duration, bool depthTest);

		// Token: 0x06000F19 RID: 3865 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawRay(start, dir, color, duration, flag);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0001EC98 File Offset: 0x0001CE98
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawRay(start, dir, color, num, flag);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, num, flag);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x06000F1D RID: 3869
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		// Token: 0x06000F1E RID: 3870
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		// Token: 0x06000F1F RID: 3871
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Log(int level, string msg, [Writable] Object obj);

		// Token: 0x06000F20 RID: 3872
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_LogException(Exception exception, [Writable] Object obj);

		// Token: 0x06000F21 RID: 3873 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		public static void Log(object message)
		{
			Debug.Internal_Log(0, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0001ED14 File Offset: 0x0001CF14
		public static void Log(object message, Object context)
		{
			Debug.Internal_Log(0, (message == null) ? "Null" : message.ToString(), context);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0001ED34 File Offset: 0x0001CF34
		public static void LogError(object message)
		{
			Debug.Internal_Log(2, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0001ED54 File Offset: 0x0001CF54
		public static void LogError(object message, Object context)
		{
			Debug.Internal_Log(2, message.ToString(), context);
		}

		// Token: 0x06000F25 RID: 3877
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F26 RID: 3878
		// (set) Token: 0x06000F27 RID: 3879
		public static extern bool developerConsoleVisible
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public static void LogException(Exception exception)
		{
			Debug.Internal_LogException(exception, null);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0001ED70 File Offset: 0x0001CF70
		public static void LogException(Exception exception, Object context)
		{
			Debug.Internal_LogException(exception, context);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		public static void LogWarning(object message)
		{
			Debug.Internal_Log(1, message.ToString(), null);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		public static void LogWarning(object message, Object context)
		{
			Debug.Internal_Log(1, message.ToString(), context);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F2C RID: 3884
		public static extern bool isDebugBuild
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000F2D RID: 3885
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OpenConsoleFile();
	}
}
