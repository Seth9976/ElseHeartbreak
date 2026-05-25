using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when an operation is attempted on a transaction that has already been rolled back, or an attempt is made to commit the transaction and the transaction aborts. </summary>
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class TransactionAbortedException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class.</summary>
		// Token: 0x06000063 RID: 99 RVA: 0x00002AAC File Offset: 0x00000CAC
		public TransactionAbortedException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x06000064 RID: 100 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public TransactionAbortedException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x06000065 RID: 101 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public TransactionAbortedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x06000066 RID: 102 RVA: 0x00002ACC File Offset: 0x00000CCC
		protected TransactionAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
