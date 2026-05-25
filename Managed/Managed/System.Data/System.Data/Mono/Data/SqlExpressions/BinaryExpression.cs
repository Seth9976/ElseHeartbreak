using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000190 RID: 400
	internal abstract class BinaryExpression : BaseExpression
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x0005ECF8 File Offset: 0x0005CEF8
		protected BinaryExpression(IExpression e1, IExpression e2)
		{
			this.expr1 = e1;
			this.expr2 = e2;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0005ED10 File Offset: 0x0005CF10
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is BinaryExpression))
			{
				return false;
			}
			BinaryExpression binaryExpression = (BinaryExpression)obj;
			return binaryExpression.expr1.Equals(this.expr1) && binaryExpression.expr2.Equals(this.expr2);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005ED70 File Offset: 0x0005CF70
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num ^= this.expr1.GetHashCode();
			return num ^ this.expr2.GetHashCode();
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005EDA4 File Offset: 0x0005CFA4
		public override bool DependsOn(DataColumn other)
		{
			return this.expr1.DependsOn(other) || this.expr2.DependsOn(other);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
		public override void ResetExpression()
		{
			this.expr1.ResetExpression();
			this.expr2.ResetExpression();
		}

		// Token: 0x04000870 RID: 2160
		protected IExpression expr1;

		// Token: 0x04000871 RID: 2161
		protected IExpression expr2;
	}
}
