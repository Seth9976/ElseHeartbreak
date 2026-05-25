using System;

namespace UnityEngine
{
	// Token: 0x02000202 RID: 514
	public struct HumanDescription
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00024858 File Offset: 0x00022A58
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x00024860 File Offset: 0x00022A60
		public float upperArmTwist
		{
			get
			{
				return this.m_ArmTwist;
			}
			set
			{
				this.m_ArmTwist = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0002486C File Offset: 0x00022A6C
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x00024874 File Offset: 0x00022A74
		public float lowerArmTwist
		{
			get
			{
				return this.m_ForeArmTwist;
			}
			set
			{
				this.m_ForeArmTwist = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00024880 File Offset: 0x00022A80
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x00024888 File Offset: 0x00022A88
		public float upperLegTwist
		{
			get
			{
				return this.m_UpperLegTwist;
			}
			set
			{
				this.m_UpperLegTwist = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00024894 File Offset: 0x00022A94
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x0002489C File Offset: 0x00022A9C
		public float lowerLegTwist
		{
			get
			{
				return this.m_LegTwist;
			}
			set
			{
				this.m_LegTwist = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000248A8 File Offset: 0x00022AA8
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x000248B0 File Offset: 0x00022AB0
		public float armStretch
		{
			get
			{
				return this.m_ArmStretch;
			}
			set
			{
				this.m_ArmStretch = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000248BC File Offset: 0x00022ABC
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x000248C4 File Offset: 0x00022AC4
		public float legStretch
		{
			get
			{
				return this.m_LegStretch;
			}
			set
			{
				this.m_LegStretch = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x000248D0 File Offset: 0x00022AD0
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x000248D8 File Offset: 0x00022AD8
		public float feetSpacing
		{
			get
			{
				return this.m_FeetSpacing;
			}
			set
			{
				this.m_FeetSpacing = value;
			}
		}

		// Token: 0x04000778 RID: 1912
		public HumanBone[] human;

		// Token: 0x04000779 RID: 1913
		public SkeletonBone[] skeleton;

		// Token: 0x0400077A RID: 1914
		private float m_ArmTwist;

		// Token: 0x0400077B RID: 1915
		private float m_ForeArmTwist;

		// Token: 0x0400077C RID: 1916
		private float m_UpperLegTwist;

		// Token: 0x0400077D RID: 1917
		private float m_LegTwist;

		// Token: 0x0400077E RID: 1918
		private float m_ArmStretch;

		// Token: 0x0400077F RID: 1919
		private float m_LegStretch;

		// Token: 0x04000780 RID: 1920
		private float m_FeetSpacing;
	}
}
