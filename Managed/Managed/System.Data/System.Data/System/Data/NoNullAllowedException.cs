using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to false.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class.</summary>
		// Token: 0x06000635 RID: 1589 RVA: 0x0001F204 File Offset: 0x0001D404
		public NoNullAllowedException()
			: base(Locale.GetText("Cannot insert a NULL value"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000636 RID: 1590 RVA: 0x0001F218 File Offset: 0x0001D418
		public NoNullAllowedException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000637 RID: 1591 RVA: 0x0001F224 File Offset: 0x0001D424
		public NoNullAllowedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000638 RID: 1592 RVA: 0x0001F230 File Offset: 0x0001D430
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
