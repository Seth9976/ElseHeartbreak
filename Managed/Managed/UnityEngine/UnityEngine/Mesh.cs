using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000AB RID: 171
	public sealed class Mesh : Object
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x0600038F RID: 911
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x06000390 RID: 912
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear([DefaultValue("true")] bool keepVertexLayout);

		// Token: 0x06000391 RID: 913 RVA: 0x0000B6D0 File Offset: 0x000098D0
		[ExcludeFromDocs]
		public void Clear()
		{
			bool flag = true;
			this.Clear(flag);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000392 RID: 914
		public extern bool isReadable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000393 RID: 915
		internal extern bool canAccess
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000394 RID: 916
		// (set) Token: 0x06000395 RID: 917
		public extern Vector3[] vertices
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000396 RID: 918
		// (set) Token: 0x06000397 RID: 919
		public extern Vector3[] normals
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000398 RID: 920
		// (set) Token: 0x06000399 RID: 921
		public extern Vector4[] tangents
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600039A RID: 922
		// (set) Token: 0x0600039B RID: 923
		public extern Vector2[] uv
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600039C RID: 924
		// (set) Token: 0x0600039D RID: 925
		public extern Vector2[] uv2
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000B6E8 File Offset: 0x000098E8
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public Vector2[] uv1
		{
			get
			{
				return this.uv2;
			}
			set
			{
				this.uv2 = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000B6FC File Offset: 0x000098FC
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000B714 File Offset: 0x00009914
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_bounds(ref value);
			}
		}

		// Token: 0x060003A2 RID: 930
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x060003A3 RID: 931
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bounds(ref Bounds value);

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003A4 RID: 932
		// (set) Token: 0x060003A5 RID: 933
		public extern Color[] colors
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003A6 RID: 934
		// (set) Token: 0x060003A7 RID: 935
		public extern Color32[] colors32
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060003A8 RID: 936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		// Token: 0x060003A9 RID: 937
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateNormals();

		// Token: 0x060003AA RID: 938
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Optimize();

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003AB RID: 939
		// (set) Token: 0x060003AC RID: 940
		public extern int[] triangles
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060003AD RID: 941
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTriangles(int submesh);

		// Token: 0x060003AE RID: 942
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTriangles(int[] triangles, int submesh);

		// Token: 0x060003AF RID: 943
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetIndices(int submesh);

		// Token: 0x060003B0 RID: 944
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIndices(int[] indices, MeshTopology topology, int submesh);

		// Token: 0x060003B1 RID: 945
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MeshTopology GetTopology(int submesh);

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B2 RID: 946
		public extern int vertexCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003B3 RID: 947
		// (set) Token: 0x060003B4 RID: 948
		public extern int subMeshCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060003B5 RID: 949
		[WrapperlessIcall]
		[Obsolete("Use SetTriangles instead. Internally this function will convert the triangle strip to a list of triangles anyway.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTriangleStrip(int[] triangles, int submesh);

		// Token: 0x060003B6 RID: 950
		[Obsolete("Use GetTriangles instead. Internally this function converts a list of triangles to a strip, so it might be slow, it might be a mess.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTriangleStrip(int submesh);

		// Token: 0x060003B7 RID: 951
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices);

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B720 File Offset: 0x00009920
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			bool flag = true;
			this.CombineMeshes(combine, mergeSubMeshes, flag);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B738 File Offset: 0x00009938
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			bool flag = true;
			bool flag2 = true;
			this.CombineMeshes(combine, flag2, flag);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003BA RID: 954
		// (set) Token: 0x060003BB RID: 955
		public extern BoneWeight[] boneWeights
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003BC RID: 956
		// (set) Token: 0x060003BD RID: 957
		public extern Matrix4x4[] bindposes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060003BE RID: 958
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkDynamic();

		// Token: 0x060003BF RID: 959
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UploadMeshData(bool markNoLogerReadable);

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C0 RID: 960
		public extern int blendShapeCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060003C1 RID: 961
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetBlendShapeName(int index);

		// Token: 0x060003C2 RID: 962
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetBlendShapeIndex(string blendShapeName);
	}
}
