using System;
using System.Collections.Generic;
using System.Linq;

namespace RelayLib
{
	// Token: 0x0200000D RID: 13
	public struct TableRow
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000034E0 File Offset: 0x000016E0
		internal TableRow(TableTwo pTable, int pRow)
		{
			this._table = pTable;
			this._row = pRow;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000034F8 File Offset: 0x000016F8
		public int row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003504 File Offset: 0x00001704
		public T Get<T>(string pFieldName)
		{
			return this._table.GetValue<T>(this.row, pFieldName);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003518 File Offset: 0x00001718
		public TableRow Set<T>(string pFieldName, T pValue)
		{
			this._table.SetValue<T>(this._row, pFieldName, pValue);
			return this;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003588 File Offset: 0x00001788
		public string[] valuesAsStrings
		{
			get
			{
				TableTwo tab = this._table;
				int r = this._row;
				IEnumerable<string> enumerable = from ITableField f in this._table.fields
					select tab.GetStringValue(r, f.name);
				return enumerable.ToArray<string>();
			}
			set
			{
				int num = 0;
				foreach (ITableField tableField in this._table.fields)
				{
					tableField.SetValueFromString(this._row, value[num++]);
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003600 File Offset: 0x00001800
		public SerializableTableRow GetSerializableTableRow()
		{
			return new SerializableTableRow
			{
				values = this.valuesAsStrings,
				row = this._row
			};
		}

		// Token: 0x04000012 RID: 18
		private TableTwo _table;

		// Token: 0x04000013 RID: 19
		private int _row;
	}
}
