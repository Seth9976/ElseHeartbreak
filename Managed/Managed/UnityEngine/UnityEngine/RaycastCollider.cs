using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	[Obsolete("Use WheelCollider or BoxCollider instead, RaycastCollider is unreliable")]
	public sealed class RaycastCollider : Collider
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x000214C8 File Offset: 0x0001F6C8
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x000214E0 File Offset: 0x0001F6E0
		[Obsolete("Use WheelCollider or BoxCollider instead, RaycastCollider is unreliable")]
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

		// Token: 0x06001343 RID: 4931
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001344 RID: 4932
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001345 RID: 4933
		// (set) Token: 0x06001346 RID: 4934
		[Obsolete("Use WheelCollider or BoxCollider instead, RaycastCollider is unreliable")]
		public extern float length
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
