using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when incorrectly trying to create or access a relation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class.</summary>
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001E45C File Offset: 0x0001C65C
		public InvalidConstraintException()
			: base(Locale.GetText("Cannot access or create this relation"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001E470 File Offset: 0x0001C670
		public InvalidConstraintException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001E47C File Offset: 0x0001C67C
		public InvalidConstraintException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060005F4 RID: 1524 RVA: 0x0001E488 File Offset: 0x0001C688
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
