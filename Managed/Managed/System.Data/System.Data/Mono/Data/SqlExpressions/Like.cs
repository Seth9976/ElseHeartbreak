using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A3 RID: 419
	internal class Like : UnaryExpression
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x00060288 File Offset: 0x0005E488
		public Like(IExpression e, IExpression pattern)
			: base(e)
		{
			this._pattern = pattern;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00060298 File Offset: 0x0005E498
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is Like))
			{
				return false;
			}
			Like like = (Like)obj;
			return this._pattern.Equals(like._pattern);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000602D8 File Offset: 0x0005E4D8
		public override int GetHashCode()
		{
			return this._pattern.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000602EC File Offset: 0x0005E4EC
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == null || obj == DBNull.Value)
			{
				return false;
			}
			string text = (string)obj;
			string text2 = (string)this._pattern.Eval(row);
			string text3 = text2;
			int length = text2.Length;
			bool flag = text2[0] == '*' || text2[0] == '%';
			bool flag2 = text2[length - 1] == '*' || text2[length - 1] == '%';
			text2 = text2.Trim(new char[] { '*', '%' });
			text2 = text2.Replace("[*]", "[[0]]");
			text2 = text2.Replace("[%]", "[[1]]");
			if (text2.IndexOf('*') != -1 || text2.IndexOf('%') != -1)
			{
				throw new EvaluateException(string.Format("Pattern '{0}' is invalid.", text3));
			}
			text2 = text2.Replace("[[0]]", "*");
			text2 = text2.Replace("[[1]]", "%");
			text2 = text2.Replace("[[]", "[");
			text2 = text2.Replace("[]]", "]");
			if (!row.Table.CaseSensitive)
			{
				text = text.ToLower();
				text2 = text2.ToLower();
			}
			int num = text.IndexOf(text2);
			if (num == -1)
			{
				return false;
			}
			return (num == 0 || flag) && (num + text2.Length == text.Length || flag2);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000604AC File Offset: 0x0005E6AC
		public override bool EvalBoolean(DataRow row)
		{
			return (bool)this.Eval(row);
		}

		// Token: 0x04000893 RID: 2195
		private readonly IExpression _pattern;
	}
}
