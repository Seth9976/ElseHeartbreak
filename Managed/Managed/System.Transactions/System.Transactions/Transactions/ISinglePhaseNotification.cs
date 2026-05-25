using System;

namespace System.Transactions
{
	/// <summary>Describes a resource object that supports single phase commit optimization to participate in a transaction.</summary>
	// Token: 0x02000014 RID: 20
	public interface ISinglePhaseNotification : IEnlistmentNotification
	{
		/// <summary>Represents the resource manager's implementation of the callback for the single phase commit optimization.  </summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" />  used to send a response to the transaction manager.</param>
		// Token: 0x06000029 RID: 41
		void SinglePhaseCommit(SinglePhaseEnlistment enlistment);
	}
}
