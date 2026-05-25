using System;

namespace UnityEngine
{
	// Token: 0x02000119 RID: 281
	public struct Ray
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x0001BA0C File Offset: 0x00019C0C
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001BA24 File Offset: 0x00019C24
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001BA38 File Offset: 0x00019C38
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0001BA40 File Offset: 0x00019C40
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001BA50 File Offset: 0x00019C50
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[] { this.m_Origin, this.m_Direction });
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		public string ToString(string format)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format),
				this.m_Direction.ToString(format)
			});
		}

		// Token: 0x0400050B RID: 1291
		private Vector3 m_Origin;

		// Token: 0x0400050C RID: 1292
		private Vector3 m_Direction;
	}
}
