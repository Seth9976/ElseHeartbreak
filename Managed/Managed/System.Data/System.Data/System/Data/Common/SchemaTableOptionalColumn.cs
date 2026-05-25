using System;

namespace System.Data.Common
{
	/// <summary>Describes optional column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DD RID: 221
	public static class SchemaTableOptionalColumn
	{
		/// <summary>Specifies the value at which the series for new identity columns is assigned.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CC RID: 972
		public static readonly string AutoIncrementSeed = "AutoIncrementSeed";

		/// <summary>Specifies the increment between values in the identity column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CD RID: 973
		public static readonly string AutoIncrementStep = "AutoIncrementStep";

		/// <summary>The name of the catalog associated with the results of the latest query.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CE RID: 974
		public static readonly string BaseCatalogName = "BaseCatalogName";

		/// <summary>The namespace of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003CF RID: 975
		public static readonly string BaseColumnNamespace = "BaseColumnNamespace";

		/// <summary>The server name of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D0 RID: 976
		public static readonly string BaseServerName = "BaseServerName";

		/// <summary>The namespace for the table that contains the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D1 RID: 977
		public static readonly string BaseTableNamespace = "BaseTableNamespace";

		/// <summary>Specifies the mapping for the column.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040003D2 RID: 978
		public static readonly string ColumnMapping = "ColumnMapping";

		/// <summary>The default value for the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D3 RID: 979
		public static readonly string DefaultValue = "DefaultValue";

		/// <summary>The expression used to compute the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D4 RID: 980
		public static readonly string Expression = "Expression";

		/// <summary>Specifies whether the column values in the column are automatically incremented.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D5 RID: 981
		public static readonly string IsAutoIncrement = "IsAutoIncrement";

		/// <summary>Specifies whether this column is hidden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D6 RID: 982
		public static readonly string IsHidden = "IsHidden";

		/// <summary>Specifies whether this column is read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D7 RID: 983
		public static readonly string IsReadOnly = "IsReadOnly";

		/// <summary>Specifies whether this column contains row version information.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D8 RID: 984
		public static readonly string IsRowVersion = "IsRowVersion";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040003D9 RID: 985
		public static readonly string ProviderSpecificDataType = "ProviderSpecificDataType";
	}
}
