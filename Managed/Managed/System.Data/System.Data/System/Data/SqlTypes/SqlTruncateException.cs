using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when you set a value into a <see cref="N:System.Data.SqlTypes" /> structure would truncate that value.</summary>
	// Token: 0x02000112 RID: 274
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class.</summary>
		// Token: 0x06000F8A RID: 3978 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		public SqlTruncateException()
			: base(Locale.GetText("This value is being truncated"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000F8B RID: 3979 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		public SqlTruncateException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message and a reference to the <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">A reference to an inner <see cref="T:System.Exception" />. </param>
		// Token: 0x06000F8C RID: 3980 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public SqlTruncateException(string message, Exception e)
			: base(message, e)
		{
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		private SqlTruncateException(SerializationInfo si, StreamingContext sc)
			: base(si.GetString("SqlTruncateExceptionMessage"))
		{
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003D210 File Offset: 0x0003B410
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("SqlTruncateExceptionMessage", this.Message, typeof(string));
		}
	}
}
