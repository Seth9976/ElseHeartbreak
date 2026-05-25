using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ControllerColliderHit
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00021A64 File Offset: 0x0001FC64
		public CharacterController controller
		{
			get
			{
				return this.m_Controller;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00021A6C File Offset: 0x0001FC6C
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00021A74 File Offset: 0x0001FC74
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Collider.attachedRigidbody;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00021A84 File Offset: 0x0001FC84
		public GameObject gameObject
		{
			get
			{
				return this.m_Collider.gameObject;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00021A94 File Offset: 0x0001FC94
		public Transform transform
		{
			get
			{
				return this.m_Collider.transform;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00021AAC File Offset: 0x0001FCAC
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00021AB4 File Offset: 0x0001FCB4
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public float moveLength
		{
			get
			{
				return this.m_MoveLength;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00021AD4 File Offset: 0x0001FCD4
		private bool push
		{
			get
			{
				return this.m_Push != 0;
			}
			set
			{
				this.m_Push = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x04000683 RID: 1667
		internal CharacterController m_Controller;

		// Token: 0x04000684 RID: 1668
		internal Collider m_Collider;

		// Token: 0x04000685 RID: 1669
		internal Vector3 m_Point;

		// Token: 0x04000686 RID: 1670
		internal Vector3 m_Normal;

		// Token: 0x04000687 RID: 1671
		internal Vector3 m_MoveDirection;

		// Token: 0x04000688 RID: 1672
		internal float m_MoveLength;

		// Token: 0x04000689 RID: 1673
		internal int m_Push;
	}
}
