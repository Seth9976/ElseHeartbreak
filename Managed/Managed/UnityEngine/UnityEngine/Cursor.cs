using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000098 RID: 152
	public sealed class Cursor
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000B54C File Offset: 0x0000974C
		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			Cursor.SetCursor(texture, Vector2.zero, cursorMode);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B55C File Offset: 0x0000975C
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.INTERNAL_CALL_SetCursor(texture, ref hotspot, cursorMode);
		}

		// Token: 0x06000331 RID: 817
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetCursor(Texture2D texture, ref Vector2 hotspot, CursorMode cursorMode);
	}
}
