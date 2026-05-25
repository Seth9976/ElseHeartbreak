using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200019D RID: 413
	internal class SubstringFunction : StringFunction
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x0005FCC0 File Offset: 0x0005DEC0
		public SubstringFunction(IExpression e, IExpression start, IExpression len)
			: base(e)
		{
			this.start = start;
			this.len = len;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005FCD8 File Offset: 0x0005DED8
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is SubstringFunction))
			{
				return false;
			}
			SubstringFunction substringFunction = (SubstringFunction)obj;
			return substringFunction.start == this.start && substringFunction.len == this.len;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005FD30 File Offset: 0x0005DF30
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num ^= this.start.GetHashCode();
			return num ^ this.len.GetHashCode();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005FD64 File Offset: 0x0005DF64
		public override object Eval(DataRow row)
		{
			string text = (string)base.Eval(row);
			object obj = this.start.Eval(row);
			int num = Convert.ToInt32(this.start.Eval(row));
			int num2 = Convert.ToInt32(this.len.Eval(row));
			if (text == null)
			{
				return null;
			}
			if (num > text.Length)
			{
				return string.Empty;
			}
			return text.Substring(num - 1, Math.Min(num2, text.Length - (num - 1)));
		}

		// Token: 0x0400088D RID: 2189
		private IExpression start;

		// Token: 0x0400088E RID: 2190
		private IExpression len;
	}
}
