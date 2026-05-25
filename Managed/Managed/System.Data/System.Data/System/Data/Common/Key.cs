using System;
using System.ComponentModel;
using Mono.Data.SqlExpressions;

namespace System.Data.Common
{
	// Token: 0x020000D6 RID: 214
	internal class Key
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x000308EC File Offset: 0x0002EAEC
		internal Key(DataTable table, DataColumn[] columns, ListSortDirection[] sort, DataViewRowState rowState, IExpression filter)
		{
			this._table = table;
			this._filter = filter;
			if (this._filter != null)
			{
				this._tmpRow = this._table.NewNotInitializedRow();
			}
			this._columns = columns;
			if (sort != null && sort.Length == columns.Length)
			{
				this._sortDirection = sort;
			}
			else
			{
				this._sortDirection = new ListSortDirection[columns.Length];
				for (int i = 0; i < this._sortDirection.Length; i++)
				{
					this._sortDirection[i] = ListSortDirection.Ascending;
				}
			}
			if (rowState != DataViewRowState.None)
			{
				this._rowStateFilter = rowState;
			}
			else
			{
				this._rowStateFilter = DataViewRowState.CurrentRows;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0003099C File Offset: 0x0002EB9C
		internal DataColumn[] Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000309A4 File Offset: 0x0002EBA4
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000309AC File Offset: 0x0002EBAC
		private ListSortDirection[] Sort
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000309B4 File Offset: 0x0002EBB4
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x000309BC File Offset: 0x0002EBBC
		internal DataViewRowState RowStateFilter
		{
			get
			{
				return this._rowStateFilter;
			}
			set
			{
				this._rowStateFilter = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x000309C8 File Offset: 0x0002EBC8
		internal bool HasFilter
		{
			get
			{
				return this._filter != null;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000309D8 File Offset: 0x0002EBD8
		internal int CompareRecords(int first, int second)
		{
			if (first == second)
			{
				return 0;
			}
			for (int i = 0; i < this.Columns.Length; i++)
			{
				int num = this.Columns[i].CompareValues(first, second);
				if (num != 0)
				{
					return (this.Sort[i] != ListSortDirection.Ascending) ? (-num) : num;
				}
			}
			return 0;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00030A3C File Offset: 0x0002EC3C
		internal int GetRecord(DataRow row)
		{
			int record = Key.GetRecord(row, this._rowStateFilter);
			if (this._filter == null)
			{
				return record;
			}
			if (record < 0)
			{
				return record;
			}
			return (!this.CanContain(record)) ? (-1) : record;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00030A80 File Offset: 0x0002EC80
		internal bool CanContain(int index)
		{
			if (this._filter == null)
			{
				return true;
			}
			this._tmpRow._current = index;
			return this._filter.EvalBoolean(this._tmpRow);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00030AB8 File Offset: 0x0002ECB8
		internal bool ContainsVersion(DataRowState state, DataRowVersion version)
		{
			switch (state)
			{
			case DataRowState.Unchanged:
				if ((this._rowStateFilter & DataViewRowState.Unchanged) != DataViewRowState.None)
				{
					return (version & DataRowVersion.Default) != (DataRowVersion)0;
				}
				break;
			default:
				if (state != DataRowState.Deleted)
				{
					if ((this._rowStateFilter & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
					{
						return (version & DataRowVersion.Default) != (DataRowVersion)0;
					}
					if ((this._rowStateFilter & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
					{
						return version == DataRowVersion.Original;
					}
				}
				else if ((this._rowStateFilter & DataViewRowState.Deleted) != DataViewRowState.None)
				{
					return version == DataRowVersion.Original;
				}
				break;
			case DataRowState.Added:
				if ((this._rowStateFilter & DataViewRowState.Added) != DataViewRowState.None)
				{
					return (version & DataRowVersion.Default) != (DataRowVersion)0;
				}
				break;
			}
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00030B7C File Offset: 0x0002ED7C
		internal static int GetRecord(DataRow row, DataViewRowState rowStateFilter)
		{
			DataRowState rowState = row.RowState;
			switch (rowState)
			{
			case DataRowState.Unchanged:
				if ((rowStateFilter & DataViewRowState.Unchanged) != DataViewRowState.None)
				{
					return (row.Proposed < 0) ? row.Current : row.Proposed;
				}
				break;
			default:
				if (rowState != DataRowState.Deleted)
				{
					if ((rowStateFilter & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
					{
						return (row.Proposed < 0) ? row.Current : row.Proposed;
					}
					if ((rowStateFilter & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
					{
						return row.Original;
					}
				}
				else if ((rowStateFilter & DataViewRowState.Deleted) != DataViewRowState.None)
				{
					return row.Original;
				}
				break;
			case DataRowState.Added:
				if ((rowStateFilter & DataViewRowState.Added) != DataViewRowState.None)
				{
					return (row.Proposed < 0) ? row.Current : row.Proposed;
				}
				break;
			}
			return -1;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00030C58 File Offset: 0x0002EE58
		internal bool Equals(DataColumn[] columns, ListSortDirection[] sort, DataViewRowState rowState, IExpression filter)
		{
			if (rowState != DataViewRowState.None && this.RowStateFilter != rowState)
			{
				return false;
			}
			if (this._filter != null)
			{
				if (!this._filter.Equals(filter))
				{
					return false;
				}
			}
			else if (filter != null)
			{
				return false;
			}
			if (this.Columns.Length != columns.Length)
			{
				return false;
			}
			if (sort != null && this.Sort.Length != sort.Length)
			{
				return false;
			}
			if (sort != null)
			{
				for (int i = 0; i < columns.Length; i++)
				{
					if (this.Sort[i] != sort[i] || this.Columns[i] != columns[i])
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = 0; j < columns.Length; j++)
				{
					if (this.Sort[j] != ListSortDirection.Ascending || this.Columns[j] != columns[j])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00030D48 File Offset: 0x0002EF48
		internal bool DependsOn(DataColumn column)
		{
			return this._filter != null && this._filter.DependsOn(column);
		}

		// Token: 0x04000398 RID: 920
		private DataTable _table;

		// Token: 0x04000399 RID: 921
		private DataColumn[] _columns;

		// Token: 0x0400039A RID: 922
		private ListSortDirection[] _sortDirection;

		// Token: 0x0400039B RID: 923
		private DataViewRowState _rowStateFilter;

		// Token: 0x0400039C RID: 924
		private IExpression _filter;

		// Token: 0x0400039D RID: 925
		private DataRow _tmpRow;
	}
}
