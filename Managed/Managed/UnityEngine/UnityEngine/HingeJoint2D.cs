using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C1 RID: 449
	public sealed class HingeJoint2D : AnchoredJoint2D
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600159C RID: 5532
		// (set) Token: 0x0600159D RID: 5533
		public extern bool useMotor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600159E RID: 5534
		// (set) Token: 0x0600159F RID: 5535
		public extern bool useLimits
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0002326C File Offset: 0x0002146C
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00023284 File Offset: 0x00021484
		public JointMotor2D motor
		{
			get
			{
				JointMotor2D jointMotor2D;
				this.INTERNAL_get_motor(out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				this.INTERNAL_set_motor(ref value);
			}
		}

		// Token: 0x060015A2 RID: 5538
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x060015A3 RID: 5539
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00023290 File Offset: 0x00021490
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000232A8 File Offset: 0x000214A8
		public JointAngleLimits2D limits
		{
			get
			{
				JointAngleLimits2D jointAngleLimits2D;
				this.INTERNAL_get_limits(out jointAngleLimits2D);
				return jointAngleLimits2D;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x060015A6 RID: 5542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointAngleLimits2D value);

		// Token: 0x060015A7 RID: 5543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointAngleLimits2D value);

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060015A8 RID: 5544
		public extern JointLimitState2D limitState
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060015A9 RID: 5545
		public extern float referenceAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060015AA RID: 5546
		public extern float jointAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060015AB RID: 5547
		public extern float jointSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000232B4 File Offset: 0x000214B4
		public Vector2 GetReactionForce(float timeStep)
		{
			Vector2 vector;
			HingeJoint2D.HingeJoint2D_CUSTOM_INTERNAL_GetReactionForce(this, timeStep, out vector);
			return vector;
		}

		// Token: 0x060015AD RID: 5549
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void HingeJoint2D_CUSTOM_INTERNAL_GetReactionForce(HingeJoint2D joint, float timeStep, out Vector2 value);

		// Token: 0x060015AE RID: 5550 RVA: 0x000232CC File Offset: 0x000214CC
		public float GetReactionTorque(float timeStep)
		{
			return HingeJoint2D.INTERNAL_CALL_GetReactionTorque(this, timeStep);
		}

		// Token: 0x060015AF RID: 5551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetReactionTorque(HingeJoint2D self, float timeStep);

		// Token: 0x060015B0 RID: 5552 RVA: 0x000232D8 File Offset: 0x000214D8
		public float GetMotorTorque(float timeStep)
		{
			return HingeJoint2D.INTERNAL_CALL_GetMotorTorque(this, timeStep);
		}

		// Token: 0x060015B1 RID: 5553
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorTorque(HingeJoint2D self, float timeStep);
	}
}
