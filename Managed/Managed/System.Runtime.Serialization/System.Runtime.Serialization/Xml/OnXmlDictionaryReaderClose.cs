using System;

namespace System.Xml
{
	/// <summary>delegate for a callback method when closing the reader.</summary>
	/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> that fires the OnClose event.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000059 RID: 89
	// (Invoke) Token: 0x060003F3 RID: 1011
	public delegate void OnXmlDictionaryReaderClose(XmlDictionaryReader reader);
}
