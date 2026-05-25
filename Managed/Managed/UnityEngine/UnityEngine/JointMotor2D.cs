using System;

namespace UnityEngine
{
	// Token: 0x020001BB RID: 443
	public struct JointMotor2D
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00023150 File Offset: 0x00021350
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x00023158 File Offset: 0x00021358
		public float motorSpeed
		{
			get
			{
				return this.m_MotorSpeed;
			}
			set
			{
				this.m_MotorSpeed = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00023164 File Offset: 0x00021364
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0002316C File Offset: 0x0002136C
		public float maxMotorTorque
		{
			get
			{
				return this.m_MaximumMotorTorque;
			}
			set
			{
				this.m_MaximumMotorTorque = value;
			}
		}

		// Token: 0x040006BB RID: 1723
		private float m_MotorSpeed;

		// Token: 0x040006BC RID: 1724
		private float m_MaximumMotorTorque;
	}
}
