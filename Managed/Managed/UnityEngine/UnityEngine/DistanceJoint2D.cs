using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C0 RID: 448
	public sealed class DistanceJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001593 RID: 5523
		// (set) Token: 0x06001594 RID: 5524
		public extern float distance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001595 RID: 5525
		// (set) Token: 0x06001596 RID: 5526
		public extern bool maxDistanceOnly
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00023240 File Offset: 0x00021440
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 vector;
			DistanceJoint2D.DistanceJoint2D_CUSTOM_INTERNAL_GetReactionForce(this, timeStep, out vector);
			return vector;
		}

		// Token: 0x06001598 RID: 5528
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DistanceJoint2D_CUSTOM_INTERNAL_GetReactionForce(DistanceJoint2D joint, float timeStep, out Vector2 value);

		// Token: 0x06001599 RID: 5529 RVA: 0x00023258 File Offset: 0x00021458
		public float GetReactionTorque(float timeStep)
		{
			return DistanceJoint2D.INTERNAL_CALL_GetReactionTorque(this, timeStep);
		}

		// Token: 0x0600159A RID: 5530
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetReactionTorque(DistanceJoint2D self, float timeStep);
	}
}
