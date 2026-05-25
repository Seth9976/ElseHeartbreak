using System;

namespace System.Transactions
{
	/// <summary>Specifies the isolation level of a transaction.</summary>
	// Token: 0x02000016 RID: 22
	public enum IsolationLevel
	{
		/// <summary>Volatile data can be read but not modified, and no new data can be added during the transaction.</summary>
		// Token: 0x04000030 RID: 48
		Serializable,
		/// <summary>Volatile data can be read but not modified during the transaction. New data can be added during the transaction.</summary>
		// Token: 0x04000031 RID: 49
		RepeatableRead,
		/// <summary>Volatile data cannot be read during the transaction, but can be modified.</summary>
		// Token: 0x04000032 RID: 50
		ReadCommitted,
		/// <summary>Volatile data can be read and modified during the transaction.</summary>
		// Token: 0x04000033 RID: 51
		ReadUncommitted,
		/// <summary>Volatile data can be read. Before a transaction modifies data, it verifies if another transaction has changed the data after it was initially read. If the data has been updated, an error is raised. This allows a transaction to get to the previously committed value of the data.</summary>
		// Token: 0x04000034 RID: 52
		Snapshot,
		/// <summary>The pending changes from more highly isolated transactions cannot be overwritten.</summary>
		// Token: 0x04000035 RID: 53
		Chaos,
		/// <summary>A different isolation level than the one specified is being used, but the level cannot be determined. An exception is thrown if this value is set.</summary>
		// Token: 0x04000036 RID: 54
		Unspecified
	}
}
