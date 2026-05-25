using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000FB RID: 251
	internal sealed class GUIClip
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x0001429C File Offset: 0x0001249C
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.INTERNAL_CALL_Push(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x06000892 RID: 2194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Push(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x06000893 RID: 2195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Pop();

		// Token: 0x06000894 RID: 2196
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Rect GetTopRect();

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000895 RID: 2197
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000142AC File Offset: 0x000124AC
		public static Vector2 Unclip(Vector2 pos)
		{
			GUIClip.Unclip_Vector2(ref pos);
			return pos;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000142B8 File Offset: 0x000124B8
		private static void Unclip_Vector2(ref Vector2 pos)
		{
			GUIClip.INTERNAL_CALL_Unclip_Vector2(ref pos);
		}

		// Token: 0x06000898 RID: 2200
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Unclip_Vector2(ref Vector2 pos);

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000142C0 File Offset: 0x000124C0
		public static Rect topmostRect
		{
			get
			{
				Rect rect;
				GUIClip.INTERNAL_get_topmostRect(out rect);
				return rect;
			}
		}

		// Token: 0x0600089A RID: 2202
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_topmostRect(out Rect value);

		// Token: 0x0600089B RID: 2203 RVA: 0x000142D8 File Offset: 0x000124D8
		public static Rect Unclip(Rect rect)
		{
			GUIClip.Unclip_Rect(ref rect);
			return rect;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000142E4 File Offset: 0x000124E4
		private static void Unclip_Rect(ref Rect rect)
		{
			GUIClip.INTERNAL_CALL_Unclip_Rect(ref rect);
		}

		// Token: 0x0600089D RID: 2205
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Unclip_Rect(ref Rect rect);

		// Token: 0x0600089E RID: 2206 RVA: 0x000142EC File Offset: 0x000124EC
		public static Vector2 Clip(Vector2 absolutePos)
		{
			GUIClip.Clip_Vector2(ref absolutePos);
			return absolutePos;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000142F8 File Offset: 0x000124F8
		private static void Clip_Vector2(ref Vector2 absolutePos)
		{
			GUIClip.INTERNAL_CALL_Clip_Vector2(ref absolutePos);
		}

		// Token: 0x060008A0 RID: 2208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Clip_Vector2(ref Vector2 absolutePos);

		// Token: 0x060008A1 RID: 2209 RVA: 0x00014300 File Offset: 0x00012500
		public static Rect Clip(Rect absoluteRect)
		{
			GUIClip.Internal_Clip_Rect(ref absoluteRect);
			return absoluteRect;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001430C File Offset: 0x0001250C
		private static void Internal_Clip_Rect(ref Rect absoluteRect)
		{
			GUIClip.INTERNAL_CALL_Internal_Clip_Rect(ref absoluteRect);
		}

		// Token: 0x060008A3 RID: 2211
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Clip_Rect(ref Rect absoluteRect);

		// Token: 0x060008A4 RID: 2212
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reapply();

		// Token: 0x060008A5 RID: 2213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Matrix4x4 GetMatrix();

		// Token: 0x060008A6 RID: 2214 RVA: 0x00014314 File Offset: 0x00012514
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.INTERNAL_CALL_SetMatrix(ref m);
		}

		// Token: 0x060008A7 RID: 2215
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(ref Matrix4x4 m);

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00014320 File Offset: 0x00012520
		public static Rect visibleRect
		{
			get
			{
				Rect rect;
				GUIClip.INTERNAL_get_visibleRect(out rect);
				return rect;
			}
		}

		// Token: 0x060008A9 RID: 2217
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_visibleRect(out Rect value);

		// Token: 0x060008AA RID: 2218 RVA: 0x00014338 File Offset: 0x00012538
		public static Vector2 GetAbsoluteMousePosition()
		{
			Vector2 vector;
			GUIClip.Internal_GetAbsoluteMousePosition(out vector);
			return vector;
		}

		// Token: 0x060008AB RID: 2219
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAbsoluteMousePosition(out Vector2 output);
	}
}
