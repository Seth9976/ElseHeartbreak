using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Describes the origin of a Compensating Resource Manager (CRM) log record.</summary>
	// Token: 0x0200005E RID: 94
	[Flags]
	[Serializable]
	public enum LogRecordFlags
	{
		/// <summary>Indicates the delivered record should be forgotten.</summary>
		// Token: 0x040000B9 RID: 185
		ForgetTarget = 1,
		/// <summary>Log record was written during prepare.</summary>
		// Token: 0x040000BA RID: 186
		WrittenDuringPrepare = 2,
		/// <summary>Log record was written during commit.</summary>
		// Token: 0x040000BB RID: 187
		WrittenDuringCommit = 4,
		/// <summary>Log record was written during abort.</summary>
		// Token: 0x040000BC RID: 188
		WrittenDuringAbort = 8,
		/// <summary>Log record was written during recovery.</summary>
		// Token: 0x040000BD RID: 189
		WrittenDurringRecovery = 16,
		/// <summary>Log record was written during replay.</summary>
		// Token: 0x040000BE RID: 190
		WrittenDuringReplay = 32,
		/// <summary>Log record was written when replay was in progress.</summary>
		// Token: 0x040000BF RID: 191
		ReplayInProgress = 64
	}
}
