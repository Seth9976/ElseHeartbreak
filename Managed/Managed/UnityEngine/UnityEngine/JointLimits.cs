using System;

namespace UnityEngine
{
	// Token: 0x02000183 RID: 387
	public struct JointLimits
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00020D8C File Offset: 0x0001EF8C
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00020D94 File Offset: 0x0001EF94
		public float min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		public float minBounce
		{
			get
			{
				return this.m_MinBounce;
			}
			set
			{
				this.m_MinBounce = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00020DBC File Offset: 0x0001EFBC
		public float max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00020DD0 File Offset: 0x0001EFD0
		public float maxBounce
		{
			get
			{
				return this.m_MaxBounce;
			}
			set
			{
				this.m_MaxBounce = value;
			}
		}

		// Token: 0x04000639 RID: 1593
		private float m_Min;

		// Token: 0x0400063A RID: 1594
		private float m_MinBounce;

		// Token: 0x0400063B RID: 1595
		private float m_MinHardness;

		// Token: 0x0400063C RID: 1596
		private float m_Max;

		// Token: 0x0400063D RID: 1597
		private float m_MaxBounce;

		// Token: 0x0400063E RID: 1598
		private float m_MaxHardness;
	}
}
