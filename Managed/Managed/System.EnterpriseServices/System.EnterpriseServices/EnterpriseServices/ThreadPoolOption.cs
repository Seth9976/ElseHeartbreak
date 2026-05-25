using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates the thread pool in which the work, submitted by <see cref="T:System.EnterpriseServices.Activity" />, runs.</summary>
	// Token: 0x02000049 RID: 73
	[ComVisible(false)]
	[Serializable]
	public enum ThreadPoolOption
	{
		/// <summary>No thread pool is used. If this value is used to configure a <see cref="T:System.EnterpriseServices.ServiceConfig" /> that is passed to an <see cref="T:System.EnterpriseServices.Activity" />, an exception is thrown.</summary>
		// Token: 0x04000087 RID: 135
		None,
		/// <summary>The same type of thread pool apartment as the caller's thread apartment is used.</summary>
		// Token: 0x04000088 RID: 136
		Inherit,
		/// <summary>A single-threaded apartment (STA) is used.</summary>
		// Token: 0x04000089 RID: 137
		STA,
		/// <summary>A multithreaded apartment (MTA) is used.</summary>
		// Token: 0x0400008A RID: 138
		MTA
	}
}
