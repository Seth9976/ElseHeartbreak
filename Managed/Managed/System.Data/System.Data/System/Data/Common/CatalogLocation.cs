using System;

namespace System.Data.Common
{
	/// <summary>Indicates the position of the catalog name in a qualified table name in a text command. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000093 RID: 147
	public enum CatalogLocation
	{
		/// <summary>Indicates that the position of the catalog name occurs after the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040002D1 RID: 721
		End = 2,
		/// <summary>Indicates that the position of the catalog name occurs before the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040002D2 RID: 722
		Start = 1
	}
}
