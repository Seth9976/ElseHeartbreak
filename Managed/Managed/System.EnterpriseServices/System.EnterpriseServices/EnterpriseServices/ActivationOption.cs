using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the manner in which serviced components are activated in the application.</summary>
	// Token: 0x02000004 RID: 4
	[Serializable]
	public enum ActivationOption
	{
		/// <summary>Specifies that serviced components in the marked application are activated in the creator's process.</summary>
		// Token: 0x04000022 RID: 34
		Library,
		/// <summary>Specifies that serviced components in the marked application are activated in a system-provided process.</summary>
		// Token: 0x04000023 RID: 35
		Server
	}
}
