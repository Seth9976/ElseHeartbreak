using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CD RID: 205
	public sealed class GL
	{
		// Token: 0x06000596 RID: 1430
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x06000597 RID: 1431 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public static void Vertex(Vector3 v)
		{
			GL.INTERNAL_CALL_Vertex(ref v);
		}

		// Token: 0x06000598 RID: 1432
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Vertex(ref Vector3 v);

		// Token: 0x06000599 RID: 1433 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public static void Color(Color c)
		{
			GL.INTERNAL_CALL_Color(ref c);
		}

		// Token: 0x0600059A RID: 1434
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Color(ref Color c);

		// Token: 0x0600059B RID: 1435 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public static void TexCoord(Vector3 v)
		{
			GL.INTERNAL_CALL_TexCoord(ref v);
		}

		// Token: 0x0600059C RID: 1436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_TexCoord(ref Vector3 v);

		// Token: 0x0600059D RID: 1437
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord2(float x, float y);

		// Token: 0x0600059E RID: 1438
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TexCoord3(float x, float y, float z);

		// Token: 0x0600059F RID: 1439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord2(int unit, float x, float y);

		// Token: 0x060005A0 RID: 1440
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MultiTexCoord3(int unit, float x, float y, float z);

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public static void MultiTexCoord(int unit, Vector3 v)
		{
			GL.INTERNAL_CALL_MultiTexCoord(unit, ref v);
		}

		// Token: 0x060005A2 RID: 1442
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MultiTexCoord(int unit, ref Vector3 v);

		// Token: 0x060005A3 RID: 1443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin(int mode);

		// Token: 0x060005A4 RID: 1444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End();

		// Token: 0x060005A5 RID: 1445
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadOrtho();

		// Token: 0x060005A6 RID: 1446
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadPixelMatrix();

		// Token: 0x060005A7 RID: 1447
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LoadPixelMatrixArgs(float left, float right, float bottom, float top);

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000C704 File Offset: 0x0000A904
		public static void LoadPixelMatrix(float left, float right, float bottom, float top)
		{
			GL.LoadPixelMatrixArgs(left, right, bottom, top);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000C710 File Offset: 0x0000A910
		public static void Viewport(Rect pixelRect)
		{
			GL.INTERNAL_CALL_Viewport(ref pixelRect);
		}

		// Token: 0x060005AA RID: 1450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Viewport(ref Rect pixelRect);

		// Token: 0x060005AB RID: 1451 RVA: 0x0000C71C File Offset: 0x0000A91C
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_LoadProjectionMatrix(ref mat);
		}

		// Token: 0x060005AC RID: 1452
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LoadProjectionMatrix(ref Matrix4x4 mat);

		// Token: 0x060005AD RID: 1453
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadIdentity();

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000C728 File Offset: 0x0000A928
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000C740 File Offset: 0x0000A940
		public static Matrix4x4 modelview
		{
			get
			{
				Matrix4x4 matrix4x;
				GL.INTERNAL_get_modelview(out matrix4x);
				return matrix4x;
			}
			set
			{
				GL.INTERNAL_set_modelview(ref value);
			}
		}

		// Token: 0x060005B0 RID: 1456
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_modelview(out Matrix4x4 value);

		// Token: 0x060005B1 RID: 1457
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_modelview(ref Matrix4x4 value);

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000C74C File Offset: 0x0000A94C
		public static void MultMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_MultMatrix(ref mat);
		}

		// Token: 0x060005B3 RID: 1459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MultMatrix(ref Matrix4x4 mat);

		// Token: 0x060005B4 RID: 1460
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PushMatrix();

		// Token: 0x060005B5 RID: 1461
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PopMatrix();

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000C758 File Offset: 0x0000A958
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			return GL.INTERNAL_CALL_GetGPUProjectionMatrix(ref proj, renderIntoTexture);
		}

		// Token: 0x060005B7 RID: 1463
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4 INTERNAL_CALL_GetGPUProjectionMatrix(ref Matrix4x4 proj, bool renderIntoTexture);

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005B8 RID: 1464
		// (set) Token: 0x060005B9 RID: 1465
		public static extern bool wireframe
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005BA RID: 1466
		// (set) Token: 0x060005BB RID: 1467
		public static extern bool sRGBWrite
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060005BC RID: 1468
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRevertBackfacing(bool revertBackFaces);

		// Token: 0x060005BD RID: 1469 RVA: 0x0000C764 File Offset: 0x0000A964
		[ExcludeFromDocs]
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			float num = 1f;
			GL.Clear(clearDepth, clearColor, backgroundColor, num);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000C780 File Offset: 0x0000A980
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.Internal_Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000C78C File Offset: 0x0000A98C
		private static void Internal_Clear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.INTERNAL_CALL_Internal_Clear(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x060005C0 RID: 1472
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Clear(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x060005C1 RID: 1473
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x060005C2 RID: 1474
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InvalidateState();

		// Token: 0x060005C3 RID: 1475
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IssuePluginEvent(int eventID);

		// Token: 0x040002BD RID: 701
		public const int TRIANGLES = 4;

		// Token: 0x040002BE RID: 702
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040002BF RID: 703
		public const int QUADS = 7;

		// Token: 0x040002C0 RID: 704
		public const int LINES = 1;
	}
}
