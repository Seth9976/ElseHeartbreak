using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when an operation is attempted on a transaction that is in doubt, or an attempt is made to commit the transaction and the transaction becomes InDoubt. </summary>
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class TransactionInDoubtException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class.</summary>
		// Token: 0x0600006D RID: 109 RVA: 0x00002B14 File Offset: 0x00000D14
		protected TransactionInDoubtException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x0600006E RID: 110 RVA: 0x00002B1C File Offset: 0x00000D1C
		public TransactionInDoubtException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x0600006F RID: 111 RVA: 0x00002B28 File Offset: 0x00000D28
		public TransactionInDoubtException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x06000070 RID: 112 RVA: 0x00002B34 File Offset: 0x00000D34
		protected TransactionInDoubtException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
