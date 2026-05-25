using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200018E RID: 398
	internal abstract class BaseExpression : IExpression
	{
		// Token: 0x06001519 RID: 5401
		public abstract object Eval(DataRow row);

		// Token: 0x0600151A RID: 5402
		public abstract bool DependsOn(DataColumn other);

		// Token: 0x0600151B RID: 5403 RVA: 0x0005EC40 File Offset: 0x0005CE40
		public virtual bool EvalBoolean(DataRow row)
		{
			throw new EvaluateException("Not a Boolean Expression");
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0005EC4C File Offset: 0x0005CE4C
		public override bool Equals(object obj)
		{
			return obj != null && obj is BaseExpression;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0005EC64 File Offset: 0x0005CE64
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0005EC68 File Offset: 0x0005CE68
		public virtual void ResetExpression()
		{
		}
	}
}
