using System;

namespace System.Data.Common
{
	/// <summary>Specifies how identifiers are treated by the data source when searching the system catalog.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D4 RID: 212
	public enum IdentifierCase
	{
		/// <summary>The data source ignores identifier case when searching the system catalog. The identifiers "ab" and "AB" will match.</summary>
		// Token: 0x0400038E RID: 910
		Insensitive = 1,
		/// <summary>The data source distinguishes identifier case when searching the system catalog. The identifiers "ab" and "AB" will not match.</summary>
		// Token: 0x0400038F RID: 911
		Sensitive,
		/// <summary>The data source has ambiguous rules regarding identifier case and cannot discern this information.</summary>
		// Token: 0x04000390 RID: 912
		Unknown = 0
	}
}
