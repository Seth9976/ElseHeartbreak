using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000023 RID: 35
	public class TdsTimeoutException : TdsInternalException
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		internal TdsTimeoutException(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
			: base(theClass, lineNumber, message, number, procedure, server, source, state)
		{
		}
	}
}
