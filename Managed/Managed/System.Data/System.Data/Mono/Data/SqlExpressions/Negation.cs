using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000198 RID: 408
	internal class Negation : UnaryExpression
	{
		// Token: 0x06001547 RID: 5447 RVA: 0x0005F744 File Offset: 0x0005D944
		public Negation(IExpression e)
			: base(e)
		{
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005F750 File Offset: 0x0005D950
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == DBNull.Value)
			{
				return obj;
			}
			return !(bool)obj;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005F788 File Offset: 0x0005D988
		public override bool EvalBoolean(DataRow row)
		{
			return !this.expr.EvalBoolean(row);
		}
	}
}
