using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004D RID: 77
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000AC54 File Offset: 0x00008E54
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000AC63 File Offset: 0x00008E63
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000AC6B File Offset: 0x00008E6B
		public virtual XmlNodeType NodeType
		{
			get
			{
				return this._xmlObject.NodeType;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000AC78 File Offset: 0x00008E78
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000AC7B File Offset: 0x00008E7B
		public virtual IList<IXmlNode> ChildNodes
		{
			get
			{
				return new List<IXmlNode>();
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000AC82 File Offset: 0x00008E82
		public virtual IList<IXmlNode> Attributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000AC85 File Offset: 0x00008E85
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000AC88 File Offset: 0x00008E88
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000AC8B File Offset: 0x00008E8B
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000AC92 File Offset: 0x00008E92
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AC99 File Offset: 0x00008E99
		public virtual string NamespaceURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000F4 RID: 244
		private readonly XObject _xmlObject;
	}
}
