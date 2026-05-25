using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000040 RID: 64
	internal class XmlBinaryDictionaryReader : XmlDictionaryReader, IXmlNamespaceResolver
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000083F0 File Offset: 0x000065F0
		public XmlBinaryDictionaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quota, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			this.source = new XmlBinaryDictionaryReader.StreamSource(new MemoryStream(buffer, offset, count));
			this.Initialize(dictionary, quota, session, onClose);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008484 File Offset: 0x00006684
		public XmlBinaryDictionaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quota, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			this.source = new XmlBinaryDictionaryReader.StreamSource(stream);
			this.Initialize(dictionary, quota, session, onClose);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008510 File Offset: 0x00006710
		private void Initialize(IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			if (quotas == null)
			{
				throw new ArgumentNullException("quotas");
			}
			if (dictionary == null)
			{
				dictionary = new XmlDictionary();
			}
			this.dictionary = dictionary;
			this.quota = quotas;
			if (session == null)
			{
				session = new XmlBinaryReaderSession();
			}
			this.session = session;
			this.on_close = onClose;
			NameTable nameTable = new NameTable();
			this.context = new XmlParserContext(nameTable, new XmlNamespaceManager(nameTable), null, XmlSpace.None);
			this.current = (this.node = new XmlBinaryDictionaryReader.NodeInfo());
			this.current.Reset();
			this.node_stack.Add(this.node);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000085B0 File Offset: 0x000067B0
		public override int AttributeCount
		{
			get
			{
				return this.attr_count;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000085B8 File Offset: 0x000067B8
		public override string BaseURI
		{
			get
			{
				return this.context.BaseURI;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000085C8 File Offset: 0x000067C8
		public override int Depth
		{
			get
			{
				return (this.current != this.node) ? ((this.NodeType != XmlNodeType.Attribute) ? (this.depth + 2) : (this.depth + 1)) : this.depth;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008608 File Offset: 0x00006808
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile || this.state == ReadState.Error;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008624 File Offset: 0x00006824
		public override bool HasValue
		{
			get
			{
				return this.Value.Length > 0;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008634 File Offset: 0x00006834
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008638 File Offset: 0x00006838
		public override XmlNodeType NodeType
		{
			get
			{
				return this.current.NodeType;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008648 File Offset: 0x00006848
		public override string Prefix
		{
			get
			{
				return (this.current_attr < 0) ? this.current.Prefix : this.attributes[this.current_attr].Prefix;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008688 File Offset: 0x00006888
		public override string LocalName
		{
			get
			{
				return (this.current_attr < 0) ? this.current.LocalName : this.attributes[this.current_attr].LocalName;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000086C8 File Offset: 0x000068C8
		public override string Name
		{
			get
			{
				return (this.current_attr < 0) ? this.current.Name : this.attributes[this.current_attr].Name;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00008708 File Offset: 0x00006908
		public override string NamespaceURI
		{
			get
			{
				return (this.current_attr < 0) ? this.current.NS : this.attributes[this.current_attr].NS;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008748 File Offset: 0x00006948
		public override XmlNameTable NameTable
		{
			get
			{
				return this.context.NameTable;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008758 File Offset: 0x00006958
		public override XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return this.quota;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008760 File Offset: 0x00006960
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008768 File Offset: 0x00006968
		public override string Value
		{
			get
			{
				return this.current.Value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008778 File Offset: 0x00006978
		public override void Close()
		{
			if (this.on_close != null)
			{
				this.on_close(this);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008794 File Offset: 0x00006994
		public override string GetAttribute(int i)
		{
			if (i >= this.attr_count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Specified attribute index is {0} and should be less than {1}", i, this.attr_count));
			}
			return this.attributes[i].Value;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000087E0 File Offset: 0x000069E0
		public override string GetAttribute(string name)
		{
			for (int i = 0; i < this.attr_count; i++)
			{
				if (this.attributes[i].Name == name)
				{
					return this.attributes[i].Value;
				}
			}
			return null;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008834 File Offset: 0x00006A34
		public override string GetAttribute(string localName, string ns)
		{
			for (int i = 0; i < this.attr_count; i++)
			{
				if (this.attributes[i].LocalName == localName && this.attributes[i].NS == ns)
				{
					return this.attributes[i].Value;
				}
			}
			return null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000088A4 File Offset: 0x00006AA4
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.context.NamespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000088B8 File Offset: 0x00006AB8
		public string LookupPrefix(string ns)
		{
			return this.context.NamespaceManager.LookupPrefix(this.NameTable.Get(ns));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000088E4 File Offset: 0x00006AE4
		public override string LookupNamespace(string prefix)
		{
			return this.context.NamespaceManager.LookupNamespace(this.NameTable.Get(prefix));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008910 File Offset: 0x00006B10
		public override bool IsArray(out Type type)
		{
			if (this.array_state == XmlNodeType.Element)
			{
				type = this.GetArrayType((int)this.array_item_type);
				return true;
			}
			type = null;
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008940 File Offset: 0x00006B40
		public override bool MoveToElement()
		{
			bool flag = this.current_attr >= 0;
			this.current_attr = -1;
			this.current = this.node;
			return flag;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008970 File Offset: 0x00006B70
		public override bool MoveToFirstAttribute()
		{
			if (this.attr_count == 0)
			{
				return false;
			}
			this.current_attr = 0;
			this.current = this.attributes[this.current_attr];
			return true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000089AC File Offset: 0x00006BAC
		public override bool MoveToNextAttribute()
		{
			if (++this.current_attr < this.attr_count)
			{
				this.current = this.attributes[this.current_attr];
				return true;
			}
			this.current_attr--;
			return false;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008A00 File Offset: 0x00006C00
		public override void MoveToAttribute(int i)
		{
			if (i >= this.attr_count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Specified attribute index is {0} and should be less than {1}", i, this.attr_count));
			}
			this.current_attr = i;
			this.current = this.attributes[i];
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008A54 File Offset: 0x00006C54
		public override bool MoveToAttribute(string name)
		{
			for (int i = 0; i < this.attributes.Count; i++)
			{
				if (this.attributes[i].Name == name)
				{
					this.MoveToAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public override bool MoveToAttribute(string localName, string ns)
		{
			for (int i = 0; i < this.attributes.Count; i++)
			{
				if (this.attributes[i].LocalName == localName && this.attributes[i].NS == ns)
				{
					this.MoveToAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008B10 File Offset: 0x00006D10
		public override bool ReadAttributeValue()
		{
			if (this.current_attr < 0)
			{
				return false;
			}
			int valueIndex = this.attributes[this.current_attr].ValueIndex;
			int num = ((this.current_attr + 1 != this.attr_count) ? this.attributes[this.current_attr + 1].ValueIndex : this.attr_value_count);
			if (valueIndex == num)
			{
				return false;
			}
			if (!this.current.IsAttributeValue)
			{
				this.current = this.attr_values[valueIndex];
				return true;
			}
			return false;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public override bool Read()
		{
			switch (this.state)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return false;
			default:
			{
				this.state = ReadState.Interactive;
				this.MoveToElement();
				this.attr_count = 0;
				this.attr_value_count = 0;
				this.ns_slot = 0;
				if (this.node.NodeType == XmlNodeType.Element)
				{
					if (this.node_stack.Count <= ++this.depth)
					{
						if (this.depth == this.quota.MaxDepth)
						{
							throw new XmlException(string.Format("Binary XML stream quota exceeded. Depth must be less than {0}", this.quota.MaxDepth));
						}
						this.node = new XmlBinaryDictionaryReader.NodeInfo();
						this.node_stack.Add(this.node);
					}
					else
					{
						this.node = this.node_stack[this.depth];
						this.node.Reset();
					}
				}
				this.current = this.node;
				if (this.is_next_end_element)
				{
					this.is_next_end_element = false;
					this.node.Reset();
					this.ProcessEndElement();
					return true;
				}
				XmlNodeType xmlNodeType = this.array_state;
				switch (xmlNodeType)
				{
				case XmlNodeType.Element:
					this.ReadArrayItem();
					return true;
				default:
				{
					if (xmlNodeType == XmlNodeType.EndElement)
					{
						if (--this.array_item_remaining != 0)
						{
							this.ShiftToArrayItemElement();
							return true;
						}
						this.array_state = XmlNodeType.None;
					}
					this.node.Reset();
					int num = ((this.next < 0) ? this.source.ReadByte() : this.next);
					this.next = -1;
					if (num < 0)
					{
						this.state = ReadState.EndOfFile;
						this.current.Reset();
						return false;
					}
					this.is_next_end_element = num > 128 && (num & 1) == 1;
					num -= ((!this.is_next_end_element) ? 0 : 1);
					int num2 = num;
					switch (num2)
					{
					case 64:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num2)
						{
						case 1:
							this.ProcessEndElement();
							return true;
						case 2:
							this.node.Value = this.ReadUTF8();
							this.node.ValueType = 2;
							this.node.NodeType = XmlNodeType.Comment;
							return true;
						case 3:
							num = (int)this.ReadByteOrError();
							this.ReadElementBinary((int)((byte)num));
							num = (int)this.ReadByteOrError();
							if (num != 1)
							{
								throw new XmlException(string.Format("EndElement is expected after element in an array. The actual byte was {0:X} in hexadecimal", num));
							}
							num = (int)(this.ReadByteOrError() - 1);
							this.VerifyValidArrayItemType(num);
							if (num < 0)
							{
								throw new XmlException("The stream has ended where the array item type is expected");
							}
							this.array_item_type = (byte)num;
							this.array_item_remaining = this.ReadVariantSize();
							if (this.array_item_remaining > this.quota.MaxArrayLength)
							{
								throw new Exception(string.Format("Binary xml stream exceeded max array length quota. Items are {0} and should be less than quota.MaxArrayLength", this.quota.MaxArrayLength));
							}
							this.array_state = XmlNodeType.Element;
							return true;
						default:
							if ((68 > num || num > 93) && (94 > num || num > 119))
							{
								this.ReadTextOrValue((byte)num, this.node, false);
								return true;
							}
							break;
						}
						break;
					}
					this.ReadElementBinary((int)((byte)num));
					return true;
				}
				case XmlNodeType.Text:
					this.ShiftToArrayItemEndElement();
					return true;
				}
				break;
			}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008F18 File Offset: 0x00007118
		private void ReadArrayItem()
		{
			this.ReadTextOrValue(this.array_item_type, this.node, false);
			this.array_state = XmlNodeType.Text;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008F38 File Offset: 0x00007138
		private void ShiftToArrayItemEndElement()
		{
			this.ProcessEndElement();
			this.array_state = XmlNodeType.EndElement;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008F48 File Offset: 0x00007148
		private void ShiftToArrayItemElement()
		{
			this.node.NodeType = XmlNodeType.Element;
			this.context.NamespaceManager.PushScope();
			this.array_state = XmlNodeType.Element;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008F70 File Offset: 0x00007170
		private void VerifyValidArrayItemType(int ident)
		{
			if (this.GetArrayType(ident) == null)
			{
				throw new XmlException(string.Format("Unexpected array item type {0:X} in hexadecimal", ident));
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008FA0 File Offset: 0x000071A0
		private Type GetArrayType(int ident)
		{
			switch (ident)
			{
			case 138:
				return typeof(short);
			default:
				switch (ident)
				{
				case 174:
					return typeof(TimeSpan);
				default:
					if (ident != 180)
					{
						return null;
					}
					return typeof(bool);
				case 176:
					return typeof(Guid);
				}
				break;
			case 140:
				return typeof(int);
			case 142:
				return typeof(long);
			case 144:
				return typeof(float);
			case 146:
				return typeof(double);
			case 148:
				return typeof(decimal);
			case 150:
				return typeof(DateTime);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009088 File Offset: 0x00007288
		private void ProcessEndElement()
		{
			if (this.depth == 0)
			{
				throw new XmlException("Unexpected end of element while there is no element started.");
			}
			this.current = (this.node = this.node_stack[--this.depth]);
			this.node.NodeType = XmlNodeType.EndElement;
			this.context.NamespaceManager.PopScope();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000090F4 File Offset: 0x000072F4
		private void ReadElementBinary(int ident)
		{
			this.node.NodeType = XmlNodeType.Element;
			this.node.Prefix = string.Empty;
			this.context.NamespaceManager.PushScope();
			switch (ident)
			{
			case 64:
				break;
			case 65:
				this.node.Prefix = this.ReadUTF8();
				this.node.NSSlot = this.ns_slot++;
				break;
			case 66:
				goto IL_0096;
			case 67:
				this.node.Prefix = this.ReadUTF8();
				this.node.NSSlot = this.ns_slot++;
				goto IL_0096;
			default:
				if (68 <= ident && ident <= 93)
				{
					this.node.Prefix = ((char)(ident - 68 + 97)).ToString();
					this.node.DictLocalName = this.ReadDictName();
				}
				else
				{
					if (94 > ident || ident > 119)
					{
						throw new XmlException(string.Format("Invalid element node type {0:X02} in hexadecimal", ident));
					}
					this.node.Prefix = ((char)(ident - 94 + 97)).ToString();
					this.node.LocalName = this.ReadUTF8();
				}
				goto IL_017F;
			}
			this.node.LocalName = this.ReadUTF8();
			goto IL_017F;
			IL_0096:
			this.node.DictLocalName = this.ReadDictName();
			IL_017F:
			bool flag = true;
			do
			{
				ident = (int)this.ReadByteOrError();
				switch (ident)
				{
				case 4:
				case 5:
				case 6:
				case 7:
					this.ReadAttribute((byte)ident);
					break;
				case 8:
				case 9:
				case 10:
				case 11:
					this.ReadNamespace((byte)ident);
					break;
				default:
					if ((38 <= ident && ident <= 63) || (12 <= ident && ident <= 37))
					{
						this.ReadAttribute((byte)ident);
					}
					else
					{
						this.next = ident;
						flag = false;
					}
					break;
				}
			}
			while (flag);
			this.node.NS = this.context.NamespaceManager.LookupNamespace(this.node.Prefix) ?? string.Empty;
			foreach (XmlBinaryDictionaryReader.AttrNodeInfo attrNodeInfo in this.attributes)
			{
				if (attrNodeInfo.Prefix.Length > 0)
				{
					attrNodeInfo.NS = this.context.NamespaceManager.LookupNamespace(attrNodeInfo.Prefix);
				}
			}
			this.ns_store.Clear();
			this.ns_dict_store.Clear();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000093DC File Offset: 0x000075DC
		private void ReadAttribute(byte ident)
		{
			if (this.attributes.Count == this.attr_count)
			{
				this.attributes.Add(new XmlBinaryDictionaryReader.AttrNodeInfo(this));
			}
			XmlBinaryDictionaryReader.AttrNodeInfo attrNodeInfo = this.attributes[this.attr_count++];
			attrNodeInfo.Reset();
			attrNodeInfo.Position = this.source.Position;
			switch (ident)
			{
			case 4:
				break;
			case 5:
				attrNodeInfo.Prefix = this.ReadUTF8();
				attrNodeInfo.NSSlot = this.ns_slot++;
				break;
			case 6:
				goto IL_00B3;
			case 7:
				attrNodeInfo.Prefix = this.ReadUTF8();
				attrNodeInfo.NSSlot = this.ns_slot++;
				goto IL_00B3;
			default:
				if (38 <= ident && ident <= 63)
				{
					attrNodeInfo.Prefix = ((char)(97 + ident - 38)).ToString();
					attrNodeInfo.LocalName = this.ReadUTF8();
					goto IL_0171;
				}
				if (12 <= ident && ident <= 37)
				{
					attrNodeInfo.Prefix = ((char)(97 + ident - 12)).ToString();
					attrNodeInfo.DictLocalName = this.ReadDictName();
					goto IL_0171;
				}
				throw new XmlException(string.Format("Unexpected attribute node type: 0x{0:X02}", ident));
			}
			attrNodeInfo.LocalName = this.ReadUTF8();
			goto IL_0171;
			IL_00B3:
			attrNodeInfo.DictLocalName = this.ReadDictName();
			IL_0171:
			this.ReadAttributeValueBinary(attrNodeInfo);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009564 File Offset: 0x00007764
		private void ReadNamespace(byte ident)
		{
			if (this.attributes.Count == this.attr_count)
			{
				this.attributes.Add(new XmlBinaryDictionaryReader.AttrNodeInfo(this));
			}
			XmlBinaryDictionaryReader.AttrNodeInfo attrNodeInfo = this.attributes[this.attr_count++];
			attrNodeInfo.Reset();
			attrNodeInfo.Position = this.source.Position;
			string text = null;
			string text2 = null;
			switch (ident)
			{
			case 8:
				text = string.Empty;
				text2 = this.ReadUTF8();
				break;
			case 9:
				text = this.ReadUTF8();
				text2 = this.ReadUTF8();
				break;
			case 10:
			{
				text = string.Empty;
				XmlDictionaryString xmlDictionaryString = this.ReadDictName();
				this.ns_dict_store.Add(this.ns_store.Count, xmlDictionaryString);
				text2 = xmlDictionaryString.Value;
				break;
			}
			case 11:
			{
				text = this.ReadUTF8();
				XmlDictionaryString xmlDictionaryString = this.ReadDictName();
				this.ns_dict_store.Add(this.ns_store.Count, xmlDictionaryString);
				text2 = xmlDictionaryString.Value;
				break;
			}
			}
			attrNodeInfo.Prefix = ((text.Length <= 0) ? string.Empty : "xmlns");
			attrNodeInfo.LocalName = ((text.Length <= 0) ? "xmlns" : text);
			attrNodeInfo.NS = "http://www.w3.org/2000/xmlns/";
			attrNodeInfo.ValueIndex = this.attr_value_count;
			if (this.attr_value_count == this.attr_values.Count)
			{
				this.attr_values.Add(new XmlBinaryDictionaryReader.NodeInfo(true));
			}
			XmlBinaryDictionaryReader.NodeInfo nodeInfo = this.attr_values[this.attr_value_count++];
			nodeInfo.Reset();
			nodeInfo.Value = text2;
			nodeInfo.ValueType = 152;
			nodeInfo.NodeType = XmlNodeType.Text;
			this.ns_store.Add(new XmlQualifiedName(text, text2));
			this.context.NamespaceManager.AddNamespace(text, text2);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009760 File Offset: 0x00007960
		private void ReadAttributeValueBinary(XmlBinaryDictionaryReader.AttrNodeInfo a)
		{
			a.ValueIndex = this.attr_value_count;
			if (this.attr_value_count == this.attr_values.Count)
			{
				this.attr_values.Add(new XmlBinaryDictionaryReader.NodeInfo(true));
			}
			XmlBinaryDictionaryReader.NodeInfo nodeInfo = this.attr_values[this.attr_value_count++];
			nodeInfo.Reset();
			int num = (int)this.ReadByteOrError();
			bool flag = num > 128 && (num & 1) == 1;
			num -= ((!flag) ? 0 : 1);
			this.ReadTextOrValue((byte)num, nodeInfo, true);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000097FC File Offset: 0x000079FC
		private bool ReadTextOrValue(byte ident, XmlBinaryDictionaryReader.NodeInfo node, bool canSkip)
		{
			node.Value = null;
			node.ValueType = ident;
			node.NodeType = XmlNodeType.Text;
			int num;
			switch (ident)
			{
			case 128:
				node.TypedValue = 0;
				return true;
			default:
				switch (ident)
				{
				case 168:
					node.Value = string.Empty;
					node.NodeType = XmlNodeType.Text;
					return true;
				default:
					switch (ident)
					{
					case 182:
					case 184:
					case 186:
						goto IL_0398;
					}
					if (!canSkip)
					{
						throw new ArgumentException(string.Format("Unexpected binary XML data at position {1}: {0:X}", (int)(ident + ((!this.is_next_end_element) ? 0 : 1)), this.source.Position));
					}
					this.next = (int)ident;
					return false;
				case 170:
					node.DictValue = this.ReadDictName();
					node.NodeType = XmlNodeType.Text;
					return true;
				case 172:
				{
					byte[] array = new byte[16];
					this.source.Reader.Read(array, 0, array.Length);
					node.TypedValue = new UniqueId(new Guid(array));
					return true;
				}
				case 174:
					node.TypedValue = new TimeSpan(this.source.Reader.ReadInt64());
					return true;
				case 176:
				{
					byte[] array = new byte[16];
					this.source.Reader.Read(array, 0, array.Length);
					node.TypedValue = new Guid(array);
					return true;
				}
				}
				break;
			case 130:
				node.TypedValue = 1;
				return true;
			case 132:
				node.TypedValue = false;
				return true;
			case 134:
				node.TypedValue = true;
				return true;
			case 136:
				node.TypedValue = this.ReadByteOrError();
				return true;
			case 138:
				node.TypedValue = this.source.Reader.ReadInt16();
				return true;
			case 140:
				node.TypedValue = this.source.Reader.ReadInt32();
				return true;
			case 142:
				node.TypedValue = this.source.Reader.ReadInt64();
				return true;
			case 144:
				node.TypedValue = this.source.Reader.ReadSingle();
				return true;
			case 146:
				node.TypedValue = this.source.Reader.ReadDouble();
				return true;
			case 148:
			{
				int[] array2 = new int[]
				{
					0,
					0,
					0,
					this.source.Reader.ReadInt32()
				};
				array2[2] = this.source.Reader.ReadInt32();
				array2[0] = this.source.Reader.ReadInt32();
				array2[1] = this.source.Reader.ReadInt32();
				node.TypedValue = new decimal(array2);
				return true;
			}
			case 150:
				node.TypedValue = new DateTime(this.source.Reader.ReadInt64());
				return true;
			case 152:
			case 154:
			case 156:
				break;
			case 158:
			case 160:
			case 162:
			{
				num = ((ident != 158) ? ((ident != 160) ? this.source.Reader.ReadInt32() : ((int)this.source.Reader.ReadUInt16())) : ((int)this.source.Reader.ReadByte()));
				byte[] array3 = this.Alloc(num);
				this.source.Reader.Read(array3, 0, array3.Length);
				node.TypedValue = array3;
				return true;
			}
			}
			IL_0398:
			Encoding encoding = ((ident > 156) ? Encoding.Unicode : Encoding.UTF8);
			num = ((ident != 152 && ident != 182) ? ((ident != 154 && ident != 184) ? this.source.Reader.ReadInt32() : ((int)this.source.Reader.ReadUInt16())) : ((int)this.source.Reader.ReadByte()));
			byte[] array4 = this.Alloc(num);
			this.source.Reader.Read(array4, 0, num);
			node.Value = encoding.GetString(array4, 0, num);
			node.NodeType = XmlNodeType.Text;
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private byte[] Alloc(int size)
		{
			if (size > this.quota.MaxStringContentLength || size < 0)
			{
				throw new XmlException(string.Format("Text content buffer exceeds the quota limitation at {2}. {0} bytes and should be less than {1} bytes", size, this.quota.MaxStringContentLength, this.source.Position));
			}
			return new byte[size];
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009D3C File Offset: 0x00007F3C
		private int ReadVariantSize()
		{
			int num = 0;
			int num2 = 0;
			byte b;
			do
			{
				b = this.ReadByteOrError();
				num += (int)(b & 127) << num2;
				num2 += 7;
			}
			while (b >= 128);
			return num;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009D80 File Offset: 0x00007F80
		private string ReadUTF8()
		{
			int num = this.ReadVariantSize();
			if (num == 0)
			{
				return string.Empty;
			}
			if (this.tmp_buffer.Length < num)
			{
				int num2 = this.tmp_buffer.Length * 2;
				this.tmp_buffer = this.Alloc((num >= num2) ? num : num2);
			}
			num = this.source.Read(this.tmp_buffer, 0, num);
			return this.utf8enc.GetString(this.tmp_buffer, 0, num);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009DFC File Offset: 0x00007FFC
		private XmlDictionaryString ReadDictName()
		{
			int num = this.ReadVariantSize();
			XmlDictionaryString xmlDictionaryString;
			if ((num & 1) == 1)
			{
				if (this.session.TryLookup(num >> 1, out xmlDictionaryString))
				{
					return xmlDictionaryString;
				}
			}
			else if (this.dictionary.TryLookup(num >> 1, out xmlDictionaryString))
			{
				return xmlDictionaryString;
			}
			throw new XmlException(string.Format("Input XML binary stream is invalid. No matching XML dictionary string entry at {0}. Binary stream position at {1}", num, this.source.Position));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009E74 File Offset: 0x00008074
		private byte ReadByteOrError()
		{
			if (this.next >= 0)
			{
				byte b = (byte)this.next;
				this.next = -1;
				return b;
			}
			int num = this.source.ReadByte();
			if (num < 0)
			{
				throw new XmlException(string.Format("Unexpected end of binary stream. Position is at {0}", this.source.Position));
			}
			return (byte)num;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009ED4 File Offset: 0x000080D4
		public override void ResolveEntity()
		{
			throw new NotSupportedException("this XmlReader does not support ResolveEntity.");
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009EE0 File Offset: 0x000080E0
		public override bool TryGetBase64ContentLength(out int length)
		{
			length = 0;
			switch (this.current.ValueType)
			{
			case 158:
			case 160:
			case 162:
				length = ((byte[])this.current.TypedValue).Length;
				return true;
			}
			return false;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009F38 File Offset: 0x00008138
		public override string ReadContentAsString()
		{
			string text = string.Empty;
			for (;;)
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					return text;
				default:
					if (nodeType == XmlNodeType.EndElement)
					{
						return text;
					}
					break;
				case XmlNodeType.Text:
					text += this.Value;
					break;
				}
				if (!this.Read())
				{
					return text;
				}
			}
			return text;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009F94 File Offset: 0x00008194
		public override int ReadContentAsInt()
		{
			int intValue = this.GetIntValue();
			this.Read();
			return intValue;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009FB0 File Offset: 0x000081B0
		private int GetIntValue()
		{
			byte valueType = this.node.ValueType;
			switch (valueType)
			{
			case 136:
				return (int)((byte)this.current.TypedValue);
			default:
				switch (valueType)
				{
				case 128:
					return 0;
				case 130:
					return 1;
				}
				throw new InvalidOperationException(string.Format("Current content is not an integer. (Internal value type:{0:X02})", (int)this.node.ValueType));
			case 138:
				return (int)((short)this.current.TypedValue);
			case 140:
				return (int)this.current.TypedValue;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A05C File Offset: 0x0000825C
		public override long ReadContentAsLong()
		{
			if (this.node.ValueType == 142)
			{
				long num = (long)this.current.TypedValue;
				this.Read();
				return num;
			}
			return (long)this.ReadContentAsInt();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public override float ReadContentAsFloat()
		{
			if (this.node.ValueType != 144)
			{
				throw new InvalidOperationException("Current content is not a single");
			}
			float num = (float)this.current.TypedValue;
			this.Read();
			return num;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public override double ReadContentAsDouble()
		{
			if (this.node.ValueType != 146)
			{
				throw new InvalidOperationException("Current content is not a double");
			}
			double num = (double)this.current.TypedValue;
			this.Read();
			return num;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A130 File Offset: 0x00008330
		private bool IsBase64Node(byte b)
		{
			switch (b)
			{
			case 158:
			case 160:
			case 162:
				return true;
			}
			return false;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A168 File Offset: 0x00008368
		public override byte[] ReadContentAsBase64()
		{
			byte[] array = null;
			if (!this.IsBase64Node(this.node.ValueType))
			{
				throw new InvalidOperationException("Current content is not base64");
			}
			while (this.NodeType == XmlNodeType.Text && this.IsBase64Node(this.node.ValueType))
			{
				if (array == null)
				{
					array = (byte[])this.node.TypedValue;
				}
				else
				{
					byte[] array2 = (byte[])this.node.TypedValue;
					byte[] array3 = this.Alloc(array.Length + array2.Length);
					Array.Copy(array, array3, array.Length);
					Array.Copy(array2, 0, array3, array.Length, array2.Length);
					array = array3;
				}
				this.Read();
			}
			return array;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A21C File Offset: 0x0000841C
		public override Guid ReadContentAsGuid()
		{
			if (this.node.ValueType != 176)
			{
				throw new InvalidOperationException("Current content is not a Guid");
			}
			Guid guid = (Guid)this.node.TypedValue;
			this.Read();
			return guid;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A264 File Offset: 0x00008464
		public override UniqueId ReadContentAsUniqueId()
		{
			byte valueType = this.node.ValueType;
			UniqueId uniqueId;
			switch (valueType)
			{
			case 152:
			case 154:
			case 156:
				break;
			default:
				switch (valueType)
				{
				case 182:
				case 184:
				case 186:
					break;
				default:
					if (valueType != 172)
					{
						throw new InvalidOperationException("Current content is not a UniqueId");
					}
					uniqueId = (UniqueId)this.node.TypedValue;
					this.Read();
					return uniqueId;
				}
				break;
			}
			uniqueId = new UniqueId(this.node.Value);
			this.Read();
			return uniqueId;
		}

		// Token: 0x040000B2 RID: 178
		private XmlBinaryDictionaryReader.ISource source;

		// Token: 0x040000B3 RID: 179
		private IXmlDictionary dictionary;

		// Token: 0x040000B4 RID: 180
		private XmlDictionaryReaderQuotas quota;

		// Token: 0x040000B5 RID: 181
		private XmlBinaryReaderSession session;

		// Token: 0x040000B6 RID: 182
		private OnXmlDictionaryReaderClose on_close;

		// Token: 0x040000B7 RID: 183
		private XmlParserContext context;

		// Token: 0x040000B8 RID: 184
		private ReadState state;

		// Token: 0x040000B9 RID: 185
		private XmlBinaryDictionaryReader.NodeInfo node;

		// Token: 0x040000BA RID: 186
		private XmlBinaryDictionaryReader.NodeInfo current;

		// Token: 0x040000BB RID: 187
		private List<XmlBinaryDictionaryReader.AttrNodeInfo> attributes = new List<XmlBinaryDictionaryReader.AttrNodeInfo>();

		// Token: 0x040000BC RID: 188
		private List<XmlBinaryDictionaryReader.NodeInfo> attr_values = new List<XmlBinaryDictionaryReader.NodeInfo>();

		// Token: 0x040000BD RID: 189
		private List<XmlBinaryDictionaryReader.NodeInfo> node_stack = new List<XmlBinaryDictionaryReader.NodeInfo>();

		// Token: 0x040000BE RID: 190
		private List<XmlQualifiedName> ns_store = new List<XmlQualifiedName>();

		// Token: 0x040000BF RID: 191
		private Dictionary<int, XmlDictionaryString> ns_dict_store = new Dictionary<int, XmlDictionaryString>();

		// Token: 0x040000C0 RID: 192
		private int attr_count;

		// Token: 0x040000C1 RID: 193
		private int attr_value_count;

		// Token: 0x040000C2 RID: 194
		private int current_attr = -1;

		// Token: 0x040000C3 RID: 195
		private int depth;

		// Token: 0x040000C4 RID: 196
		private int ns_slot;

		// Token: 0x040000C5 RID: 197
		private int next = -1;

		// Token: 0x040000C6 RID: 198
		private bool is_next_end_element;

		// Token: 0x040000C7 RID: 199
		private byte[] tmp_buffer = new byte[128];

		// Token: 0x040000C8 RID: 200
		private UTF8Encoding utf8enc = new UTF8Encoding();

		// Token: 0x040000C9 RID: 201
		private int array_item_remaining;

		// Token: 0x040000CA RID: 202
		private byte array_item_type;

		// Token: 0x040000CB RID: 203
		private XmlNodeType array_state;

		// Token: 0x02000041 RID: 65
		internal interface ISource
		{
			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060001BC RID: 444
			int Position { get; }

			// Token: 0x060001BD RID: 445
			int ReadByte();

			// Token: 0x060001BE RID: 446
			int Read(byte[] data, int offset, int count);

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060001BF RID: 447
			BinaryReader Reader { get; }
		}

		// Token: 0x02000042 RID: 66
		internal class StreamSource : XmlBinaryDictionaryReader.ISource
		{
			// Token: 0x060001C0 RID: 448 RVA: 0x0000A30C File Offset: 0x0000850C
			public StreamSource(Stream stream)
			{
				this.reader = new BinaryReader(stream);
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A320 File Offset: 0x00008520
			public int Position
			{
				get
				{
					return (int)this.reader.BaseStream.Position;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A334 File Offset: 0x00008534
			public BinaryReader Reader
			{
				get
				{
					return this.reader;
				}
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x0000A33C File Offset: 0x0000853C
			public int ReadByte()
			{
				if (this.reader.PeekChar() < 0)
				{
					return -1;
				}
				return (int)this.reader.ReadByte();
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x0000A35C File Offset: 0x0000855C
			public int Read(byte[] data, int offset, int count)
			{
				return this.reader.Read(data, offset, count);
			}

			// Token: 0x040000CC RID: 204
			private BinaryReader reader;
		}

		// Token: 0x02000043 RID: 67
		private class NodeInfo
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x0000A36C File Offset: 0x0000856C
			public NodeInfo()
			{
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000A398 File Offset: 0x00008598
			public NodeInfo(bool isAttr)
			{
				this.IsAttributeValue = isAttr;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A3D4 File Offset: 0x000085D4
			// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A3F8 File Offset: 0x000085F8
			public string LocalName
			{
				get
				{
					return (this.DictLocalName == null) ? this.local_name : this.DictLocalName.Value;
				}
				set
				{
					this.DictLocalName = null;
					this.local_name = value;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A408 File Offset: 0x00008608
			// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A42C File Offset: 0x0000862C
			public string NS
			{
				get
				{
					return (this.DictNS == null) ? this.ns : this.DictNS.Value;
				}
				set
				{
					this.DictNS = null;
					this.ns = value;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A43C File Offset: 0x0000863C
			public string Name
			{
				get
				{
					if (this.name.Length == 0)
					{
						this.name = ((this.Prefix.Length <= 0) ? this.LocalName : (this.Prefix + ":" + this.LocalName));
					}
					return this.name;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060001CC RID: 460 RVA: 0x0000A498 File Offset: 0x00008698
			// (set) Token: 0x060001CD RID: 461 RVA: 0x0000A6CC File Offset: 0x000088CC
			public virtual string Value
			{
				get
				{
					byte valueType = this.ValueType;
					switch (valueType)
					{
					case 128:
						return "0";
					default:
						switch (valueType)
						{
						case 150:
							return XmlConvert.ToString((DateTime)this.TypedValue, XmlDateTimeSerializationMode.RoundtripKind);
						default:
							switch (valueType)
							{
							case 168:
								break;
							default:
								switch (valueType)
								{
								case 182:
								case 184:
								case 186:
									break;
								default:
									switch (valueType)
									{
									case 0:
									case 2:
										goto IL_0106;
									}
									throw new NotImplementedException(string.Concat(new object[] { "ValueType ", this.ValueType, " on node ", this.NodeType }));
								}
								break;
							case 170:
								return this.DictValue.Value;
							case 172:
								return this.TypedValue.ToString();
							case 174:
								return XmlConvert.ToString((TimeSpan)this.TypedValue);
							case 176:
								return XmlConvert.ToString((Guid)this.TypedValue);
							}
							break;
						case 152:
						case 154:
						case 156:
							break;
						case 158:
						case 160:
						case 162:
							return Convert.ToBase64String((byte[])this.TypedValue);
						}
						IL_0106:
						return this.value;
					case 130:
						return "1";
					case 132:
						return "false";
					case 134:
						return "true";
					case 136:
						return XmlConvert.ToString((byte)this.TypedValue);
					case 138:
						return XmlConvert.ToString((short)this.TypedValue);
					case 140:
						return XmlConvert.ToString((int)this.TypedValue);
					case 142:
						return XmlConvert.ToString((long)this.TypedValue);
					case 144:
						return XmlConvert.ToString((float)this.TypedValue);
					case 146:
						return XmlConvert.ToString((double)this.TypedValue);
					}
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x060001CE RID: 462 RVA: 0x0000A6D8 File Offset: 0x000088D8
			public virtual void Reset()
			{
				this.Position = 0;
				this.DictLocalName = (this.DictNS = null);
				string text = string.Empty;
				this.Value = text;
				text = (this.Prefix = text);
				this.NS = text;
				this.LocalName = text;
				this.NodeType = XmlNodeType.None;
				this.TypedValue = null;
				this.ValueType = 0;
				this.NSSlot = -1;
			}

			// Token: 0x040000CD RID: 205
			public bool IsAttributeValue;

			// Token: 0x040000CE RID: 206
			public int Position;

			// Token: 0x040000CF RID: 207
			public string Prefix;

			// Token: 0x040000D0 RID: 208
			public XmlDictionaryString DictLocalName;

			// Token: 0x040000D1 RID: 209
			public XmlDictionaryString DictNS;

			// Token: 0x040000D2 RID: 210
			public XmlDictionaryString DictValue;

			// Token: 0x040000D3 RID: 211
			public XmlNodeType NodeType;

			// Token: 0x040000D4 RID: 212
			public object TypedValue;

			// Token: 0x040000D5 RID: 213
			public byte ValueType;

			// Token: 0x040000D6 RID: 214
			public int NSSlot;

			// Token: 0x040000D7 RID: 215
			private string name = string.Empty;

			// Token: 0x040000D8 RID: 216
			private string local_name = string.Empty;

			// Token: 0x040000D9 RID: 217
			private string ns = string.Empty;

			// Token: 0x040000DA RID: 218
			private string value;
		}

		// Token: 0x02000044 RID: 68
		private class AttrNodeInfo : XmlBinaryDictionaryReader.NodeInfo
		{
			// Token: 0x060001CF RID: 463 RVA: 0x0000A740 File Offset: 0x00008940
			public AttrNodeInfo(XmlBinaryDictionaryReader owner)
			{
				this.owner = owner;
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x0000A750 File Offset: 0x00008950
			public override void Reset()
			{
				base.Reset();
				this.ValueIndex = -1;
				this.NodeType = XmlNodeType.Attribute;
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A768 File Offset: 0x00008968
			public override string Value
			{
				get
				{
					return this.owner.attr_values[this.ValueIndex].Value;
				}
			}

			// Token: 0x040000DB RID: 219
			private XmlBinaryDictionaryReader owner;

			// Token: 0x040000DC RID: 220
			public int ValueIndex;
		}
	}
}
