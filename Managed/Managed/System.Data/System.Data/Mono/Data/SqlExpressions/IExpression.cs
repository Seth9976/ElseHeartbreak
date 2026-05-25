using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200018D RID: 397
	internal interface IExpression
	{
		// Token: 0x06001514 RID: 5396
		object Eval(DataRow row);

		// Token: 0x06001515 RID: 5397
		bool DependsOn(DataColumn other);

		// Token: 0x06001516 RID: 5398
		bool EvalBoolean(DataRow row);

		// Token: 0x06001517 RID: 5399
		void ResetExpression();
	}
}
