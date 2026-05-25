using System;

namespace UnityEngine
{
	// Token: 0x020001C9 RID: 457
	public struct NavMeshHit
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00023568 File Offset: 0x00021768
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x00023570 File Offset: 0x00021770
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

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0002357C File Offset: 0x0002177C
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x00023584 File Offset: 0x00021784
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00023590 File Offset: 0x00021790
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x00023598 File Offset: 0x00021798
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000235A4 File Offset: 0x000217A4
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x000235AC File Offset: 0x000217AC
		public int mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000235B8 File Offset: 0x000217B8
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x000235C8 File Offset: 0x000217C8
		public bool hit
		{
			get
			{
				return this.m_Hit != 0;
			}
			set
			{
				this.m_Hit = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x040006D0 RID: 1744
		private Vector3 m_Position;

		// Token: 0x040006D1 RID: 1745
		private Vector3 m_Normal;

		// Token: 0x040006D2 RID: 1746
		private float m_Distance;

		// Token: 0x040006D3 RID: 1747
		private int m_Mask;

		// Token: 0x040006D4 RID: 1748
		private int m_Hit;
	}
}
