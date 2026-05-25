using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the value of the <see cref="T:System.EnterpriseServices.TransactionAttribute" />.</summary>
	// Token: 0x02000051 RID: 81
	[Serializable]
	public enum TransactionIsolationLevel
	{
		/// <summary>The isolation level for the component is obtained from the calling component's isolation level. If this is the root component, the isolation level used is <see cref="F:System.EnterpriseServices.TransactionIsolationLevel.Serializable" />.</summary>
		// Token: 0x04000090 RID: 144
		Any,
		/// <summary>Shared locks are held while the data is being read to avoid reading modified data, but the data can be changed before the end of the transaction, resulting in non-repeatable reads or phantom data.</summary>
		// Token: 0x04000091 RID: 145
		ReadCommitted = 2,
		/// <summary>Shared locks are issued and no exclusive locks are honored.</summary>
		// Token: 0x04000092 RID: 146
		ReadUncommitted = 1,
		/// <summary>Locks are placed on all data that is used in a query, preventing other users from updating the data. Prevents non-repeatable reads, but phantom rows are still possible.</summary>
		// Token: 0x04000093 RID: 147
		RepeatableRead = 3,
		/// <summary>Prevents updating or inserting until the transaction is complete.</summary>
		// Token: 0x04000094 RID: 148
		Serializable
	}
}
