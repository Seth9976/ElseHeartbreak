using System;

namespace UnityEngine
{
	// Token: 0x02000188 RID: 392
	public struct SoftJointLimit
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00020ED4 File Offset: 0x0001F0D4
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x00020EDC File Offset: 0x0001F0DC
		public float limit
		{
			get
			{
				return this.m_Limit;
			}
			set
			{
				this.m_Limit = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		public float spring
		{
			get
			{
				return this.m_Spring;
			}
			set
			{
				this.m_Spring = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00020EFC File Offset: 0x0001F0FC
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00020F04 File Offset: 0x0001F104
		public float damper
		{
			get
			{
				return this.m_Damper;
			}
			set
			{
				this.m_Damper = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00020F10 File Offset: 0x0001F110
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00020F18 File Offset: 0x0001F118
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00020F24 File Offset: 0x0001F124
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00020F2C File Offset: 0x0001F12C
		[Obsolete("Use SoftJointLimit.bounciness instead", true)]
		public float bouncyness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x0400063F RID: 1599
		private float m_Limit;

		// Token: 0x04000640 RID: 1600
		private float m_Bounciness;

		// Token: 0x04000641 RID: 1601
		private float m_Spring;

		// Token: 0x04000642 RID: 1602
		private float m_Damper;
	}
}
