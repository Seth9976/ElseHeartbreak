using System;

namespace System.Data.Common
{
	/// <summary>Describes the column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DC RID: 220
	public static class SchemaTableColumn
	{
		/// <summary>Specifies whether value DBNull is allowed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003BB RID: 955
		public static readonly string AllowDBNull = "AllowDBNull";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003BC RID: 956
		public static readonly string BaseColumnName = "BaseColumnName";

		/// <summary>Specifies the name of the schema in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003BD RID: 957
		public static readonly string BaseSchemaName = "BaseSchemaName";

		/// <summary>Specifies the name of the table in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003BE RID: 958
		public static readonly string BaseTableName = "BaseTableName";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003BF RID: 959
		public static readonly string ColumnName = "ColumnName";

		/// <summary>Specifies the ordinal of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C0 RID: 960
		public static readonly string ColumnOrdinal = "ColumnOrdinal";

		/// <summary>Specifies the size of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C1 RID: 961
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Specifies the type of data in the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C2 RID: 962
		public static readonly string DataType = "DataType";

		/// <summary>Specifies whether this column is aliased.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C3 RID: 963
		public static readonly string IsAliased = "IsAliased";

		/// <summary>Specifies whether this column is an expression.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C4 RID: 964
		public static readonly string IsExpression = "IsExpression";

		/// <summary>Specifies whether this column is a key for the table. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C5 RID: 965
		public static readonly string IsKey = "IsKey";

		/// <summary>Specifies whether this column contains long data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C6 RID: 966
		public static readonly string IsLong = "IsLong";

		/// <summary>Specifies whether a unique constraint applies to this column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C7 RID: 967
		public static readonly string IsUnique = "IsUnique";

		/// <summary>Specifies the non-versioned provider-specific data type of the column.</summary>
		// Token: 0x040003C8 RID: 968
		public static readonly string NonVersionedProviderType = "NonVersionedProviderType";

		/// <summary>Specifies the precision of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003C9 RID: 969
		public static readonly string NumericPrecision = "NumericPrecision";

		/// <summary>Specifies the scale of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CA RID: 970
		public static readonly string NumericScale = "NumericScale";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CB RID: 971
		public static readonly string ProviderType = "ProviderType";
	}
}
