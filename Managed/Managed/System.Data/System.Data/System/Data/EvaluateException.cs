using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> cannot be evaluated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class.</summary>
		// Token: 0x06000544 RID: 1348 RVA: 0x0001D814 File Offset: 0x0001BA14
		public EvaluateException()
			: base(Locale.GetText("This expression cannot be evaluated"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000545 RID: 1349 RVA: 0x0001D828 File Offset: 0x0001BA28
		public EvaluateException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000546 RID: 1350 RVA: 0x0001D834 File Offset: 0x0001BA34
		public EvaluateException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a particular serialized stream. </param>
		// Token: 0x06000547 RID: 1351 RVA: 0x0001D840 File Offset: 0x0001BA40
		protected EvaluateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
