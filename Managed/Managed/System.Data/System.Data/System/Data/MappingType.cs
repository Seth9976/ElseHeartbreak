using System;

namespace System.Data
{
	/// <summary>Specifies how a <see cref="T:System.Data.DataColumn" /> is mapped.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005F RID: 95
	[Serializable]
	public enum MappingType
	{
		/// <summary>The column is mapped to an XML element.</summary>
		// Token: 0x040001DF RID: 479
		Element = 1,
		/// <summary>The column is mapped to an XML attribute.</summary>
		// Token: 0x040001E0 RID: 480
		Attribute,
		/// <summary>The column is mapped to an <see cref="T:System.Xml.XmlText" /> node.</summary>
		// Token: 0x040001E1 RID: 481
		SimpleContent,
		/// <summary>The column is mapped to an internal structure.</summary>
		// Token: 0x040001E2 RID: 482
		Hidden
	}
}
