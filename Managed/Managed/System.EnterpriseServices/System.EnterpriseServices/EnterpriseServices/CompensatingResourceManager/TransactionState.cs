using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Specifies the state of the current Compensating Resource Manager (CRM) transaction.</summary>
	// Token: 0x0200005F RID: 95
	[Serializable]
	public enum TransactionState
	{
		/// <summary>The transaction is active.</summary>
		// Token: 0x040000C1 RID: 193
		Active,
		/// <summary>The transaction is commited.</summary>
		// Token: 0x040000C2 RID: 194
		Committed,
		/// <summary>The transaction is aborted.</summary>
		// Token: 0x040000C3 RID: 195
		Aborted,
		/// <summary>The transaction is in-doubt.</summary>
		// Token: 0x040000C4 RID: 196
		Indoubt
	}
}
