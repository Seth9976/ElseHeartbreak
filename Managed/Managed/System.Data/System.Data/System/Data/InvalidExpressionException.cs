using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to add a <see cref="T:System.Data.DataColumn" /> that contains an invalid <see cref="P:System.Data.DataColumn.Expression" /> to a <see cref="T:System.Data.DataColumnCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000058 RID: 88
	[Serializable]
	public class InvalidExpressionException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class.</summary>
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001E494 File Offset: 0x0001C694
		public InvalidExpressionException()
			: base(Locale.GetText("This Expression is invalid"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		public InvalidExpressionException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		public InvalidExpressionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidExpressionException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream. </param>
		// Token: 0x060005F8 RID: 1528 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		protected InvalidExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
