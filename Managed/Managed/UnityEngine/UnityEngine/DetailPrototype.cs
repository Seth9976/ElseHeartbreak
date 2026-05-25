using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000212 RID: 530
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DetailPrototype
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000249F8 File Offset: 0x00022BF8
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00024A00 File Offset: 0x00022C00
		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00024A0C File Offset: 0x00022C0C
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00024A14 File Offset: 0x00022C14
		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00024A20 File Offset: 0x00022C20
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x00024A28 File Offset: 0x00022C28
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00024A34 File Offset: 0x00022C34
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x00024A3C File Offset: 0x00022C3C
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00024A48 File Offset: 0x00022C48
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x00024A50 File Offset: 0x00022C50
		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00024A5C File Offset: 0x00022C5C
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x00024A64 File Offset: 0x00022C64
		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00024A70 File Offset: 0x00022C70
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x00024A78 File Offset: 0x00022C78
		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00024A84 File Offset: 0x00022C84
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00024A8C File Offset: 0x00022C8C
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00024A98 File Offset: 0x00022C98
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x00024AA0 File Offset: 0x00022CA0
		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00024AAC File Offset: 0x00022CAC
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x00024AB4 File Offset: 0x00022CB4
		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00024AC0 File Offset: 0x00022CC0
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x00024AC8 File Offset: 0x00022CC8
		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00024AD4 File Offset: 0x00022CD4
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x00024AE4 File Offset: 0x00022CE4
		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x0400080F RID: 2063
		private GameObject m_Prototype;

		// Token: 0x04000810 RID: 2064
		private Texture2D m_PrototypeTexture;

		// Token: 0x04000811 RID: 2065
		private Color m_HealthyColor = new Color(0.2627451f, 0.9764706f, 0.16470589f, 1f);

		// Token: 0x04000812 RID: 2066
		private Color m_DryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		// Token: 0x04000813 RID: 2067
		private float m_MinWidth = 1f;

		// Token: 0x04000814 RID: 2068
		private float m_MaxWidth = 2f;

		// Token: 0x04000815 RID: 2069
		private float m_MinHeight = 1f;

		// Token: 0x04000816 RID: 2070
		private float m_MaxHeight = 2f;

		// Token: 0x04000817 RID: 2071
		private float m_NoiseSpread = 0.1f;

		// Token: 0x04000818 RID: 2072
		private float m_BendFactor = 0.1f;

		// Token: 0x04000819 RID: 2073
		private int m_RenderMode = 2;

		// Token: 0x0400081A RID: 2074
		private int m_UsePrototypeMesh;
	}
}
