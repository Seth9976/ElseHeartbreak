using System;

namespace System.Data
{
	/// <summary>Specifies the transaction locking behavior for the connection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005A RID: 90
	public enum IsolationLevel
	{
		/// <summary>The pending changes from more highly isolated transactions cannot be overwritten.</summary>
		// Token: 0x040001D0 RID: 464
		Chaos = 16,
		/// <summary>A dirty read is possible, meaning that no shared locks are issued and no exclusive locks are honored.</summary>
		// Token: 0x040001D1 RID: 465
		ReadUncommitted = 256,
		/// <summary>Shared locks are held while the data is being read to avoid dirty reads, but the data can be changed before the end of the transaction, resulting in non-repeatable reads or phantom data.</summary>
		// Token: 0x040001D2 RID: 466
		ReadCommitted = 4096,
		/// <summary>Locks are placed on all data that is used in a query, preventing other users from updating the data. Prevents non-repeatable reads but phantom rows are still possible.</summary>
		// Token: 0x040001D3 RID: 467
		RepeatableRead = 65536,
		/// <summary>A range lock is placed on the <see cref="T:System.Data.DataSet" />, preventing other users from updating or inserting rows into the dataset until the transaction is complete.</summary>
		// Token: 0x040001D4 RID: 468
		Serializable = 1048576,
		/// <summary>Reduces blocking by storing a version of data that one application can read while another is modifying the same data. Indicates that from one transaction you cannot see changes made in other transactions, even if you requery.</summary>
		// Token: 0x040001D5 RID: 469
		Snapshot = 16777216,
		/// <summary>A different isolation level than the one specified is being used, but the level cannot be determined.</summary>
		// Token: 0x040001D6 RID: 470
		Unspecified = -1
	}
}
