using System;

namespace System.Data.Common
{
	/// <summary>Specifies what types of Transact-SQL join statements are supported by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DE RID: 222
	[Flags]
	public enum SupportedJoinOperators
	{
		/// <summary>The data source does not support join queries.</summary>
		// Token: 0x040003DB RID: 987
		None = 0,
		/// <summary>The data source supports inner joins.</summary>
		// Token: 0x040003DC RID: 988
		Inner = 1,
		/// <summary>The data source supports left outer joins.</summary>
		// Token: 0x040003DD RID: 989
		LeftOuter = 2,
		/// <summary>The data source supports right outer joins.</summary>
		// Token: 0x040003DE RID: 990
		RightOuter = 4,
		/// <summary>The data source supports full outer joins.</summary>
		// Token: 0x040003DF RID: 991
		FullOuter = 8
	}
}
