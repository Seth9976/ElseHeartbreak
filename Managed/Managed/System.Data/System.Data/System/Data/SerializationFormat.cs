using System;

namespace System.Data
{
	/// <summary>Determines the serialization format for a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000070 RID: 112
	public enum SerializationFormat
	{
		/// <summary>Serialize as XML content. The default.</summary>
		// Token: 0x04000209 RID: 521
		Xml,
		/// <summary>Serialize as binary content. Available in ADO.NET 2.0 only.</summary>
		// Token: 0x0400020A RID: 522
		Binary
	}
}
