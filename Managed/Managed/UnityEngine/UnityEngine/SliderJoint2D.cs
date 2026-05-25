using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C2 RID: 450
	public sealed class SliderJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060015B3 RID: 5555
		// (set) Token: 0x060015B4 RID: 5556
		public extern float angle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060015B5 RID: 5557
		// (set) Token: 0x060015B6 RID: 5558
		public extern bool useMotor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060015B7 RID: 5559
		// (set) Token: 0x060015B8 RID: 5560
		public extern bool useLimits
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000232EC File Offset: 0x000214EC
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00023304 File Offset: 0x00021504
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

		// Token: 0x060015BB RID: 5563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x060015BC RID: 5564
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00023310 File Offset: 0x00021510
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x00023328 File Offset: 0x00021528
		public JointTranslationLimits2D limits
		{
			get
			{
				JointTranslationLimits2D jointTranslationLimits2D;
				this.INTERNAL_get_limits(out jointTranslationLimits2D);
				return jointTranslationLimits2D;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x060015BF RID: 5567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointTranslationLimits2D value);

		// Token: 0x060015C0 RID: 5568
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointTranslationLimits2D value);

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060015C1 RID: 5569
		public extern JointLimitState2D limitState
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060015C2 RID: 5570
		public extern float referenceAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060015C3 RID: 5571
		public extern float jointTranslation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060015C4 RID: 5572
		public extern float jointSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00023334 File Offset: 0x00021534
		public float GetMotorForce(float timeStep)
		{
			return SliderJoint2D.INTERNAL_CALL_GetMotorForce(this, timeStep);
		}

		// Token: 0x060015C6 RID: 5574
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorForce(SliderJoint2D self, float timeStep);
	}
}
