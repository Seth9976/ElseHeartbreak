using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to return a version of a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class.</summary>
		// Token: 0x0600068D RID: 1677 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public VersionNotFoundException()
			: base(Locale.GetText("This DataRow has been deleted"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600068E RID: 1678 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public VersionNotFoundException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600068F RID: 1679 RVA: 0x0001FFAC File Offset: 0x0001E1AC
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000690 RID: 1680 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public VersionNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
