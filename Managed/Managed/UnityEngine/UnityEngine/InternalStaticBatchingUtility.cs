using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000070 RID: 112
	internal class InternalStaticBatchingUtility
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public static void Combine(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot, false);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject in array)
			{
				if (!(staticBatchRoot != null) || gameObject.transform.IsChildOf(staticBatchRoot.transform))
				{
					if (!combineOnlyStatic || gameObject.isStaticBatchable)
					{
						list.Add(gameObject);
					}
				}
			}
			array = list.ToArray();
			if (!Application.HasProLicense() && !Application.HasAdvancedLicense() && staticBatchRoot != null && array.Length > 0)
			{
				Debug.LogError("Your Unity license is not sufficient for Static Batching.");
			}
			InternalStaticBatchingUtility.Combine(array, staticBatchRoot);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			Matrix4x4 matrix4x = Matrix4x4.identity;
			Transform transform = null;
			if (staticBatchRoot)
			{
				matrix4x = staticBatchRoot.transform.worldToLocalMatrix;
				transform = staticBatchRoot.transform;
			}
			int num = 0;
			int num2 = 0;
			List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
			List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
			List<GameObject> list3 = new List<GameObject>();
			Array.Sort(gos, new InternalStaticBatchingUtility.SortGO());
			foreach (GameObject gameObject in gos)
			{
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (!(meshFilter == null))
				{
					if (!(meshFilter.sharedMesh == null))
					{
						if (meshFilter.sharedMesh.canAccess)
						{
							if (!(meshFilter.renderer == null) && meshFilter.renderer.enabled)
							{
								if (meshFilter.renderer.staticBatchIndex == 0)
								{
									if (num2 + meshFilter.sharedMesh.vertexCount > 64000)
									{
										InternalStaticBatchingUtility.MakeBatch(list, list2, list3, transform, num++);
										list.Clear();
										list2.Clear();
										list3.Clear();
										num2 = 0;
									}
									MeshSubsetCombineUtility.MeshInstance meshInstance = default(MeshSubsetCombineUtility.MeshInstance);
									Mesh sharedMesh = meshFilter.sharedMesh;
									meshInstance.meshInstanceID = sharedMesh.GetInstanceID();
									meshInstance.transform = matrix4x * meshFilter.transform.localToWorldMatrix;
									meshInstance.lightmapTilingOffset = meshFilter.renderer.lightmapTilingOffset;
									list.Add(meshInstance);
									Material[] array = meshFilter.renderer.sharedMaterials;
									if (array.Length > sharedMesh.subMeshCount)
									{
										Debug.LogWarning(string.Concat(new object[] { "Mesh has more materials (", array.Length, ") than subsets (", sharedMesh.subMeshCount, ")" }));
										Material[] array2 = new Material[sharedMesh.subMeshCount];
										for (int j = 0; j < sharedMesh.subMeshCount; j++)
										{
											array2[j] = meshFilter.renderer.sharedMaterials[j];
										}
										meshFilter.renderer.sharedMaterials = array2;
										array = array2;
									}
									for (int k = 0; k < Math.Min(array.Length, sharedMesh.subMeshCount); k++)
									{
										list2.Add(new MeshSubsetCombineUtility.SubMeshInstance
										{
											meshInstanceID = meshFilter.sharedMesh.GetInstanceID(),
											vertexOffset = num2,
											subMeshIndex = k,
											gameObjectInstanceID = gameObject.GetInstanceID(),
											transform = meshInstance.transform
										});
										list3.Add(gameObject);
									}
									num2 += sharedMesh.vertexCount;
								}
							}
						}
					}
				}
			}
			InternalStaticBatchingUtility.MakeBatch(list, list2, list3, transform, num);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000ACAC File Offset: 0x00008EAC
		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshInstance> meshes, List<MeshSubsetCombineUtility.SubMeshInstance> subsets, List<GameObject> subsetGOs, Transform staticBatchRootTransform, int batchIndex)
		{
			if (meshes.Count < 2)
			{
				return;
			}
			MeshSubsetCombineUtility.MeshInstance[] array = meshes.ToArray();
			MeshSubsetCombineUtility.SubMeshInstance[] array2 = subsets.ToArray();
			string text = "Combined Mesh";
			text = text + " (root: " + ((!(staticBatchRootTransform != null)) ? "scene" : staticBatchRootTransform.name) + ")";
			if (batchIndex > 0)
			{
				text = text + " " + (batchIndex + 1);
			}
			Mesh mesh = StaticBatchingUtility.InternalCombineVertices(array, text);
			StaticBatchingUtility.InternalCombineIndices(array2, mesh);
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				MeshSubsetCombineUtility.SubMeshInstance subMeshInstance = array2[i];
				GameObject gameObject = subsetGOs[i];
				Mesh mesh2 = mesh;
				MeshFilter meshFilter = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
				meshFilter.sharedMesh = mesh2;
				gameObject.renderer.SetSubsetIndex(subMeshInstance.subMeshIndex, num);
				gameObject.renderer.staticBatchRootTransform = staticBatchRootTransform;
				gameObject.renderer.enabled = false;
				gameObject.renderer.enabled = true;
				num++;
			}
		}

		// Token: 0x0400019E RID: 414
		private const int MaxVerticesInBatch = 64000;

		// Token: 0x0400019F RID: 415
		private const string CombinedMeshPrefix = "Combined Mesh";

		// Token: 0x02000071 RID: 113
		internal class SortGO : IComparer
		{
			// Token: 0x06000298 RID: 664 RVA: 0x0000ADD8 File Offset: 0x00008FD8
			int IComparer.Compare(object a, object b)
			{
				if (a == b)
				{
					return 0;
				}
				Renderer renderer = InternalStaticBatchingUtility.SortGO.GetRenderer(a as GameObject);
				Renderer renderer2 = InternalStaticBatchingUtility.SortGO.GetRenderer(b as GameObject);
				int num = InternalStaticBatchingUtility.SortGO.GetMaterialId(renderer).CompareTo(InternalStaticBatchingUtility.SortGO.GetMaterialId(renderer2));
				if (num == 0)
				{
					num = InternalStaticBatchingUtility.SortGO.GetLightmapIndex(renderer).CompareTo(InternalStaticBatchingUtility.SortGO.GetLightmapIndex(renderer2));
				}
				return num;
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000AE38 File Offset: 0x00009038
			private static int GetMaterialId(Renderer renderer)
			{
				if (renderer == null || renderer.sharedMaterial == null)
				{
					return 0;
				}
				return renderer.sharedMaterial.GetInstanceID();
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000AE70 File Offset: 0x00009070
			private static int GetLightmapIndex(Renderer renderer)
			{
				if (renderer == null)
				{
					return -1;
				}
				return renderer.lightmapIndex;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x0000AE88 File Offset: 0x00009088
			private static Renderer GetRenderer(GameObject go)
			{
				if (go == null)
				{
					return null;
				}
				MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (meshFilter == null)
				{
					return null;
				}
				return meshFilter.renderer;
			}
		}
	}
}
