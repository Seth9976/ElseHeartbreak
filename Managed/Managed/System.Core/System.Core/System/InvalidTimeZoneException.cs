using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when time zone information is invalid.</summary>
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a system-supplied message.</summary>
		// Token: 0x060000FB RID: 251 RVA: 0x000067E4 File Offset: 0x000049E4
		public InvalidTimeZoneException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x060000FC RID: 252 RVA: 0x000067EC File Offset: 0x000049EC
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.    </param>
		/// <param name="innerException">The exception that is the cause of the current exception.  </param>
		// Token: 0x060000FD RID: 253 RVA: 0x000067F8 File Offset: 0x000049F8
		public InvalidTimeZoneException(string message, Exception e)
			: base(message, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidTimeZoneException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data. </param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.-or-The <paramref name="context" /> parameter is null.</exception>
		// Token: 0x060000FE RID: 254 RVA: 0x00006804 File Offset: 0x00004A04
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext sc)
			: base(info, sc)
		{
		}
	}
}
