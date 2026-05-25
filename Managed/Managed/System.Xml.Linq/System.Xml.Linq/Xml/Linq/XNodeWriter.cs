using System;

namespace System.Xml.Linq
{
	// Token: 0x0200001E RID: 30
	internal class XNodeWriter : XmlWriter
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00008128 File Offset: 0x00006328
		public XNodeWriter(XContainer fragment)
		{
			this.root = fragment;
			this.state = XmlNodeType.None;
			this.current = fragment;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008148 File Offset: 0x00006348
		public override WriteState WriteState
		{
			get
			{
				if (this.is_closed)
				{
					return WriteState.Closed;
				}
				if (this.attribute != null)
				{
					return WriteState.Attribute;
				}
				XmlNodeType xmlNodeType = this.state;
				if (xmlNodeType == XmlNodeType.None)
				{
					return WriteState.Start;
				}
				if (xmlNodeType == XmlNodeType.DocumentType)
				{
					return WriteState.Element;
				}
				if (xmlNodeType != XmlNodeType.XmlDeclaration)
				{
					return WriteState.Content;
				}
				return WriteState.Prolog;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008198 File Offset: 0x00006398
		private void CheckState()
		{
			if (this.is_closed)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000081AC File Offset: 0x000063AC
		private void WritePossiblyTopLevelNode(XNode n, bool possiblyAttribute)
		{
			this.CheckState();
			if (!possiblyAttribute && this.attribute != null)
			{
				throw new InvalidOperationException(string.Format("Current state is not acceptable for {0}.", n.NodeType));
			}
			if (this.state != XmlNodeType.Element)
			{
				this.root.Add(n);
			}
			else if (this.attribute != null)
			{
				XAttribute xattribute = this.attribute;
				xattribute.Value += XUtil.ToString(n);
			}
			else
			{
				this.current.Add(n);
			}
			if (this.state == XmlNodeType.None)
			{
				this.state = XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008254 File Offset: 0x00006454
		private void FillXmlns(XElement el, string prefix, XNamespace xns)
		{
			if (xns == XNamespace.Xmlns)
			{
				return;
			}
			if (xns == XNamespace.None)
			{
				if (el.GetPrefixOfNamespace(xns) != prefix)
				{
					el.SetAttributeValue((!(prefix == string.Empty)) ? XNamespace.Xmlns.GetName(prefix) : XNamespace.None.GetName("xmlns"), xns.NamespaceName);
				}
				else if (el.GetDefaultNamespace() != XNamespace.None)
				{
					el.SetAttributeValue(XNamespace.None.GetName("xmlns"), xns.NamespaceName);
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008304 File Offset: 0x00006504
		public override void Close()
		{
			this.CheckState();
			this.is_closed = true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008314 File Offset: 0x00006514
		public override void Flush()
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008318 File Offset: 0x00006518
		public override string LookupPrefix(string ns)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException();
			}
			XElement xelement = (this.current as XElement) ?? this.current.Parent;
			return (xelement == null) ? null : xelement.GetPrefixOfNamespace(XNamespace.Get(ns));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008374 File Offset: 0x00006574
		public override void WriteStartDocument()
		{
			this.WriteStartDocument(null);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008380 File Offset: 0x00006580
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocument((!standalone) ? "no" : "yes");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000083A0 File Offset: 0x000065A0
		private void WriteStartDocument(string sddecl)
		{
			this.CheckState();
			if (this.state != XmlNodeType.None)
			{
				throw new InvalidOperationException("Current state is not acceptable for xmldecl.");
			}
			XDocument xdocument = this.current as XDocument;
			if (xdocument == null)
			{
				throw new InvalidOperationException("Only document node can accept xml declaration");
			}
			xdocument.Declaration = new XDeclaration("1.0", null, sddecl);
			this.state = XmlNodeType.XmlDeclaration;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008400 File Offset: 0x00006600
		public override void WriteEndDocument()
		{
			this.CheckState();
			this.is_closed = true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008410 File Offset: 0x00006610
		public override void WriteDocType(string name, string publicId, string systemId, string internalSubset)
		{
			this.CheckState();
			XmlNodeType xmlNodeType = this.state;
			if (xmlNodeType != XmlNodeType.None && xmlNodeType != XmlNodeType.XmlDeclaration)
			{
				throw new InvalidOperationException("Current state is not acceptable for doctype.");
			}
			XDocument xdocument = this.current as XDocument;
			if (xdocument == null)
			{
				throw new InvalidOperationException("Only document node can accept doctype declaration");
			}
			xdocument.Add(new XDocumentType(name, publicId, systemId, internalSubset));
			this.state = XmlNodeType.DocumentType;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008484 File Offset: 0x00006684
		public override void WriteStartElement(string prefix, string name, string ns)
		{
			this.CheckState();
			XNamespace xnamespace = XNamespace.Get(ns ?? string.Empty);
			XElement xelement = new XElement(xnamespace.GetName(name));
			if (this.current == null)
			{
				this.root.Add(xelement);
				this.state = XmlNodeType.Element;
			}
			else
			{
				this.current.Add(xelement);
				this.state = XmlNodeType.Element;
			}
			this.FillXmlns(xelement, prefix ?? string.Empty, xnamespace);
			this.current = xelement;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000850C File Offset: 0x0000670C
		public override void WriteEndElement()
		{
			this.WriteEndElementInternal(false);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008518 File Offset: 0x00006718
		public override void WriteFullEndElement()
		{
			this.WriteEndElementInternal(true);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008524 File Offset: 0x00006724
		private void WriteEndElementInternal(bool forceFull)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for endElement.");
			}
			XElement xelement = this.current as XElement;
			if (forceFull)
			{
				xelement.IsEmpty = false;
			}
			this.current = this.current.Parent;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008578 File Offset: 0x00006778
		public override void WriteStartAttribute(string prefix, string name, string ns)
		{
			this.CheckState();
			if (this.attribute != null)
			{
				throw new InvalidOperationException("There is an open attribute.");
			}
			XElement xelement = this.current as XElement;
			if (xelement == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for startAttribute.");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (prefix.Length == 0 && name == "xmlns" && ns == XNamespace.Xmlns.NamespaceName)
			{
				ns = string.Empty;
			}
			XNamespace xnamespace = XNamespace.Get(ns);
			xelement.SetAttributeValue(xnamespace.GetName(name), string.Empty);
			this.attribute = xelement.LastAttribute;
			this.FillXmlns(xelement, prefix, xnamespace);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008630 File Offset: 0x00006830
		public override void WriteEndAttribute()
		{
			this.CheckState();
			if (this.attribute == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for startAttribute.");
			}
			this.attribute = null;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008658 File Offset: 0x00006858
		public override void WriteCData(string data)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for CDATAsection.");
			}
			this.current.Add(new XCData(data));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008688 File Offset: 0x00006888
		public override void WriteComment(string comment)
		{
			this.WritePossiblyTopLevelNode(new XComment(comment), false);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008698 File Offset: 0x00006898
		public override void WriteProcessingInstruction(string name, string value)
		{
			this.WritePossiblyTopLevelNode(new XProcessingInstruction(name, value), false);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000086A8 File Offset: 0x000068A8
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000086B0 File Offset: 0x000068B0
		public override void WriteCharEntity(char c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000086B8 File Offset: 0x000068B8
		public override void WriteWhitespace(string ws)
		{
			this.WritePossiblyTopLevelNode(new XText(ws), true);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000086C8 File Offset: 0x000068C8
		public override void WriteString(string data)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for Text.");
			}
			if (this.attribute != null)
			{
				XAttribute xattribute = this.attribute;
				xattribute.Value += data;
			}
			else
			{
				this.current.Add(data);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008724 File Offset: 0x00006924
		public override void WriteName(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008730 File Offset: 0x00006930
		public override void WriteNmToken(string nmtoken)
		{
			this.WriteString(nmtoken);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000873C File Offset: 0x0000693C
		public override void WriteQualifiedName(string name, string ns)
		{
			string text = this.LookupPrefix(ns);
			if (text == null)
			{
				throw new ArgumentException(string.Format("Invalid namespace {0}", ns));
			}
			if (text != string.Empty)
			{
				this.WriteString(name);
			}
			else
			{
				this.WriteString(text + ":" + name);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008798 File Offset: 0x00006998
		public override void WriteChars(char[] chars, int start, int len)
		{
			this.WriteString(new string(chars, start, len));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000087A8 File Offset: 0x000069A8
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000087B4 File Offset: 0x000069B4
		public override void WriteRaw(char[] chars, int start, int len)
		{
			this.WriteChars(chars, start, len);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000087C0 File Offset: 0x000069C0
		public override void WriteBase64(byte[] data, int start, int len)
		{
			this.WriteString(Convert.ToBase64String(data, start, len));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000087D0 File Offset: 0x000069D0
		public override void WriteBinHex(byte[] data, int start, int len)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000087D8 File Offset: 0x000069D8
		public override void WriteSurrogateCharEntity(char c1, char c2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000060 RID: 96
		private XContainer root;

		// Token: 0x04000061 RID: 97
		private bool is_closed;

		// Token: 0x04000062 RID: 98
		private XContainer current;

		// Token: 0x04000063 RID: 99
		private XAttribute attribute;

		// Token: 0x04000064 RID: 100
		private XmlNodeType state;
	}
}
