using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Represents a structure used in the <see cref="T:System.EnterpriseServices.ITransaction" /> interface.</summary>
	// Token: 0x02000055 RID: 85
	[ComVisible(false)]
	public struct XACTTRANSINFO
	{
		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000A4 RID: 164
		public int grfRMSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000A5 RID: 165
		public int grfRMSupportedRetaining;

		/// <summary>Represents a bitmask that indicates which grfTC flags this transaction implementation supports.</summary>
		// Token: 0x040000A6 RID: 166
		public int grfTCSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000A7 RID: 167
		public int grfTCSupportedRetaining;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000A8 RID: 168
		public int isoFlags;

		/// <summary>Represents the isolation level associated with this transaction object. ISOLATIONLEVEL_UNSPECIFIED indicates that no isolation level was specified.</summary>
		// Token: 0x040000A9 RID: 169
		public int isoLevel;

		/// <summary>Represents the unit of work associated with this transaction.</summary>
		// Token: 0x040000AA RID: 170
		public BOID uow;
	}
}
