using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential)]
	internal class GdaList
	{
		// Token: 0x04000415 RID: 1045
		public IntPtr data;

		// Token: 0x04000416 RID: 1046
		public IntPtr next;

		// Token: 0x04000417 RID: 1047
		public IntPtr prev;
	}
}
