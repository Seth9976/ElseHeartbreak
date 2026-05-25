using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000050 RID: 80
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000AE1B File Offset: 0x0000901B
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000AE28 File Offset: 0x00009028
		public XDocumentWrapper(XDocument document)
			: base(document)
		{
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AE34 File Offset: 0x00009034
		public override IList<IXmlNode> ChildNodes
		{
			get
			{
				IList<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null)
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AE6D File Offset: 0x0000906D
		public IXmlNode CreateComment(string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000AE7A File Offset: 0x0000907A
		public IXmlNode CreateTextNode(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000AE87 File Offset: 0x00009087
		public IXmlNode CreateCDataSection(string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000AE94 File Offset: 0x00009094
		public IXmlNode CreateWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000AEA1 File Offset: 0x000090A1
		public IXmlNode CreateSignificantWhitespace(string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000AEAE File Offset: 0x000090AE
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000AEBD File Offset: 0x000090BD
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AECB File Offset: 0x000090CB
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public IXmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			string localName = MiscellaneousUtils.GetLocalName(qualifiedName);
			return new XElementWrapper(new XElement(XName.Get(localName, namespaceURI)));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000AF05 File Offset: 0x00009105
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000AF18 File Offset: 0x00009118
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceURI, string value)
		{
			string localName = MiscellaneousUtils.GetLocalName(qualifiedName);
			return new XAttributeWrapper(new XAttribute(XName.Get(localName, namespaceURI), value));
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000AF3E File Offset: 0x0000913E
		public IXmlElement DocumentElement
		{
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AF60 File Offset: 0x00009160
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper._declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
