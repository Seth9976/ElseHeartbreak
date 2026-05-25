using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x020000D8 RID: 216
	internal class RecordCache
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00030F90 File Offset: 0x0002F190
		internal RecordCache(DataTable table)
		{
			this._table = table;
			this._rowsToRecords = table.NewRowArray(16);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00030FC8 File Offset: 0x0002F1C8
		internal int CurrentCapacity
		{
			get
			{
				return this._currentCapacity;
			}
		}

		// Token: 0x170001E3 RID: 483
		internal DataRow this[int index]
		{
			get
			{
				return this._rowsToRecords[index];
			}
			set
			{
				if (index >= 0)
				{
					this._rowsToRecords[index] = value;
				}
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		internal int NewRecord()
		{
			if (this._records.Count > 0)
			{
				return (int)this._records.Pop();
			}
			DataColumnCollection columns = this._table.Columns;
			if (this._nextFreeIndex >= this._currentCapacity)
			{
				this._currentCapacity *= 2;
				if (this._currentCapacity < 128)
				{
					this._currentCapacity = 128;
				}
				for (int i = 0; i < columns.Count; i++)
				{
					columns[i].DataContainer.Capacity = this._currentCapacity;
				}
				DataRow[] rowsToRecords = this._rowsToRecords;
				this._rowsToRecords = this._table.NewRowArray(this._currentCapacity);
				Array.Copy(rowsToRecords, 0, this._rowsToRecords, 0, rowsToRecords.Length);
			}
			return this._nextFreeIndex++;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000310D4 File Offset: 0x0002F2D4
		internal void DisposeRecord(int index)
		{
			if (index < 0)
			{
				throw new ArgumentException();
			}
			if (!this._records.Contains(index))
			{
				this._records.Push(index);
			}
			this[index] = null;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00031120 File Offset: 0x0002F320
		internal int CopyRecord(DataTable fromTable, int fromRecordIndex, int toRecordIndex)
		{
			int num = toRecordIndex;
			if (toRecordIndex == -1)
			{
				num = this.NewRecord();
			}
			int num2;
			try
			{
				foreach (object obj in fromTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					DataColumn dataColumn2 = this._table.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						dataColumn2.DataContainer.CopyValue(dataColumn.DataContainer, fromRecordIndex, num);
					}
				}
				num2 = num;
			}
			catch
			{
				if (toRecordIndex == -1)
				{
					this.DisposeRecord(num);
				}
				throw;
			}
			return num2;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00031208 File Offset: 0x0002F408
		internal void ReadIDataRecord(int recordIndex, IDataRecord record, int[] mapping, int length)
		{
			if (mapping.Length > this._table.Columns.Count)
			{
				throw new ArgumentException();
			}
			int i;
			for (i = 0; i < length; i++)
			{
				DataColumn dataColumn = this._table.Columns[mapping[i]];
				dataColumn.DataContainer.SetItemFromDataRecord(recordIndex, record, i);
			}
			while (i < mapping.Length)
			{
				DataColumn dataColumn2 = this._table.Columns[mapping[i]];
				if (dataColumn2.AutoIncrement)
				{
					dataColumn2.DataContainer[recordIndex] = dataColumn2.AutoIncrementValue();
				}
				else
				{
					dataColumn2.DataContainer[recordIndex] = dataColumn2.DefaultValue;
				}
				i++;
			}
		}

		// Token: 0x0400039E RID: 926
		private const int MIN_CACHE_SIZE = 128;

		// Token: 0x0400039F RID: 927
		private Stack _records = new Stack(16);

		// Token: 0x040003A0 RID: 928
		private int _nextFreeIndex;

		// Token: 0x040003A1 RID: 929
		private int _currentCapacity;

		// Token: 0x040003A2 RID: 930
		private DataTable _table;

		// Token: 0x040003A3 RID: 931
		private DataRow[] _rowsToRecords;
	}
}
