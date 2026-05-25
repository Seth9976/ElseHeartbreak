using System;

namespace UnityEngine
{
	// Token: 0x02000181 RID: 385
	public struct JointMotor
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00020D50 File Offset: 0x0001EF50
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00020D58 File Offset: 0x0001EF58
		public float targetVelocity
		{
			get
			{
				return this.m_TargetVelocity;
			}
			set
			{
				this.m_TargetVelocity = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00020D64 File Offset: 0x0001EF64
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00020D6C File Offset: 0x0001EF6C
		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00020D78 File Offset: 0x0001EF78
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00020D80 File Offset: 0x0001EF80
		public bool freeSpin
		{
			get
			{
				return this.m_FreeSpin;
			}
			set
			{
				this.m_FreeSpin = value;
			}
		}

		// Token: 0x04000633 RID: 1587
		private float m_TargetVelocity;

		// Token: 0x04000634 RID: 1588
		private float m_Force;

		// Token: 0x04000635 RID: 1589
		private bool m_FreeSpin;
	}
}
