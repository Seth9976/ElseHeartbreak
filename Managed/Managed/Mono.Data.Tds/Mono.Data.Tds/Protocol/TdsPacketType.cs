using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000022 RID: 34
	public enum TdsPacketType
	{
		// Token: 0x0400012A RID: 298
		None,
		// Token: 0x0400012B RID: 299
		Query,
		// Token: 0x0400012C RID: 300
		Logon,
		// Token: 0x0400012D RID: 301
		Proc,
		// Token: 0x0400012E RID: 302
		Reply,
		// Token: 0x0400012F RID: 303
		Cancel = 6,
		// Token: 0x04000130 RID: 304
		Bulk,
		// Token: 0x04000131 RID: 305
		Logon70 = 16,
		// Token: 0x04000132 RID: 306
		SspAuth,
		// Token: 0x04000133 RID: 307
		Logoff = 113,
		// Token: 0x04000134 RID: 308
		Normal = 15,
		// Token: 0x04000135 RID: 309
		DBRPC = 230,
		// Token: 0x04000136 RID: 310
		RPC = 3
	}
}
