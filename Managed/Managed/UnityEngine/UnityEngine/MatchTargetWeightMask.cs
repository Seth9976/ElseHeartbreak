using System;

namespace UnityEngine
{
	// Token: 0x020001FC RID: 508
	public struct MatchTargetWeightMask
	{
		// Token: 0x06001859 RID: 6233 RVA: 0x000242E0 File Offset: 0x000224E0
		public MatchTargetWeightMask(Vector3 positionXYZWeight, float rotationWeight)
		{
			this.m_PositionXYZWeight = positionXYZWeight;
			this.m_RotationWeight = rotationWeight;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000242F0 File Offset: 0x000224F0
		// (set) Token: 0x0600185B RID: 6235 RVA: 0x000242F8 File Offset: 0x000224F8
		public Vector3 positionXYZWeight
		{
			get
			{
				return this.m_PositionXYZWeight;
			}
			set
			{
				this.m_PositionXYZWeight = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00024304 File Offset: 0x00022504
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x0002430C File Offset: 0x0002250C
		public float rotationWeight
		{
			get
			{
				return this.m_RotationWeight;
			}
			set
			{
				this.m_RotationWeight = value;
			}
		}

		// Token: 0x04000769 RID: 1897
		private Vector3 m_PositionXYZWeight;

		// Token: 0x0400076A RID: 1898
		private float m_RotationWeight;
	}
}
