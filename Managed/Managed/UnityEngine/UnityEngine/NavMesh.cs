using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001CB RID: 459
	public sealed class NavMesh : Object
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x000235E8 File Offset: 0x000217E8
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int passableMask)
		{
			return NavMesh.INTERNAL_CALL_Raycast(ref sourcePosition, ref targetPosition, out hit, passableMask);
		}

		// Token: 0x06001644 RID: 5700
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Raycast(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int passableMask);

		// Token: 0x06001645 RID: 5701 RVA: 0x000235F8 File Offset: 0x000217F8
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int passableMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, passableMask, path);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0002360C File Offset: 0x0002180C
		internal static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int passableMask, NavMeshPath path)
		{
			return NavMesh.INTERNAL_CALL_CalculatePathInternal(ref sourcePosition, ref targetPosition, passableMask, path);
		}

		// Token: 0x06001647 RID: 5703
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CalculatePathInternal(ref Vector3 sourcePosition, ref Vector3 targetPosition, int passableMask, NavMeshPath path);

		// Token: 0x06001648 RID: 5704 RVA: 0x0002361C File Offset: 0x0002181C
		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int passableMask)
		{
			return NavMesh.INTERNAL_CALL_FindClosestEdge(ref sourcePosition, out hit, passableMask);
		}

		// Token: 0x06001649 RID: 5705
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_FindClosestEdge(ref Vector3 sourcePosition, out NavMeshHit hit, int passableMask);

		// Token: 0x0600164A RID: 5706 RVA: 0x00023628 File Offset: 0x00021828
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int allowedMask)
		{
			return NavMesh.INTERNAL_CALL_SamplePosition(ref sourcePosition, out hit, maxDistance, allowedMask);
		}

		// Token: 0x0600164B RID: 5707
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SamplePosition(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int allowedMask);

		// Token: 0x0600164C RID: 5708
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLayerCost(int layer, float cost);

		// Token: 0x0600164D RID: 5709
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetLayerCost(int layer);

		// Token: 0x0600164E RID: 5710
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNavMeshLayerFromName(string layerName);

		// Token: 0x0600164F RID: 5711 RVA: 0x00023634 File Offset: 0x00021834
		public static NavMeshTriangulation CalculateTriangulation()
		{
			NavMeshTriangulation navMeshTriangulation = default(NavMeshTriangulation);
			NavMesh.TriangulateInternal(ref navMeshTriangulation.vertices, ref navMeshTriangulation.indices, ref navMeshTriangulation.layers);
			return navMeshTriangulation;
		}

		// Token: 0x06001650 RID: 5712
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void TriangulateInternal(ref Vector3[] vertices, ref int[] indices, ref int[] layers);

		// Token: 0x06001651 RID: 5713
		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Triangulate(out Vector3[] vertices, out int[] indices);

		// Token: 0x06001652 RID: 5714
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddOffMeshLinks();

		// Token: 0x06001653 RID: 5715
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RestoreNavMesh();
	}
}
