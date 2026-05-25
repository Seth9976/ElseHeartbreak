using System;

namespace UnityEngine
{
	// Token: 0x02000200 RID: 512
	public struct HumanLimit
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x000247B8 File Offset: 0x000229B8
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x000247C8 File Offset: 0x000229C8
		public bool useDefaultValues
		{
			get
			{
				return this.m_UseDefaultValues != 0;
			}
			set
			{
				this.m_UseDefaultValues = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000247E0 File Offset: 0x000229E0
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x000247E8 File Offset: 0x000229E8
		public Vector3 min
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

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000247F4 File Offset: 0x000229F4
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x000247FC File Offset: 0x000229FC
		public Vector3 max
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

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00024808 File Offset: 0x00022A08
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x00024810 File Offset: 0x00022A10
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0002481C File Offset: 0x00022A1C
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x00024824 File Offset: 0x00022A24
		public float axisLength
		{
			get
			{
				return this.m_AxisLength;
			}
			set
			{
				this.m_AxisLength = value;
			}
		}

		// Token: 0x04000770 RID: 1904
		private Vector3 m_Min;

		// Token: 0x04000771 RID: 1905
		private Vector3 m_Max;

		// Token: 0x04000772 RID: 1906
		private Vector3 m_Center;

		// Token: 0x04000773 RID: 1907
		private float m_AxisLength;

		// Token: 0x04000774 RID: 1908
		private int m_UseDefaultValues;
	}
}
