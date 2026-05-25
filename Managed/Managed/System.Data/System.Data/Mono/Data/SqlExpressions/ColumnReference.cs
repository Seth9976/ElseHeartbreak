using System;
using System.ComponentModel;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000195 RID: 405
	internal class ColumnReference : BaseExpression
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		public ColumnReference(string columnName)
			: this(ReferencedTable.Self, null, columnName)
		{
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0005EF04 File Offset: 0x0005D104
		public ColumnReference(ReferencedTable refTable, string relationName, string columnName)
		{
			this.refTable = refTable;
			this.relationName = relationName;
			this.columnName = columnName;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005EF24 File Offset: 0x0005D124
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is ColumnReference))
			{
				return false;
			}
			ColumnReference columnReference = (ColumnReference)obj;
			return columnReference.refTable == this.refTable && !(columnReference.columnName != this.columnName) && !(columnReference.relationName != this.relationName);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0005EF98 File Offset: 0x0005D198
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num ^= this.refTable.GetHashCode();
			num ^= this.columnName.GetHashCode();
			return num ^ this.relationName.GetHashCode();
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		public ReferencedTable ReferencedTable
		{
			get
			{
				return this.refTable;
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
		private DataRelation GetRelation(DataRow row)
		{
			if (this._cachedRelation == null)
			{
				DataTable table = row.Table;
				if (this.relationName != null)
				{
					DataRelationCollection dataRelationCollection = table.DataSet.Relations;
					this._cachedRelation = dataRelationCollection[dataRelationCollection.IndexOf(this.relationName)];
				}
				else
				{
					DataRelationCollection dataRelationCollection;
					if (this.refTable == ReferencedTable.Parent)
					{
						dataRelationCollection = table.ParentRelations;
					}
					else
					{
						dataRelationCollection = table.ChildRelations;
					}
					if (dataRelationCollection.Count > 1)
					{
						throw new EvaluateException(string.Format("The table [{0}] is involved in more than one relation.You must explicitly mention a relation name.", table.TableName));
					}
					this._cachedRelation = dataRelationCollection[0];
				}
				this._cachedRelation.DataSet.Relations.CollectionChanged += this.OnRelationRemoved;
			}
			return this._cachedRelation;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0005F0AC File Offset: 0x0005D2AC
		private DataColumn GetColumn(DataRow row)
		{
			if (this._cachedColumn == null)
			{
				DataTable dataTable = row.Table;
				ReferencedTable referencedTable = this.refTable;
				if (referencedTable != ReferencedTable.Parent)
				{
					if (referencedTable == ReferencedTable.Child)
					{
						dataTable = this.GetRelation(row).ChildTable;
					}
				}
				else
				{
					dataTable = this.GetRelation(row).ParentTable;
				}
				this._cachedColumn = dataTable.Columns[this.columnName];
				if (this._cachedColumn == null)
				{
					throw new EvaluateException(string.Format("Cannot find column [{0}].", this.columnName));
				}
				this._cachedColumn.PropertyChanged += this.OnColumnPropertyChanged;
				this._cachedColumn.Table.Columns.CollectionChanged += this.OnColumnRemoved;
			}
			return this._cachedColumn;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0005F180 File Offset: 0x0005D380
		public DataRow GetReferencedRow(DataRow row)
		{
			this.GetColumn(row);
			switch (this.refTable)
			{
			default:
				return row;
			case ReferencedTable.Parent:
				return row.GetParentRow(this.GetRelation(row));
			case ReferencedTable.Child:
				return row.GetChildRows(this.GetRelation(row))[0];
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0005F1D4 File Offset: 0x0005D3D4
		public DataRow[] GetReferencedRows(DataRow row)
		{
			this.GetColumn(row);
			switch (this.refTable)
			{
			default:
			{
				DataRow[] array = row.Table.NewRowArray(row.Table.Rows.Count);
				row.Table.Rows.CopyTo(array, 0);
				return array;
			}
			case ReferencedTable.Parent:
				return row.GetParentRows(this.GetRelation(row));
			case ReferencedTable.Child:
				return row.GetChildRows(this.GetRelation(row));
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005F254 File Offset: 0x0005D454
		public object[] GetValues(DataRow[] rows)
		{
			object[] array = new object[rows.Length];
			for (int i = 0; i < rows.Length; i++)
			{
				array[i] = this.Unify(rows[i][this.GetColumn(rows[i])]);
			}
			return array;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005F29C File Offset: 0x0005D49C
		private object Unify(object val)
		{
			if (Numeric.IsNumeric(val))
			{
				return Numeric.Unify((IConvertible)val);
			}
			if (val == null || val == DBNull.Value)
			{
				return null;
			}
			if (val is bool || val is string || val is DateTime || val is Guid || val is char)
			{
				return val;
			}
			if (val is Enum)
			{
				return (int)val;
			}
			throw new EvaluateException(string.Format("Cannot handle data type found in column '{0}'.", this.columnName));
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005F338 File Offset: 0x0005D538
		public override object Eval(DataRow row)
		{
			DataRow referencedRow = this.GetReferencedRow(row);
			if (referencedRow == null)
			{
				return null;
			}
			object obj;
			try
			{
				referencedRow._inExpressionEvaluation = true;
				obj = referencedRow[this.GetColumn(row)];
				referencedRow._inExpressionEvaluation = false;
			}
			catch (IndexOutOfRangeException)
			{
				throw new EvaluateException(string.Format("Cannot find column [{0}].", this.columnName));
			}
			return this.Unify(obj);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0005F3B8 File Offset: 0x0005D5B8
		public override bool EvalBoolean(DataRow row)
		{
			DataColumn column = this.GetColumn(row);
			if (column.DataType != typeof(bool))
			{
				throw new EvaluateException("Not a Boolean Expression");
			}
			object obj = this.Eval(row);
			return obj != null && obj != DBNull.Value && (bool)obj;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005F410 File Offset: 0x0005D610
		public override bool DependsOn(DataColumn other)
		{
			return this.refTable == ReferencedTable.Self && this.columnName == other.ColumnName;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005F434 File Offset: 0x0005D634
		private void DropCached(DataColumnCollection columnCollection, DataRelationCollection relationCollection)
		{
			if (this._cachedColumn != null)
			{
				this._cachedColumn.PropertyChanged -= this.OnColumnPropertyChanged;
				if (columnCollection != null)
				{
					columnCollection.CollectionChanged -= this.OnColumnRemoved;
				}
				else if (this._cachedColumn.Table != null)
				{
					this._cachedColumn.Table.Columns.CollectionChanged -= this.OnColumnRemoved;
				}
				this._cachedColumn = null;
			}
			if (this._cachedRelation != null)
			{
				if (relationCollection != null)
				{
					relationCollection.CollectionChanged -= this.OnRelationRemoved;
				}
				else if (this._cachedRelation.DataSet != null)
				{
					this._cachedRelation.DataSet.Relations.CollectionChanged -= this.OnRelationRemoved;
				}
				this._cachedRelation = null;
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005F518 File Offset: 0x0005D718
		private void OnColumnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (!(sender is DataColumn))
			{
				return;
			}
			DataColumn dataColumn = (DataColumn)sender;
			if (dataColumn == this._cachedColumn && args.PropertyName == "ColumnName")
			{
				this.DropCached(null, null);
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005F564 File Offset: 0x0005D764
		private void OnColumnRemoved(object sender, CollectionChangeEventArgs args)
		{
			if (!(args.Element is DataColumnCollection))
			{
				return;
			}
			if (args.Action != CollectionChangeAction.Remove)
			{
				return;
			}
			DataColumnCollection dataColumnCollection = (DataColumnCollection)args.Element;
			if (this._cachedColumn != null && dataColumnCollection != null && dataColumnCollection.IndexOf(this._cachedColumn) == -1)
			{
				this.DropCached(dataColumnCollection, null);
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005F5C8 File Offset: 0x0005D7C8
		private void OnRelationRemoved(object sender, CollectionChangeEventArgs args)
		{
			if (!(args.Element is DataRelationCollection))
			{
				return;
			}
			if (args.Action != CollectionChangeAction.Remove)
			{
				return;
			}
			DataRelationCollection dataRelationCollection = (DataRelationCollection)args.Element;
			if (this._cachedRelation != null && dataRelationCollection != null && dataRelationCollection.IndexOf(this._cachedRelation) == -1)
			{
				this.DropCached(null, dataRelationCollection);
			}
		}

		// Token: 0x04000886 RID: 2182
		private ReferencedTable refTable;

		// Token: 0x04000887 RID: 2183
		private string relationName;

		// Token: 0x04000888 RID: 2184
		private string columnName;

		// Token: 0x04000889 RID: 2185
		private DataColumn _cachedColumn;

		// Token: 0x0400088A RID: 2186
		private DataRelation _cachedRelation;
	}
}
