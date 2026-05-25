using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the automatic transaction type requested by the component.</summary>
	// Token: 0x02000052 RID: 82
	[Serializable]
	public enum TransactionOption
	{
		/// <summary>Ignores any transaction in the current context.</summary>
		// Token: 0x04000096 RID: 150
		Disabled,
		/// <summary>Creates the component in a context with no governing transaction.</summary>
		// Token: 0x04000097 RID: 151
		NotSupported,
		/// <summary>Shares a transaction, if one exists.</summary>
		// Token: 0x04000098 RID: 152
		Supported,
		/// <summary>Shares a transaction, if one exists, and creates a new transaction if necessary.</summary>
		// Token: 0x04000099 RID: 153
		Required,
		/// <summary>Creates the component with a new transaction, regardless of the state of the current context.</summary>
		// Token: 0x0400009A RID: 154
		RequiresNew
	}
}
