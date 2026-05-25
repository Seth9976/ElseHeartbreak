using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	public sealed class SphereCollider : Collider
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00021454 File Offset: 0x0001F654
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0002146C File Offset: 0x0001F66C
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_center(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x06001328 RID: 4904
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001329 RID: 4905
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600132A RID: 4906
		// (set) Token: 0x0600132B RID: 4907
		public extern float radius
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
