using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004A RID: 74
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000ABB5 File Offset: 0x00008DB5
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000ABF3 File Offset: 0x00008DF3
		public string GetPrefixOfNamespace(string namespaceURI)
		{
			return this._element.GetPrefixOfNamespace(namespaceURI);
		}

		// Token: 0x040000F2 RID: 242
		private XmlElement _element;
	}
}
