using System;

namespace UnityEngine
{
	// Token: 0x02000169 RID: 361
	public struct AccelerationEvent
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0001F074 File Offset: 0x0001D274
		public Vector3 acceleration
		{
			get
			{
				return this.m_Acceleration;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0001F07C File Offset: 0x0001D27C
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x04000605 RID: 1541
		private Vector3 m_Acceleration;

		// Token: 0x04000606 RID: 1542
		private float m_TimeDelta;
	}
}
