using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A0 RID: 416
	internal class IifFunction : UnaryExpression
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x0005FE58 File Offset: 0x0005E058
		public IifFunction(IExpression e, IExpression trueExpr, IExpression falseExpr)
			: base(e)
		{
			this.trueExpr = trueExpr;
			this.falseExpr = falseExpr;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0005FE70 File Offset: 0x0005E070
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is IifFunction))
			{
				return false;
			}
			IifFunction iifFunction = (IifFunction)obj;
			return iifFunction.falseExpr.Equals(this.falseExpr) && iifFunction.trueExpr.Equals(this.trueExpr);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0005FED0 File Offset: 0x0005E0D0
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num ^= this.falseExpr.GetHashCode();
			return num ^ this.trueExpr.GetHashCode();
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0005FF04 File Offset: 0x0005E104
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == DBNull.Value)
			{
				return obj;
			}
			bool flag = Convert.ToBoolean(obj);
			return (!flag) ? this.falseExpr.Eval(row) : this.trueExpr.Eval(row);
		}

		// Token: 0x0400088F RID: 2191
		private IExpression trueExpr;

		// Token: 0x04000890 RID: 2192
		private IExpression falseExpr;
	}
}
