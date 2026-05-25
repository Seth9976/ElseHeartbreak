using System;

namespace UnityEngine
{
	// Token: 0x020001E4 RID: 484
	public struct WebCamDevice
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00023A8C File Offset: 0x00021C8C
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00023A94 File Offset: 0x00021C94
		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) == 1;
			}
		}

		// Token: 0x04000725 RID: 1829
		internal string m_Name;

		// Token: 0x04000726 RID: 1830
		internal int m_Flags;
	}
}
