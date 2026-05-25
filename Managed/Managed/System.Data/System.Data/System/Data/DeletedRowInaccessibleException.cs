using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when an action is tried on a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class DeletedRowInaccessibleException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class.</summary>
		// Token: 0x0600053C RID: 1340 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		public DeletedRowInaccessibleException()
			: base(Locale.GetText("This DataRow has been deleted"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600053D RID: 1341 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public DeletedRowInaccessibleException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x0600053E RID: 1342 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		public DeletedRowInaccessibleException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DeletedRowInaccessibleException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600053F RID: 1343 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		protected DeletedRowInaccessibleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
