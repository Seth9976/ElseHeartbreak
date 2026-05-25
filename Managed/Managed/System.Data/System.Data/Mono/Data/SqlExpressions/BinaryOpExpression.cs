using System;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000192 RID: 402
	internal abstract class BinaryOpExpression : BinaryExpression
	{
		// Token: 0x06001529 RID: 5417 RVA: 0x0005EDEC File Offset: 0x0005CFEC
		protected BinaryOpExpression(Operation op, IExpression e1, IExpression e2)
			: base(e1, e2)
		{
			this.op = op;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0005EE00 File Offset: 0x0005D000
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is BinaryOpExpression))
			{
				return false;
			}
			BinaryOpExpression binaryOpExpression = (BinaryOpExpression)obj;
			return binaryOpExpression.op == this.op;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005EE44 File Offset: 0x0005D044
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.op.GetHashCode();
		}

		// Token: 0x04000880 RID: 2176
		protected Operation op;
	}
}
