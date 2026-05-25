using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C9 RID: 201
	public sealed class GeometryUtility
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0000C650 File Offset: 0x0000A850
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			return GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000C668 File Offset: 0x0000A868
		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.Internal_ExtractPlanes(array, worldToProjectionMatrix);
			return array;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000C684 File Offset: 0x0000A884
		private static void Internal_ExtractPlanes(Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			GeometryUtility.INTERNAL_CALL_Internal_ExtractPlanes(planes, ref worldToProjectionMatrix);
		}

		// Token: 0x06000576 RID: 1398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ExtractPlanes(Plane[] planes, ref Matrix4x4 worldToProjectionMatrix);

		// Token: 0x06000577 RID: 1399 RVA: 0x0000C690 File Offset: 0x0000A890
		public static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			return GeometryUtility.INTERNAL_CALL_TestPlanesAABB(planes, ref bounds);
		}

		// Token: 0x06000578 RID: 1400
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_TestPlanesAABB(Plane[] planes, ref Bounds bounds);
	}
}
