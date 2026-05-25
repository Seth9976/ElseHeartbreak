using System;

namespace UnityEngine
{
	// Token: 0x020001FA RID: 506
	public struct AnimatorStateInfo
	{
		// Token: 0x0600184D RID: 6221 RVA: 0x0002423C File Offset: 0x0002243C
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00024268 File Offset: 0x00022468
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00024270 File Offset: 0x00022470
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00024278 File Offset: 0x00022478
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00024280 File Offset: 0x00022480
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00024288 File Offset: 0x00022488
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00024298 File Offset: 0x00022498
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x04000760 RID: 1888
		private int m_Name;

		// Token: 0x04000761 RID: 1889
		private int m_Path;

		// Token: 0x04000762 RID: 1890
		private float m_NormalizedTime;

		// Token: 0x04000763 RID: 1891
		private float m_Length;

		// Token: 0x04000764 RID: 1892
		private int m_Tag;

		// Token: 0x04000765 RID: 1893
		private int m_Loop;
	}
}
