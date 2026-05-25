using System;

namespace System.Data
{
	/// <summary>Provides a description of the results of the query and its effect on the database.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum CommandBehavior
	{
		/// <summary>The query may return multiple result sets. Execution of the query may affect the database state. Default sets no <see cref="T:System.Data.CommandBehavior" /> flags, so calling ExecuteReader(CommandBehavior.Default) is functionally equivalent to calling ExecuteReader().</summary>
		// Token: 0x04000063 RID: 99
		Default = 0,
		/// <summary>The query returns a single result set.</summary>
		// Token: 0x04000064 RID: 100
		SingleResult = 1,
		/// <summary>The query returns column information only. When using <see cref="F:System.Data.CommandBehavior.SchemaOnly" />, the .NET Framework Data Provider for SQL Server precedes the statement being executed with SET FMTONLY ON.</summary>
		// Token: 0x04000065 RID: 101
		SchemaOnly = 2,
		/// <summary>The query returns column and primary key information. </summary>
		// Token: 0x04000066 RID: 102
		KeyInfo = 4,
		/// <summary>The query is expected to return a single row. Execution of the query may affect the database state. Some .NET Framework data providers may, but are not required to, use this information to optimize the performance of the command. When you specify <see cref="F:System.Data.CommandBehavior.SingleRow" /> with the <see cref="M:System.Data.OleDb.OleDbCommand.ExecuteReader" /> method of the <see cref="T:System.Data.OleDb.OleDbCommand" /> object, the .NET Framework Data Provider for OLE DB performs binding using the OLE DB IRow interface if it is available. Otherwise, it uses the IRowset interface. If your SQL statement is expected to return only a single row, specifying <see cref="F:System.Data.CommandBehavior.SingleRow" /> can also improve application performance. It is possible to specify SingleRow when executing queries that return multiple result sets. In that case, multiple result sets are still returned, but each result set has a single row.</summary>
		// Token: 0x04000067 RID: 103
		SingleRow = 8,
		/// <summary>Provides a way for the DataReader to handle rows that contain columns with large binary values. Rather than loading the entire row, SequentialAccess enables the DataReader to load data as a stream. You can then use the GetBytes or GetChars method to specify a byte location to start the read operation, and a limited buffer size for the data being returned.</summary>
		// Token: 0x04000068 RID: 104
		SequentialAccess = 16,
		/// <summary>When the command is executed, the associated Connection object is closed when the associated DataReader object is closed.</summary>
		// Token: 0x04000069 RID: 105
		CloseConnection = 32
	}
}
