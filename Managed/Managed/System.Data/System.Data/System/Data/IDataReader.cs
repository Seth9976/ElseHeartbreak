using System;

namespace System.Data
{
	/// <summary>Provides a means of reading one or more forward-only streams of result sets obtained by executing a command at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004E RID: 78
	public interface IDataReader : IDisposable, IDataRecord
	{
		/// <summary>Closes the <see cref="T:System.Data.IDataReader" /> Object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000593 RID: 1427
		void Close();

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.IDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000594 RID: 1428
		DataTable GetSchemaTable();

		/// <summary>Advances the data reader to the next result, when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000595 RID: 1429
		bool NextResult();

		/// <summary>Advances the <see cref="T:System.Data.IDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000596 RID: 1430
		bool Read();

		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The level of nesting.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000597 RID: 1431
		int Depth { get; }

		/// <summary>Gets a value indicating whether the data reader is closed.</summary>
		/// <returns>true if the data reader is closed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000598 RID: 1432
		bool IsClosed { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000599 RID: 1433
		int RecordsAffected { get; }
	}
}
