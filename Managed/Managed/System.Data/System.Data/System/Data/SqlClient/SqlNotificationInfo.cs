using System;

namespace System.Data.SqlClient
{
	/// <summary>This enumeration provides additional information about the different notifications that can be received by the dependency event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	public enum SqlNotificationInfo
	{
		/// <summary>The SqlDependency object already fired, and new commands cannot be added to it.</summary>
		// Token: 0x040006FD RID: 1789
		AlreadyChanged = -2,
		/// <summary>An underlying server object related to the query was modified.</summary>
		// Token: 0x040006FE RID: 1790
		Alter = 5,
		/// <summary>Data was changed by a DELETE statement.</summary>
		// Token: 0x040006FF RID: 1791
		Delete = 3,
		/// <summary>An underlying object related to the query was dropped.</summary>
		// Token: 0x04000700 RID: 1792
		Drop,
		/// <summary>An internal server error occurred.</summary>
		// Token: 0x04000701 RID: 1793
		Error = 7,
		/// <summary>The SqlDependency object has expired.</summary>
		// Token: 0x04000702 RID: 1794
		Expired = 12,
		/// <summary>Data was changed by an INSERT statement.</summary>
		// Token: 0x04000703 RID: 1795
		Insert = 1,
		/// <summary>A statement was provided that cannot be notified (for example, an UPDATE statement).</summary>
		// Token: 0x04000704 RID: 1796
		Invalid = 9,
		/// <summary>The statement was executed under an isolation mode that was not valid (for example, Snapshot).</summary>
		// Token: 0x04000705 RID: 1797
		Isolation = 11,
		/// <summary>The SET options were not set appropriately at subscription time.</summary>
		// Token: 0x04000706 RID: 1798
		Options = 10,
		/// <summary>A previous statement has caused query notifications to fire under the current transaction.</summary>
		// Token: 0x04000707 RID: 1799
		PreviousFire = 14,
		/// <summary>A SELECT statement that cannot be notified or was provided.</summary>
		// Token: 0x04000708 RID: 1800
		Query = 8,
		/// <summary>Fires as a result of server resource pressure.</summary>
		// Token: 0x04000709 RID: 1801
		Resource = 13,
		/// <summary>The server was restarted (notifications are sent during restart.).</summary>
		// Token: 0x0400070A RID: 1802
		Restart = 6,
		/// <summary>The subscribing query causes the number of templates on one of the target tables to exceed the maximum allowable limit.</summary>
		// Token: 0x0400070B RID: 1803
		TemplateLimit = 15,
		/// <summary>One or more tables were truncated.</summary>
		// Token: 0x0400070C RID: 1804
		Truncate = 0,
		/// <summary>Used when the info option sent by the server was not recognized by the client.</summary>
		// Token: 0x0400070D RID: 1805
		Unknown = -1,
		/// <summary>Data was changed by an UPDATE statement.</summary>
		// Token: 0x0400070E RID: 1806
		Update = 2
	}
}
