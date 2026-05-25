using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to user data for a user-defined method or function.</summary>
	// Token: 0x02000145 RID: 325
	[Serializable]
	public enum DataAccessKind
	{
		/// <summary>The method or function does not access user data.</summary>
		// Token: 0x04000678 RID: 1656
		None,
		/// <summary>The method or function reads user data.</summary>
		// Token: 0x04000679 RID: 1657
		Read
	}
}
