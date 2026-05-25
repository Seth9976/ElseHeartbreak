using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		// Token: 0x06000F94 RID: 3988 RVA: 0x0003D290 File Offset: 0x0003B490
		public SqlAlreadyFilledException()
			: base(Locale.GetText("A SqlAlreadyFilled exception has occured."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x06000F95 RID: 3989 RVA: 0x0003D2A4 File Offset: 0x0003B4A4
		public SqlAlreadyFilledException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		public SqlAlreadyFilledException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		private new void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("SqlAlreadyFilledExceptionMessage", this.Message, typeof(string));
		}
	}
}
