using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x0200001E RID: 30
	internal class Doublet
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000AC64 File Offset: 0x00008E64
		public Doublet(int count, string columnname)
		{
			this.count = count;
			this.columnNames.Add(columnname);
		}

		// Token: 0x040000BC RID: 188
		public int count;

		// Token: 0x040000BD RID: 189
		public ArrayList columnNames = new ArrayList();
	}
}
