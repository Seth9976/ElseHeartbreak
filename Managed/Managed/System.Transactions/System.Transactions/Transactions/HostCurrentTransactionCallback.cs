using System;

namespace System.Transactions
{
	/// <summary>Provides a mechanism for the hosting environment to supply its own default notion of <see cref="P:System.Transactions.Transaction.Current" />.</summary>
	/// <returns>A <see cref="T:System.Transactions.Transaction" /> object.</returns>
	// Token: 0x02000029 RID: 41
	// (Invoke) Token: 0x060000AE RID: 174
	public delegate Transaction HostCurrentTransactionCallback();
}
