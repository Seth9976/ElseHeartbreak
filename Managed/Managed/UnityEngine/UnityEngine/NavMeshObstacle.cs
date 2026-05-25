using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001CF RID: 463
	public sealed class NavMeshObstacle : Behaviour
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600166F RID: 5743
		// (set) Token: 0x06001670 RID: 5744
		public extern float height
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001671 RID: 5745
		// (set) Token: 0x06001672 RID: 5746
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000236F8 File Offset: 0x000218F8
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00023710 File Offset: 0x00021910
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x06001675 RID: 5749
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x06001676 RID: 5750
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001677 RID: 5751
		// (set) Token: 0x06001678 RID: 5752
		public extern bool carving
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001679 RID: 5753
		// (set) Token: 0x0600167A RID: 5754
		public extern float carvingMoveThreshold
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
