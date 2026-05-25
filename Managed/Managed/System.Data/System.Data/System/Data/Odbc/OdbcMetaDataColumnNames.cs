using System;

namespace System.Data.Odbc
{
	/// <summary>Provides static values that are used for the column names in the <see cref="T:System.Data.Odbc.OdbcMetaDataCollectionNames" /> objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000125 RID: 293
	public static class OdbcMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the BooleanFalseLiteral column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056D RID: 1389
		public static readonly string BooleanFalseLiteral;

		/// <summary>Used by the GetSchema method to create the BooleanTrueLiteral column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056E RID: 1390
		public static readonly string BooleanTrueLiteral;

		/// <summary>Used by the GetSchema method to create the SQLType column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400056F RID: 1391
		public static readonly string SQLType;
	}
}
