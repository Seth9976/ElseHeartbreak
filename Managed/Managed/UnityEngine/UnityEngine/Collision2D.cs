using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001B7 RID: 439
	[StructLayout(LayoutKind.Sequential)]
	public class Collision2D
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00023068 File Offset: 0x00021268
		public Rigidbody2D rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00023070 File Offset: 0x00021270
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00023078 File Offset: 0x00021278
		public Transform transform
		{
			get
			{
				return (!(this.rigidbody != null)) ? this.collider.transform : this.rigidbody.transform;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000230B4 File Offset: 0x000212B4
		public GameObject gameObject
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider.gameObject : this.m_Rigidbody.gameObject;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000230F0 File Offset: 0x000212F0
		public ContactPoint2D[] contacts
		{
			get
			{
				return this.m_Contacts;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000230F8 File Offset: 0x000212F8
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x040006AE RID: 1710
		internal Rigidbody2D m_Rigidbody;

		// Token: 0x040006AF RID: 1711
		internal Collider2D m_Collider;

		// Token: 0x040006B0 RID: 1712
		internal ContactPoint2D[] m_Contacts;

		// Token: 0x040006B1 RID: 1713
		internal Vector2 m_RelativeVelocity;
	}
}
