using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000048 RID: 72
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000AA85 File Offset: 0x00008C85
		public XmlDocumentWrapper(XmlDocument document)
			: base(document)
		{
			this._document = document;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AA95 File Offset: 0x00008C95
		public IXmlNode CreateComment(string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public IXmlNode CreateTextNode(string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000AABB File Offset: 0x00008CBB
		public IXmlNode CreateCDataSection(string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AACE File Offset: 0x00008CCE
		public IXmlNode CreateWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AAE1 File Offset: 0x00008CE1
		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlNodeWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000AB09 File Offset: 0x00008D09
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000AB1D File Offset: 0x00008D1D
		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AB30 File Offset: 0x00008D30
		public IXmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceURI));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000AB44 File Offset: 0x00008D44
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceURI, string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceURI))
			{
				Value = value
			};
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000AB94 File Offset: 0x00008D94
		public IXmlElement DocumentElement
		{
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		// Token: 0x040000F1 RID: 241
		private XmlDocument _document;
	}
}
