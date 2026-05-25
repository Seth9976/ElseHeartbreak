using System;

namespace System.Data
{
	/// <summary>Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000050 RID: 80
	public interface IDbCommand : IDisposable
	{
		/// <summary>Attempts to cancels the execution of an <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B3 RID: 1459
		void Cancel();

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An IDbDataParameter object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B4 RID: 1460
		IDbDataParameter CreateParameter();

		/// <summary>Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B5 RID: 1461
		int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B6 RID: 1462
		IDataReader ExecuteReader();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B7 RID: 1463
		IDataReader ExecuteReader(CommandBehavior behavior);

		/// <summary>Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the resultset.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B8 RID: 1464
		object ExecuteScalar();

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not <see cref="M:System.Data.OleDb.OleDbConnection.Open" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005B9 RID: 1465
		void Prepare();

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005BA RID: 1466
		// (set) Token: 0x060005BB RID: 1467
		string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default value is 30 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The property value assigned is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005BC RID: 1468
		// (set) Token: 0x060005BD RID: 1469
		int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.IDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005BE RID: 1470
		// (set) Token: 0x060005BF RID: 1471
		CommandType CommandType { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005C0 RID: 1472
		// (set) Token: 0x060005C1 RID: 1473
		IDbConnection Connection { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005C2 RID: 1474
		IDataParameterCollection Parameters { get; }

		/// <summary>Gets or sets the transaction within which the Command object of a .NET Framework data provider executes.</summary>
		/// <returns>the Command object of a .NET Framework data provider executes. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005C3 RID: 1475
		// (set) Token: 0x060005C4 RID: 1476
		IDbTransaction Transaction { get; set; }

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005C5 RID: 1477
		// (set) Token: 0x060005C6 RID: 1478
		UpdateRowSource UpdatedRowSource { get; set; }
	}
}
