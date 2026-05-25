using System;

namespace RelayLib
{
	// Token: 0x0200000C RID: 12
	public class SerializableTableRow
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000034A4 File Offset: 0x000016A4
		public void InsertToTable(TableTwo pTable)
		{
			pTable._usedRows[this.row] = true;
			pTable.GetRow(this.row).valuesAsStrings = this.values;
		}

		// Token: 0x04000010 RID: 16
		public int row;

		// Token: 0x04000011 RID: 17
		public string[] values;
	}
}
