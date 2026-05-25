using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000184 RID: 388
	public class Joint : Component
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001237 RID: 4663
		// (set) Token: 0x06001238 RID: 4664
		public extern Rigidbody connectedBody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x00020DFC File Offset: 0x0001EFFC
		public Vector3 axis
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_axis(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_axis(ref value);
			}
		}

		// Token: 0x0600123B RID: 4667
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_axis(out Vector3 value);

		// Token: 0x0600123C RID: 4668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_axis(ref Vector3 value);

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00020E08 File Offset: 0x0001F008
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x00020E20 File Offset: 0x0001F020
		public Vector3 anchor
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_anchor(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_anchor(ref value);
			}
		}

		// Token: 0x0600123F RID: 4671
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchor(out Vector3 value);

		// Token: 0x06001240 RID: 4672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchor(ref Vector3 value);

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00020E2C File Offset: 0x0001F02C
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x00020E44 File Offset: 0x0001F044
		public Vector3 connectedAnchor
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_connectedAnchor(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_connectedAnchor(ref value);
			}
		}

		// Token: 0x06001243 RID: 4675
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_connectedAnchor(out Vector3 value);

		// Token: 0x06001244 RID: 4676
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_connectedAnchor(ref Vector3 value);

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001245 RID: 4677
		// (set) Token: 0x06001246 RID: 4678
		public extern bool autoConfigureConnectedAnchor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001247 RID: 4679
		// (set) Token: 0x06001248 RID: 4680
		public extern float breakForce
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001249 RID: 4681
		// (set) Token: 0x0600124A RID: 4682
		public extern float breakTorque
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600124B RID: 4683
		// (set) Token: 0x0600124C RID: 4684
		public extern bool enableCollision
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
