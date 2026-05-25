using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200018F RID: 399
	internal abstract class UnaryExpression : BaseExpression
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x0005EC6C File Offset: 0x0005CE6C
		public UnaryExpression(IExpression e)
		{
			this.expr = e;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0005EC7C File Offset: 0x0005CE7C
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
			UnaryExpression unaryExpression = (UnaryExpression)obj;
			return unaryExpression.expr.Equals(this.expr);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0005ECC4 File Offset: 0x0005CEC4
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.expr.GetHashCode();
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0005ECD8 File Offset: 0x0005CED8
		public override bool DependsOn(DataColumn other)
		{
			return this.expr.DependsOn(other);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
		public override bool EvalBoolean(DataRow row)
		{
			return (bool)this.Eval(row);
		}

		// Token: 0x0400086F RID: 2159
		protected IExpression expr;
	}
}
