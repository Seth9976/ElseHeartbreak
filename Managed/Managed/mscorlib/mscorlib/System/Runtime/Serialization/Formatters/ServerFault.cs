using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Contains information for a server fault. This class cannot be inherited.</summary>
	// Token: 0x02000516 RID: 1302
	[SoapType]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.ServerFault" /> class.</summary>
		/// <param name="exceptionType">The type of the exception that occurred on the server. </param>
		/// <param name="message">The message that accompanied the exception. </param>
		/// <param name="stackTrace">The stack trace of the thread that threw the exception on the server. </param>
		// Token: 0x060033A9 RID: 13225 RVA: 0x000A6D6C File Offset: 0x000A4F6C
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		/// <summary>Gets or sets the type of exception that was thrown by the server.</summary>
		/// <returns>The type of exception that was thrown by the server.</returns>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000A6D8C File Offset: 0x000A4F8C
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x000A6D94 File Offset: 0x000A4F94
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		/// <summary>Gets or sets the exception message that accompanied the exception thrown on the server.</summary>
		/// <returns>The exception message that accompanied the exception thrown on the server.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000A6DA0 File Offset: 0x000A4FA0
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000A6DA8 File Offset: 0x000A4FA8
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		/// <summary>Gets or sets the stack trace of the thread that threw the exception on the server.</summary>
		/// <returns>The stack trace of the thread that threw the exception on the server.</returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000A6DB4 File Offset: 0x000A4FB4
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x000A6DBC File Offset: 0x000A4FBC
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x0400157C RID: 5500
		private string exceptionType;

		// Token: 0x0400157D RID: 5501
		private string message;

		// Token: 0x0400157E RID: 5502
		private string stackTrace;

		// Token: 0x0400157F RID: 5503
		private Exception exception;
	}
}
