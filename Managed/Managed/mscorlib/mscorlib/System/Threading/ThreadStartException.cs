using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.</summary>
	// Token: 0x020006B3 RID: 1715
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x060041A7 RID: 16807 RVA: 0x000E15B8 File Offset: 0x000DF7B8
		internal ThreadStartException()
			: base("Thread Start Error")
		{
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E15C8 File Offset: 0x000DF7C8
		internal ThreadStartException(string message)
			: base(message)
		{
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000E15D4 File Offset: 0x000DF7D4
		internal ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E15E0 File Offset: 0x000DF7E0
		internal ThreadStartException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
