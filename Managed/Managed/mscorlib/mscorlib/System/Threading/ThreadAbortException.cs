using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a call is made to the <see cref="M:System.Threading.Thread.Abort(System.Object)" /> method. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006AF RID: 1711
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x0600418D RID: 16781 RVA: 0x000E13D0 File Offset: 0x000DF5D0
		private ThreadAbortException()
			: base("Thread was being aborted")
		{
			base.HResult = -2146233040;
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000E13E8 File Offset: 0x000DF5E8
		private ThreadAbortException(SerializationInfo info, StreamingContext sc)
			: base(info, sc)
		{
		}

		/// <summary>Gets an object that contains application-specific information related to the thread abort.</summary>
		/// <returns>An object containing application-specific information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000E13F4 File Offset: 0x000DF5F4
		public object ExceptionState
		{
			get
			{
				return Thread.CurrentThread.GetAbortExceptionState();
			}
		}
	}
}
