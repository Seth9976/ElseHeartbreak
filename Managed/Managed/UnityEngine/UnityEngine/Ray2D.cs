using System;

namespace UnityEngine
{
	// Token: 0x0200011A RID: 282
	public struct Ray2D
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001BAFC File Offset: 0x00019CFC
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0001BB04 File Offset: 0x00019D04
		public Vector2 origin
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0001BB10 File Offset: 0x00019D10
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0001BB18 File Offset: 0x00019D18
		public Vector2 direction
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

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001BB28 File Offset: 0x00019D28
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001BB44 File Offset: 0x00019D44
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[] { this.m_Origin, this.m_Direction });
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001BB80 File Offset: 0x00019D80
		public string ToString(string format)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format),
				this.m_Direction.ToString(format)
			});
		}

		// Token: 0x0400050D RID: 1293
		private Vector2 m_Origin;

		// Token: 0x0400050E RID: 1294
		private Vector2 m_Direction;
	}
}
