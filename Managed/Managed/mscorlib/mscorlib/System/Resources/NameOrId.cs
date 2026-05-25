using System;

namespace System.Resources
{
	// Token: 0x02000314 RID: 788
	internal class NameOrId
	{
		// Token: 0x06002850 RID: 10320 RVA: 0x00090BF8 File Offset: 0x0008EDF8
		public NameOrId(string name)
		{
			this.name = name;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00090C08 File Offset: 0x0008EE08
		public NameOrId(int id)
		{
			this.id = id;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x00090C18 File Offset: 0x0008EE18
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x00090C28 File Offset: 0x0008EE28
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x00090C30 File Offset: 0x0008EE30
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00090C38 File Offset: 0x0008EE38
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id + ")";
		}

		// Token: 0x0400105E RID: 4190
		private string name;

		// Token: 0x0400105F RID: 4191
		private int id;
	}
}
