using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000057 RID: 87
	internal class XmlSimpleDictionaryReader : XmlDictionaryReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x0600039F RID: 927 RVA: 0x00010990 File Offset: 0x0000EB90
		public XmlSimpleDictionaryReader(XmlReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001099C File Offset: 0x0000EB9C
		public XmlSimpleDictionaryReader(XmlReader reader, XmlDictionary dictionary)
			: this(reader, dictionary, null)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000109A8 File Offset: 0x0000EBA8
		public XmlSimpleDictionaryReader(XmlReader reader, XmlDictionary dictionary, OnXmlDictionaryReaderClose onClose)
		{
			this.reader = reader;
			this.onClose = onClose;
			this.as_line_info = reader as IXmlLineInfo;
			this.as_dict_reader = reader as XmlDictionaryReader;
			if (dictionary == null)
			{
				dictionary = new XmlDictionary();
			}
			this.dict = dictionary;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000109F8 File Offset: 0x0000EBF8
		public int LineNumber
		{
			get
			{
				return (this.as_line_info == null) ? 0 : this.as_line_info.LineNumber;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00010A18 File Offset: 0x0000EC18
		public int LinePosition
		{
			get
			{
				return (this.as_line_info == null) ? 0 : this.as_line_info.LinePosition;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010A38 File Offset: 0x0000EC38
		public bool HasLineInfo()
		{
			return this.as_line_info != null && this.as_line_info.HasLineInfo();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00010A58 File Offset: 0x0000EC58
		public override bool CanCanonicalize
		{
			get
			{
				return this.as_dict_reader != null && this.as_dict_reader.CanCanonicalize;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010A78 File Offset: 0x0000EC78
		public override void EndCanonicalization()
		{
			if (this.as_dict_reader != null)
			{
				this.as_dict_reader.EndCanonicalization();
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public override bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
		{
			localName = null;
			return false;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		public override bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString namespaceUri)
		{
			namespaceUri = null;
			return false;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00010AAC File Offset: 0x0000ECAC
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IXmlNamespaceResolver xmlNamespaceResolver = this.reader as IXmlNamespaceResolver;
			return xmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010ACC File Offset: 0x0000ECCC
		public string LookupPrefix(string ns)
		{
			IXmlNamespaceResolver xmlNamespaceResolver = this.reader as IXmlNamespaceResolver;
			return xmlNamespaceResolver.LookupPrefix(this.NameTable.Get(ns));
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010B08 File Offset: 0x0000ED08
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010B18 File Offset: 0x0000ED18
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00010B28 File Offset: 0x0000ED28
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00010B38 File Offset: 0x0000ED38
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00010B48 File Offset: 0x0000ED48
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00010B58 File Offset: 0x0000ED58
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00010B68 File Offset: 0x0000ED68
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00010B78 File Offset: 0x0000ED78
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00010B88 File Offset: 0x0000ED88
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00010B98 File Offset: 0x0000ED98
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170000A4 RID: 164
		public override string this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x170000A5 RID: 165
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x170000A6 RID: 166
		public override string this[string localName, string namespaceURI]
		{
			get
			{
				return this.reader[localName, namespaceURI];
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00010C18 File Offset: 0x0000EE18
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00010C28 File Offset: 0x0000EE28
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00010C38 File Offset: 0x0000EE38
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00010C48 File Offset: 0x0000EE48
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00010C58 File Offset: 0x0000EE58
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.reader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00010C68 File Offset: 0x0000EE68
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010C78 File Offset: 0x0000EE78
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010C88 File Offset: 0x0000EE88
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.reader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010C98 File Offset: 0x0000EE98
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public override void Close()
		{
			this.reader.Close();
			if (this.onClose != null)
			{
				this.onClose(this);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00010D08 File Offset: 0x0000EF08
		public override bool Read()
		{
			if (!this.reader.Read())
			{
				return false;
			}
			this.dict.Add(this.reader.Prefix);
			this.dict.Add(this.reader.LocalName);
			this.dict.Add(this.reader.NamespaceURI);
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					this.dict.Add(this.reader.Prefix);
					this.dict.Add(this.reader.LocalName);
					this.dict.Add(this.reader.NamespaceURI);
					this.dict.Add(this.reader.Value);
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			return true;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010E08 File Offset: 0x0000F008
		public override string ReadInnerXml()
		{
			return this.reader.ReadInnerXml();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010E18 File Offset: 0x0000F018
		public override string ReadOuterXml()
		{
			return this.reader.ReadOuterXml();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010E28 File Offset: 0x0000F028
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010E38 File Offset: 0x0000F038
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010E48 File Offset: 0x0000F048
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x04000178 RID: 376
		private XmlDictionary dict;

		// Token: 0x04000179 RID: 377
		private XmlReader reader;

		// Token: 0x0400017A RID: 378
		private XmlDictionaryReader as_dict_reader;

		// Token: 0x0400017B RID: 379
		private IXmlLineInfo as_line_info;

		// Token: 0x0400017C RID: 380
		private OnXmlDictionaryReaderClose onClose;
	}
}
