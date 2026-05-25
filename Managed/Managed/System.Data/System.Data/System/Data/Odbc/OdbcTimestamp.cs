using System;

namespace System.Data.Odbc
{
	// Token: 0x02000138 RID: 312
	internal struct OdbcTimestamp
	{
		// Token: 0x04000644 RID: 1604
		internal short year;

		// Token: 0x04000645 RID: 1605
		internal ushort month;

		// Token: 0x04000646 RID: 1606
		internal ushort day;

		// Token: 0x04000647 RID: 1607
		internal ushort hour;

		// Token: 0x04000648 RID: 1608
		internal ushort minute;

		// Token: 0x04000649 RID: 1609
		internal ushort second;

		// Token: 0x0400064A RID: 1610
		internal ulong fraction;
	}
}
