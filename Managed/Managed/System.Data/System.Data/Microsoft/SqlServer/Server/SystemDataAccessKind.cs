using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to system data for a user-defined method or function.</summary>
	// Token: 0x0200014D RID: 333
	[Serializable]
	public enum SystemDataAccessKind
	{
		/// <summary>The method or function does not access system data.</summary>
		// Token: 0x0400069A RID: 1690
		None,
		/// <summary>The method or function reads system data.</summary>
		// Token: 0x0400069B RID: 1691
		Read
	}
}
