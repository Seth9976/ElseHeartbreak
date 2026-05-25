using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>The exception that is thrown when an error is detected in a serviced component.</summary>
	// Token: 0x02000040 RID: 64
	[ComVisible(false)]
	[Serializable]
	public sealed class ServicedComponentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		// Token: 0x060000FF RID: 255 RVA: 0x000029E8 File Offset: 0x00000BE8
		public ServicedComponentException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class with a specified error message.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown. </param>
		// Token: 0x06000100 RID: 256 RVA: 0x000029F0 File Offset: 0x00000BF0
		public ServicedComponentException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown. </param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
		// Token: 0x06000101 RID: 257 RVA: 0x000029FC File Offset: 0x00000BFC
		public ServicedComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
