using System;

namespace UnityEngine
{
	// Token: 0x02000199 RID: 409
	public struct WheelHit
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00021550 File Offset: 0x0001F750
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x00021558 File Offset: 0x0001F758
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
			set
			{
				this.m_Collider = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x00021564 File Offset: 0x0001F764
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x0002156C File Offset: 0x0001F76C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00021578 File Offset: 0x0001F778
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00021580 File Offset: 0x0001F780
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

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0002158C File Offset: 0x0001F78C
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x00021594 File Offset: 0x0001F794
		public Vector3 forwardDir
		{
			get
			{
				return this.m_ForwardDir;
			}
			set
			{
				this.m_ForwardDir = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000215A0 File Offset: 0x0001F7A0
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x000215A8 File Offset: 0x0001F7A8
		public Vector3 sidewaysDir
		{
			get
			{
				return this.m_SidewaysDir;
			}
			set
			{
				this.m_SidewaysDir = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000215B4 File Offset: 0x0001F7B4
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x000215BC File Offset: 0x0001F7BC
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000215C8 File Offset: 0x0001F7C8
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x000215D0 File Offset: 0x0001F7D0
		public float forwardSlip
		{
			get
			{
				return this.m_ForwardSlip;
			}
			set
			{
				this.m_Force = this.m_ForwardSlip;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x000215E0 File Offset: 0x0001F7E0
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x000215E8 File Offset: 0x0001F7E8
		public float sidewaysSlip
		{
			get
			{
				return this.m_SidewaysSlip;
			}
			set
			{
				this.m_SidewaysSlip = value;
			}
		}

		// Token: 0x04000660 RID: 1632
		private Vector3 m_Point;

		// Token: 0x04000661 RID: 1633
		private Vector3 m_Normal;

		// Token: 0x04000662 RID: 1634
		private Vector3 m_ForwardDir;

		// Token: 0x04000663 RID: 1635
		private Vector3 m_SidewaysDir;

		// Token: 0x04000664 RID: 1636
		private float m_Force;

		// Token: 0x04000665 RID: 1637
		private float m_ForwardSlip;

		// Token: 0x04000666 RID: 1638
		private float m_SidewaysSlip;

		// Token: 0x04000667 RID: 1639
		private Collider m_Collider;
	}
}
