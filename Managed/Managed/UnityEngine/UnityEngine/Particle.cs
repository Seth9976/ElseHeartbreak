using System;

namespace UnityEngine
{
	// Token: 0x020000B5 RID: 181
	public struct Particle
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000BB58 File Offset: 0x00009D58
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000BB60 File Offset: 0x00009D60
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000BB6C File Offset: 0x00009D6C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000BB74 File Offset: 0x00009D74
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000BB80 File Offset: 0x00009D80
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000BB88 File Offset: 0x00009D88
		public float energy
		{
			get
			{
				return this.m_Energy;
			}
			set
			{
				this.m_Energy = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000BB94 File Offset: 0x00009D94
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public float startEnergy
		{
			get
			{
				return this.m_StartEnergy;
			}
			set
			{
				this.m_StartEnergy = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000BBBC File Offset: 0x00009DBC
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public float rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public float angularVelocity
		{
			get
			{
				return this.m_AngularVelocity;
			}
			set
			{
				this.m_AngularVelocity = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x0400027A RID: 634
		private Vector3 m_Position;

		// Token: 0x0400027B RID: 635
		private Vector3 m_Velocity;

		// Token: 0x0400027C RID: 636
		private float m_Size;

		// Token: 0x0400027D RID: 637
		private float m_Rotation;

		// Token: 0x0400027E RID: 638
		private float m_AngularVelocity;

		// Token: 0x0400027F RID: 639
		private float m_Energy;

		// Token: 0x04000280 RID: 640
		private float m_StartEnergy;

		// Token: 0x04000281 RID: 641
		private Color m_Color;
	}
}
