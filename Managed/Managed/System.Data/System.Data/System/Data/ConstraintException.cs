using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when attempting an action that violates a constraint.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class ConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class. This is the default constructor.</summary>
		// Token: 0x0600006F RID: 111 RVA: 0x0000434C File Offset: 0x0000254C
		public ConstraintException()
			: base(Locale.GetText("This operation violates a constraint"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000070 RID: 112 RVA: 0x00004360 File Offset: 0x00002560
		public ConstraintException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified string and inner exception.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		/// <param name="innerException">Gets the Exception instance that caused the current exception.</param>
		// Token: 0x06000071 RID: 113 RVA: 0x0000436C File Offset: 0x0000256C
		public ConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified serialization and stream context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000072 RID: 114 RVA: 0x00004378 File Offset: 0x00002578
		protected ConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
