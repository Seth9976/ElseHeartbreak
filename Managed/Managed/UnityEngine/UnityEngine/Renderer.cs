using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B1 RID: 177
	public class Renderer : Component
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003F8 RID: 1016
		// (set) Token: 0x060003F9 RID: 1017
		internal extern Transform staticBatchRootTransform
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003FA RID: 1018
		internal extern int staticBatchIndex
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060003FB RID: 1019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSubsetIndex(int index, int subSetIndexForMaterial);

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003FC RID: 1020
		public extern bool isPartOfStaticBatch
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000BA88 File Offset: 0x00009C88
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_worldToLocalMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060003FE RID: 1022
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_localToWorldMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000400 RID: 1024
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000401 RID: 1025
		// (set) Token: 0x06000402 RID: 1026
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000403 RID: 1027
		// (set) Token: 0x06000404 RID: 1028
		public extern bool castShadows
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000405 RID: 1029
		// (set) Token: 0x06000406 RID: 1030
		public extern bool receiveShadows
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000407 RID: 1031
		// (set) Token: 0x06000408 RID: 1032
		public extern Material material
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000409 RID: 1033
		// (set) Token: 0x0600040A RID: 1034
		public extern Material sharedMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600040B RID: 1035
		// (set) Token: 0x0600040C RID: 1036
		public extern Material[] sharedMaterials
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040D RID: 1037
		// (set) Token: 0x0600040E RID: 1038
		public extern Material[] materials
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
		}

		// Token: 0x06000410 RID: 1040
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000411 RID: 1041
		// (set) Token: 0x06000412 RID: 1042
		public extern int lightmapIndex
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public Vector4 lightmapTilingOffset
		{
			get
			{
				Vector4 vector;
				this.INTERNAL_get_lightmapTilingOffset(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_lightmapTilingOffset(ref value);
			}
		}

		// Token: 0x06000415 RID: 1045
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lightmapTilingOffset(out Vector4 value);

		// Token: 0x06000416 RID: 1046
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lightmapTilingOffset(ref Vector4 value);

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000417 RID: 1047
		public extern bool isVisible
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000418 RID: 1048
		// (set) Token: 0x06000419 RID: 1049
		public extern bool useLightProbes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600041A RID: 1050
		// (set) Token: 0x0600041B RID: 1051
		public extern Transform lightProbeAnchor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600041C RID: 1052
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPropertyBlock(MaterialPropertyBlock properties);

		// Token: 0x0600041D RID: 1053
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetPropertyBlock(MaterialPropertyBlock dest);

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600041E RID: 1054
		// (set) Token: 0x0600041F RID: 1055
		public extern string sortingLayerName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000420 RID: 1056
		// (set) Token: 0x06000421 RID: 1057
		public extern int sortingLayerID
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000422 RID: 1058
		// (set) Token: 0x06000423 RID: 1059
		public extern int sortingOrder
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000424 RID: 1060
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render(int material);
	}
}
