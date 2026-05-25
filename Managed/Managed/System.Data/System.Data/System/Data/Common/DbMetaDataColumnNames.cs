using System;

namespace System.Data.Common
{
	/// <summary>Provides static values that are used for the column names in the MetaDataCollection objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C7 RID: 199
	public static class DbMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the CollectionName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000340 RID: 832
		public static readonly string CollectionName = "CollectionName";

		/// <summary>Used by the GetSchema method to create the ColumnSize column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000341 RID: 833
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Used by the GetSchema method to create the CompositeIdentifierSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000342 RID: 834
		public static readonly string CompositeIdentifierSeparatorPattern = "CompositeIdentifierSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the CreateFormat column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000343 RID: 835
		public static readonly string CreateFormat = "CreateFormat";

		/// <summary>Used by the GetSchema method to create the CreateParameters column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000344 RID: 836
		public static readonly string CreateParameters = "CreateParameters";

		/// <summary>Used by the GetSchema method to create the DataSourceProductName column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000345 RID: 837
		public static readonly string DataSourceProductName = "DataSourceProductName";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersion column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000346 RID: 838
		public static readonly string DataSourceProductVersion = "DataSourceProductVersion";

		/// <summary>Used by the GetSchema method to create the DataType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000347 RID: 839
		public static readonly string DataType = "DataType";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersionNormalized column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000348 RID: 840
		public static readonly string DataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		/// <summary>Used by the GetSchema method to create the GroupByBehavior column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000349 RID: 841
		public static readonly string GroupByBehavior = "GroupByBehavior";

		/// <summary>Used by the GetSchema method to create the IdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034A RID: 842
		public static readonly string IdentifierCase = "IdentifierCase";

		/// <summary>Used by the GetSchema method to create the IdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034B RID: 843
		public static readonly string IdentifierPattern = "IdentifierPattern";

		/// <summary>Used by the GetSchema method to create the IsAutoIncrementable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034C RID: 844
		public static readonly string IsAutoIncrementable = "IsAutoIncrementable";

		/// <summary>Used by the GetSchema method to create the IsBestMatch column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034D RID: 845
		public static readonly string IsBestMatch = "IsBestMatch";

		/// <summary>Used by the GetSchema method to create the IsCaseSensitive column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034E RID: 846
		public static readonly string IsCaseSensitive = "IsCaseSensitive";

		/// <summary>Used by the GetSchema method to create the IsConcurrencyType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400034F RID: 847
		public static readonly string IsConcurrencyType = "IsConcurrencyType";

		/// <summary>Used by the GetSchema method to create the IsFixedLength column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000350 RID: 848
		public static readonly string IsFixedLength = "IsFixedLength";

		/// <summary>Used by the GetSchema method to create the IsFixedPrecisionScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000351 RID: 849
		public static readonly string IsFixedPrecisionScale = "IsFixedPrecisionScale";

		/// <summary>Used by the GetSchema method to create the IsLiteralSupported column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000352 RID: 850
		public static readonly string IsLiteralSupported = "IsLiteralSupported";

		/// <summary>Used by the GetSchema method to create the IsLong column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000353 RID: 851
		public static readonly string IsLong = "IsLong";

		/// <summary>Used by the GetSchema method to create the IsNullable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000354 RID: 852
		public static readonly string IsNullable = "IsNullable";

		/// <summary>Used by the GetSchema method to create the IsSearchable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000355 RID: 853
		public static readonly string IsSearchable = "IsSearchable";

		/// <summary>Used by the GetSchema method to create the IsSearchableWithLike column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000356 RID: 854
		public static readonly string IsSearchableWithLike = "IsSearchableWithLike";

		/// <summary>Used by the GetSchema method to create the IsUnsigned column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000357 RID: 855
		public static readonly string IsUnsigned = "IsUnsigned";

		/// <summary>Used by the GetSchema method to create the LiteralPrefix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000358 RID: 856
		public static readonly string LiteralPrefix = "LiteralPrefix";

		/// <summary>Used by the GetSchema method to create the LiteralSuffix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000359 RID: 857
		public static readonly string LiteralSuffix = "LiteralSuffix";

		/// <summary>Used by the GetSchema method to create the MaximumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035A RID: 858
		public static readonly string MaximumScale = "MaximumScale";

		/// <summary>Used by the GetSchema method to create the MinimumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035B RID: 859
		public static readonly string MinimumScale = "MinimumScale";

		/// <summary>Used by the GetSchema method to create the NumberOfIdentifierParts column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035C RID: 860
		public static readonly string NumberOfIdentifierParts = "NumberOfIdentifierParts";

		/// <summary>Used by the GetSchema method to create the NumberOfRestrictions column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035D RID: 861
		public static readonly string NumberOfRestrictions = "NumberOfRestrictions";

		/// <summary>Used by the GetSchema method to create the OrderByColumnsInSelect column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035E RID: 862
		public static readonly string OrderByColumnsInSelect = "OrderByColumnsInSelect";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerFormat column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400035F RID: 863
		public static readonly string ParameterMarkerFormat = "ParameterMarkerFormat";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000360 RID: 864
		public static readonly string ParameterMarkerPattern = "ParameterMarkerPattern";

		/// <summary>Used by the GetSchema method to create the ParameterNameMaxLength column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000361 RID: 865
		public static readonly string ParameterNameMaxLength = "ParameterNameMaxLength";

		/// <summary>Used by the GetSchema method to create the ParameterNamePattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000362 RID: 866
		public static readonly string ParameterNamePattern = "ParameterNamePattern";

		/// <summary>Used by the GetSchema method to create the ProviderDbType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000363 RID: 867
		public static readonly string ProviderDbType = "ProviderDbType";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000364 RID: 868
		public static readonly string QuotedIdentifierCase = "QuotedIdentifierCase";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000365 RID: 869
		public static readonly string QuotedIdentifierPattern = "QuotedIdentifierPattern";

		/// <summary>Used by the GetSchema method to create the ReservedWord column in the ReservedWords collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000366 RID: 870
		public static readonly string ReservedWord = "ReservedWord";

		/// <summary>Used by the GetSchema method to create the StatementSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000367 RID: 871
		public static readonly string StatementSeparatorPattern = "StatementSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the StringLiteralPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000368 RID: 872
		public static readonly string StringLiteralPattern = "StringLiteralPattern";

		/// <summary>Used by the GetSchema method to create the SupportedJoinOperators column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000369 RID: 873
		public static readonly string SupportedJoinOperators = "SupportedJoinOperators";

		/// <summary>Used by the GetSchema method to create the TypeName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400036A RID: 874
		public static readonly string TypeName = "TypeName";
	}
}
