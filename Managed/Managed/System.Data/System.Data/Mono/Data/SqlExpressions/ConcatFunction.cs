using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200019C RID: 412
	internal class ConcatFunction : StringFunction
	{
		// Token: 0x06001554 RID: 5460 RVA: 0x0005FC18 File Offset: 0x0005DE18
		public ConcatFunction(IExpression e, IExpression add)
			: base(e)
		{
			this._add = add;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0005FC28 File Offset: 0x0005DE28
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is ConcatFunction))
			{
				return false;
			}
			ConcatFunction concatFunction = (ConcatFunction)obj;
			return this._add.Equals(concatFunction._add);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0005FC68 File Offset: 0x0005DE68
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();
			return hashCode ^ this._add.GetHashCode();
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0005FC8C File Offset: 0x0005DE8C
		public override object Eval(DataRow row)
		{
			string text = (string)base.Eval(row);
			string text2 = (string)this._add.Eval(row);
			return text + text2;
		}

		// Token: 0x0400088C RID: 2188
		private readonly IExpression _add;
	}
}
