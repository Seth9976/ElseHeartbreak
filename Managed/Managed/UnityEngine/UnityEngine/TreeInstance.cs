using System;

namespace UnityEngine
{
	// Token: 0x02000214 RID: 532
	public struct TreeInstance
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00024B8C File Offset: 0x00022D8C
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x00024B94 File Offset: 0x00022D94
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00024BA0 File Offset: 0x00022DA0
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x00024BA8 File Offset: 0x00022DA8
		public float widthScale
		{
			get
			{
				return this.m_WidthScale;
			}
			set
			{
				this.m_WidthScale = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00024BB4 File Offset: 0x00022DB4
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x00024BBC File Offset: 0x00022DBC
		public float heightScale
		{
			get
			{
				return this.m_HeightScale;
			}
			set
			{
				this.m_HeightScale = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00024BC8 File Offset: 0x00022DC8
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x00024BD8 File Offset: 0x00022DD8
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00024BE8 File Offset: 0x00022DE8
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x00024BF8 File Offset: 0x00022DF8
		public Color lightmapColor
		{
			get
			{
				return this.m_LightmapColor;
			}
			set
			{
				this.m_LightmapColor = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00024C08 File Offset: 0x00022E08
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x00024C10 File Offset: 0x00022E10
		public int prototypeIndex
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00024C1C File Offset: 0x00022E1C
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x00024C24 File Offset: 0x00022E24
		internal float temporaryDistance
		{
			get
			{
				return this.m_TemporaryDistance;
			}
			set
			{
				this.m_TemporaryDistance = value;
			}
		}

		// Token: 0x0400081F RID: 2079
		private Vector3 m_Position;

		// Token: 0x04000820 RID: 2080
		private float m_WidthScale;

		// Token: 0x04000821 RID: 2081
		private float m_HeightScale;

		// Token: 0x04000822 RID: 2082
		private Color32 m_Color;

		// Token: 0x04000823 RID: 2083
		private Color32 m_LightmapColor;

		// Token: 0x04000824 RID: 2084
		private int m_Index;

		// Token: 0x04000825 RID: 2085
		private float m_TemporaryDistance;
	}
}
