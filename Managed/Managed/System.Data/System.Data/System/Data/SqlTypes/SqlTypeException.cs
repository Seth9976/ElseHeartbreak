using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The base exception class for the <see cref="N:System.Data.SqlTypes" />.</summary>
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class SqlTypeException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class.</summary>
		// Token: 0x06000F8F RID: 3983 RVA: 0x0003D230 File Offset: 0x0003B430
		public SqlTypeException()
			: base(Locale.GetText("A sql exception has occured."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000F90 RID: 3984 RVA: 0x0003D244 File Offset: 0x0003B444
		public SqlTypeException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000F91 RID: 3985 RVA: 0x0003D250 File Offset: 0x0003B450
		public SqlTypeException(string message, Exception e)
			: base(message, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with serialized data.</summary>
		/// <param name="si">The object that holds the serialized object data. </param>
		/// <param name="sc">The contextual information about the source or destination. </param>
		// Token: 0x06000F92 RID: 3986 RVA: 0x0003D25C File Offset: 0x0003B45C
		protected SqlTypeException(SerializationInfo si, StreamingContext sc)
			: base(si.GetString("SqlTypeExceptionMessage"))
		{
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003D270 File Offset: 0x0003B470
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("SqlTypeExceptionMessage", this.Message, typeof(string));
		}
	}
}
