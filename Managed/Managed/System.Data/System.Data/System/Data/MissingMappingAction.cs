using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when a mapping is missing from a source table or a source column.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000062 RID: 98
	public enum MissingMappingAction
	{
		/// <summary>The source column or source table is created and added to the <see cref="T:System.Data.DataSet" /> using its original name.</summary>
		// Token: 0x040001E6 RID: 486
		Passthrough = 1,
		/// <summary>The column or table not having a mapping is ignored. Returns null.</summary>
		// Token: 0x040001E7 RID: 487
		Ignore,
		/// <summary>An <see cref="T:System.InvalidOperationException" /> is generated if the specified column mapping is missing.</summary>
		// Token: 0x040001E8 RID: 488
		Error
	}
}
