using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>An abstract class that the Windows Communication Foundation (WCF) derives from to do serialization and deserialization.</summary>
	// Token: 0x02000050 RID: 80
	public abstract class XmlDictionaryWriter : XmlWriter
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		/// <summary>This property always returns false. Its derived classes can override to return true if they support canonicalization.</summary>
		/// <returns>false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public virtual bool CanCanonicalize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		// Token: 0x060002F2 RID: 754 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, null, null, false);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		// Token: 0x060002F3 RID: 755 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, dictionary, null, false);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryWriterSession" /> to use.</param>
		// Token: 0x060002F4 RID: 756 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session)
		{
			return XmlDictionaryWriter.CreateBinaryWriter(stream, dictionary, session, false);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes WCF binary XML format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use as the shared dictionary.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryWriterSession" /> to use.</param>
		/// <param name="ownsStream">If true, stream is closed by the writer when done; otherwise false.</param>
		// Token: 0x060002F5 RID: 757 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public static XmlDictionaryWriter CreateBinaryWriter(Stream stream, IXmlDictionary dictionary, XmlBinaryWriterSession session, bool ownsStream)
		{
			return new XmlBinaryDictionaryWriter(stream, dictionary, session, ownsStream);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> from an existing <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="writer">An instance of <see cref="T:System.Xml.XmlWriter" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x0000EC14 File Offset: 0x0000CE14
		public static XmlDictionaryWriter CreateDictionaryWriter(XmlWriter writer)
		{
			return new XmlSimpleDictionaryWriter(writer);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="maxSizeInBytes">The maximum number of bytes that are buffered in the writer.</param>
		/// <param name="startInfo">An attribute in the ContentType SOAP header.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002F7 RID: 759 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public static XmlDictionaryWriter CreateMtomWriter(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo)
		{
			return XmlDictionaryWriter.CreateMtomWriter(stream, encoding, maxSizeInBytes, startInfo, Guid.NewGuid() + "id=1", "http://tempuri.org/0/" + DateTime.Now.Ticks, true, false);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="maxSizeInBytes">The maximum number of bytes that are buffered in the writer.</param>
		/// <param name="startInfo">The content-type of the MIME part that contains the Infoset.</param>
		/// <param name="boundary">The MIME boundary in the message.</param>
		/// <param name="startUri">The content-id URI of the MIME part that contains the Infoset.</param>
		/// <param name="writeMessageHeaders">If true, write message headers.</param>
		/// <param name="ownsStream">If true, the stream is closed by the writer when done; otherwise false.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002F8 RID: 760 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public static XmlDictionaryWriter CreateMtomWriter(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream)
		{
			return new XmlMtomDictionaryWriter(stream, encoding, maxSizeInBytes, startInfo, boundary, startUri, writeMessageHeaders, ownsStream);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML. </summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		// Token: 0x060002F9 RID: 761 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public static XmlDictionaryWriter CreateTextWriter(Stream stream)
		{
			return XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the output.</param>
		// Token: 0x060002FA RID: 762 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public static XmlDictionaryWriter CreateTextWriter(Stream stream, Encoding encoding)
		{
			return XmlDictionaryWriter.CreateTextWriter(stream, encoding, false);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryWriter" /> that writes text XML.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryWriter" />.</returns>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="ownsStream">If true, stream is closed by the writer when done; otherwise false.</param>
		// Token: 0x060002FB RID: 763 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public static XmlDictionaryWriter CreateTextWriter(Stream stream, Encoding encoding, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int codePage = encoding.CodePage;
			if (codePage != 1200 && codePage != 1201 && codePage != 65001)
			{
				throw new XmlException(string.Format("XML declaration is required for encoding code page {0} but this XmlWriter does not support XML declaration.", encoding.CodePage));
			}
			encoding = XmlDictionaryWriter.utf8_unmarked;
			return XmlDictionaryWriter.CreateDictionaryWriter(XmlWriter.Create(stream, new XmlWriterSettings
			{
				Encoding = encoding,
				CloseOutput = ownsStream,
				OmitXmlDeclaration = true
			}));
		}

		/// <summary>This method is not yet implemented. </summary>
		/// <exception cref="T:System.NotSupportedException">Method is not implemented yet.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002FC RID: 764 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		public virtual void EndCanonicalization()
		{
			throw new NotSupportedException();
		}

		/// <summary>This method is not yet implemented. </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="includeComments">Determines whether comments are included.</param>
		/// <param name="inclusivePrefixes">The prefixes to be included.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not implemented yet.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002FD RID: 765 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public virtual void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			throw new NotSupportedException();
		}

		/// <summary>Writes an attribute qualified name and value.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		/// <param name="value">The attribute.</param>
		// Token: 0x060002FE RID: 766 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public void WriteAttributeString(XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteAttributeString(null, localName, namespaceUri, value);
		}

		/// <summary>Writes an attribute qualified name and value.</summary>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		/// <param name="value">The attribute.</param>
		// Token: 0x060002FF RID: 767 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public void WriteAttributeString(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteStartAttribute(prefix, localName, namespaceUri);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		/// <summary>Writes an element with a text content.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="value">The element content.</param>
		// Token: 0x06000300 RID: 768 RVA: 0x0000ED84 File Offset: 0x0000CF84
		public void WriteElementString(XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteElementString(null, localName, namespaceUri, value);
		}

		/// <summary>Writes an element with a text content.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="value">The element content.</param>
		// Token: 0x06000301 RID: 769 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public void WriteElementString(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, string value)
		{
			this.WriteStartElement(prefix, localName, namespaceUri);
			this.WriteString(value);
			this.WriteEndElement();
		}

		/// <summary>Writes the current XML node from an <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" />.</param>
		/// <param name="defattr">If true, copy the default attributes from the XmlReader; otherwise if true, use default attributes; otherwise false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		// Token: 0x06000302 RID: 770 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		public virtual void WriteNode(XmlDictionaryReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			switch (reader.NodeType)
			{
			case XmlNodeType.Element:
			{
				XmlDictionaryString xmlDictionaryString;
				XmlDictionaryString xmlDictionaryString2;
				if (reader.TryGetLocalNameAsDictionaryString(out xmlDictionaryString) && reader.TryGetLocalNameAsDictionaryString(out xmlDictionaryString2))
				{
					this.WriteStartElement(reader.Prefix, xmlDictionaryString, xmlDictionaryString2);
				}
				else
				{
					this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
				}
				if (reader.HasAttributes)
				{
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						reader.MoveToAttribute(i);
						this.WriteAttribute(reader, defattr);
					}
					reader.MoveToElement();
				}
				if (reader.IsEmptyElement)
				{
					this.WriteEndElement();
				}
				else
				{
					int num = reader.Depth;
					reader.Read();
					if (reader.NodeType != XmlNodeType.EndElement)
					{
						do
						{
							this.WriteNode(reader, defattr);
						}
						while (num < reader.Depth);
					}
					this.WriteFullEndElement();
				}
				reader.Read();
				break;
			}
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
				this.WriteTextNode(reader, defattr);
				break;
			default:
				base.WriteNode(reader, defattr);
				break;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		private void WriteAttribute(XmlDictionaryReader reader, bool defattr)
		{
			if (!defattr && reader.IsDefault)
			{
				return;
			}
			XmlDictionaryString xmlDictionaryString;
			XmlDictionaryString xmlDictionaryString2;
			if (reader.TryGetLocalNameAsDictionaryString(out xmlDictionaryString) && reader.TryGetLocalNameAsDictionaryString(out xmlDictionaryString2))
			{
				this.WriteStartAttribute(reader.Prefix, xmlDictionaryString, xmlDictionaryString2);
			}
			else
			{
				this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
			}
			while (reader.ReadAttributeValue())
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Text:
					this.WriteTextNode(reader, true);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				}
			}
			this.WriteEndAttribute();
		}

		/// <summary>Writes the current XML node from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" />.</param>
		/// <param name="defattr">If true, copy the default attributes from the <see cref="T:System.Xml.XmlReader" />; otherwise false.If true, use default attributes; otherwise false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		// Token: 0x06000304 RID: 772 RVA: 0x0000EF94 File Offset: 0x0000D194
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			XmlDictionaryReader xmlDictionaryReader = reader as XmlDictionaryReader;
			if (xmlDictionaryReader != null)
			{
				this.WriteNode(xmlDictionaryReader, defattr);
			}
			else
			{
				base.WriteNode(reader, defattr);
			}
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name of the qualified name.</param>
		/// <param name="namespaceUri">The namespace URI of the qualified name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localName" /> is null. </exception>
		// Token: 0x06000305 RID: 773 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public virtual void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteQualifiedName(localName.Value, namespaceUri.Value);
		}

		/// <summary>Writes the start of an attribute with the specified local name, and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		// Token: 0x06000306 RID: 774 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public void WriteStartAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			base.WriteStartAttribute(localName.Value, namespaceUri.Value);
		}

		/// <summary>Writes the start of an attribute with the specified prefix, local name, and namespace URI.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute.</param>
		// Token: 0x06000307 RID: 775 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public virtual void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartAttribute(prefix, localName.Value, namespaceUri.Value);
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000308 RID: 776 RVA: 0x0000F01C File Offset: 0x0000D21C
		public void WriteStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.WriteStartElement(null, localName, namespaceUri);
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000309 RID: 777 RVA: 0x0000F028 File Offset: 0x0000D228
		public virtual void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentException("localName must not be null.", "localName");
			}
			this.WriteStartElement(prefix, localName.Value, (namespaceUri == null) ? null : namespaceUri.Value);
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="value">The text to write.</param>
		// Token: 0x0600030A RID: 778 RVA: 0x0000F06C File Offset: 0x0000D26C
		public virtual void WriteString(XmlDictionaryString value)
		{
			this.WriteString(value.Value);
		}

		/// <summary>Writes the text node that an <see cref="T:System.Xml.XmlDictionaryReader" /> is currently positioned on.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> to get the text value from.</param>
		/// <param name="isAttribute">Specifies whether the reader is positioned on an attribute value or element content.</param>
		// Token: 0x0600030B RID: 779 RVA: 0x0000F07C File Offset: 0x0000D27C
		protected virtual void WriteTextNode(XmlDictionaryReader reader, bool isAttribute)
		{
			this.WriteString(reader.Value);
			if (!isAttribute)
			{
				reader.Read();
			}
		}

		/// <summary>Writes a <see cref="T:System.Guid" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x0600030C RID: 780 RVA: 0x0000F098 File Offset: 0x0000D298
		public virtual void WriteValue(Guid guid)
		{
			this.WriteString(guid.ToString());
		}

		/// <summary>Writes a value from an <see cref="T:System.Xml.IStreamProvider" />.</summary>
		/// <param name="value">The <see cref="T:System.Xml.IStreamProvider" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="value" /> returns a null stream object.</exception>
		// Token: 0x0600030D RID: 781 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public virtual void WriteValue(IStreamProvider value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Stream stream = value.GetStream();
			byte[] array = new byte[Math.Min(2048L, (!stream.CanSeek) ? 2048L : stream.Length)];
			int num;
			while ((num = stream.Read(array, 0, array.Length)) > 0)
			{
				this.WriteBase64(array, 0, num);
			}
			value.ReleaseStream(stream);
		}

		/// <summary>Writes a <see cref="T:System.TimeSpan" /> value.</summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x0600030E RID: 782 RVA: 0x0000F124 File Offset: 0x0000D324
		public virtual void WriteValue(TimeSpan duration)
		{
			this.WriteString(XmlConvert.ToString(duration));
		}

		/// <summary>Writes a Unique Id value.</summary>
		/// <param name="value">The Unique Id value to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		// Token: 0x0600030F RID: 783 RVA: 0x0000F134 File Offset: 0x0000D334
		public virtual void WriteValue(UniqueId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.WriteString(id.ToString());
		}

		/// <summary>Writes a <see cref="T:System.Xml.XmlDictionaryString" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> value.</param>
		// Token: 0x06000310 RID: 784 RVA: 0x0000F15C File Offset: 0x0000D35C
		public virtual void WriteValue(XmlDictionaryString value)
		{
			this.WriteValue(value.Value);
		}

		/// <summary>Writes a standard XML attribute in the current node.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		// Token: 0x06000311 RID: 785 RVA: 0x0000F16C File Offset: 0x0000D36C
		public virtual void WriteXmlAttribute(string localName, string value)
		{
			base.WriteAttributeString("xml", localName, "http://www.w3.org/XML/1998/namespace", value);
		}

		/// <summary>Writes an XML attribute in the current node.</summary>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		// Token: 0x06000312 RID: 786 RVA: 0x0000F180 File Offset: 0x0000D380
		public virtual void WriteXmlAttribute(XmlDictionaryString localName, XmlDictionaryString value)
		{
			this.WriteXmlAttribute(localName.Value, value.Value);
		}

		/// <summary>Writes a namespace declaration attribute.</summary>
		/// <param name="prefix">The prefix that is bound to the given namespace.</param>
		/// <param name="namespaceUri">The namespace to which the prefix is bound.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x06000313 RID: 787 RVA: 0x0000F194 File Offset: 0x0000D394
		public virtual void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			if (prefix == null)
			{
				prefix = "d" + this.Depth + "p1";
			}
			if (prefix == string.Empty)
			{
				base.WriteAttributeString("xmlns", namespaceUri);
			}
			else
			{
				base.WriteAttributeString("xmlns", prefix, "http://www.w3.org/2000/xmlns/", namespaceUri);
			}
		}

		/// <summary>Writes a namespace declaration attribute.</summary>
		/// <param name="prefix">The prefix that is bound to the given namespace.</param>
		/// <param name="namespaceUri">The namespace to which the prefix is bound.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x06000314 RID: 788 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public virtual void WriteXmlnsAttribute(string prefix, XmlDictionaryString namespaceUri)
		{
			this.WriteXmlnsAttribute(prefix, namespaceUri.Value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F208 File Offset: 0x0000D408
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

		// Token: 0x06000316 RID: 790 RVA: 0x0000F27C File Offset: 0x0000D47C
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

		/// <summary>Writes nodes from a <see cref="T:System.Boolean" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000317 RID: 791 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes values from a <see cref="T:System.Boolean" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the data.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of values to write from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000318 RID: 792 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000319 RID: 793 RVA: 0x0000F324 File Offset: 0x0000D524
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.DateTime" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x0000F354 File Offset: 0x0000D554
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031B RID: 795 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Decimal" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031C RID: 796 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Double" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031D RID: 797 RVA: 0x0000F42C File Offset: 0x0000D62C
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Double" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031E RID: 798 RVA: 0x0000F45C File Offset: 0x0000D65C
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Guid" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600031F RID: 799 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Guid" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000320 RID: 800 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int16" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000321 RID: 801 RVA: 0x0000F528 File Offset: 0x0000D728
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int16" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000322 RID: 802 RVA: 0x0000F558 File Offset: 0x0000D758
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue((int)array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int32" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000323 RID: 803 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int32" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000324 RID: 804 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int64" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000325 RID: 805 RVA: 0x0000F618 File Offset: 0x0000D818
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Int64" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000326 RID: 806 RVA: 0x0000F648 File Offset: 0x0000D848
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.Single" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000327 RID: 807 RVA: 0x0000F690 File Offset: 0x0000D890
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.Single" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000328 RID: 808 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		/// <summary>Writes nodes from a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x06000329 RID: 809 RVA: 0x0000F708 File Offset: 0x0000D908
		public virtual void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			this.WriteArray(prefix, localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Writes nodes from a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array that contains the nodes.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to get from the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x0600032A RID: 810 RVA: 0x0000F738 File Offset: 0x0000D938
		public virtual void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckWriteArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.WriteStartElement(prefix, localName, namespaceUri);
				this.WriteValue(array[offset + i]);
				this.WriteEndElement();
			}
		}

		// Token: 0x0400014F RID: 335
		private static readonly Encoding utf8_unmarked = new UTF8Encoding(false);

		// Token: 0x04000150 RID: 336
		private int depth;
	}
}
