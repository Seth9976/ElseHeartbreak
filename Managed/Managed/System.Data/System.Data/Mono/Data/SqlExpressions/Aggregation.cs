using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A6 RID: 422
	internal class Aggregation : BaseExpression
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x000606B4 File Offset: 0x0005E8B4
		public Aggregation(bool cacheResults, DataRow[] rows, AggregationFunction function, ColumnReference column)
		{
			this.cacheResults = cacheResults;
			this.rows = rows;
			this.column = column;
			this.function = function;
			this.result = null;
			if (cacheResults)
			{
				this.RowChangeHandler = new DataRowChangeEventHandler(this.InvalidateCache);
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00060704 File Offset: 0x0005E904
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is Aggregation))
			{
				return false;
			}
			Aggregation aggregation = (Aggregation)obj;
			if (!aggregation.function.Equals(this.function))
			{
				return false;
			}
			if (!aggregation.column.Equals(this.column))
			{
				return false;
			}
			if (aggregation.rows != null && this.rows != null)
			{
				if (aggregation.rows.Length != this.rows.Length)
				{
					return false;
				}
				for (int i = 0; i < this.rows.Length; i++)
				{
					if (aggregation.rows[i] != this.rows[i])
					{
						return false;
					}
				}
			}
			else if (aggregation.rows != null || this.rows != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000607E8 File Offset: 0x0005E9E8
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num ^= this.function.GetHashCode();
			num ^= this.column.GetHashCode();
			for (int i = 0; i < this.rows.Length; i++)
			{
				num ^= this.rows[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00060848 File Offset: 0x0005EA48
		public override object Eval(DataRow row)
		{
			if (this.cacheResults && this.result != null && this.column.ReferencedTable == ReferencedTable.Self)
			{
				return this.result;
			}
			this.count = 0;
			this.result = null;
			object[] array;
			if (this.rows == null)
			{
				array = this.column.GetValues(this.column.GetReferencedRows(row));
			}
			else
			{
				array = this.column.GetValues(this.rows);
			}
			foreach (object obj in array)
			{
				if (obj != null)
				{
					this.count++;
					this.Aggregate((IConvertible)obj);
				}
			}
			switch (this.function)
			{
			case AggregationFunction.Count:
				this.result = this.count;
				break;
			case AggregationFunction.Avg:
			{
				IConvertible convertible;
				if (this.count == 0)
				{
					IConvertible value = DBNull.Value;
					convertible = value;
				}
				else
				{
					convertible = Numeric.Divide(this.result, this.count);
				}
				this.result = convertible;
				break;
			}
			case AggregationFunction.StDev:
			case AggregationFunction.Var:
				this.result = this.CalcStatisticalFunction(array);
				break;
			}
			if (this.result == null)
			{
				this.result = DBNull.Value;
			}
			if (this.cacheResults && this.column.ReferencedTable == ReferencedTable.Self)
			{
				this.table = row.Table;
				row.Table.RowChanged += this.RowChangeHandler;
			}
			return this.result;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000609F0 File Offset: 0x0005EBF0
		public override bool DependsOn(DataColumn other)
		{
			return this.column.DependsOn(other);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00060A00 File Offset: 0x0005EC00
		private void Aggregate(IConvertible val)
		{
			switch (this.function)
			{
			case AggregationFunction.Sum:
			case AggregationFunction.Avg:
			case AggregationFunction.StDev:
			case AggregationFunction.Var:
			{
				IConvertible convertible2;
				if (this.result != null)
				{
					IConvertible convertible = Numeric.Add(this.result, val);
					convertible2 = convertible;
				}
				else
				{
					convertible2 = val;
				}
				this.result = convertible2;
				return;
			}
			case AggregationFunction.Min:
			{
				IConvertible convertible3;
				if (this.result != null)
				{
					IConvertible convertible = Numeric.Min(this.result, val);
					convertible3 = convertible;
				}
				else
				{
					convertible3 = val;
				}
				this.result = convertible3;
				return;
			}
			case AggregationFunction.Max:
			{
				IConvertible convertible4;
				if (this.result != null)
				{
					IConvertible convertible = Numeric.Max(this.result, val);
					convertible4 = convertible;
				}
				else
				{
					convertible4 = val;
				}
				this.result = convertible4;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00060AAC File Offset: 0x0005ECAC
		private IConvertible CalcStatisticalFunction(object[] values)
		{
			if (this.count < 2)
			{
				return DBNull.Value;
			}
			double num = (double)Convert.ChangeType(this.result, TypeCode.Double) / (double)this.count;
			double num2 = 0.0;
			foreach (object obj in values)
			{
				if (obj != null)
				{
					double num3 = num - (double)Convert.ChangeType(obj, TypeCode.Double);
					num2 += Math.Pow(num3, 2.0);
				}
			}
			num2 /= (double)(this.count - 1);
			if (this.function == AggregationFunction.StDev)
			{
				num2 = Math.Sqrt(num2);
			}
			return num2;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00060B64 File Offset: 0x0005ED64
		public override void ResetExpression()
		{
			if (this.table != null)
			{
				this.InvalidateCache(this.table, null);
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00060B80 File Offset: 0x0005ED80
		private void InvalidateCache(object sender, DataRowChangeEventArgs args)
		{
			this.result = null;
			((DataTable)sender).RowChanged -= this.RowChangeHandler;
		}

		// Token: 0x0400089D RID: 2205
		private bool cacheResults;

		// Token: 0x0400089E RID: 2206
		private DataRow[] rows;

		// Token: 0x0400089F RID: 2207
		private ColumnReference column;

		// Token: 0x040008A0 RID: 2208
		private AggregationFunction function;

		// Token: 0x040008A1 RID: 2209
		private int count;

		// Token: 0x040008A2 RID: 2210
		private IConvertible result;

		// Token: 0x040008A3 RID: 2211
		private DataRowChangeEventHandler RowChangeHandler;

		// Token: 0x040008A4 RID: 2212
		private DataTable table;
	}
}
