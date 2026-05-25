using System;

namespace System.Data.Odbc
{
	// Token: 0x02000135 RID: 309
	internal enum OdbcIsolationLevel
	{
		// Token: 0x0400061F RID: 1567
		ReadUncommitted = 1,
		// Token: 0x04000620 RID: 1568
		ReadCommitted,
		// Token: 0x04000621 RID: 1569
		RepeatableRead = 4,
		// Token: 0x04000622 RID: 1570
		Serializable = 8,
		// Token: 0x04000623 RID: 1571
		Snapshot = 32
	}
}
