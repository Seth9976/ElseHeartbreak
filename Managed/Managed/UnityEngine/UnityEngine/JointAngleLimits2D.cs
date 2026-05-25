using System;

namespace UnityEngine
{
	// Token: 0x020001B9 RID: 441
	public struct JointAngleLimits2D
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00023100 File Offset: 0x00021300
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x00023108 File Offset: 0x00021308
		public float min
		{
			get
			{
				return this.m_LowerAngle;
			}
			set
			{
				this.m_LowerAngle = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00023114 File Offset: 0x00021314
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x0002311C File Offset: 0x0002131C
		public float max
		{
			get
			{
				return this.m_UpperAngle;
			}
			set
			{
				this.m_UpperAngle = value;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private float m_LowerAngle;

		// Token: 0x040006B8 RID: 1720
		private float m_UpperAngle;
	}
}
