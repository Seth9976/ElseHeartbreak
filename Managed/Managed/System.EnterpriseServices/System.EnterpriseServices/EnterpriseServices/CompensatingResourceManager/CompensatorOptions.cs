using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Specifies flags that control which phases of transaction completion should be received by the Compensating Resource Manager (CRM) Compensator, and whether recovery should fail if questionable transactions remain after recovery has been attempted.</summary>
	// Token: 0x0200005B RID: 91
	[Flags]
	[Serializable]
	public enum CompensatorOptions
	{
		/// <summary>Represents the prepare phase.</summary>
		// Token: 0x040000AD RID: 173
		PreparePhase = 1,
		/// <summary>Represents the commit phase.</summary>
		// Token: 0x040000AE RID: 174
		CommitPhase = 2,
		/// <summary>Represents the abort phase.</summary>
		// Token: 0x040000AF RID: 175
		AbortPhase = 4,
		/// <summary>Represents all phases.</summary>
		// Token: 0x040000B0 RID: 176
		AllPhases = 7,
		/// <summary>Fails if in-doubt transactions remain after recovery has been attempted.</summary>
		// Token: 0x040000B1 RID: 177
		FailIfInDoubtsRemain = 16
	}
}
