using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000199 RID: 409
	internal class BoolOperation : BinaryOpExpression
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x0005F79C File Offset: 0x0005D99C
		public BoolOperation(Operation op, IExpression e1, IExpression e2)
			: base(op, e1, e2)
		{
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
		public override object Eval(DataRow row)
		{
			return this.EvalBoolean(row);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
		public override bool EvalBoolean(DataRow row)
		{
			if (this.op == Operation.OR)
			{
				return this.expr1.EvalBoolean(row) || this.expr2.EvalBoolean(row);
			}
			return this.op == Operation.AND && this.expr1.EvalBoolean(row) && this.expr2.EvalBoolean(row);
		}
	}
}
