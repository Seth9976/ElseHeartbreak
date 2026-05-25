using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200019F RID: 415
	internal class TrimFunction : StringFunction
	{
		// Token: 0x0600155E RID: 5470 RVA: 0x0005FE24 File Offset: 0x0005E024
		public TrimFunction(IExpression e)
			: base(e)
		{
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0005FE30 File Offset: 0x0005E030
		public override object Eval(DataRow row)
		{
			string text = (string)base.Eval(row);
			if (text == null)
			{
				return null;
			}
			return text.Trim();
		}
	}
}
