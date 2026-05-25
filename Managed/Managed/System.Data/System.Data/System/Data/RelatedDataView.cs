using System;
using Mono.Data.SqlExpressions;

namespace System.Data
{
	// Token: 0x02000022 RID: 34
	internal class RelatedDataView : DataView, IExpression
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000C014 File Offset: 0x0000A214
		internal RelatedDataView(DataColumn[] relatedColumns, object[] keyValues)
		{
			this.dataTable = relatedColumns[0].Table;
			this.rowState = DataViewRowState.CurrentRows;
			this._columns = relatedColumns;
			this._keyValues = keyValues;
			base.Open();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C054 File Offset: 0x0000A254
		void IExpression.ResetExpression()
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000C058 File Offset: 0x0000A258
		internal override IExpression FilterExpression
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C05C File Offset: 0x0000A25C
		public override bool Equals(object obj)
		{
			if (!(obj is RelatedDataView))
			{
				return base.FilterExpression != null && base.FilterExpression.Equals(obj);
			}
			RelatedDataView relatedDataView = (RelatedDataView)obj;
			if (this._columns.Length != relatedDataView._columns.Length)
			{
				return false;
			}
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (!this._columns[i].Equals(relatedDataView._columns[i]) || !this._keyValues[i].Equals(relatedDataView._keyValues[i]))
				{
					return false;
				}
			}
			return relatedDataView.FilterExpression.Equals(base.FilterExpression);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C114 File Offset: 0x0000A314
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this._columns.Length; i++)
			{
				num ^= this._columns[i].GetHashCode();
				num ^= this._keyValues[i].GetHashCode();
			}
			if (base.FilterExpression != null)
			{
				num ^= base.FilterExpression.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C178 File Offset: 0x0000A378
		public object Eval(DataRow row)
		{
			return this.EvalBoolean(row);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C188 File Offset: 0x0000A388
		public bool EvalBoolean(DataRow row)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (!row[this._columns[i]].Equals(this._keyValues[i]))
				{
					return false;
				}
			}
			IExpression filterExpression = base.FilterExpression;
			return filterExpression == null || filterExpression.EvalBoolean(row);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public bool DependsOn(DataColumn other)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (this._columns[i] == other)
				{
					return true;
				}
			}
			IExpression filterExpression = base.FilterExpression;
			return filterExpression != null && filterExpression.DependsOn(other);
		}

		// Token: 0x040000CD RID: 205
		private object[] _keyValues;

		// Token: 0x040000CE RID: 206
		private DataColumn[] _columns;
	}
}
