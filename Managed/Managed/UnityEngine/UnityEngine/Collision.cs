using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200019F RID: 415
	[StructLayout(LayoutKind.Sequential)]
	public class Collision
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00021974 File Offset: 0x0001FB74
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0002197C File Offset: 0x0001FB7C
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00021984 File Offset: 0x0001FB84
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0002198C File Offset: 0x0001FB8C
		public Transform transform
		{
			get
			{
				return (!(this.rigidbody != null)) ? this.collider.transform : this.rigidbody.transform;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x000219C8 File Offset: 0x0001FBC8
		public GameObject gameObject
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider.gameObject : this.m_Rigidbody.gameObject;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00021A04 File Offset: 0x0001FC04
		public ContactPoint[] contacts
		{
			get
			{
				return this.m_Contacts;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00021A0C File Offset: 0x0001FC0C
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00021A1C File Offset: 0x0001FC1C
		[Obsolete("use Collision.relativeVelocity instead.")]
		public Vector3 impactForceSum
		{
			get
			{
				return this.relativeVelocity;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00021A24 File Offset: 0x0001FC24
		[Obsolete("will always return zero.")]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00021A2C File Offset: 0x0001FC2C
		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead")]
		public Component other
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider : this.m_Rigidbody;
			}
		}

		// Token: 0x04000677 RID: 1655
		internal Vector3 m_RelativeVelocity;

		// Token: 0x04000678 RID: 1656
		internal Rigidbody m_Rigidbody;

		// Token: 0x04000679 RID: 1657
		internal Collider m_Collider;

		// Token: 0x0400067A RID: 1658
		internal ContactPoint[] m_Contacts;
	}
}
