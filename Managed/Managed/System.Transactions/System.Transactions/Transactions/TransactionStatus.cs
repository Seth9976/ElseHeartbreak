using System;

namespace System.Transactions
{
	/// <summary>Describes the current status of a distributed transaction.</summary>
	// Token: 0x02000027 RID: 39
	public enum TransactionStatus
	{
		/// <summary>The status of the transaction is unknown, because some participants must still be polled.</summary>
		// Token: 0x0400005F RID: 95
		Active,
		/// <summary>The transaction has been committed.</summary>
		// Token: 0x04000060 RID: 96
		Committed,
		/// <summary>The transaction has been rolled back.</summary>
		// Token: 0x04000061 RID: 97
		Aborted,
		/// <summary>The status of the transaction is unknown.</summary>
		// Token: 0x04000062 RID: 98
		InDoubt
	}
}
