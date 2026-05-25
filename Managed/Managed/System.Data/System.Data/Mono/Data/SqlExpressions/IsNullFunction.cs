using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A1 RID: 417
	internal class IsNullFunction : UnaryExpression
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x0005FF58 File Offset: 0x0005E158
		public IsNullFunction(IExpression e, IExpression defaultExpr)
			: base(e)
		{
			this.defaultExpr = defaultExpr;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0005FF68 File Offset: 0x0005E168
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is UnaryExpression))
			{
				return false;
			}
			IsNullFunction isNullFunction = (IsNullFunction)obj;
			return isNullFunction.defaultExpr.Equals(this.defaultExpr);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0005FFB0 File Offset: 0x0005E1B0
		public override int GetHashCode()
		{
			return this.defaultExpr.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0005FFC4 File Offset: 0x0005E1C4
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == null || obj == DBNull.Value)
			{
				return this.defaultExpr.Eval(row);
			}
			return obj;
		}

		// Token: 0x04000891 RID: 2193
		private IExpression defaultExpr;
	}
}
