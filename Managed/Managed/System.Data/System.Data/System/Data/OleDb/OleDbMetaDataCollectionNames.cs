using System;

namespace System.Data.OleDb
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F6 RID: 246
	public static class OleDbMetaDataCollectionNames
	{
		/// <summary>A constant for use with the GetSchema method that represents the Catalogs collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400045E RID: 1118
		public static readonly string Catalogs = "Catalogs";

		/// <summary>A constant for use with the GetSchema method that represents the Collations collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400045F RID: 1119
		public static readonly string Collations = "Collations";

		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000460 RID: 1120
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000461 RID: 1121
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000462 RID: 1122
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureParameters collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000463 RID: 1123
		public static readonly string ProcedureParameters = "ProcedureParameters";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000464 RID: 1124
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000465 RID: 1125
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000466 RID: 1126
		public static readonly string Views = "Views";
	}
}
