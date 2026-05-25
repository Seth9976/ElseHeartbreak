using System;

namespace System.Transactions
{
	/// <summary>Represents the method that handles the <see cref="E:System.Transactions.Transaction.TransactionCompleted" /> event of a <see cref="T:System.Transactions.Transaction" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Transactions.TransactionEventArgs" /> that contains the event data.</param>
	// Token: 0x0200002A RID: 42
	// (Invoke) Token: 0x060000B2 RID: 178
	public delegate void TransactionCompletedEventHandler(object o, TransactionEventArgs e);
}
