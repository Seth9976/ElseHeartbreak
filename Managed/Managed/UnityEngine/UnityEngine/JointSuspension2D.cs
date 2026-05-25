using System;

namespace UnityEngine
{
	// Token: 0x020001BC RID: 444
	public struct JointSuspension2D
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00023178 File Offset: 0x00021378
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00023180 File Offset: 0x00021380
		public float dampingRatio
		{
			get
			{
				return this.m_DampingRatio;
			}
			set
			{
				this.m_DampingRatio = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0002318C File Offset: 0x0002138C
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x00023194 File Offset: 0x00021394
		public float frequency
		{
			get
			{
				return this.m_Frequency;
			}
			set
			{
				this.m_Frequency = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000231A0 File Offset: 0x000213A0
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x000231A8 File Offset: 0x000213A8
		public float angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x040006BD RID: 1725
		private float m_DampingRatio;

		// Token: 0x040006BE RID: 1726
		private float m_Frequency;

		// Token: 0x040006BF RID: 1727
		private float m_Angle;
	}
}
