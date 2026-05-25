using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C8 RID: 456
	public struct OffMeshLinkData
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00023520 File Offset: 0x00021720
		public bool valid
		{
			get
			{
				return this.m_Valid != 0;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00023530 File Offset: 0x00021730
		public bool activated
		{
			get
			{
				return this.m_Activated != 0;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00023540 File Offset: 0x00021740
		public OffMeshLinkType linkType
		{
			get
			{
				return this.m_LinkType;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00023548 File Offset: 0x00021748
		public Vector3 startPos
		{
			get
			{
				return this.m_StartPos;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00023550 File Offset: 0x00021750
		public Vector3 endPos
		{
			get
			{
				return this.m_EndPos;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00023558 File Offset: 0x00021758
		public OffMeshLink offMeshLink
		{
			get
			{
				return this.GetOffMeshLinkInternal(this.m_InstanceID);
			}
		}

		// Token: 0x06001637 RID: 5687
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLink GetOffMeshLinkInternal(int instanceID);

		// Token: 0x040006CA RID: 1738
		private int m_Valid;

		// Token: 0x040006CB RID: 1739
		private int m_Activated;

		// Token: 0x040006CC RID: 1740
		private int m_InstanceID;

		// Token: 0x040006CD RID: 1741
		private OffMeshLinkType m_LinkType;

		// Token: 0x040006CE RID: 1742
		private Vector3 m_StartPos;

		// Token: 0x040006CF RID: 1743
		private Vector3 m_EndPos;
	}
}
