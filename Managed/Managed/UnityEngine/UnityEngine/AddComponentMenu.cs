using System;

namespace UnityEngine
{
	// Token: 0x0200008C RID: 140
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000B0BC File Offset: 0x000092BC
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B0EC File Offset: 0x000092EC
		public string componentMenu
		{
			get
			{
				return this.m_AddComponentMenu;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public int componentOrder
		{
			get
			{
				return this.m_Ordering;
			}
		}

		// Token: 0x0400021E RID: 542
		private string m_AddComponentMenu;

		// Token: 0x0400021F RID: 543
		private int m_Ordering;
	}
}
