using System;
using System.Runtime.InteropServices;

namespace System.Transactions
{
	/// <summary>Describes a DTC transaction.</summary>
	// Token: 0x02000010 RID: 16
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDtcTransaction
	{
		/// <summary>Aborts a transaction.</summary>
		/// <param name="reason">An optional <see cref="T:System.EnterpriseServices.BOID" /> that indicates why the transaction is being aborted. This parameter can be null, indicating that no reason for the abort is provided.</param>
		/// <param name="retaining">This value must be false.</param>
		/// <param name="async">When <paramref name="async" /> is true, an asynchronous abort is performed and the caller must use ITransactionOutcomeEvents to learn about the outcome of the transaction. </param>
		// Token: 0x0600001E RID: 30
		void Abort(IntPtr manager, int whatever, int whatever2);

		/// <summary>Commits a transaction.</summary>
		/// <param name="retaining">This value must be false.</param>
		/// <param name="commitType">A value taken from the OLE DB enumeration XACTTC.</param>
		/// <param name="reserved">This value must be zero.</param>
		// Token: 0x0600001F RID: 31
		void Commit(int whatever, int whatever2, int whatever3);

		/// <summary>Retrieves information about a transaction.</summary>
		/// <param name="transactionInformation">Pointer to the caller-allocated <see cref="T:System.EnterpriseServices.XACTTRANSINFO" /> structure that will receive information about the transaction. This value must not be null. </param>
		// Token: 0x06000020 RID: 32
		void GetTransactionInfo(IntPtr whatever);
	}
}
