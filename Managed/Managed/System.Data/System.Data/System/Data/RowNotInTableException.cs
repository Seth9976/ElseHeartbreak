using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to perform an operation on a <see cref="T:System.Data.DataRow" /> that is not in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class RowNotInTableException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class.</summary>
		// Token: 0x06000645 RID: 1605 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
		public RowNotInTableException()
			: base(Locale.GetText("This DataRow is not in this DataTable"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000646 RID: 1606 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public RowNotInTableException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000647 RID: 1607 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		public RowNotInTableException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.RowNotInTableException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000648 RID: 1608 RVA: 0x0001F304 File Offset: 0x0001D504
		protected RowNotInTableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
