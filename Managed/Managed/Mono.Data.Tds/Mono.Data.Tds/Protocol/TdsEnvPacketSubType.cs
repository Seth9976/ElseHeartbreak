using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001B RID: 27
	public enum TdsEnvPacketSubType
	{
		// Token: 0x040000F6 RID: 246
		Database = 1,
		// Token: 0x040000F7 RID: 247
		CharSet = 3,
		// Token: 0x040000F8 RID: 248
		BlockSize,
		// Token: 0x040000F9 RID: 249
		Locale,
		// Token: 0x040000FA RID: 250
		CollationInfo = 7
	}
}
