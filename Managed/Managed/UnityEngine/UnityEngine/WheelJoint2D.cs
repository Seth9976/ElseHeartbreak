using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C3 RID: 451
	public sealed class WheelJoint2D : AnchoredJoint2D
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00023348 File Offset: 0x00021548
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00023360 File Offset: 0x00021560
		public JointSuspension2D suspension
		{
			get
			{
				JointSuspension2D jointSuspension2D;
				this.INTERNAL_get_suspension(out jointSuspension2D);
				return jointSuspension2D;
			}
			set
			{
				this.INTERNAL_set_suspension(ref value);
			}
		}

		// Token: 0x060015CA RID: 5578
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_suspension(out JointSuspension2D value);

		// Token: 0x060015CB RID: 5579
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_suspension(ref JointSuspension2D value);

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060015CC RID: 5580
		// (set) Token: 0x060015CD RID: 5581
		public extern bool useMotor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0002336C File Offset: 0x0002156C
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00023384 File Offset: 0x00021584
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

		// Token: 0x060015D0 RID: 5584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor2D value);

		// Token: 0x060015D1 RID: 5585
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor2D value);

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060015D2 RID: 5586
		public extern float jointTranslation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060015D3 RID: 5587
		public extern float jointSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00023390 File Offset: 0x00021590
		public float GetMotorTorque(float timeStep)
		{
			return WheelJoint2D.INTERNAL_CALL_GetMotorTorque(this, timeStep);
		}

		// Token: 0x060015D5 RID: 5589
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_GetMotorTorque(WheelJoint2D self, float timeStep);
	}
}
