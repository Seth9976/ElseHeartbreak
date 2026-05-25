using System;

namespace UnityEngine
{
	// Token: 0x0200019E RID: 414
	public struct ContactPoint
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0002194C File Offset: 0x0001FB4C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00021954 File Offset: 0x0001FB54
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0002195C File Offset: 0x0001FB5C
		public Collider thisCollider
		{
			get
			{
				return this.m_ThisCollider;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00021964 File Offset: 0x0001FB64
		public Collider otherCollider
		{
			get
			{
				return this.m_OtherCollider;
			}
		}

		// Token: 0x04000673 RID: 1651
		internal Vector3 m_Point;

		// Token: 0x04000674 RID: 1652
		internal Vector3 m_Normal;

		// Token: 0x04000675 RID: 1653
		internal Collider m_ThisCollider;

		// Token: 0x04000676 RID: 1654
		internal Collider m_OtherCollider;
	}
}
