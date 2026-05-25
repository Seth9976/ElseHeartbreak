using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200010E RID: 270
	public sealed class Gizmos
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x00016E60 File Offset: 0x00015060
		public static void DrawRay(Ray r)
		{
			Gizmos.DrawLine(r.origin, r.origin + r.direction);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00016E8C File Offset: 0x0001508C
		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			Gizmos.DrawLine(from, from + direction);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00016E9C File Offset: 0x0001509C
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.INTERNAL_CALL_DrawLine(ref from, ref to);
		}

		// Token: 0x060009F2 RID: 2546
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 from, ref Vector3 to);

		// Token: 0x060009F3 RID: 2547 RVA: 0x00016EA8 File Offset: 0x000150A8
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawWireSphere(ref center, radius);
		}

		// Token: 0x060009F4 RID: 2548
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawWireSphere(ref Vector3 center, float radius);

		// Token: 0x060009F5 RID: 2549 RVA: 0x00016EB4 File Offset: 0x000150B4
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawSphere(ref center, radius);
		}

		// Token: 0x060009F6 RID: 2550
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawSphere(ref Vector3 center, float radius);

		// Token: 0x060009F7 RID: 2551 RVA: 0x00016EC0 File Offset: 0x000150C0
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawWireCube(ref center, ref size);
		}

		// Token: 0x060009F8 RID: 2552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawWireCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060009F9 RID: 2553 RVA: 0x00016ECC File Offset: 0x000150CC
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawCube(ref center, ref size);
		}

		// Token: 0x060009FA RID: 2554
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060009FB RID: 2555 RVA: 0x00016ED8 File Offset: 0x000150D8
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling)
		{
			Gizmos.INTERNAL_CALL_DrawIcon(ref center, name, allowScaling);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00016EE4 File Offset: 0x000150E4
		[ExcludeFromDocs]
		public static void DrawIcon(Vector3 center, string name)
		{
			bool flag = true;
			Gizmos.INTERNAL_CALL_DrawIcon(ref center, name, flag);
		}

		// Token: 0x060009FD RID: 2557
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawIcon(ref Vector3 center, string name, bool allowScaling);

		// Token: 0x060009FE RID: 2558 RVA: 0x00016EFC File Offset: 0x000150FC
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, material);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00016F14 File Offset: 0x00015114
		public static void DrawGUITexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00016F24 File Offset: 0x00015124
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Gizmos.INTERNAL_CALL_DrawGUITexture(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00016F38 File Offset: 0x00015138
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Gizmos.INTERNAL_CALL_DrawGUITexture(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x06000A02 RID: 2562
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawGUITexture(ref Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat);

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00016F58 File Offset: 0x00015158
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00016F70 File Offset: 0x00015170
		public static Color color
		{
			get
			{
				Color color;
				Gizmos.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				Gizmos.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06000A05 RID: 2565
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000A06 RID: 2566
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00016F7C File Offset: 0x0001517C
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00016F94 File Offset: 0x00015194
		public static Matrix4x4 matrix
		{
			get
			{
				Matrix4x4 matrix4x;
				Gizmos.INTERNAL_get_matrix(out matrix4x);
				return matrix4x;
			}
			set
			{
				Gizmos.INTERNAL_set_matrix(ref value);
			}
		}

		// Token: 0x06000A09 RID: 2569
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_matrix(out Matrix4x4 value);

		// Token: 0x06000A0A RID: 2570
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_matrix(ref Matrix4x4 value);

		// Token: 0x06000A0B RID: 2571 RVA: 0x00016FA0 File Offset: 0x000151A0
		public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
		{
			Gizmos.INTERNAL_CALL_DrawFrustum(ref center, fov, maxRange, minRange, aspect);
		}

		// Token: 0x06000A0C RID: 2572
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawFrustum(ref Vector3 center, float fov, float maxRange, float minRange, float aspect);
	}
}
