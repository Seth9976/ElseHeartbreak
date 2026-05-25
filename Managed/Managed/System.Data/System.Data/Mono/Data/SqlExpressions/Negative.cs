using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000196 RID: 406
	internal class Negative : UnaryExpression
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x0005F62C File Offset: 0x0005D82C
		public Negative(IExpression e)
			: base(e)
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005F638 File Offset: 0x0005D838
		public override object Eval(DataRow row)
		{
			return Numeric.Negative((IConvertible)this.expr.Eval(row));
		}
	}
}
