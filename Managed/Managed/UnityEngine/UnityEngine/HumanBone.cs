using System;

namespace UnityEngine
{
	// Token: 0x02000201 RID: 513
	public struct HumanBone
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00024830 File Offset: 0x00022A30
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x00024838 File Offset: 0x00022A38
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00024844 File Offset: 0x00022A44
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0002484C File Offset: 0x00022A4C
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000775 RID: 1909
		private string m_BoneName;

		// Token: 0x04000776 RID: 1910
		private string m_HumanName;

		// Token: 0x04000777 RID: 1911
		public HumanLimit limit;
	}
}
