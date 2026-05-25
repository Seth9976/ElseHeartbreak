using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Implements error trapping on the asynchronous batch work that is submitted by the <see cref="T:System.EnterpriseServices.Activity" /> object.</summary>
	// Token: 0x02000018 RID: 24
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FE6777FB-A674-4177-8F32-6D707E113484")]
	[ComImport]
	public interface IAsyncErrorNotify
	{
		/// <summary>Handles errors for asynchronous batch work.</summary>
		/// <param name="hresult">The HRESULT of the error that occurred while the batch work was running asynchronously. </param>
		// Token: 0x06000063 RID: 99
		void OnError(int hresult);
	}
}
