using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when you attempt to do work on a transaction that cannot accept new work.  </summary>
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class TransactionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class.</summary>
		// Token: 0x06000069 RID: 105 RVA: 0x00002AE8 File Offset: 0x00000CE8
		protected TransactionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x0600006A RID: 106 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public TransactionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x0600006B RID: 107 RVA: 0x00002AFC File Offset: 0x00000CFC
		public TransactionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x0600006C RID: 108 RVA: 0x00002B08 File Offset: 0x00000D08
		protected TransactionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
