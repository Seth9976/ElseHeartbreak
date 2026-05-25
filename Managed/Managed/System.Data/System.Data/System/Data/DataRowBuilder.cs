using System;

namespace System.Data
{
	/// <summary>The DataRowBuilder type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002B RID: 43
	public sealed class DataRowBuilder
	{
		// Token: 0x0600025E RID: 606 RVA: 0x00010B30 File Offset: 0x0000ED30
		internal DataRowBuilder(DataTable table, int rowID, int y)
		{
			this.table = table;
			this._rowId = rowID;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00010B48 File Offset: 0x0000ED48
		internal DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x04000101 RID: 257
		private DataTable table;

		// Token: 0x04000102 RID: 258
		internal int _rowId;
	}
}
