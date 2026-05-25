using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you call the <see cref="M:System.Data.DataRow.EndEdit" /> method within the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class.</summary>
		// Token: 0x060005E4 RID: 1508 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		public InRowChangingEventException()
			: base(Locale.GetText("Cannot EndEdit within a RowChanging event"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public InRowChangingEventException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		public InRowChangingEventException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060005E7 RID: 1511 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
