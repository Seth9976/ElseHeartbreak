using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies how rows of data are sorted. </summary>
	// Token: 0x02000153 RID: 339
	public enum SortOrder
	{
		/// <summary>Rows are sorted in ascending order.</summary>
		// Token: 0x040006F5 RID: 1781
		Ascending,
		/// <summary>Rows are sorted in descending order.</summary>
		// Token: 0x040006F6 RID: 1782
		Descending,
		/// <summary>The default. No sort order is specified.</summary>
		// Token: 0x040006F7 RID: 1783
		Unspecified = -1
	}
}
