using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000116 RID: 278
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		// Token: 0x06000F98 RID: 3992 RVA: 0x0003D2DC File Offset: 0x0003B4DC
		public SqlNotFilledException()
			: base(Locale.GetText("A SqlNotFilled exception has occured."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x06000F99 RID: 3993 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		public SqlNotFilledException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x06000F9A RID: 3994 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		public SqlNotFilledException(string message, Exception e)
			: base(message, e)
		{
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003D308 File Offset: 0x0003B508
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("SqlNotFilledExceptionMessage", this.Message, typeof(string));
		}
	}
}
