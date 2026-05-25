using System;

namespace UnityEngine
{
	// Token: 0x02000198 RID: 408
	public struct WheelFrictionCurve
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000214EC File Offset: 0x0001F6EC
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x000214F4 File Offset: 0x0001F6F4
		public float extremumSlip
		{
			get
			{
				return this.m_ExtremumSlip;
			}
			set
			{
				this.m_ExtremumSlip = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00021500 File Offset: 0x0001F700
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x00021508 File Offset: 0x0001F708
		public float extremumValue
		{
			get
			{
				return this.m_ExtremumValue;
			}
			set
			{
				this.m_ExtremumValue = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00021514 File Offset: 0x0001F714
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0002151C File Offset: 0x0001F71C
		public float asymptoteSlip
		{
			get
			{
				return this.m_AsymptoteSlip;
			}
			set
			{
				this.m_AsymptoteSlip = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00021528 File Offset: 0x0001F728
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00021530 File Offset: 0x0001F730
		public float asymptoteValue
		{
			get
			{
				return this.m_AsymptoteValue;
			}
			set
			{
				this.m_AsymptoteValue = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0002153C File Offset: 0x0001F73C
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00021544 File Offset: 0x0001F744
		public float stiffness
		{
			get
			{
				return this.m_Stiffness;
			}
			set
			{
				this.m_Stiffness = value;
			}
		}

		// Token: 0x0400065B RID: 1627
		private float m_ExtremumSlip;

		// Token: 0x0400065C RID: 1628
		private float m_ExtremumValue;

		// Token: 0x0400065D RID: 1629
		private float m_AsymptoteSlip;

		// Token: 0x0400065E RID: 1630
		private float m_AsymptoteValue;

		// Token: 0x0400065F RID: 1631
		private float m_Stiffness;
	}
}
