using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates whether to create a new context based on the current context or on the information in <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
	// Token: 0x0200001C RID: 28
	[ComVisible(false)]
	[Serializable]
	public enum InheritanceOption
	{
		/// <summary>The new context is created from the existing context. <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" /> is the default value for <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" />.</summary>
		// Token: 0x04000050 RID: 80
		Inherit,
		/// <summary>The new context is created from the default context.</summary>
		// Token: 0x04000051 RID: 81
		Ignore
	}
}
