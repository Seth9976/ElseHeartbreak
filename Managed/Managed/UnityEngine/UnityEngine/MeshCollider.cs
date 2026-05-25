using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	public sealed class MeshCollider : Collider
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00021480 File Offset: 0x0001F680
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x00021488 File Offset: 0x0001F688
		[Obsolete("mesh has been replaced with sharedMesh and will be deprecated")]
		public Mesh mesh
		{
			get
			{
				return this.sharedMesh;
			}
			set
			{
				this.sharedMesh = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600132F RID: 4911
		// (set) Token: 0x06001330 RID: 4912
		public extern Mesh sharedMesh
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001331 RID: 4913
		// (set) Token: 0x06001332 RID: 4914
		public extern bool convex
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001333 RID: 4915
		// (set) Token: 0x06001334 RID: 4916
		public extern bool smoothSphereCollisions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
