using System;

namespace UnityEngine
{
	// Token: 0x02000167 RID: 359
	public struct Touch
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0001F03C File Offset: 0x0001D23C
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0001F044 File Offset: 0x0001D244
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0001F04C File Offset: 0x0001D24C
		public Vector2 rawPosition
		{
			get
			{
				return this.m_RawPosition;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0001F054 File Offset: 0x0001D254
		public Vector2 deltaPosition
		{
			get
			{
				return this.m_PositionDelta;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0001F05C File Offset: 0x0001D25C
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0001F064 File Offset: 0x0001D264
		public int tapCount
		{
			get
			{
				return this.m_TapCount;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0001F06C File Offset: 0x0001D26C
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
		}

		// Token: 0x040005F6 RID: 1526
		private int m_FingerId;

		// Token: 0x040005F7 RID: 1527
		private Vector2 m_Position;

		// Token: 0x040005F8 RID: 1528
		private Vector2 m_RawPosition;

		// Token: 0x040005F9 RID: 1529
		private Vector2 m_PositionDelta;

		// Token: 0x040005FA RID: 1530
		private float m_TimeDelta;

		// Token: 0x040005FB RID: 1531
		private int m_TapCount;

		// Token: 0x040005FC RID: 1532
		private TouchPhase m_Phase;
	}
}
