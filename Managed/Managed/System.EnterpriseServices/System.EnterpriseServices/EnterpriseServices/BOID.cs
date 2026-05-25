using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Represents the unit of work associated with a transaction. This structure is used in <see cref="T:System.EnterpriseServices.XACTTRANSINFO" />.</summary>
	// Token: 0x0200000E RID: 14
	[ComVisible(false)]
	public struct BOID
	{
		/// <summary>Represents an array that contains the data.</summary>
		// Token: 0x0400003C RID: 60
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] rgb;
	}
}
