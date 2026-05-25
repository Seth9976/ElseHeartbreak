using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000CF RID: 207
	public sealed class StaticBatchingUtility
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public static void Combine(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.Combine(gos, staticBatchRoot);
		}

		// Token: 0x060005C8 RID: 1480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		// Token: 0x060005C9 RID: 1481
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, [Writable] Mesh combinedMesh);
	}
}
