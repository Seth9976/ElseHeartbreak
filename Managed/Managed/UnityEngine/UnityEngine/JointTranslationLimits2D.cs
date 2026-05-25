using System;

namespace UnityEngine
{
	// Token: 0x020001BA RID: 442
	public struct JointTranslationLimits2D
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00023128 File Offset: 0x00021328
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x00023130 File Offset: 0x00021330
		public float min
		{
			get
			{
				return this.m_LowerTranslation;
			}
			set
			{
				this.m_LowerTranslation = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0002313C File Offset: 0x0002133C
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x00023144 File Offset: 0x00021344
		public float max
		{
			get
			{
				return this.m_UpperTranslation;
			}
			set
			{
				this.m_UpperTranslation = value;
			}
		}

		// Token: 0x040006B9 RID: 1721
		private float m_LowerTranslation;

		// Token: 0x040006BA RID: 1722
		private float m_UpperTranslation;
	}
}
