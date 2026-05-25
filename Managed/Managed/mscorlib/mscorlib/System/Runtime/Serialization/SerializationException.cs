using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>The exception thrown when an error occurs during serialization or deserialization.</summary>
	// Token: 0x02000509 RID: 1289
	[ComVisible(true)]
	[Serializable]
	public class SerializationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with default properties.</summary>
		// Token: 0x06003347 RID: 13127 RVA: 0x000A612C File Offset: 0x000A432C
		public SerializationException()
			: base("An error occurred during (de)serialization")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified message.</summary>
		/// <param name="message">Indicates the reason why the exception occurred. </param>
		// Token: 0x06003348 RID: 13128 RVA: 0x000A613C File Offset: 0x000A433C
		public SerializationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003349 RID: 13129 RVA: 0x000A6148 File Offset: 0x000A4348
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class from serialized data.</summary>
		/// <param name="info">The serialization information object holding the serialized object data in the name-value form. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		// Token: 0x0600334A RID: 13130 RVA: 0x000A6154 File Offset: 0x000A4354
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
