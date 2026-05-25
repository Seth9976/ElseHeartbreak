using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000045 RID: 69
	internal class XmlBinaryDictionaryWriter : XmlDictionaryWriter
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A788 File Offset: 0x00008988
		public XmlBinaryDictionaryWriter(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream)
		{
			if (dictionary == null)
			{
				dictionary = new XmlDictionary();
			}
			if (session == null)
			{
				session = new XmlBinaryWriterSession();
			}
			this.original = new XmlBinaryDictionaryWriter.MyBinaryWriter(stream);
			this.writer = this.original;
			this.buffer_writer = new XmlBinaryDictionaryWriter.MyBinaryWriter(this.buffer);
			this.dict_ext = dictionary;
			this.session = session;
			this.owns_stream = ownsStream;
			this.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
			this.AddNamespace("xml", "http://www.w3.org/2000/xmlns/");
			this.ns_index = 2;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A880 File Offset: 0x00008A80
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A888 File Offset: 0x00008A88
		public override string XmlLang
		{
			get
			{
				return this.xml_lang;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A890 File Offset: 0x00008A90
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xml_space;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A898 File Offset: 0x00008A98
		private void AddMissingElementXmlns()
		{
			for (int i = this.ns_index; i < this.namespaces.Count; i++)
			{
				KeyValuePair<string, object> keyValuePair = this.namespaces[i];
				string key = keyValuePair.Key;
				string text = keyValuePair.Value as string;
				XmlDictionaryString xmlDictionaryString = keyValuePair.Value as XmlDictionaryString;
				if (text != null)
				{
					if (key.Length > 0)
					{
						this.writer.Write(9);
						this.writer.Write(key);
					}
					else
					{
						this.writer.Write(8);
					}
					this.writer.Write(text);
				}
				else
				{
					if (key.Length > 0)
					{
						this.writer.Write(11);
						this.writer.Write(key);
					}
					else
					{
						this.writer.Write(10);
					}
					this.WriteDictionaryIndex(xmlDictionaryString);
				}
			}
			this.ns_index = this.namespaces.Count;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A994 File Offset: 0x00008B94
		private void CheckState()
		{
			if (this.state == WriteState.Closed)
			{
				throw new InvalidOperationException("The Writer is closed.");
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		private void ProcessStateForContent()
		{
			this.CheckState();
			if (this.state == WriteState.Element)
			{
				this.CloseStartElement();
			}
			this.ProcessPendingBuffer(false, false);
			if (this.state != WriteState.Attribute)
			{
				this.writer = this.buffer_writer;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		private void ProcessTypedValue()
		{
			this.ProcessStateForContent();
			if (this.state == WriteState.Attribute)
			{
				if (this.attr_typed_value)
				{
					throw new InvalidOperationException(string.Format("A typed value for the attribute '{0}' in namespace '{1}' was already written", this.current_attr_name, this.current_attr_ns));
				}
				this.attr_typed_value = true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AA48 File Offset: 0x00008C48
		private void ProcessPendingBuffer(bool last, bool endElement)
		{
			if (this.buffer.Position > 0L)
			{
				byte[] array = this.buffer.GetBuffer();
				if (endElement)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] += 1;
				}
				this.original.Write(array, 0, (int)this.buffer.Position);
				this.buffer.SetLength(0L);
			}
			if (last)
			{
				this.writer = this.original;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public override void Close()
		{
			this.CloseOpenAttributeAndElements();
			if (this.owns_stream)
			{
				this.writer.Close();
			}
			else if (this.state != WriteState.Closed)
			{
				this.writer.Flush();
			}
			this.state = WriteState.Closed;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000AB0C File Offset: 0x00008D0C
		private void CloseOpenAttributeAndElements()
		{
			this.CloseStartElement();
			while (this.element_count > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AB2C File Offset: 0x00008D2C
		private void CloseStartElement()
		{
			if (!this.open_start_element)
			{
				return;
			}
			if (this.state == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			this.AddMissingElementXmlns();
			this.state = WriteState.Content;
			this.open_start_element = false;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public override string LookupPrefix(string ns)
		{
			if (ns == null || ns == string.Empty)
			{
				throw new ArgumentException("The Namespace cannot be empty.");
			}
			return this.namespaces.LastOrDefault((KeyValuePair<string, object> i) => i.Value.ToString() == ns).Key;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (count < 0)
			{
				throw new IndexOutOfRangeException("Negative count");
			}
			this.ProcessStateForContent();
			if (count < 256)
			{
				this.writer.Write(158);
				this.writer.Write((byte)count);
				this.writer.Write(buffer, index, count);
			}
			else if (count < 65536)
			{
				this.writer.Write(158);
				this.writer.Write((ushort)count);
				this.writer.Write(buffer, index, count);
			}
			else
			{
				this.writer.Write(162);
				this.writer.Write(count);
				this.writer.Write(buffer, index, count);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public override void WriteCData(string text)
		{
			if (text.IndexOf("]]>") >= 0)
			{
				throw new ArgumentException("CDATA section cannot contain text \"]]>\".");
			}
			this.ProcessStateForContent();
			this.WriteTextBinary(text);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public override void WriteCharEntity(char ch)
		{
			this.WriteChars(new char[] { ch }, 0, 1);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.ProcessStateForContent();
			int byteCount = Encoding.UTF8.GetByteCount(buffer, index, count);
			if (byteCount == 0)
			{
				this.writer.Write(168);
			}
			else if (count == 1 && buffer[0] == '0')
			{
				this.writer.Write(128);
			}
			else if (count == 1 && buffer[0] == '1')
			{
				this.writer.Write(130);
			}
			else if (byteCount < 256)
			{
				this.writer.Write(152);
				this.writer.Write((byte)byteCount);
				this.writer.Write(buffer, index, count);
			}
			else if (byteCount < 65536)
			{
				this.writer.Write(154);
				this.writer.Write((ushort)byteCount);
				this.writer.Write(buffer, index, count);
			}
			else
			{
				this.writer.Write(156);
				this.writer.Write(byteCount);
				this.writer.Write(buffer, index, count);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AE10 File Offset: 0x00009010
		public override void WriteComment(string text)
		{
			if (text.EndsWith("-"))
			{
				throw new ArgumentException("An XML comment cannot contain \"--\" inside.");
			}
			if (text.IndexOf("--") > 0)
			{
				throw new ArgumentException("An XML comment cannot end with \"-\".");
			}
			this.ProcessStateForContent();
			if (this.state == WriteState.Attribute)
			{
				throw new InvalidOperationException("Comment node is not allowed inside an attribute");
			}
			this.writer.Write(2);
			this.writer.Write(text);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AE8C File Offset: 0x0000908C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw new NotSupportedException("This XmlWriter implementation does not support document type.");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AE98 File Offset: 0x00009098
		public override void WriteEndAttribute()
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Token EndAttribute in state Start would result in an invalid XML document.");
			}
			this.CheckState();
			if (this.attr_value == null)
			{
				this.attr_value = string.Empty;
			}
			switch (this.save_target)
			{
			case XmlBinaryDictionaryWriter.SaveTarget.Namespaces:
				if (this.current_attr_name.ToString().Length > 0 && this.attr_value.Length == 0)
				{
					throw new ArgumentException("Cannot use prefix with an empty namespace.");
				}
				this.AddNamespaceChecked(this.current_attr_name.ToString(), this.attr_value);
				goto IL_0160;
			case XmlBinaryDictionaryWriter.SaveTarget.XmlLang:
				this.xml_lang = this.attr_value;
				break;
			case XmlBinaryDictionaryWriter.SaveTarget.XmlSpace:
			{
				string text = this.attr_value;
				if (text != null)
				{
					if (XmlBinaryDictionaryWriter.<>f__switch$map3 == null)
					{
						XmlBinaryDictionaryWriter.<>f__switch$map3 = new Dictionary<string, int>(2)
						{
							{ "preserve", 0 },
							{ "default", 1 }
						};
					}
					int num;
					if (XmlBinaryDictionaryWriter.<>f__switch$map3.TryGetValue(text, out num))
					{
						if (num != 0)
						{
							if (num != 1)
							{
								goto IL_00DC;
							}
							this.xml_space = XmlSpace.Default;
						}
						else
						{
							this.xml_space = XmlSpace.Preserve;
						}
						break;
					}
				}
				IL_00DC:
				throw new ArgumentException(string.Format("Invalid xml:space value: '{0}'", this.attr_value));
			}
			}
			if (!this.attr_typed_value)
			{
				this.WriteTextBinary(this.attr_value);
			}
			IL_0160:
			if (this.current_attr_prefix.Length > 0 && this.save_target != XmlBinaryDictionaryWriter.SaveTarget.Namespaces)
			{
				this.AddNamespaceChecked(this.current_attr_prefix, this.current_attr_ns);
			}
			this.state = WriteState.Element;
			this.current_attr_prefix = null;
			this.current_attr_name = null;
			this.current_attr_ns = null;
			this.attr_value = null;
			this.attr_typed_value = false;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B060 File Offset: 0x00009260
		public override void WriteEndDocument()
		{
			this.CloseOpenAttributeAndElements();
			WriteState writeState = this.state;
			if (writeState == WriteState.Start)
			{
				throw new InvalidOperationException("Document has not started.");
			}
			if (writeState != WriteState.Prolog)
			{
				this.state = WriteState.Start;
				return;
			}
			throw new ArgumentException("This document does not have a root element.");
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B0AC File Offset: 0x000092AC
		private bool SupportsCombinedEndElementSupport(byte operation)
		{
			return operation != 2;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B0CC File Offset: 0x000092CC
		public override void WriteEndElement()
		{
			if (this.element_count-- == 0)
			{
				throw new InvalidOperationException("There was no XML start tag open.");
			}
			if (this.state == WriteState.Attribute)
			{
				this.WriteEndAttribute();
			}
			bool flag = this.buffer.Position == 0L || !this.SupportsCombinedEndElementSupport(this.buffer.GetBuffer()[0]);
			this.ProcessPendingBuffer(true, !flag);
			this.CheckState();
			this.AddMissingElementXmlns();
			if (flag)
			{
				this.writer.Write(1);
			}
			this.element_ns = this.element_ns_stack.Pop();
			this.xml_lang = this.xml_lang_stack.Pop();
			this.xml_space = this.xml_space_stack.Pop();
			int count = this.namespaces.Count;
			this.ns_index = this.ns_index_stack.Pop();
			this.namespaces.RemoveRange(this.ns_index, count - this.ns_index);
			this.open_start_element = false;
			base.Depth--;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException("This XmlWriter implementation does not support entity references.");
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B1EC File Offset: 0x000093EC
		public override void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (name != "xml")
			{
				throw new ArgumentException("Processing instructions are not supported. ('xml' is allowed for XmlDeclaration; this is because of design problem of ECMA XmlWriter)");
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B214 File Offset: 0x00009414
		public override void WriteQualifiedName(XmlDictionaryString local, XmlDictionaryString ns)
		{
			string text = this.namespaces.LastOrDefault((KeyValuePair<string, object> i) => i.Value.ToString() == ns.ToString()).Key;
			bool flag = text != null;
			if (text == null)
			{
				text = this.LookupPrefix(ns.Value);
			}
			if (text == null)
			{
				throw new ArgumentException(string.Format("Namespace URI '{0}' is not bound to any of the prefixes", ns));
			}
			this.ProcessTypedValue();
			if (flag && text.Length == 1)
			{
				this.writer.Write(188);
				this.writer.Write((byte)(text[0] - 'a'));
				this.WriteDictionaryIndex(local);
			}
			else
			{
				this.WriteString(text);
				this.WriteString(":");
				this.WriteString(local);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B2FC File Offset: 0x000094FC
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteChars(buffer, index, count);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B308 File Offset: 0x00009508
		private void CheckStateForAttribute()
		{
			this.CheckState();
			if (this.state != WriteState.Element)
			{
				throw new InvalidOperationException("Token StartAttribute in state " + this.WriteState + " would result in an invalid XML document.");
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B348 File Offset: 0x00009548
		private string CreateNewPrefix()
		{
			return this.CreateNewPrefix(string.Empty);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B358 File Offset: 0x00009558
		private string CreateNewPrefix(string p)
		{
			XmlBinaryDictionaryWriter.<CreateNewPrefix>c__AnonStorey2 <CreateNewPrefix>c__AnonStorey = new XmlBinaryDictionaryWriter.<CreateNewPrefix>c__AnonStorey2();
			<CreateNewPrefix>c__AnonStorey.p = p;
			char c;
			for (c = 'a'; c <= 'z'; c += '\u0001')
			{
				if (!this.namespaces.Any((KeyValuePair<string, object> iter) => iter.Key == <CreateNewPrefix>c__AnonStorey.p + c))
				{
					return <CreateNewPrefix>c__AnonStorey.p + c;
				}
			}
			for (char c2 = 'a'; c2 <= 'z'; c2 += '\u0001')
			{
				string text = this.CreateNewPrefix(c2.ToString());
				if (text != null)
				{
					return text;
				}
			}
			throw new InvalidOperationException("too many prefix population");
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B410 File Offset: 0x00009610
		private bool CollectionContains(ICollection col, string value)
		{
			foreach (object obj in col)
			{
				string text = (string)obj;
				if (text == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B48C File Offset: 0x0000968C
		private void ProcessStartAttributeCommon(ref string prefix, string localName, string ns, object nameObj, object nsObj)
		{
			string text;
			if (prefix.Length == 0 && ns.Length > 0)
			{
				prefix = this.LookupPrefix(ns);
				if (string.IsNullOrEmpty(prefix))
				{
					prefix = this.CreateNewPrefix();
				}
			}
			else if (prefix.Length > 0 && ns.Length == 0)
			{
				text = prefix;
				if (text != null)
				{
					if (XmlBinaryDictionaryWriter.<>f__switch$map4 == null)
					{
						XmlBinaryDictionaryWriter.<>f__switch$map4 = new Dictionary<string, int>(2)
						{
							{ "xml", 0 },
							{ "xmlns", 1 }
						};
					}
					int num;
					if (XmlBinaryDictionaryWriter.<>f__switch$map4.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							ns = (nsObj = "http://www.w3.org/XML/1998/namespace");
							goto IL_00D7;
						}
						if (num == 1)
						{
							ns = (nsObj = "http://www.w3.org/2000/xmlns/");
							goto IL_00D7;
						}
					}
				}
				throw new ArgumentException("Cannot use prefix with an empty namespace.");
			}
			IL_00D7:
			if (prefix == "xmlns" && ns != "http://www.w3.org/2000/xmlns/")
			{
				throw new ArgumentException(string.Format("The 'xmlns' attribute is bound to the reserved namespace '{0}'", "http://www.w3.org/2000/xmlns/"));
			}
			this.CheckStateForAttribute();
			this.state = WriteState.Attribute;
			this.save_target = XmlBinaryDictionaryWriter.SaveTarget.None;
			text = prefix;
			if (text != null)
			{
				if (XmlBinaryDictionaryWriter.<>f__switch$map6 == null)
				{
					XmlBinaryDictionaryWriter.<>f__switch$map6 = new Dictionary<string, int>(2)
					{
						{ "xml", 0 },
						{ "xmlns", 1 }
					};
				}
				int num;
				if (XmlBinaryDictionaryWriter.<>f__switch$map6.TryGetValue(text, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							this.save_target = XmlBinaryDictionaryWriter.SaveTarget.Namespaces;
						}
					}
					else
					{
						ns = "http://www.w3.org/XML/1998/namespace";
						if (localName != null)
						{
							if (XmlBinaryDictionaryWriter.<>f__switch$map5 == null)
							{
								XmlBinaryDictionaryWriter.<>f__switch$map5 = new Dictionary<string, int>(2)
								{
									{ "lang", 0 },
									{ "space", 1 }
								};
							}
							int num2;
							if (XmlBinaryDictionaryWriter.<>f__switch$map5.TryGetValue(localName, out num2))
							{
								if (num2 != 0)
								{
									if (num2 == 1)
									{
										this.save_target = XmlBinaryDictionaryWriter.SaveTarget.XmlSpace;
									}
								}
								else
								{
									this.save_target = XmlBinaryDictionaryWriter.SaveTarget.XmlLang;
								}
							}
						}
					}
				}
			}
			this.current_attr_prefix = prefix;
			this.current_attr_name = nameObj;
			this.current_attr_ns = nsObj;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B6BC File Offset: 0x000098BC
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (localName == "xmlns" && prefix.Length == 0)
			{
				prefix = "xmlns";
				localName = string.Empty;
			}
			this.ProcessStartAttributeCommon(ref prefix, localName, ns, localName, ns);
			if (this.save_target == XmlBinaryDictionaryWriter.SaveTarget.Namespaces)
			{
				return;
			}
			byte b = ((prefix.Length != 1 || 'a' > prefix[0] || prefix[0] > 'z') ? ((prefix.Length != 0) ? 5 : 4) : ((byte)(prefix[0] - 'a' + '&')));
			if (38 <= b && b <= 63)
			{
				this.writer.Write(b);
				this.writer.Write(localName);
			}
			else
			{
				this.writer.Write(b);
				if (prefix.Length > 0)
				{
					this.writer.Write(prefix);
				}
				this.writer.Write(localName);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public override void WriteStartDocument()
		{
			this.WriteStartDocument(false);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B7DC File Offset: 0x000099DC
		public override void WriteStartDocument(bool standalone)
		{
			if (this.state != WriteState.Start)
			{
				throw new InvalidOperationException("WriteStartDocument should be the first call.");
			}
			this.CheckState();
			this.state = WriteState.Prolog;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B804 File Offset: 0x00009A04
		private void PrepareStartElement()
		{
			this.ProcessPendingBuffer(true, false);
			this.CheckState();
			this.CloseStartElement();
			base.Depth++;
			this.element_ns_stack.Push(this.element_ns);
			this.xml_lang_stack.Push(this.xml_lang);
			this.xml_space_stack.Push(this.xml_space);
			this.ns_index_stack.Push(this.ns_index);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B878 File Offset: 0x00009A78
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.PrepareStartElement();
			if (prefix != null && prefix != string.Empty && (ns == null || ns == string.Empty))
			{
				throw new ArgumentException("Cannot use a prefix with an empty namespace.");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (ns == string.Empty)
			{
				prefix = string.Empty;
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			byte b = ((prefix.Length != 1 || 'a' > prefix[0] || prefix[0] > 'z') ? ((prefix.Length != 0) ? 65 : 64) : ((byte)(prefix[0] - 'a' + '^')));
			if (94 <= b && b <= 119)
			{
				this.writer.Write(b);
				this.writer.Write(localName);
			}
			else
			{
				this.writer.Write(b);
				if (prefix.Length > 0)
				{
					this.writer.Write(prefix);
				}
				this.writer.Write(localName);
			}
			this.OpenElement(prefix, ns);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		private void OpenElement(string prefix, object nsobj)
		{
			string text = nsobj.ToString();
			this.state = WriteState.Element;
			this.open_start_element = true;
			this.element_prefix = prefix;
			this.element_count++;
			this.element_ns = nsobj.ToString();
			if (this.element_ns != string.Empty && this.LookupPrefix(this.element_ns) != prefix)
			{
				this.AddNamespace(prefix, nsobj);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000BA20 File Offset: 0x00009C20
		private void AddNamespace(string prefix, object nsobj)
		{
			this.namespaces.Add(new KeyValuePair<string, object>(prefix, nsobj));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000BA34 File Offset: 0x00009C34
		private void CheckIfTextAllowed()
		{
			WriteState writeState = this.state;
			if (writeState != WriteState.Start && writeState != WriteState.Prolog)
			{
				return;
			}
			throw new InvalidOperationException("Token content in state Prolog would result in an invalid XML document.");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000BA68 File Offset: 0x00009C68
		public override void WriteString(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this.CheckIfTextAllowed();
			if (text == null)
			{
				text = string.Empty;
			}
			this.ProcessStateForContent();
			if (this.state == WriteState.Attribute)
			{
				this.attr_value += text;
			}
			else
			{
				this.WriteTextBinary(text);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000BACC File Offset: 0x00009CCC
		public override void WriteString(XmlDictionaryString text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this.CheckIfTextAllowed();
			if (text == null)
			{
				text = XmlDictionaryString.Empty;
			}
			this.ProcessStateForContent();
			if (this.state == WriteState.Attribute)
			{
				this.attr_value += text.Value;
			}
			else if (text.Equals(XmlDictionary.Empty))
			{
				this.writer.Write(168);
			}
			else
			{
				this.writer.Write(170);
				this.WriteDictionaryIndex(text);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000BB68 File Offset: 0x00009D68
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteChars(new char[] { highChar, lowChar }, 0, 2);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000BB80 File Offset: 0x00009D80
		public override void WriteWhitespace(string ws)
		{
			foreach (char c in ws)
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					if (c != ' ')
					{
						throw new ArgumentException("Invalid Whitespace");
					}
					break;
				}
			}
			this.ProcessStateForContent();
			this.WriteTextBinary(ws);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		public override void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			if (string.IsNullOrEmpty(prefix))
			{
				prefix = this.CreateNewPrefix();
			}
			this.CheckStateForAttribute();
			this.AddNamespaceChecked(prefix, namespaceUri);
			this.state = WriteState.Element;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000BC3C File Offset: 0x00009E3C
		private void AddNamespaceChecked(string prefix, object ns)
		{
			string text = ns.ToString();
			if (text != null)
			{
				if (XmlBinaryDictionaryWriter.<>f__switch$map7 == null)
				{
					XmlBinaryDictionaryWriter.<>f__switch$map7 = new Dictionary<string, int>(2)
					{
						{ "http://www.w3.org/2000/xmlns/", 0 },
						{ "http://www.w3.org/XML/1998/namespace", 0 }
					};
				}
				int num;
				if (XmlBinaryDictionaryWriter.<>f__switch$map7.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return;
					}
				}
			}
			if (prefix == null)
			{
				throw new InvalidOperationException();
			}
			KeyValuePair<string, object> keyValuePair = this.namespaces.LastOrDefault((KeyValuePair<string, object> i) => i.Key == prefix);
			if (keyValuePair.Key != null)
			{
				if (keyValuePair.Value.ToString() != ns.ToString())
				{
					if (this.namespaces.LastIndexOf(keyValuePair) >= this.ns_index)
					{
						throw new ArgumentException(string.Format("The prefix '{0}' is already mapped to another namespace URI '{1}' in this element scope and cannot be mapped to '{2}'", prefix ?? "(null)", keyValuePair.Value ?? "(null)", ns.ToString()));
					}
					this.AddNamespace(prefix, ns);
				}
			}
			else
			{
				this.AddNamespace(prefix, ns);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BD74 File Offset: 0x00009F74
		private void WriteDictionaryIndex(XmlDictionaryString ds)
		{
			bool flag = false;
			int key = ds.Key;
			if (ds.Dictionary != this.dict_ext)
			{
				flag = true;
				XmlDictionaryString xmlDictionaryString;
				if (this.dict_int.TryLookup(ds.Value, out xmlDictionaryString))
				{
					ds = xmlDictionaryString;
				}
				if (!this.session.TryLookup(ds, out key))
				{
					this.session.TryAdd(this.dict_int.Add(ds.Value), out key);
				}
			}
			if (key >= 128)
			{
				this.writer.Write((byte)(128 + (key % 128 << 1) + ((!flag) ? 0 : 1)));
				this.writer.Write((byte)((byte)(key / 128) << 1));
			}
			else
			{
				this.writer.Write((byte)((key % 128 << 1) + ((!flag) ? 0 : 1)));
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public override void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.PrepareStartElement();
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			byte b = ((prefix.Length != 1 || 'a' > prefix[0] || prefix[0] > 'z') ? ((prefix.Length != 0) ? 67 : 66) : ((byte)(prefix[0] - 'a' + 'D')));
			if (68 <= b && b <= 93)
			{
				this.writer.Write(b);
				this.WriteDictionaryIndex(localName);
			}
			else
			{
				this.writer.Write(b);
				if (prefix.Length > 0)
				{
					this.writer.Write(prefix);
				}
				this.WriteDictionaryIndex(localName);
			}
			this.OpenElement(prefix, namespaceUri);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000BF28 File Offset: 0x0000A128
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = XmlDictionaryString.Empty;
			}
			if (localName.Value == "xmlns" && prefix.Length == 0)
			{
				prefix = "xmlns";
				localName = XmlDictionaryString.Empty;
			}
			this.ProcessStartAttributeCommon(ref prefix, localName.Value, ns.Value, localName, ns);
			if (this.save_target == XmlBinaryDictionaryWriter.SaveTarget.Namespaces)
			{
				return;
			}
			if (prefix.Length == 1 && 'a' <= prefix[0] && prefix[0] <= 'z')
			{
				this.writer.Write((byte)(prefix[0] - 'a' + '\f'));
				this.WriteDictionaryIndex(localName);
			}
			else
			{
				byte b = ((ns.Value.Length != 0) ? 7 : 6);
				this.writer.Write(b);
				if (prefix.Length > 0)
				{
					this.writer.Write(prefix);
				}
				this.WriteDictionaryIndex(localName);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C044 File Offset: 0x0000A244
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			if (string.IsNullOrEmpty(prefix))
			{
				prefix = this.CreateNewPrefix();
			}
			this.CheckStateForAttribute();
			this.AddNamespaceChecked(prefix, namespaceUri);
			this.state = WriteState.Element;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C08C File Offset: 0x0000A28C
		public override void WriteValue(bool value)
		{
			this.ProcessTypedValue();
			this.writer.Write((!value) ? 132 : 134);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		public override void WriteValue(int value)
		{
			this.WriteValue((long)value);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public override void WriteValue(long value)
		{
			this.ProcessTypedValue();
			if (value == 0L)
			{
				this.writer.Write(128);
			}
			else if (value == 1L)
			{
				this.writer.Write(130);
			}
			else if (value < 0L || value > (long)((ulong)(-1)))
			{
				this.writer.Write(142);
				for (int i = 0; i < 8; i++)
				{
					this.writer.Write((byte)(value & 255L));
					value >>= 8;
				}
			}
			else if (value <= 255L)
			{
				this.writer.Write(136);
				this.writer.Write((byte)value);
			}
			else if (value <= 32767L)
			{
				this.writer.Write(138);
				this.writer.Write((byte)(value & 255L));
				this.writer.Write((byte)(value >> 8));
			}
			else if (value <= 2147483647L)
			{
				this.writer.Write(140);
				for (int j = 0; j < 4; j++)
				{
					this.writer.Write((byte)(value & 255L));
					value >>= 8;
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C220 File Offset: 0x0000A420
		public override void WriteValue(float value)
		{
			this.ProcessTypedValue();
			this.writer.Write(144);
			this.WriteValueContent(value);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C240 File Offset: 0x0000A440
		private void WriteValueContent(float value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C250 File Offset: 0x0000A450
		public override void WriteValue(double value)
		{
			this.ProcessTypedValue();
			this.writer.Write(146);
			this.WriteValueContent(value);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C270 File Offset: 0x0000A470
		private void WriteValueContent(double value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C280 File Offset: 0x0000A480
		public override void WriteValue(decimal value)
		{
			this.ProcessTypedValue();
			this.writer.Write(148);
			this.WriteValueContent(value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private void WriteValueContent(decimal value)
		{
			int[] bits = decimal.GetBits(value);
			this.writer.Write(bits[3]);
			this.writer.Write(bits[2]);
			this.writer.Write(bits[0]);
			this.writer.Write(bits[1]);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public override void WriteValue(DateTime value)
		{
			this.ProcessTypedValue();
			this.writer.Write(150);
			this.WriteValueContent(value);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C30C File Offset: 0x0000A50C
		private void WriteValueContent(DateTime value)
		{
			this.writer.Write(value.Ticks);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C320 File Offset: 0x0000A520
		public override void WriteValue(Guid value)
		{
			this.ProcessTypedValue();
			this.writer.Write(176);
			this.WriteValueContent(value);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C340 File Offset: 0x0000A540
		private void WriteValueContent(Guid value)
		{
			byte[] array = value.ToByteArray();
			this.writer.Write(array, 0, array.Length);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C368 File Offset: 0x0000A568
		public override void WriteValue(UniqueId value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Guid guid;
			if (value.TryGetGuid(out guid))
			{
				this.ProcessTypedValue();
				this.writer.Write(172);
				byte[] array = guid.ToByteArray();
				this.writer.Write(array, 0, array.Length);
			}
			else
			{
				this.WriteValue(value.ToString());
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public override void WriteValue(TimeSpan value)
		{
			this.ProcessTypedValue();
			this.writer.Write(174);
			this.WriteValueContent(value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		private void WriteValueContent(TimeSpan value)
		{
			this.WriteBigEndian(value.Ticks, 8);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C408 File Offset: 0x0000A608
		private void WriteBigEndian(long value, int digits)
		{
			long num = 0L;
			for (int i = 0; i < digits; i++)
			{
				num = (num << 8) + (value & 255L);
				value >>= 8;
			}
			for (int j = 0; j < digits; j++)
			{
				this.writer.Write((byte)(num & 255L));
				num >>= 8;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C468 File Offset: 0x0000A668
		private void WriteTextBinary(string text)
		{
			if (text.Length == 0)
			{
				this.writer.Write(168);
			}
			else
			{
				char[] array = text.ToCharArray();
				this.WriteChars(array, 0, array.Length);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		private void WriteValueContent(bool value)
		{
			this.writer.Write((!value) ? 0 : 1);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		private void WriteValueContent(short value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		private void WriteValueContent(int value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		private void WriteValueContent(long value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		private void CheckWriteArrayArguments(Array array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is negative");
			}
			if (offset > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the length of the destination array");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length is negative");
			}
			if (length > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("length + offset exceeds the length of the destination array");
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C568 File Offset: 0x0000A768
		private void CheckDictionaryStringArgs(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C598 File Offset: 0x0000A798
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		public override void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C614 File Offset: 0x0000A814
		private void WriteArrayRemaining(bool[] array, int offset, int length)
		{
			this.writer.Write(181);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C65C File Offset: 0x0000A85C
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C698 File Offset: 0x0000A898
		public override void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		private void WriteArrayRemaining(DateTime[] array, int offset, int length)
		{
			this.writer.Write(151);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C728 File Offset: 0x0000A928
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C764 File Offset: 0x0000A964
		public override void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		private void WriteArrayRemaining(decimal[] array, int offset, int length)
		{
			this.writer.Write(149);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C830 File Offset: 0x0000AA30
		public override void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C870 File Offset: 0x0000AA70
		private void WriteArrayRemaining(double[] array, int offset, int length)
		{
			this.writer.Write(147);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public override void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C934 File Offset: 0x0000AB34
		private void WriteArrayRemaining(Guid[] array, int offset, int length)
		{
			this.writer.Write(177);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C984 File Offset: 0x0000AB84
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public override void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000CA00 File Offset: 0x0000AC00
		private void WriteArrayRemaining(short[] array, int offset, int length)
		{
			this.writer.Write(139);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public override void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		private void WriteArrayRemaining(int[] array, int offset, int length)
		{
			this.writer.Write(141);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public override void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CB88 File Offset: 0x0000AD88
		private void WriteArrayRemaining(long[] array, int offset, int length)
		{
			this.writer.Write(143);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		public override void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		private void WriteArrayRemaining(float[] array, int offset, int length)
		{
			this.writer.Write(145);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000CC94 File Offset: 0x0000AE94
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public override void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			this.writer.Write(3);
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteEndElement();
			this.WriteArrayRemaining(array, offset, length);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000CD10 File Offset: 0x0000AF10
		private void WriteArrayRemaining(TimeSpan[] array, int offset, int length)
		{
			this.writer.Write(175);
			this.writer.WriteFlexibleInt(length);
			for (int i = offset; i < offset + length; i++)
			{
				this.WriteValueContent(array[i]);
			}
		}

		// Token: 0x040000DD RID: 221
		private const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x040000DE RID: 222
		private const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040000DF RID: 223
		private XmlBinaryDictionaryWriter.MyBinaryWriter original;

		// Token: 0x040000E0 RID: 224
		private XmlBinaryDictionaryWriter.MyBinaryWriter writer;

		// Token: 0x040000E1 RID: 225
		private XmlBinaryDictionaryWriter.MyBinaryWriter buffer_writer;

		// Token: 0x040000E2 RID: 226
		private IXmlDictionary dict_ext;

		// Token: 0x040000E3 RID: 227
		private XmlDictionary dict_int = new XmlDictionary();

		// Token: 0x040000E4 RID: 228
		private XmlBinaryWriterSession session;

		// Token: 0x040000E5 RID: 229
		private bool owns_stream;

		// Token: 0x040000E6 RID: 230
		private Encoding utf8Enc = new UTF8Encoding();

		// Token: 0x040000E7 RID: 231
		private MemoryStream buffer = new MemoryStream();

		// Token: 0x040000E8 RID: 232
		private WriteState state;

		// Token: 0x040000E9 RID: 233
		private bool open_start_element;

		// Token: 0x040000EA RID: 234
		private List<KeyValuePair<string, object>> namespaces = new List<KeyValuePair<string, object>>();

		// Token: 0x040000EB RID: 235
		private string xml_lang;

		// Token: 0x040000EC RID: 236
		private XmlSpace xml_space;

		// Token: 0x040000ED RID: 237
		private int ns_index;

		// Token: 0x040000EE RID: 238
		private Stack<int> ns_index_stack = new Stack<int>();

		// Token: 0x040000EF RID: 239
		private Stack<string> xml_lang_stack = new Stack<string>();

		// Token: 0x040000F0 RID: 240
		private Stack<XmlSpace> xml_space_stack = new Stack<XmlSpace>();

		// Token: 0x040000F1 RID: 241
		private Stack<string> element_ns_stack = new Stack<string>();

		// Token: 0x040000F2 RID: 242
		private string element_ns = string.Empty;

		// Token: 0x040000F3 RID: 243
		private int element_count;

		// Token: 0x040000F4 RID: 244
		private string element_prefix;

		// Token: 0x040000F5 RID: 245
		private string attr_value;

		// Token: 0x040000F6 RID: 246
		private string current_attr_prefix;

		// Token: 0x040000F7 RID: 247
		private object current_attr_name;

		// Token: 0x040000F8 RID: 248
		private object current_attr_ns;

		// Token: 0x040000F9 RID: 249
		private bool attr_typed_value;

		// Token: 0x040000FA RID: 250
		private XmlBinaryDictionaryWriter.SaveTarget save_target;

		// Token: 0x02000046 RID: 70
		private class MyBinaryWriter : BinaryWriter
		{
			// Token: 0x0600023D RID: 573 RVA: 0x0000CD60 File Offset: 0x0000AF60
			public MyBinaryWriter(Stream s)
				: base(s)
			{
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000CD6C File Offset: 0x0000AF6C
			public void WriteFlexibleInt(int value)
			{
				base.Write7BitEncodedInt(value);
			}
		}

		// Token: 0x02000047 RID: 71
		private enum SaveTarget
		{
			// Token: 0x04000101 RID: 257
			None,
			// Token: 0x04000102 RID: 258
			Namespaces,
			// Token: 0x04000103 RID: 259
			XmlLang,
			// Token: 0x04000104 RID: 260
			XmlSpace
		}
	}
}
