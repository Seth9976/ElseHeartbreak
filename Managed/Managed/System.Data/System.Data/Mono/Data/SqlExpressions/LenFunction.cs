using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200019E RID: 414
	internal class LenFunction : StringFunction
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
		public LenFunction(IExpression e)
			: base(e)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		public override object Eval(DataRow row)
		{
			string text = (string)base.Eval(row);
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}
	}
}
