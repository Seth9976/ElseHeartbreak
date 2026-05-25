using System;

namespace System.Data
{
	/// <summary>Specifies the type of SQL query to be used by the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" />, <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" />, <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" />, or <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000075 RID: 117
	public enum StatementType
	{
		/// <summary>An SQL query that is a SELECT statement.</summary>
		// Token: 0x0400022F RID: 559
		Select,
		/// <summary>An SQL query that is an INSERT statement.</summary>
		// Token: 0x04000230 RID: 560
		Insert,
		/// <summary>An SQL query that is an UPDATE statement.</summary>
		// Token: 0x04000231 RID: 561
		Update,
		/// <summary>A SQL query that is a batch statement.</summary>
		// Token: 0x04000232 RID: 562
		Batch = 4,
		/// <summary>An SQL query that is a DELETE statement.</summary>
		// Token: 0x04000233 RID: 563
		Delete = 3
	}
}
