using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000055 RID: 85
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B0E9 File Offset: 0x000092E9
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B0F6 File Offset: 0x000092F6
		public XElementWrapper(XElement element)
			: base(element)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B100 File Offset: 0x00009300
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B12D File Offset: 0x0000932D
		public override IList<IXmlNode> Attributes
		{
			get
			{
				return (from a in this.Element.Attributes()
					select new XAttributeWrapper(a)).Cast<IXmlNode>().ToList<IXmlNode>();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B166 File Offset: 0x00009366
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000B173 File Offset: 0x00009373
		public override string Value
		{
			get
			{
				return this.Element.Value;
			}
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B181 File Offset: 0x00009381
		public override string LocalName
		{
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B193 File Offset: 0x00009393
		public override string NamespaceURI
		{
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B1A5 File Offset: 0x000093A5
		public string GetPrefixOfNamespace(string namespaceURI)
		{
			return this.Element.GetPrefixOfNamespace(namespaceURI);
		}
	}
}
