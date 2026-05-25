using System;

namespace System.Transactions
{
	/// <summary>Provides data for the following transaction events: <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" />, <see cref="E:System.Transactions.Transaction.TransactionCompleted" />.</summary>
	// Token: 0x0200001C RID: 28
	public class TransactionEventArgs : EventArgs
	{
		/// <summary>Gets the transaction for which event status is provided.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> for which event status is provided.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public Transaction Transaction
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
