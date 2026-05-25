using System;
using System.Collections;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A4 RID: 420
	internal class In : UnaryExpression
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x000604BC File Offset: 0x0005E6BC
		public In(IExpression e, IList set)
			: base(e)
		{
			this.set = set;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000604CC File Offset: 0x0005E6CC
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is In))
			{
				return false;
			}
			In @in = (In)obj;
			if (@in.set.Count != this.set.Count)
			{
				return false;
			}
			int i = 0;
			int count = this.set.Count;
			while (i < count)
			{
				object obj2 = this.set[i];
				object obj3 = @in.set[i];
				if (obj2 == null && obj3 != null)
				{
					return false;
				}
				if (!obj2.Equals(obj3))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00060570 File Offset: 0x0005E770
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			int i = 0;
			int count = this.set.Count;
			while (i < count)
			{
				object obj = this.set[i];
				if (obj != null)
				{
					num ^= obj.GetHashCode();
				}
				i++;
			}
			return num;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000605C0 File Offset: 0x0005E7C0
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == DBNull.Value)
			{
				return obj;
			}
			IComparable comparable = obj as IComparable;
			if (comparable == null)
			{
				return false;
			}
			foreach (object obj2 in this.set)
			{
				IExpression expression = (IExpression)obj2;
				IComparable comparable2 = (IComparable)expression.Eval(row);
				if (comparable2 != null)
				{
					if (Comparison.Compare(comparable, comparable2, row.Table.CaseSensitive) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000606A4 File Offset: 0x0005E8A4
		public override bool EvalBoolean(DataRow row)
		{
			return (bool)this.Eval(row);
		}

		// Token: 0x04000894 RID: 2196
		private IList set;
	}
}
