using System;

namespace UnityEngine
{
	// Token: 0x020001B6 RID: 438
	public struct ContactPoint2D
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00023040 File Offset: 0x00021240
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00023048 File Offset: 0x00021248
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00023050 File Offset: 0x00021250
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00023058 File Offset: 0x00021258
		public Collider2D otherCollider
		{
			get
			{
				return this.m_OtherCollider;
			}
		}

		// Token: 0x040006AA RID: 1706
		internal Vector2 m_Point;

		// Token: 0x040006AB RID: 1707
		internal Vector2 m_Normal;

		// Token: 0x040006AC RID: 1708
		internal Collider2D m_Collider;

		// Token: 0x040006AD RID: 1709
		internal Collider2D m_OtherCollider;
	}
}
