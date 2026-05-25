using System;

namespace Mono.Data.SqlExpressions.yyParser
{
	// Token: 0x02000008 RID: 8
	internal class yyUnexpectedEof : yyException
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003968 File Offset: 0x00001B68
		public yyUnexpectedEof(string message)
			: base(message)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003974 File Offset: 0x00001B74
		public yyUnexpectedEof()
			: base(string.Empty)
		{
		}
	}
}
