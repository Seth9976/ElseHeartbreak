using System;

namespace System.Data.Common
{
	/// <summary>Specifies the relationship between the columns in a GROUP BY clause and the non-aggregated columns in the select-list of a SELECT statement.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D3 RID: 211
	public enum GroupByBehavior
	{
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and must not contain other columns not in the select list.</summary>
		// Token: 0x04000388 RID: 904
		ExactMatch = 4,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and can contain other columns not in the select list.</summary>
		// Token: 0x04000389 RID: 905
		MustContainAll = 3,
		/// <summary>The GROUP BY clause is not supported.</summary>
		// Token: 0x0400038A RID: 906
		NotSupported = 1,
		/// <summary>The support for the GROUP BY clause is unknown.</summary>
		// Token: 0x0400038B RID: 907
		Unknown = 0,
		/// <summary>There is no relationship between the columns in the GROUP BY clause and the nonaggregated columns in the SELECT list. You may group by any column.</summary>
		// Token: 0x0400038C RID: 908
		Unrelated = 2
	}
}
