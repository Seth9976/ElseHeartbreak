using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> contains a syntax error.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class SyntaxErrorException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class.</summary>
		// Token: 0x06000652 RID: 1618 RVA: 0x0001F388 File Offset: 0x0001D588
		public SyntaxErrorException()
			: base(Locale.GetText("There is a syntax error in this Expression"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000653 RID: 1619 RVA: 0x0001F39C File Offset: 0x0001D59C
		public SyntaxErrorException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a specific serialized stream. </param>
		// Token: 0x06000654 RID: 1620 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		protected SyntaxErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000655 RID: 1621 RVA: 0x0001F3B4 File Offset: 0x0001D5B4
		public SyntaxErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
