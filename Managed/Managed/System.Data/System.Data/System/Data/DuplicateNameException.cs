using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when a duplicate database object name is encountered during an add operation in a <see cref="T:System.Data.DataSet" /> -related object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class DuplicateNameException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class.</summary>
		// Token: 0x06000540 RID: 1344 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public DuplicateNameException()
			: base(Locale.GetText("There is a database object with the same name"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000541 RID: 1345 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		public DuplicateNameException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000542 RID: 1346 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public DuplicateNameException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000543 RID: 1347 RVA: 0x0001D808 File Offset: 0x0001BA08
		protected DuplicateNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
