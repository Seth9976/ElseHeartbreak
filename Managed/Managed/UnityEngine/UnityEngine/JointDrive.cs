using System;

namespace UnityEngine
{
	// Token: 0x0200018B RID: 395
	public struct JointDrive
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00020F38 File Offset: 0x0001F138
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00020F40 File Offset: 0x0001F140
		public JointDriveMode mode
		{
			get
			{
				return (JointDriveMode)this.m_Mode;
			}
			set
			{
				this.m_Mode = (int)value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00020F4C File Offset: 0x0001F14C
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00020F54 File Offset: 0x0001F154
		public float positionSpring
		{
			get
			{
				return this.m_PositionSpring;
			}
			set
			{
				this.m_PositionSpring = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00020F60 File Offset: 0x0001F160
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00020F68 File Offset: 0x0001F168
		public float positionDamper
		{
			get
			{
				return this.m_PositionDamper;
			}
			set
			{
				this.m_PositionDamper = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00020F74 File Offset: 0x0001F174
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00020F7C File Offset: 0x0001F17C
		public float maximumForce
		{
			get
			{
				return this.m_MaximumForce;
			}
			set
			{
				this.m_MaximumForce = value;
			}
		}

		// Token: 0x0400064C RID: 1612
		private int m_Mode;

		// Token: 0x0400064D RID: 1613
		private float m_PositionSpring;

		// Token: 0x0400064E RID: 1614
		private float m_PositionDamper;

		// Token: 0x0400064F RID: 1615
		private float m_MaximumForce;
	}
}
