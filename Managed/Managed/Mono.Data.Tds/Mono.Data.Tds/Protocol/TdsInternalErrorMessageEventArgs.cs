using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001E RID: 30
	public sealed class TdsInternalErrorMessageEventArgs : TdsInternalInfoMessageEventArgs
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public TdsInternalErrorMessageEventArgs(TdsInternalError error)
			: base(error)
		{
		}
	}
}
