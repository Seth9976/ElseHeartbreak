using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Implements the batch work that is submitted through the activity created by <see cref="T:System.EnterpriseServices.Activity" />.</summary>
	// Token: 0x02000027 RID: 39
	[Guid("BD3E2E12-42DD-40f4-A09A-95A50C58304B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IServiceCall
	{
		/// <summary>Starts the execution of the batch work implemented in this method.</summary>
		// Token: 0x06000085 RID: 133
		void OnCall();
	}
}
