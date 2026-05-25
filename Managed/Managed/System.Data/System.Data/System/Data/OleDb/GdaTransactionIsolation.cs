using System;

namespace System.Data.OleDb
{
	// Token: 0x020000E2 RID: 226
	internal enum GdaTransactionIsolation
	{
		// Token: 0x040003EE RID: 1006
		Unknown,
		// Token: 0x040003EF RID: 1007
		ReadCommitted,
		// Token: 0x040003F0 RID: 1008
		ReadUncommitted,
		// Token: 0x040003F1 RID: 1009
		RepeatableRead,
		// Token: 0x040003F2 RID: 1010
		Serializable
	}
}
