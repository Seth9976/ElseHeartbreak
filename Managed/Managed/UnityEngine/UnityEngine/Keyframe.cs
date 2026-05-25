using System;

namespace UnityEngine
{
	// Token: 0x020001EB RID: 491
	public struct Keyframe
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x00023D5C File Offset: 0x00021F5C
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00023D90 File Offset: 0x00021F90
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00023DB0 File Offset: 0x00021FB0
		// (set) Token: 0x060017C8 RID: 6088 RVA: 0x00023DB8 File Offset: 0x00021FB8
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00023DC4 File Offset: 0x00021FC4
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x00023DCC File Offset: 0x00021FCC
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x00023DD8 File Offset: 0x00021FD8
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x00023DE0 File Offset: 0x00021FE0
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x00023DEC File Offset: 0x00021FEC
		// (set) Token: 0x060017CE RID: 6094 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00023E00 File Offset: 0x00022000
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x00023E04 File Offset: 0x00022004
		public int tangentMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x04000732 RID: 1842
		private float m_Time;

		// Token: 0x04000733 RID: 1843
		private float m_Value;

		// Token: 0x04000734 RID: 1844
		private float m_InTangent;

		// Token: 0x04000735 RID: 1845
		private float m_OutTangent;
	}
}
