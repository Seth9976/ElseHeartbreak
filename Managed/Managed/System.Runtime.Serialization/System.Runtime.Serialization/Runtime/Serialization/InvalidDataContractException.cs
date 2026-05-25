using System;

namespace System.Runtime.Serialization
{
	/// <summary>The exception that is thrown when the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> or <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> encounters an invalid data contract during serialization and deserialization. </summary>
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class InvalidDataContractException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class. </summary>
		// Token: 0x0600007C RID: 124 RVA: 0x00002B74 File Offset: 0x00000D74
		public InvalidDataContractException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified error message. </summary>
		/// <param name="message">A description of the error. </param>
		// Token: 0x0600007D RID: 125 RVA: 0x00002B7C File Offset: 0x00000D7C
		public InvalidDataContractException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains data needed to serialize and deserialize an object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies user context during serialization and deserialization.</param>
		// Token: 0x0600007E RID: 126 RVA: 0x00002B88 File Offset: 0x00000D88
		protected InvalidDataContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified error message and inner exception. </summary>
		/// <param name="message">A description of the error. </param>
		/// <param name="innerException">The original <see cref="T:System.Exception" />. </param>
		// Token: 0x0600007F RID: 127 RVA: 0x00002B94 File Offset: 0x00000D94
		public InvalidDataContractException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
