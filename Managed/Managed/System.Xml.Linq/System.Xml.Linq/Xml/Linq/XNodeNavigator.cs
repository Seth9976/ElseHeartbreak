using System;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Linq
{
	// Token: 0x0200001C RID: 28
	internal class XNodeNavigator : XPathNavigator
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00006804 File Offset: 0x00004A04
		public XNodeNavigator(XNode node, XmlNameTable nameTable)
		{
			this.node = node;
			this.name_table = nameTable;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000681C File Offset: 0x00004A1C
		public XNodeNavigator(XNodeNavigator other)
		{
			this.node = other.node;
			this.attr = other.attr;
			this.name_table = other.name_table;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006884 File Offset: 0x00004A84
		public override string BaseURI
		{
			get
			{
				return this.node.BaseUri ?? string.Empty;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000068A0 File Offset: 0x00004AA0
		public override bool CanEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000068A4 File Offset: 0x00004AA4
		public override bool HasAttributes
		{
			get
			{
				XElement xelement = this.node as XElement;
				return xelement != null && xelement.HasAttributes;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000068CC File Offset: 0x00004ACC
		public override bool HasChildren
		{
			get
			{
				XContainer xcontainer = this.node as XContainer;
				return xcontainer != null && xcontainer.FirstNode != null;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000068FC File Offset: 0x00004AFC
		public override bool IsEmptyElement
		{
			get
			{
				XElement xelement = this.node as XElement;
				return xelement != null && xelement.IsEmpty;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006924 File Offset: 0x00004B24
		public override string LocalName
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Element:
					return ((XElement)this.node).Name.LocalName;
				case XPathNodeType.Attribute:
					return this.attr.Name.LocalName;
				case XPathNodeType.Namespace:
					return (!(this.attr.Name.Namespace == XNamespace.None)) ? this.attr.Name.LocalName : string.Empty;
				case XPathNodeType.ProcessingInstruction:
					return ((XProcessingInstruction)this.node).Target;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000069D8 File Offset: 0x00004BD8
		public override string Name
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				XName xname;
				if (nodeType != XPathNodeType.Element)
				{
					if (nodeType != XPathNodeType.Attribute)
					{
						return this.LocalName;
					}
					xname = this.attr.Name;
				}
				else
				{
					xname = ((XElement)this.node).Name;
				}
				if (xname.Namespace == XNamespace.None)
				{
					return xname.LocalName;
				}
				XElement xelement = (this.node as XElement) ?? this.node.Parent;
				if (xelement == null)
				{
					return xname.LocalName;
				}
				string prefixOfNamespace = xelement.GetPrefixOfNamespace(xname.Namespace);
				return (prefixOfNamespace.Length <= 0) ? xname.LocalName : (prefixOfNamespace + ":" + xname.LocalName);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006AAC File Offset: 0x00004CAC
		public override string NamespaceURI
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Element:
					return ((XElement)this.node).Name.NamespaceName;
				case XPathNodeType.Attribute:
					return this.attr.Name.NamespaceName;
				case XPathNodeType.Namespace:
					return this.attr.Value;
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006B14 File Offset: 0x00004D14
		public override XmlNameTable NameTable
		{
			get
			{
				return this.name_table;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006B1C File Offset: 0x00004D1C
		public override XPathNodeType NodeType
		{
			get
			{
				if (this.attr != null)
				{
					return (!this.attr.IsNamespaceDeclaration) ? XPathNodeType.Attribute : XPathNodeType.Namespace;
				}
				XmlNodeType nodeType = this.node.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					return XPathNodeType.ProcessingInstruction;
				case XmlNodeType.Comment:
					return XPathNodeType.Comment;
				case XmlNodeType.Document:
					return XPathNodeType.Root;
				default:
					if (nodeType != XmlNodeType.Element)
					{
						return XPathNodeType.Text;
					}
					return XPathNodeType.Element;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006B84 File Offset: 0x00004D84
		public override string Prefix
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				XName xname;
				if (nodeType != XPathNodeType.Element)
				{
					if (nodeType != XPathNodeType.Attribute)
					{
						return this.LocalName;
					}
					xname = this.attr.Name;
				}
				else
				{
					xname = ((XElement)this.node).Name;
				}
				if (xname.Namespace == XNamespace.None)
				{
					return string.Empty;
				}
				XElement xelement = (this.node as XElement) ?? this.node.Parent;
				if (xelement == null)
				{
					return string.Empty;
				}
				return xelement.GetPrefixOfNamespace(xname.Namespace);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006C2C File Offset: 0x00004E2C
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006C30 File Offset: 0x00004E30
		public override object UnderlyingObject
		{
			get
			{
				return (this.attr == null) ? this.node : this.attr;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006C50 File Offset: 0x00004E50
		public override string Value
		{
			get
			{
				if (this.attr != null)
				{
					return this.attr.Value;
				}
				switch (this.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
					return this.GetInnerText((XContainer)this.node);
				case XPathNodeType.Text:
					return ((XText)this.node).Value;
				case XPathNodeType.ProcessingInstruction:
					return ((XProcessingInstruction)this.node).Data;
				case XPathNodeType.Comment:
					return ((XComment)this.node).Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006CF4 File Offset: 0x00004EF4
		private string GetInnerText(XContainer node)
		{
			StringBuilder stringBuilder = null;
			foreach (XNode xnode in node.Nodes())
			{
				this.GetInnerText(xnode, ref stringBuilder);
			}
			return (stringBuilder == null) ? string.Empty : stringBuilder.ToString();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006D74 File Offset: 0x00004F74
		private void GetInnerText(XNode n, ref StringBuilder sb)
		{
			switch (n.NodeType)
			{
			case XmlNodeType.Element:
				foreach (XNode xnode in ((XElement)n).Nodes())
				{
					this.GetInnerText(xnode, ref sb);
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				if (sb == null)
				{
					sb = new StringBuilder();
				}
				sb.Append(((XText)n).Value);
				break;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006E2C File Offset: 0x0000502C
		public override XPathNavigator Clone()
		{
			return new XNodeNavigator(this);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006E34 File Offset: 0x00005034
		public override bool IsSamePosition(XPathNavigator other)
		{
			XNodeNavigator xnodeNavigator = other as XNodeNavigator;
			return xnodeNavigator != null && xnodeNavigator.node.Owner == this.node.Owner && this.node == xnodeNavigator.node && this.attr == xnodeNavigator.attr;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006E90 File Offset: 0x00005090
		public override bool MoveTo(XPathNavigator other)
		{
			XNodeNavigator xnodeNavigator = other as XNodeNavigator;
			if (xnodeNavigator == null || xnodeNavigator.node.Owner != this.node.Owner)
			{
				return false;
			}
			this.node = xnodeNavigator.node;
			this.attr = xnodeNavigator.attr;
			return true;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006EE0 File Offset: 0x000050E0
		public override bool MoveToFirstAttribute()
		{
			XElement xelement = this.node as XElement;
			if (xelement == null || !xelement.HasAttributes)
			{
				return false;
			}
			foreach (XAttribute xattribute in xelement.Attributes())
			{
				if (!xattribute.IsNamespaceDeclaration)
				{
					this.attr = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006F78 File Offset: 0x00005178
		public override bool MoveToFirstChild()
		{
			XContainer xcontainer = this.node as XContainer;
			if (xcontainer == null)
			{
				return false;
			}
			this.node = xcontainer.FirstNode;
			this.attr = null;
			return true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006FB0 File Offset: 0x000051B0
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XElement xelement = this.node as XElement;
			while (xelement != null)
			{
				foreach (XAttribute xattribute in xelement.Attributes())
				{
					if (xattribute.IsNamespaceDeclaration)
					{
						this.attr = xattribute;
						return true;
					}
				}
				if (scope == XPathNamespaceScope.Local)
				{
					return false;
				}
				xelement = xelement.Parent;
				continue;
			}
			if (scope != XPathNamespaceScope.All)
			{
				return false;
			}
			this.attr = XNodeNavigator.attr_ns_xml;
			return true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007064 File Offset: 0x00005264
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException("This XPathNavigator does not support IDs");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007070 File Offset: 0x00005270
		public override bool MoveToNext()
		{
			if (this.node.NextNode == null)
			{
				return false;
			}
			this.node = this.node.NextNode;
			this.attr = null;
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000070A0 File Offset: 0x000052A0
		public override bool MoveToNextAttribute()
		{
			if (this.attr == null)
			{
				return false;
			}
			if (this.attr.NextAttribute == null)
			{
				return false;
			}
			for (XAttribute xattribute = this.attr.NextAttribute; xattribute != null; xattribute = xattribute.NextAttribute)
			{
				if (!xattribute.IsNamespaceDeclaration)
				{
					this.attr = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007100 File Offset: 0x00005300
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			if (this.attr == null)
			{
				return false;
			}
			for (XAttribute xattribute = this.attr.NextAttribute; xattribute != null; xattribute = xattribute.NextAttribute)
			{
				if (xattribute.IsNamespaceDeclaration)
				{
					this.attr = xattribute;
					return true;
				}
			}
			if (scope == XPathNamespaceScope.Local)
			{
				return false;
			}
			for (XElement xelement = this.attr.Parent.Parent; xelement != null; xelement = xelement.Parent)
			{
				foreach (XAttribute xattribute2 in xelement.Attributes())
				{
					if (xattribute2.IsNamespaceDeclaration)
					{
						this.attr = xattribute2;
						return true;
					}
				}
			}
			if (scope != XPathNamespaceScope.All)
			{
				return false;
			}
			this.attr = XNodeNavigator.attr_ns_xml;
			return true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000071F8 File Offset: 0x000053F8
		public override bool MoveToParent()
		{
			if (this.attr != null)
			{
				this.attr = null;
				return true;
			}
			if (this.node.Parent == null)
			{
				return false;
			}
			this.node = this.node.Parent;
			return true;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007240 File Offset: 0x00005440
		public override bool MoveToPrevious()
		{
			if (this.node.PreviousNode == null)
			{
				return false;
			}
			this.node = this.node.PreviousNode;
			this.attr = null;
			return true;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007270 File Offset: 0x00005470
		public override void MoveToRoot()
		{
			this.node = this.node.Owner;
			this.attr = null;
		}

		// Token: 0x04000053 RID: 83
		private static readonly XAttribute attr_ns_xml = new XAttribute(XNamespace.Xmlns.GetName("xml"), XNamespace.Xml.NamespaceName);

		// Token: 0x04000054 RID: 84
		private XNode node;

		// Token: 0x04000055 RID: 85
		private XAttribute attr;

		// Token: 0x04000056 RID: 86
		private XmlNameTable name_table;
	}
}
