using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies load options when parsing XML. </summary>
	// Token: 0x0200000B RID: 11
	[Flags]
	public enum LoadOptions
	{
		/// <summary>Does not preserve insignificant white space or load base URI and line information.</summary>
		// Token: 0x0400001F RID: 31
		None = 0,
		/// <summary>Preserves insignificant white space while parsing.</summary>
		// Token: 0x04000020 RID: 32
		PreserveWhitespace = 1,
		/// <summary>Requests the base URI information from the <see cref="T:System.Xml.XmlReader" />, and makes it available via the <see cref="P:System.Xml.Linq.XObject.BaseUri" /> property.</summary>
		// Token: 0x04000021 RID: 33
		SetBaseUri = 2,
		/// <summary>Requests the line information from the <see cref="T:System.Xml.XmlReader" /> and makes it available via properties on <see cref="T:System.Xml.Linq.XObject" />.</summary>
		// Token: 0x04000022 RID: 34
		SetLineInfo = 4
	}
}
