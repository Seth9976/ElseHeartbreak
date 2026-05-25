using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates the context in which to run the COM+ partition.</summary>
	// Token: 0x02000030 RID: 48
	[ComVisible(false)]
	[Serializable]
	public enum PartitionOption
	{
		/// <summary>The enclosed context runs in the Global Partition. <see cref="F:System.EnterpriseServices.PartitionOption.Ignore" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Ignore" />.</summary>
		// Token: 0x04000067 RID: 103
		Ignore,
		/// <summary>The enclosed context runs in the current containing COM+ partition. This is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</summary>
		// Token: 0x04000068 RID: 104
		Inherit,
		/// <summary>The enclosed context runs in a COM+ partition that is different from the current containing partition.</summary>
		// Token: 0x04000069 RID: 105
		New
	}
}
