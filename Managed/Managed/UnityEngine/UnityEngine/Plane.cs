using System;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	public struct Plane
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0001BBBC File Offset: 0x00019DBC
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0001BC24 File Offset: 0x00019E24
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0001BC2C File Offset: 0x00019E2C
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0001BC38 File Offset: 0x00019E38
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0001BC40 File Offset: 0x00019E40
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001BC4C File Offset: 0x00019E4C
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.normal = Vector3.Normalize(inNormal);
			this.distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001BC68 File Offset: 0x00019E68
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.distance = -Vector3.Dot(this.normal, a);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		public float GetDistanceToPoint(Vector3 inPt)
		{
			return Vector3.Dot(this.normal, inPt) + this.distance;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001BCC0 File Offset: 0x00019EC0
		public bool GetSide(Vector3 inPt)
		{
			return Vector3.Dot(this.normal, inPt) + this.distance > 0f;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0001BCDC File Offset: 0x00019EDC
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0001BD2C File Offset: 0x00019F2C
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.normal);
			float num2 = -Vector3.Dot(ray.origin, this.normal) - this.distance;
			if (Mathf.Approximately(num, 0f))
			{
				enter = 0f;
				return false;
			}
			enter = num2 / num;
			return enter > 0f;
		}

		// Token: 0x0400050F RID: 1295
		private Vector3 m_Normal;

		// Token: 0x04000510 RID: 1296
		private float m_Distance;
	}
}
