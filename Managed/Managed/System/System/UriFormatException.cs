using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an invalid Uniform Resource Identifier (URI) is detected.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004B5 RID: 1205
	[Serializable]
	public class UriFormatException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class.</summary>
		// Token: 0x06002B8D RID: 11149 RVA: 0x00097DE4 File Offset: 0x00095FE4
		public UriFormatException()
			: base(global::Locale.GetText("Invalid URI format"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class with the specified message.</summary>
		/// <param name="textString">The error message string. </param>
		// Token: 0x06002B8E RID: 11150 RVA: 0x00097DF8 File Offset: 0x00095FF8
		public UriFormatException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information that is required to serialize the new <see cref="T:System.UriFormatException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.UriFormatException" />. </param>
		// Token: 0x06002B8F RID: 11151 RVA: 0x00097E04 File Offset: 0x00096004
		protected UriFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.UriFormatException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.UriFormatException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.UriFormatException" />. </param>
		// Token: 0x06002B90 RID: 11152 RVA: 0x00097E10 File Offset: 0x00096010
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
