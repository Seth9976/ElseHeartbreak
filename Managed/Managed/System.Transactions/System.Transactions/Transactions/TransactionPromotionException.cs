using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when a promotion fails.</summary>
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class TransactionPromotionException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class. </summary>
		// Token: 0x06000097 RID: 151 RVA: 0x00002DD0 File Offset: 0x00000FD0
		protected TransactionPromotionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x06000098 RID: 152 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public TransactionPromotionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x06000099 RID: 153 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public TransactionPromotionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x0600009A RID: 154 RVA: 0x00002DF0 File Offset: 0x00000FF0
		protected TransactionPromotionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
