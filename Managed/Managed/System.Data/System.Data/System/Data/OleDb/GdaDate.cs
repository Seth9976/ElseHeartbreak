using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Sequential)]
	internal class GdaDate
	{
		// Token: 0x04000406 RID: 1030
		public short year;

		// Token: 0x04000407 RID: 1031
		public ushort month;

		// Token: 0x04000408 RID: 1032
		public ushort day;
	}
}
