using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the values allowed for transaction outcome voting.</summary>
	// Token: 0x02000054 RID: 84
	[ComVisible(false)]
	[Serializable]
	public enum TransactionVote
	{
		/// <summary>Aborts the current transaction.</summary>
		// Token: 0x040000A2 RID: 162
		Abort = 1,
		/// <summary>Commits the current transaction.</summary>
		// Token: 0x040000A3 RID: 163
		Commit = 0
	}
}
