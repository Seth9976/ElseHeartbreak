using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000183 RID: 387
	internal class TableStructure
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x00056C08 File Offset: 0x00054E08
		public TableStructure(DataTable table)
		{
			this.Table = table;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00056C30 File Offset: 0x00054E30
		public bool ContainsColumn(string name)
		{
			foreach (object obj in this.NonOrdinalColumns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName == name)
				{
					return true;
				}
			}
			foreach (object obj2 in this.OrdinalColumns.Keys)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (dataColumn2.ColumnName == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400082D RID: 2093
		public DataTable Table;

		// Token: 0x0400082E RID: 2094
		public Hashtable OrdinalColumns = new Hashtable();

		// Token: 0x0400082F RID: 2095
		public ArrayList NonOrdinalColumns = new ArrayList();

		// Token: 0x04000830 RID: 2096
		public DataColumn PrimaryKey;
	}
}
