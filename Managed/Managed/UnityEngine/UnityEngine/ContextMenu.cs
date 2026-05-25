using System;

namespace UnityEngine
{
	// Token: 0x0200008D RID: 141
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000B0FC File Offset: 0x000092FC
		public ContextMenu(string name)
		{
			this.m_ItemName = name;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B10C File Offset: 0x0000930C
		public string menuItem
		{
			get
			{
				return this.m_ItemName;
			}
		}

		// Token: 0x04000220 RID: 544
		private string m_ItemName;
	}
}
