using System;

namespace System.Data
{
	/// <summary>Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and update a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000052 RID: 82
	public interface IDbDataAdapter : IDataAdapter
	{
		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005D2 RID: 1490
		// (set) Token: 0x060005D3 RID: 1491
		IDbCommand DeleteCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005D4 RID: 1492
		// (set) Token: 0x060005D5 RID: 1493
		IDbCommand InsertCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D7 RID: 1495
		IDbCommand SelectCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005D8 RID: 1496
		// (set) Token: 0x060005D9 RID: 1497
		IDbCommand UpdateCommand { get; set; }
	}
}
