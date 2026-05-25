using System;

namespace System.Data.SqlClient
{
	/// <summary>Indicates the source of the notification received by the dependency event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000156 RID: 342
	public enum SqlNotificationSource
	{
		/// <summary>A client-initiated notification occurred, such as a client-side time-out or as a result of attempting to add a command to a dependency that has already fired.</summary>
		// Token: 0x04000710 RID: 1808
		Client = -2,
		/// <summary>Data has changed; for example, an insert, update, delete, or truncate operation occurred.</summary>
		// Token: 0x04000711 RID: 1809
		Data = 0,
		/// <summary>The database state changed; for example, the database related to the query was dropped or detached.</summary>
		// Token: 0x04000712 RID: 1810
		Database = 3,
		/// <summary>The run-time environment was not compatible with notifications; for example, the isolation level was set to snapshot, or one or more SET options are not compatible.</summary>
		// Token: 0x04000713 RID: 1811
		Environment = 6,
		/// <summary>A run-time error occurred during execution.</summary>
		// Token: 0x04000714 RID: 1812
		Execution,
		/// <summary>A database object changed; for example, an underlying object related to the query was dropped or modified.</summary>
		// Token: 0x04000715 RID: 1813
		Object = 2,
		/// <summary>Internal only; not intended to be used in your code.</summary>
		// Token: 0x04000716 RID: 1814
		Owner = 8,
		/// <summary>The Transact-SQL statement is not valid for notifications; for example, a SELECT statement that could not be notified or a non-SELECT statement was executed.</summary>
		// Token: 0x04000717 RID: 1815
		Statement = 5,
		/// <summary>A system-related event occurred. For example, there was an internal error, the server was restarted, or resource pressure caused the invalidation.</summary>
		// Token: 0x04000718 RID: 1816
		System = 4,
		/// <summary>The subscription time-out expired.</summary>
		// Token: 0x04000719 RID: 1817
		Timeout = 1,
		/// <summary>Used when the source option sent by the server was not recognized by the client. </summary>
		// Token: 0x0400071A RID: 1818
		Unknown = -1
	}
}
