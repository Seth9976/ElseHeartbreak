using System;

namespace UnityEngine
{
	// Token: 0x020001AA RID: 426
	public struct RaycastHit2D
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00022BEC File Offset: 0x00020DEC
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00022BF4 File Offset: 0x00020DF4
		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00022C00 File Offset: 0x00020E00
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00022C08 File Offset: 0x00020E08
		public Vector2 point
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

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00022C14 File Offset: 0x00020E14
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00022C1C File Offset: 0x00020E1C
		public Vector2 normal
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

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00022C28 File Offset: 0x00020E28
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00022C30 File Offset: 0x00020E30
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

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00022C3C File Offset: 0x00020E3C
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x00022C44 File Offset: 0x00020E44
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00022C50 File Offset: 0x00020E50
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00022C58 File Offset: 0x00020E58
		public Rigidbody2D rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00022C88 File Offset: 0x00020E88
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				if (rigidbody != null)
				{
					return rigidbody.transform;
				}
				if (this.collider != null)
				{
					return this.collider.transform;
				}
				return null;
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00022CD0 File Offset: 0x00020ED0
		public int CompareTo(RaycastHit2D other)
		{
			if (this.collider == null)
			{
				return 1;
			}
			if (other.collider == null)
			{
				return -1;
			}
			return this.fraction.CompareTo(other.fraction);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00022D1C File Offset: 0x00020F1C
		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		// Token: 0x04000692 RID: 1682
		private Vector2 m_Centroid;

		// Token: 0x04000693 RID: 1683
		private Vector2 m_Point;

		// Token: 0x04000694 RID: 1684
		private Vector2 m_Normal;

		// Token: 0x04000695 RID: 1685
		private float m_Distance;

		// Token: 0x04000696 RID: 1686
		private float m_Fraction;

		// Token: 0x04000697 RID: 1687
		private Collider2D m_Collider;
	}
}
