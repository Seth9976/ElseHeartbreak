using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000AE RID: 174
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003D8 RID: 984
		// (set) Token: 0x060003D9 RID: 985
		public extern Transform[] bones
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003DA RID: 986
		// (set) Token: 0x060003DB RID: 987
		public extern Transform rootBone
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003DC RID: 988
		// (set) Token: 0x060003DD RID: 989
		public extern SkinQuality quality
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DE RID: 990
		// (set) Token: 0x060003DF RID: 991
		public extern Mesh sharedMesh
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000BA20 File Offset: 0x00009C20
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000BA24 File Offset: 0x00009C24
		[Obsolete("Has no effect.")]
		public bool skinNormals
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003E2 RID: 994
		// (set) Token: 0x060003E3 RID: 995
		public extern bool updateWhenOffscreen
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000BA28 File Offset: 0x00009C28
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000BA40 File Offset: 0x00009C40
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_localBounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060003E6 RID: 998
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060003E7 RID: 999
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x060003E8 RID: 1000
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh(Mesh mesh);

		// Token: 0x060003E9 RID: 1001
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x060003EA RID: 1002
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBlendShapeWeight(int index, float value);
	}
}
