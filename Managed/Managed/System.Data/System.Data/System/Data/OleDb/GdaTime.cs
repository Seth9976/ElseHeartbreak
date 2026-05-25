using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Sequential)]
	internal class GdaTime
	{
		// Token: 0x04000409 RID: 1033
		public ushort hour;

		// Token: 0x0400040A RID: 1034
		public ushort minute;

		// Token: 0x0400040B RID: 1035
		public ushort second;

		// Token: 0x0400040C RID: 1036
		public long timezone;
	}
}
