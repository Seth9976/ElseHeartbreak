using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown to communicate errors to the client when the client connects to non-.NET Framework applications that cannot throw exceptions.</summary>
	// Token: 0x0200042C RID: 1068
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with default properties.</summary>
		// Token: 0x06002D84 RID: 11652 RVA: 0x00097AC0 File Offset: 0x00095CC0
		public ServerException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified message.</summary>
		/// <param name="message">The message that describes the exception </param>
		// Token: 0x06002D85 RID: 11653 RVA: 0x00097AC8 File Offset: 0x00095CC8
		public ServerException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002D86 RID: 11654 RVA: 0x00097AD4 File Offset: 0x00095CD4
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x00097AE0 File Offset: 0x00095CE0
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
