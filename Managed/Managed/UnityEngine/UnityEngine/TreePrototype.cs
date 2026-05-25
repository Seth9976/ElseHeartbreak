using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000210 RID: 528
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TreePrototype
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x00024934 File Offset: 0x00022B34
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x0002493C File Offset: 0x00022B3C
		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00024948 File Offset: 0x00022B48
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00024950 File Offset: 0x00022B50
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x04000809 RID: 2057
		internal GameObject m_Prefab;

		// Token: 0x0400080A RID: 2058
		internal float m_BendFactor;
	}
}
