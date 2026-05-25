using System;

namespace UnityEngine
{
	// Token: 0x0200016B RID: 363
	public struct LocationInfo
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0001F124 File Offset: 0x0001D324
		public float latitude
		{
			get
			{
				return this.m_Latitude;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0001F12C File Offset: 0x0001D32C
		public float longitude
		{
			get
			{
				return this.m_Longitude;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0001F134 File Offset: 0x0001D334
		public float altitude
		{
			get
			{
				return this.m_Altitude;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0001F13C File Offset: 0x0001D33C
		public float horizontalAccuracy
		{
			get
			{
				return this.m_HorizontalAccuracy;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0001F144 File Offset: 0x0001D344
		public float verticalAccuracy
		{
			get
			{
				return this.m_VerticalAccuracy;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0001F14C File Offset: 0x0001D34C
		public double timestamp
		{
			get
			{
				return this.m_Timestamp;
			}
		}

		// Token: 0x04000608 RID: 1544
		private double m_Timestamp;

		// Token: 0x04000609 RID: 1545
		private float m_Latitude;

		// Token: 0x0400060A RID: 1546
		private float m_Longitude;

		// Token: 0x0400060B RID: 1547
		private float m_Altitude;

		// Token: 0x0400060C RID: 1548
		private float m_HorizontalAccuracy;

		// Token: 0x0400060D RID: 1549
		private float m_VerticalAccuracy;
	}
}
