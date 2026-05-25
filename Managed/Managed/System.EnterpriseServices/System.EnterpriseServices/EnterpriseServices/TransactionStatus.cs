using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates the transaction status.</summary>
	// Token: 0x02000053 RID: 83
	[ComVisible(false)]
	[Serializable]
	public enum TransactionStatus
	{
		/// <summary>The transaction has committed.</summary>
		// Token: 0x0400009C RID: 156
		Commited,
		/// <summary>The transaction has neither committed nor aborted.</summary>
		// Token: 0x0400009D RID: 157
		LocallyOk,
		/// <summary>No transactions are being used through <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" />.</summary>
		// Token: 0x0400009E RID: 158
		NoTransaction,
		/// <summary>The transaction is in the process of aborting.</summary>
		// Token: 0x0400009F RID: 159
		Aborting,
		/// <summary>The transaction is aborted.</summary>
		// Token: 0x040000A0 RID: 160
		Aborted
	}
}
