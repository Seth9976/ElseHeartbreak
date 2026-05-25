using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000046 RID: 70
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A903 File Offset: 0x00008B03
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A90B File Offset: 0x00008B0B
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A918 File Offset: 0x00008B18
		public string Name
		{
			get
			{
				return this._node.Name;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A925 File Offset: 0x00008B25
		public string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A93B File Offset: 0x00008B3B
		public IList<IXmlNode> ChildNodes
		{
			get
			{
				return (from XmlNode n in this._node.ChildNodes
					select this.WrapNode(n)).ToList<IXmlNode>();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A964 File Offset: 0x00008B64
		private IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A9AA File Offset: 0x00008BAA
		public IList<IXmlNode> Attributes
		{
			get
			{
				if (this._node.Attributes == null)
				{
					return null;
				}
				return (from XmlAttribute a in this._node.Attributes
					select this.WrapNode(a)).ToList<IXmlNode>();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public IXmlNode ParentNode
		{
			get
			{
				XmlNode xmlNode = ((this._node is XmlAttribute) ? ((XmlAttribute)this._node).OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return this.WrapNode(xmlNode);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000AA28 File Offset: 0x00008C28
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000AA35 File Offset: 0x00008C35
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000AA44 File Offset: 0x00008C44
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			return newChild;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000AA6B File Offset: 0x00008C6B
		public string Prefix
		{
			get
			{
				return this._node.Prefix;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000AA78 File Offset: 0x00008C78
		public string NamespaceURI
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x040000F0 RID: 240
		private readonly XmlNode _node;
	}
}
