using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Sequential)]
	internal class GdaTimestamp
	{
		// Token: 0x0400040D RID: 1037
		public short year;

		// Token: 0x0400040E RID: 1038
		public ushort month;

		// Token: 0x0400040F RID: 1039
		public ushort day;

		// Token: 0x04000410 RID: 1040
		public ushort hour;

		// Token: 0x04000411 RID: 1041
		public ushort minute;

		// Token: 0x04000412 RID: 1042
		public ushort second;

		// Token: 0x04000413 RID: 1043
		public ulong fraction;

		// Token: 0x04000414 RID: 1044
		public long timezone;
	}
}
