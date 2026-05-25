using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</summary>
	// Token: 0x0200010F RID: 271
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000F08 RID: 3848 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		public SqlNullValueException()
			: base(Locale.GetText("Data is Null. This method or property cannot be called on Null values."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000F09 RID: 3849 RVA: 0x0003BFB4 File Offset: 0x0003A1B4
		public SqlNullValueException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000F0A RID: 3850 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
		public SqlNullValueException(string message, Exception e)
			: base(message, e)
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003BFCC File Offset: 0x0003A1CC
		private SqlNullValueException(SerializationInfo si, StreamingContext sc)
			: base(si.GetString("SqlNullValueExceptionMessage"))
		{
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("SqlNullValueExceptionMessage", this.Message, typeof(string));
		}
	}
}
