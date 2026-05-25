using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown by a strongly typed <see cref="T:System.Data.DataSet" /> when the user accesses a DBNull value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class StrongTypingException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class.</summary>
		// Token: 0x0600064E RID: 1614 RVA: 0x0001F350 File Offset: 0x0001D550
		public StrongTypingException()
			: base(Locale.GetText("Trying to access a DBNull value in a strongly-typed DataSet"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string.</summary>
		/// <param name="message">The string to display when the exception is thrown. </param>
		// Token: 0x0600064F RID: 1615 RVA: 0x0001F364 File Offset: 0x0001D564
		public StrongTypingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x06000650 RID: 1616 RVA: 0x0001F370 File Offset: 0x0001D570
		public StrongTypingException(string s, Exception innerException)
			: base(s, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000651 RID: 1617 RVA: 0x0001F37C File Offset: 0x0001D57C
		protected StrongTypingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
