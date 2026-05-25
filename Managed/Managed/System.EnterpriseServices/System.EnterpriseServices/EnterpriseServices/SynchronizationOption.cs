using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the type of automatic synchronization requested by the component.</summary>
	// Token: 0x02000048 RID: 72
	[Serializable]
	public enum SynchronizationOption
	{
		/// <summary>COM+ ignores the synchronization requirements of the component when determining context for the object.</summary>
		// Token: 0x04000081 RID: 129
		Disabled,
		/// <summary>An object with this value never participates in synchronization, regardless of the status of its caller. This setting is only available for components that are non-transactional and do not use just-in-time (JIT) activation.</summary>
		// Token: 0x04000082 RID: 130
		NotSupported,
		/// <summary>Ensures that all objects created from the component are synchronized.</summary>
		// Token: 0x04000083 RID: 131
		Required = 3,
		/// <summary>An object with this value must participate in a new synchronization where COM+ manages contexts and apartments on behalf of all components involved in the call.</summary>
		// Token: 0x04000084 RID: 132
		RequiresNew,
		/// <summary>An object with this value participates in synchronization, if it exists.</summary>
		// Token: 0x04000085 RID: 133
		Supported = 2
	}
}
