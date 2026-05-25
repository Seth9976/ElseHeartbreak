using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001BE RID: 446
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000231C4 File Offset: 0x000213C4
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x000231DC File Offset: 0x000213DC
		public Vector2 anchor
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_anchor(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_anchor(ref value);
			}
		}

		// Token: 0x06001581 RID: 5505
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchor(out Vector2 value);

		// Token: 0x06001582 RID: 5506
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchor(ref Vector2 value);

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x000231E8 File Offset: 0x000213E8
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00023200 File Offset: 0x00021400
		public Vector2 connectedAnchor
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_connectedAnchor(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_connectedAnchor(ref value);
			}
		}

		// Token: 0x06001585 RID: 5509
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_connectedAnchor(out Vector2 value);

		// Token: 0x06001586 RID: 5510
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_connectedAnchor(ref Vector2 value);
	}
}
