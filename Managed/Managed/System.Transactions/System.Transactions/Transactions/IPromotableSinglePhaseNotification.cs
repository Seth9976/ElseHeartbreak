using System;

namespace System.Transactions
{
	/// <summary>Describes an object that acts as a commit delegate for a non-distributed transaction internal to a resource manager.</summary>
	// Token: 0x02000012 RID: 18
	public interface IPromotableSinglePhaseNotification : ITransactionPromoter
	{
		/// <summary>Notifies a transaction participant that enlistment has completed successfully.</summary>
		/// <exception cref="T:System.Transactions.TransactionException">An attempt to enlist or serialize a transaction.</exception>
		// Token: 0x06000025 RID: 37
		void Initialize();

		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000026 RID: 38
		void Rollback(SinglePhaseEnlistment enlistment);

		/// <summary>Notifies an enlisted object that the transaction is being committed.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface used to send a response to the transaction manager.</param>
		// Token: 0x06000027 RID: 39
		void SinglePhaseCommit(SinglePhaseEnlistment enlistment);
	}
}
