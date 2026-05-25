using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when errors are generated using ADO.NET components.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class DataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class. This is the default constructor.</summary>
		// Token: 0x06000194 RID: 404 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public DataException()
			: base(Locale.GetText("A Data exception has occurred"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000195 RID: 405 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		public DataException(string s)
			: base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000196 RID: 406 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		protected DataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		/// <param name="innerException">A reference to an inner exception. </param>
		// Token: 0x06000197 RID: 407 RVA: 0x0000C008 File Offset: 0x0000A208
		public DataException(string s, Exception innerException)
			: base(s, innerException)
		{
		}
	}
}
