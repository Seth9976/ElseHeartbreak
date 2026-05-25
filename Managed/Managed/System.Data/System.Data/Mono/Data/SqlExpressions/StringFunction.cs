using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200019B RID: 411
	internal abstract class StringFunction : UnaryExpression
	{
		// Token: 0x06001552 RID: 5458 RVA: 0x0005FB98 File Offset: 0x0005DD98
		protected StringFunction(IExpression e)
			: base(e)
		{
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == null)
			{
				return null;
			}
			if (!(obj is string))
			{
				string text = base.GetType().ToString();
				int num = text.LastIndexOf('.') + 1;
				text = text.Substring(num, text.Length - num - "Function".Length);
				throw new EvaluateException(string.Format("'{0}' can be applied only to strings.", text));
			}
			return obj;
		}
	}
}
