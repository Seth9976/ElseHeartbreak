using System;

namespace System.Data.Odbc
{
	// Token: 0x02000130 RID: 304
	internal enum OdbcReturn : short
	{
		// Token: 0x04000601 RID: 1537
		Error = -1,
		// Token: 0x04000602 RID: 1538
		InvalidHandle = -2,
		// Token: 0x04000603 RID: 1539
		StillExecuting = 2,
		// Token: 0x04000604 RID: 1540
		NeedData = 99,
		// Token: 0x04000605 RID: 1541
		Success = 0,
		// Token: 0x04000606 RID: 1542
		SuccessWithInfo,
		// Token: 0x04000607 RID: 1543
		NoData = 100
	}
}
