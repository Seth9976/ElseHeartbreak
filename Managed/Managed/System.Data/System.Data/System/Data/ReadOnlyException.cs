using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to change the value of a read-only column.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class ReadOnlyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class.</summary>
		// Token: 0x0600063F RID: 1599 RVA: 0x0001F288 File Offset: 0x0001D488
		public ReadOnlyException()
			: base(Locale.GetText("Cannot change a value in a read-only column"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000640 RID: 1600 RVA: 0x0001F29C File Offset: 0x0001D49C
		public ReadOnlyException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000641 RID: 1601 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		public ReadOnlyException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000642 RID: 1602 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		protected ReadOnlyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
