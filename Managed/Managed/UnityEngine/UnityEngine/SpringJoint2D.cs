using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001BF RID: 447
	public sealed class SpringJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001588 RID: 5512
		// (set) Token: 0x06001589 RID: 5513
		public extern float distance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600158A RID: 5514
		// (set) Token: 0x0600158B RID: 5515
		public extern float dampingRatio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600158C RID: 5516
		// (set) Token: 0x0600158D RID: 5517
		public extern float frequency
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00023214 File Offset: 0x00021414
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 vector;
			SpringJoint2D.SpringJoint2D_CUSTOM_INTERNAL_GetReactionForce(this, timeStep, out vector);
			return vector;
		}

		// Token: 0x0600158F RID: 5519
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpringJoint2D_CUSTOM_INTERNAL_GetReactionForce(SpringJoint2D joint, float timeStep, out Vector2 value);

		// Token: 0x06001590 RID: 5520 RVA: 0x0002322C File Offset: 0x0002142C
		public float GetReactionTorque(float timeStep)
		{
			return SpringJoint2D.INTERNAL_CALL_GetReactionTorque(this, timeStep);
		}

		// Token: 0x06001591 RID: 5521
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetReactionTorque(SpringJoint2D self, float timeStep);
	}
}
