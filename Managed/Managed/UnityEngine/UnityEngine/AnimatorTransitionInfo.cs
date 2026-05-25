using System;

namespace UnityEngine
{
	// Token: 0x020001FB RID: 507
	public struct AnimatorTransitionInfo
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x000242A8 File Offset: 0x000224A8
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000242B8 File Offset: 0x000224B8
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000242C8 File Offset: 0x000224C8
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000242D0 File Offset: 0x000224D0
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000242D8 File Offset: 0x000224D8
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x04000766 RID: 1894
		private int m_Name;

		// Token: 0x04000767 RID: 1895
		private int m_UserName;

		// Token: 0x04000768 RID: 1896
		private float m_NormalizedTime;
	}
}
