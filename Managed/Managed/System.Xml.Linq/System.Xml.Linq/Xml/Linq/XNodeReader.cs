using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x0200001D RID: 29
	internal class XNodeReader : XmlReader
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000728C File Offset: 0x0000548C
		public XNodeReader(XNode node)
		{
			this.node = node;
			this.start = node;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000072C0 File Offset: 0x000054C0
		public override int AttributeCount
		{
			get
			{
				if (this.state != ReadState.Interactive || this.end_element)
				{
					return 0;
				}
				int num = 0;
				XmlNodeType nodeType = this.node.NodeType;
				if (nodeType == XmlNodeType.Document)
				{
					XDeclaration declaration = ((XDocument)this.node).Declaration;
					return ((declaration.Version == null) ? 0 : 1) + ((declaration.Encoding == null) ? 0 : 1) + ((declaration.Standalone == null) ? 0 : 1);
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					XDocumentType xdocumentType = (XDocumentType)this.node;
					return ((xdocumentType.PublicId == null) ? 0 : 1) + ((xdocumentType.SystemId == null) ? 0 : 1) + ((xdocumentType.InternalSubset == null) ? 0 : 1);
				}
				if (nodeType != XmlNodeType.Element)
				{
					return 0;
				}
				XElement xelement = (XElement)this.node;
				for (XAttribute xattribute = xelement.FirstAttribute; xattribute != null; xattribute = xattribute.NextAttribute)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000073D8 File Offset: 0x000055D8
		public override string BaseURI
		{
			get
			{
				return this.node.BaseUri ?? string.Empty;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000073F4 File Offset: 0x000055F4
		public override int Depth
		{
			get
			{
				if (this.EOF)
				{
					return 0;
				}
				int num = 0;
				for (XNode xnode = this.node.Parent; xnode != null; xnode = xnode.Parent)
				{
					num++;
				}
				if (this.attr >= 0)
				{
					num++;
				}
				if (this.attr_value)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007454 File Offset: 0x00005654
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile || this.state == ReadState.Error;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007470 File Offset: 0x00005670
		public override bool HasAttributes
		{
			get
			{
				if (this.EOF || this.end_element || this.node == null)
				{
					return false;
				}
				if (this.node is XElement)
				{
					return ((XElement)this.node).HasAttributes;
				}
				return this.AttributeCount > 0;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000074CC File Offset: 0x000056CC
		public override bool HasValue
		{
			get
			{
				if (this.EOF)
				{
					return false;
				}
				if (this.attr >= 0)
				{
					return true;
				}
				XmlNodeType nodeType = this.node.NodeType;
				return nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Document && nodeType != XmlNodeType.EndElement;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007520 File Offset: 0x00005720
		public override bool IsEmptyElement
		{
			get
			{
				return !this.EOF && this.attr < 0 && this.node is XElement && ((XElement)this.node).IsEmpty;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007560 File Offset: 0x00005760
		private XAttribute GetCurrentAttribute()
		{
			return this.GetXAttribute(this.attr);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007570 File Offset: 0x00005770
		private XAttribute GetXAttribute(int idx)
		{
			if (this.EOF)
			{
				return null;
			}
			XElement xelement = this.node as XElement;
			if (xelement == null)
			{
				return null;
			}
			int num = 0;
			foreach (XAttribute xattribute in xelement.Attributes())
			{
				if (num++ == idx)
				{
					return xattribute;
				}
			}
			return null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007608 File Offset: 0x00005808
		private object GetCurrentName()
		{
			if (this.EOF || this.attr_value)
			{
				return null;
			}
			return this.GetName(this.attr);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000763C File Offset: 0x0000583C
		private object GetName(int attr)
		{
			if (attr >= 0)
			{
				XmlNodeType xmlNodeType = this.node.NodeType;
				if (xmlNodeType != XmlNodeType.Document)
				{
					if (xmlNodeType != XmlNodeType.DocumentType)
					{
						if (xmlNodeType == XmlNodeType.Element)
						{
							XAttribute xattribute = this.GetXAttribute(attr);
							return xattribute.Name;
						}
					}
					else
					{
						if (attr == 0)
						{
							return (((XDocumentType)this.node).PublicId == null) ? "SYSTEM" : "PUBLIC";
						}
						return "SYSTEM";
					}
				}
				else
				{
					XDeclaration declaration = ((XDocument)this.node).Declaration;
					if (attr == 0)
					{
						return (declaration.Version == null) ? ((declaration.Encoding == null) ? "standalone" : "encoding") : "version";
					}
					if (attr != 1)
					{
						return "standalone";
					}
					return (declaration.Version == null) ? "standalone" : ((declaration.Encoding == null) ? "standalone" : "encoding");
				}
			}
			else
			{
				XmlNodeType xmlNodeType = this.node.NodeType;
				switch (xmlNodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					return ((XProcessingInstruction)this.node).Target;
				default:
					if (xmlNodeType == XmlNodeType.Element)
					{
						return ((XElement)this.node).Name;
					}
					break;
				case XmlNodeType.Document:
					return "xml";
				case XmlNodeType.DocumentType:
					return ((XDocumentType)this.node).Name;
				}
			}
			return null;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000077B4 File Offset: 0x000059B4
		public override string LocalName
		{
			get
			{
				object currentName = this.GetCurrentName();
				if (currentName == null)
				{
					return string.Empty;
				}
				if (currentName is string)
				{
					return (string)currentName;
				}
				return ((XName)currentName).LocalName;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000077F4 File Offset: 0x000059F4
		public override string NamespaceURI
		{
			get
			{
				XName xname = this.GetCurrentName() as XName;
				if (xname != null)
				{
					return (!(xname.LocalName == "xmlns") || !(xname.Namespace == XNamespace.None)) ? xname.NamespaceName : XNamespace.Xmlns.NamespaceName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007860 File Offset: 0x00005A60
		public override XmlNameTable NameTable
		{
			get
			{
				return this.name_table;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007868 File Offset: 0x00005A68
		public override XmlNodeType NodeType
		{
			get
			{
				return (this.state == ReadState.Interactive) ? ((!this.end_element) ? ((!this.attr_value) ? ((this.attr < 0) ? ((this.node.NodeType != XmlNodeType.Document) ? this.node.NodeType : XmlNodeType.XmlDeclaration) : XmlNodeType.Attribute) : XmlNodeType.Text) : XmlNodeType.EndElement) : XmlNodeType.None;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000078E0 File Offset: 0x00005AE0
		public override string Prefix
		{
			get
			{
				XName xname = this.GetCurrentName() as XName;
				if (xname == null || xname.Namespace == XNamespace.None)
				{
					return string.Empty;
				}
				XElement xelement = (this.node as XElement) ?? this.node.Parent;
				if (xelement == null)
				{
					return string.Empty;
				}
				return xelement.GetPrefixOfNamespace(xname.Namespace) ?? string.Empty;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007964 File Offset: 0x00005B64
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000796C File Offset: 0x00005B6C
		public override string Value
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				XAttribute currentAttribute = this.GetCurrentAttribute();
				if (currentAttribute != null)
				{
					return currentAttribute.Value;
				}
				switch (this.node.NodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					return ((XText)this.node).Value;
				case XmlNodeType.ProcessingInstruction:
					return ((XProcessingInstruction)this.node).Data;
				case XmlNodeType.Comment:
					return ((XComment)this.node).Value;
				case XmlNodeType.Document:
				{
					XDeclaration declaration = ((XDocument)this.node).Declaration;
					if (this.attr >= 0)
					{
						string text = this.LocalName;
						if (text != null)
						{
							if (XNodeReader.<>f__switch$map0 == null)
							{
								XNodeReader.<>f__switch$map0 = new Dictionary<string, int>(2)
								{
									{ "version", 0 },
									{ "encoding", 1 }
								};
							}
							int num;
							if (XNodeReader.<>f__switch$map0.TryGetValue(text, out num))
							{
								if (num == 0)
								{
									return declaration.Version;
								}
								if (num == 1)
								{
									return declaration.Encoding;
								}
							}
						}
						return declaration.Standalone;
					}
					string text2 = declaration.ToString();
					return text2.Substring(6, text2.Length - 6 - 2);
				}
				case XmlNodeType.DocumentType:
				{
					XDocumentType xdocumentType = (XDocumentType)this.node;
					string text = this.LocalName;
					if (text != null)
					{
						if (XNodeReader.<>f__switch$map1 == null)
						{
							XNodeReader.<>f__switch$map1 = new Dictionary<string, int>(2)
							{
								{ "PUBLIC", 0 },
								{ "SYSTEM", 1 }
							};
						}
						int num;
						if (XNodeReader.<>f__switch$map1.TryGetValue(text, out num))
						{
							if (num == 0)
							{
								return xdocumentType.PublicId;
							}
							if (num == 1)
							{
								return xdocumentType.SystemId;
							}
						}
					}
					return xdocumentType.InternalSubset;
				}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007B50 File Offset: 0x00005D50
		public override void Close()
		{
			this.state = ReadState.Closed;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007B5C File Offset: 0x00005D5C
		public override string LookupNamespace(string prefix)
		{
			if (this.EOF)
			{
				return null;
			}
			XElement xelement = (this.node as XElement) ?? this.node.Parent;
			if (xelement == null)
			{
				return null;
			}
			XNamespace namespaceOfPrefix = xelement.GetNamespaceOfPrefix(prefix);
			return (!(namespaceOfPrefix != XNamespace.None)) ? null : namespaceOfPrefix.NamespaceName;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public override bool MoveToElement()
		{
			if (this.attr >= 0)
			{
				this.attr_value = false;
				this.attr = -1;
				return true;
			}
			return false;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public override bool MoveToFirstAttribute()
		{
			if (this.AttributeCount > 0)
			{
				this.attr = 0;
				this.attr_value = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007C00 File Offset: 0x00005E00
		public override bool MoveToNextAttribute()
		{
			int attributeCount = this.AttributeCount;
			if (this.attr + 1 < attributeCount)
			{
				this.attr++;
				this.attr_value = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007C3C File Offset: 0x00005E3C
		public override bool MoveToAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int attributeCount = this.AttributeCount;
			bool flag = false;
			for (int i = 0; i < attributeCount; i++)
			{
				object name2 = this.GetName(i);
				if (name2 != null)
				{
					if (name2 as string == name)
					{
						flag = true;
					}
					XName xname = (XName)name2;
					if (name.EndsWith(xname.LocalName, StringComparison.Ordinal) && name == this.GetPrefixedName((XName)name2))
					{
						flag = true;
					}
					if (flag)
					{
						this.attr = i;
						this.attr_value = false;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007CE8 File Offset: 0x00005EE8
		private string GetPrefixedName(XName name)
		{
			XElement xelement = (this.node as XElement) ?? this.node.Parent;
			if (xelement == null || name.Namespace == XNamespace.None || xelement.GetPrefixOfNamespace(name.Namespace) == string.Empty)
			{
				return name.LocalName;
			}
			return xelement.GetPrefixOfNamespace(name.Namespace) + ":" + name.LocalName;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override bool MoveToAttribute(string local, string ns)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			int attributeCount = this.AttributeCount;
			bool flag = false;
			for (int i = 0; i < attributeCount; i++)
			{
				object name = this.GetName(i);
				if (name != null)
				{
					if (name as string == local && ns.Length == 0)
					{
						flag = true;
					}
					XName xname = (XName)name;
					if (local == xname.LocalName && ns == xname.NamespaceName)
					{
						flag = true;
					}
					if (flag)
					{
						this.attr = i;
						this.attr_value = false;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007E2C File Offset: 0x0000602C
		public override string GetAttribute(int i)
		{
			int num = this.attr;
			bool flag = this.attr_value;
			string value;
			try
			{
				this.MoveToElement();
				this.MoveToAttribute(i);
				value = this.Value;
			}
			finally
			{
				this.attr = num;
				this.attr_value = flag;
			}
			return value;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007E94 File Offset: 0x00006094
		public override string GetAttribute(string name)
		{
			int num = this.attr;
			bool flag = this.attr_value;
			string text;
			try
			{
				this.MoveToElement();
				text = ((!this.MoveToAttribute(name)) ? null : this.Value);
			}
			finally
			{
				this.attr = num;
				this.attr_value = flag;
			}
			return text;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007F08 File Offset: 0x00006108
		public override string GetAttribute(string local, string ns)
		{
			int num = this.attr;
			bool flag = this.attr_value;
			string text;
			try
			{
				this.MoveToElement();
				text = ((!this.MoveToAttribute(local, ns)) ? null : this.Value);
			}
			finally
			{
				this.attr = num;
				this.attr_value = flag;
			}
			return text;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007F7C File Offset: 0x0000617C
		public override bool Read()
		{
			this.attr = -1;
			this.attr_value = false;
			ReadState readState = this.state;
			if (readState != ReadState.Initial)
			{
				if (readState != ReadState.Interactive)
				{
					return false;
				}
			}
			else
			{
				this.state = ReadState.Interactive;
				XDocument xdocument = this.node as XDocument;
				if (xdocument == null)
				{
					return true;
				}
				if (xdocument.Declaration != null)
				{
					return true;
				}
			}
			if (this.node is XDocument)
			{
				XDocument xdocument2 = this.node as XDocument;
				this.node = xdocument2.FirstNode;
				if (this.node == null)
				{
					this.state = ReadState.EndOfFile;
					return false;
				}
				this.node = xdocument2.FirstNode;
				return true;
			}
			else
			{
				XElement xelement = this.node as XElement;
				if (xelement != null && !this.end_element)
				{
					if (xelement.FirstNode != null)
					{
						this.node = xelement.FirstNode;
						return true;
					}
					if (!xelement.IsEmpty)
					{
						this.end_element = true;
						return true;
					}
				}
				this.end_element = false;
				if (this.node.NextNode != null && this.node != this.start)
				{
					this.node = this.node.NextNode;
					return true;
				}
				if (this.node.Parent == null || this.node == this.start)
				{
					this.state = ReadState.EndOfFile;
					return false;
				}
				this.node = this.node.Parent;
				this.end_element = true;
				return true;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000080FC File Offset: 0x000062FC
		public override bool ReadAttributeValue()
		{
			if (this.attr < 0 || this.attr_value)
			{
				return false;
			}
			this.attr_value = true;
			return true;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008120 File Offset: 0x00006320
		public override void ResolveEntity()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000057 RID: 87
		private ReadState state;

		// Token: 0x04000058 RID: 88
		private XNode node;

		// Token: 0x04000059 RID: 89
		private XNode start;

		// Token: 0x0400005A RID: 90
		private int attr = -1;

		// Token: 0x0400005B RID: 91
		private bool attr_value;

		// Token: 0x0400005C RID: 92
		private bool end_element;

		// Token: 0x0400005D RID: 93
		private NameTable name_table = new NameTable();
	}
}
