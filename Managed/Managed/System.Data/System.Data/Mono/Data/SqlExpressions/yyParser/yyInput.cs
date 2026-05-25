using System;

namespace Mono.Data.SqlExpressions.yyParser
{
	// Token: 0x02000009 RID: 9
	internal interface yyInput
	{
		// Token: 0x0600002A RID: 42
		bool advance();

		// Token: 0x0600002B RID: 43
		int token();

		// Token: 0x0600002C RID: 44
		object value();
	}
}
