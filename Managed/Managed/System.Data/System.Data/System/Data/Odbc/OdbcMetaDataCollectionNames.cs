using System;

namespace System.Data.Odbc
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000126 RID: 294
	public static class OdbcMetaDataCollectionNames
	{
		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000570 RID: 1392
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000571 RID: 1393
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000572 RID: 1394
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureParameters collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000573 RID: 1395
		public static readonly string ProcedureParameters = "ProcedureParameters";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000574 RID: 1396
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000575 RID: 1397
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000576 RID: 1398
		public static readonly string Views = "Views";
	}
}
