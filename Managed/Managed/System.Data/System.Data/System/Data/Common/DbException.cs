using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	/// <summary>The base class for all exceptions thrown on behalf of the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C5 RID: 197
	[Serializable]
	public abstract class DbException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class.</summary>
		// Token: 0x060009A5 RID: 2469 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		protected DbException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message.</summary>
		/// <param name="message">The message to display for this exception.</param>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		protected DbException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message string.</param>
		/// <param name="innerException">The inner exception reference.</param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002E7F8 File Offset: 0x0002C9F8
		protected DbException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060009A8 RID: 2472 RVA: 0x0002E804 File Offset: 0x0002CA04
		protected DbException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbException" /> class with the specified error message and error code.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="errorCode">The error code for the exception.</param>
		// Token: 0x060009A9 RID: 2473 RVA: 0x0002E810 File Offset: 0x0002CA10
		protected DbException(string message, int errorcode)
			: base(message, errorcode)
		{
		}
	}
}
