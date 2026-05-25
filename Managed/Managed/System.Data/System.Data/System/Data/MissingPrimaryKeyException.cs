using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to access a row in a table that has no primary key.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class MissingPrimaryKeyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class.</summary>
		// Token: 0x06000622 RID: 1570 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		public MissingPrimaryKeyException()
			: base(Locale.GetText("This table has no primary key"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000623 RID: 1571 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		public MissingPrimaryKeyException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000624 RID: 1572 RVA: 0x0001F008 File Offset: 0x0001D208
		public MissingPrimaryKeyException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">A description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000625 RID: 1573 RVA: 0x0001F014 File Offset: 0x0001D214
		protected MissingPrimaryKeyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
