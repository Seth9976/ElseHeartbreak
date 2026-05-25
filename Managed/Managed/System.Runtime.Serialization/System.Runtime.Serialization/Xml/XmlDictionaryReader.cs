using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Xml
{
	/// <summary>An abstract class that the Windows Communication Foundation (WCF) derives from to do serialization and deserialization.</summary>
	// Token: 0x0200004D RID: 77
	public abstract class XmlDictionaryReader : XmlReader
	{
		/// <summary>This property always returns false. Its derived classes can override to return true if they support canonicalization.</summary>
		/// <returns>Returns false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public virtual bool CanCanonicalize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the quota values that apply to the current instance of this class.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> that applies to the current instance of this class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public virtual XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				if (this.quotas == null)
				{
					this.quotas = new XmlDictionaryReaderQuotas();
				}
				return this.quotas;
			}
		}

		/// <summary>This method is not yet implemented.</summary>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000259 RID: 601 RVA: 0x0000D218 File Offset: 0x0000B418
		public virtual void EndCanonicalization()
		{
			throw new NotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets the value of an attribute.</summary>
		/// <returns>The value of the attribute.</returns>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the attribute.</param>
		/// <param name="namespaceUri">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the namespace of the attribute.</param>
		// Token: 0x0600025A RID: 602 RVA: 0x0000D220 File Offset: 0x0000B420
		public virtual string GetAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			return this.GetAttribute(localName.Value, namespaceUri.Value);
		}

		/// <summary>Gets the index of the local name of the current node within an array of names.</summary>
		/// <returns>The index of the local name of the current node within an array of names.</returns>
		/// <param name="localNames">The string array of local names to be searched.</param>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localNames" /> or any of the names in the array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x0600025B RID: 603 RVA: 0x0000D264 File Offset: 0x0000B464
		public virtual int IndexOfLocalName(string[] localNames, string namespaceUri)
		{
			if (localNames == null)
			{
				throw new ArgumentNullException("localNames");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			if (this.NamespaceURI != namespaceUri)
			{
				return -1;
			}
			for (int i = 0; i < localNames.Length; i++)
			{
				if (localNames[i] == this.LocalName)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the local name of the current node within an array of names.</summary>
		/// <returns>The index of the local name of the current node within an array of names.</returns>
		/// <param name="localNames">The <see cref="T:System.Xml.XmlDictionaryString" /> array of local names to be searched.</param>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localNames" /> or any of the names in the array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x0600025C RID: 604 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		public virtual int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString namespaceUri)
		{
			if (localNames == null)
			{
				throw new ArgumentNullException("localNames");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			if (this.NamespaceURI != namespaceUri.Value)
			{
				return -1;
			}
			XmlDictionaryString xmlDictionaryString;
			if (!this.TryGetLocalNameAsDictionaryString(out xmlDictionaryString))
			{
				return -1;
			}
			IXmlDictionary dictionary = xmlDictionaryString.Dictionary;
			for (int i = 0; i < localNames.Length; i++)
			{
				XmlDictionaryString xmlDictionaryString2;
				if (dictionary.TryLookup(localNames[i], out xmlDictionaryString2) && object.ReferenceEquals(xmlDictionaryString2, xmlDictionaryString))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D360 File Offset: 0x0000B560
		public virtual bool IsArray(out Type type)
		{
			type = null;
			return false;
		}

		/// <summary>Checks whether the parameter, <paramref name="localName" />, is the local name of the current node.</summary>
		/// <returns>true if <paramref name="localName" /> matches local name of the current node; otherwise false.</returns>
		/// <param name="localName">The local name of the current node.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x0000D368 File Offset: 0x0000B568
		public virtual bool IsLocalName(string localName)
		{
			return this.LocalName == localName;
		}

		/// <summary>Checks whether the parameter, <paramref name="localName" />, is the local name of the current node.</summary>
		/// <returns>true if <paramref name="localName" /> matches local name of the current node; otherwise false.</returns>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the current node.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localName" /> is null.</exception>
		// Token: 0x0600025F RID: 607 RVA: 0x0000D378 File Offset: 0x0000B578
		public virtual bool IsLocalName(XmlDictionaryString localName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			return this.LocalName == localName.Value;
		}

		/// <summary>Checks whether the parameter, <paramref name="namespaceUri" />, is the namespace of the current node.</summary>
		/// <returns>true if <paramref name="namespaceUri" /> matches namespace of the current node; otherwise false.</returns>
		/// <param name="namespaceUri">The namespace of current node.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x06000260 RID: 608 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public virtual bool IsNamespaceUri(string namespaceUri)
		{
			return this.NamespaceURI == namespaceUri;
		}

		/// <summary>Checks whether the parameter, <paramref name="namespaceUri" />, is the namespace of the current node.</summary>
		/// <returns>true if <paramref name="namespaceUri" /> matches namespace of the current node; otherwise false.</returns>
		/// <param name="namespaceUri">Namespace of current node.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceUri" /> is null.</exception>
		// Token: 0x06000261 RID: 609 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public virtual bool IsNamespaceUri(XmlDictionaryString namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			return this.NamespaceURI == namespaceUri.Value;
		}

		/// <summary>Checks whether the reader is positioned at the start of an array. This class returns false, but derived classes that have the concept of arrays might return true.</summary>
		/// <returns>true if the reader is positioned at the start of an array node; otherwise false.</returns>
		/// <param name="type">Type of the node, if a valid node; otherwise null.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000262 RID: 610 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		public virtual bool IsStartArray(out Type type)
		{
			type = null;
			return false;
		}

		/// <summary>Tests whether the first tag is a start tag or empty element tag and if the local name and namespace URI match those of the current node.</summary>
		/// <returns>true if the first tag in the array is a start tag or empty element tag and matches <paramref name="localName" /> and <paramref name="namespaceUri" />; otherwise false.</returns>
		/// <param name="localName">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the local name of the attribute.</param>
		/// <param name="namespaceUri">An <see cref="T:System.Xml.XmlDictionaryString" /> that represents the namespace of the attribute.</param>
		// Token: 0x06000263 RID: 611 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		public virtual bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			return this.IsStartElement(localName.Value, namespaceUri.Value);
		}

		/// <summary>Tests whether the current node is a text node.</summary>
		/// <returns>true if the node type is <see cref="F:System.Xml.XmlNodetype.Text" />, <see cref="F:System.Xml.XmlNodetype.Whitespace" />, <see cref="F:System.Xml.XmlNodetype.SignificantWhitespace" />, <see cref="F:System.Xml.XmlNodetype.CDATA" />, or <see cref="F:System.Xml.XmlNodetype.Attribute" />; otherwise false.</returns>
		/// <param name="nodeType">Type of the node being tested.</param>
		// Token: 0x06000264 RID: 612 RVA: 0x0000D434 File Offset: 0x0000B634
		protected bool IsTextNode(XmlNodeType nodeType)
		{
			switch (nodeType)
			{
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			default:
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.SignificantWhitespace)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D470 File Offset: 0x0000B670
		private XmlException XmlError(string message)
		{
			IXmlLineInfo xmlLineInfo = this as IXmlLineInfo;
			if (xmlLineInfo == null || !xmlLineInfo.HasLineInfo())
			{
				return new XmlException(message);
			}
			return new XmlException(string.Format("{0} in {1} , at ({2},{3})", new object[] { message, this.BaseURI, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition }));
		}

		/// <summary>Tests whether the current content node is a start element or an empty element.</summary>
		// Token: 0x06000266 RID: 614 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		public virtual void MoveToStartElement()
		{
			this.MoveToContent();
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.XmlError(string.Format("Element node is expected, but got {0} node.", this.NodeType));
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.Name" /> property of the element matches the given argument.</summary>
		/// <param name="name">The <see cref="P:System.Xml.XmlReader.Name" /> property of the element.</param>
		// Token: 0x06000267 RID: 615 RVA: 0x0000D518 File Offset: 0x0000B718
		public virtual void MoveToStartElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.MoveToStartElement();
			if (this.Name != name)
			{
				throw this.XmlError(string.Format("Element node '{0}' is expected, but got '{1}' element.", name, this.Name));
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element matches the given arguments.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x06000268 RID: 616 RVA: 0x0000D568 File Offset: 0x0000B768
		public virtual void MoveToStartElement(string localName, string namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.MoveToStartElement();
			if (this.LocalName != localName || this.NamespaceURI != namespaceUri)
			{
				throw this.XmlError(string.Format("Element node '{0}' in namespace '{1}' is expected, but got '{2}' in namespace '{3}' element.", new object[] { localName, namespaceUri, this.LocalName, this.NamespaceURI }));
			}
		}

		/// <summary>Tests whether the current content node is a start element or an empty element and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element matches the given argument.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x06000269 RID: 617 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public virtual void MoveToStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.MoveToStartElement(localName.Value, namespaceUri.Value);
		}

		/// <summary>This method is not yet implemented.</summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="includeComments">Determines whether comments are included.</param>
		/// <param name="inclusivePrefixes">The prefixes to be included.</param>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600026A RID: 618 RVA: 0x0000D634 File Offset: 0x0000B834
		public virtual void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not implemented in this class (it always returns false). May be overridden in derived classes.</summary>
		/// <returns>false, unless overridden in a derived class.</returns>
		/// <param name="count">Returns 0, unless overridden in a derived class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600026B RID: 619 RVA: 0x0000D63C File Offset: 0x0000B83C
		public virtual bool TryGetArrayLength(out int count)
		{
			count = -1;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns false). May be overridden in derived classes.</summary>
		/// <returns>false, unless overridden in a derived class.</returns>
		/// <param name="length">Returns 0, unless overridden in a derived class.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600026C RID: 620 RVA: 0x0000D644 File Offset: 0x0000B844
		public virtual bool TryGetBase64ContentLength(out int count)
		{
			count = -1;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns false). May be overridden in derived classes.</summary>
		/// <returns>false, unless overridden in a derived class.</returns>
		/// <param name="localName">Returns null, unless overridden in a derived class..</param>
		// Token: 0x0600026D RID: 621 RVA: 0x0000D64C File Offset: 0x0000B84C
		public virtual bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
		{
			localName = null;
			return false;
		}

		/// <summary>Not implemented in this class (it always returns false). May be overridden in derived classes.</summary>
		/// <returns>false, unless overridden in a derived class.</returns>
		/// <param name="namespaceUri">Returns null, unless overridden in a derived class.</param>
		// Token: 0x0600026E RID: 622 RVA: 0x0000D654 File Offset: 0x0000B854
		public virtual bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString namespaceUri)
		{
			namespaceUri = null;
			return false;
		}

		/// <summary>Converts a node's content to a specified type.</summary>
		/// <returns>The concatenated text content or attribute value converted to the requested type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to be returned.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion. For example, this can be used when converting an <see cref="T:System.Xml.XmlQualifiedName" /> object to an xs:string. This value can be a null reference.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600026F RID: 623 RVA: 0x0000D65C File Offset: 0x0000B85C
		public override object ReadContentAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			return base.ReadContentAs(type, nsResolver);
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>A byte array that contains the Base64 decoded binary bytes.</returns>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than the MaxArrayLength quota for this reader.</exception>
		// Token: 0x06000270 RID: 624 RVA: 0x0000D668 File Offset: 0x0000B868
		public virtual byte[] ReadContentAsBase64()
		{
			int num;
			if (!this.TryGetBase64ContentLength(out num))
			{
				return Convert.FromBase64String(this.ReadContentAsString());
			}
			byte[] array = new byte[num];
			this.ReadContentAsBase64(array, 0, num);
			return array;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		private byte[] FromBinHexString(string s)
		{
			return (byte[])this.xmlconv_from_bin_hex.Invoke(null, new object[] { s });
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>A byte array that contains the BinHex decoded binary bytes.</returns>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000272 RID: 626 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public virtual byte[] ReadContentAsBinHex()
		{
			int num;
			if (!this.TryGetArrayLength(out num))
			{
				return this.FromBinHexString(this.ReadContentAsString());
			}
			return this.ReadContentAsBinHex(num);
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>A byte array that contains the BinHex decoded binary bytes.</returns>
		/// <param name="maxByteArrayContentLength">The maximum array length.</param>
		/// <exception cref="T:System.Xml.XmlException">The array size is greater than <paramref name="maxByteArrayContentLength" />.</exception>
		// Token: 0x06000273 RID: 627 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		protected byte[] ReadContentAsBinHex(int maxByteArrayContentLength)
		{
			byte[] array = new byte[maxByteArrayContentLength];
			this.ReadContentAsBinHex(array, 0, maxByteArrayContentLength);
			return array;
		}

		/// <summary>Reads the content into a char array.</summary>
		/// <returns>Number of characters read.</returns>
		/// <param name="chars">The array into which the characters are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of characters to put in the array.</param>
		// Token: 0x06000274 RID: 628 RVA: 0x0000D710 File Offset: 0x0000B910
		[MonoTODO]
		public virtual int ReadContentAsChars(char[] chars, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a node's content to decimal.</summary>
		/// <returns>The decimal representation of node's content.</returns>
		// Token: 0x06000275 RID: 629 RVA: 0x0000D718 File Offset: 0x0000B918
		public override decimal ReadContentAsDecimal()
		{
			return base.ReadContentAsDecimal();
		}

		/// <summary>Converts a node's content to float.</summary>
		/// <returns>The float representation of node's content.</returns>
		// Token: 0x06000276 RID: 630 RVA: 0x0000D720 File Offset: 0x0000B920
		public override float ReadContentAsFloat()
		{
			return base.ReadContentAsFloat();
		}

		/// <summary>Converts a node's content to guid.</summary>
		/// <returns>The guid representation of node's content.</returns>
		// Token: 0x06000277 RID: 631 RVA: 0x0000D728 File Offset: 0x0000B928
		public virtual Guid ReadContentAsGuid()
		{
			return XmlConvert.ToGuid(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to a qualified name representation.</summary>
		/// <param name="localName">The <see cref="P:System.Xml.XmlReader.LocalName" /> part of the qualified name (out parameter).</param>
		/// <param name="namespaceUri">The <see cref="P:System.Xml.XmlReader.NamespaceURI" /> part of the qualified name (out parameter).</param>
		// Token: 0x06000278 RID: 632 RVA: 0x0000D738 File Offset: 0x0000B938
		public virtual void ReadContentAsQualifiedName(out string localName, out string namespaceUri)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)this.ReadContentAs(typeof(XmlQualifiedName), this as IXmlNamespaceResolver);
			localName = xmlQualifiedName.Name;
			namespaceUri = xmlQualifiedName.Namespace;
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <returns>The node content in a string representation.</returns>
		// Token: 0x06000279 RID: 633 RVA: 0x0000D774 File Offset: 0x0000B974
		public override string ReadContentAsString()
		{
			return this.ReadContentAsString(this.Quotas.MaxStringContentLength);
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <returns>Node content in string representation.</returns>
		/// <param name="maxStringContentLength">The maximum string length.</param>
		// Token: 0x0600027A RID: 634 RVA: 0x0000D788 File Offset: 0x0000B988
		[MonoTODO]
		protected string ReadContentAsString(int maxStringContentLength)
		{
			return base.ReadContentAsString();
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <returns>The node content in a string representation.</returns>
		/// <param name="strings">The array of strings to match content against.</param>
		/// <param name="index">The index of the entry in <paramref name="strings" /> that matches the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="strings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">An entry in<paramref name=" strings" /> is null.</exception>
		// Token: 0x0600027B RID: 635 RVA: 0x0000D790 File Offset: 0x0000B990
		[MonoTODO("there is exactly no information on the web")]
		public virtual string ReadContentAsString(string[] strings, out int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a node's content to a string.</summary>
		/// <returns>The node content in a string representation.</returns>
		/// <param name="strings">The array of <see cref="T:System.Xml.XmlDictionaryString" /> objects to match content against.</param>
		/// <param name="index">The index of the entry in <paramref name="strings" /> that matches the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="strings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">An entry in<paramref name=" strings" /> is null.</exception>
		// Token: 0x0600027C RID: 636 RVA: 0x0000D798 File Offset: 0x0000B998
		[MonoTODO("there is exactly no information on the web")]
		public virtual string ReadContentAsString(XmlDictionaryString[] strings, out int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a node's content to <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>
		///   <see cref="T:System.TimeSpan" /> representation of node's content.</returns>
		// Token: 0x0600027D RID: 637 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public virtual TimeSpan ReadContentAsTimeSpan()
		{
			return XmlConvert.ToTimeSpan(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to a unique identifier.</summary>
		/// <returns>The node's content represented as a unique identifier.</returns>
		// Token: 0x0600027E RID: 638 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public virtual UniqueId ReadContentAsUniqueId()
		{
			return new UniqueId(this.ReadContentAsString());
		}

		/// <summary>Converts a node's content to a array of Base64 bytes.</summary>
		/// <returns>The node's content represented as an array of Base64 bytes.</returns>
		// Token: 0x0600027F RID: 639 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public virtual byte[] ReadElementContentAsBase64()
		{
			this.ReadStartElement();
			byte[] array = this.ReadContentAsBase64();
			this.ReadEndElement();
			return array;
		}

		/// <summary>Converts a node's content to an array of BinHex bytes.</summary>
		/// <returns>The node's content represented as an array of BinHex bytes.</returns>
		// Token: 0x06000280 RID: 640 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public virtual byte[] ReadElementContentAsBinHex()
		{
			this.ReadStartElement();
			byte[] array = this.ReadContentAsBinHex();
			this.ReadEndElement();
			return array;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Guid" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Guid" />.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x06000281 RID: 641 RVA: 0x0000D808 File Offset: 0x0000BA08
		public virtual Guid ReadElementContentAsGuid()
		{
			this.ReadStartElement();
			Guid guid = this.ReadContentAsGuid();
			this.ReadEndElement();
			return guid;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.TimeSpan" />.</returns>
		// Token: 0x06000282 RID: 642 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public virtual TimeSpan ReadElementContentAsTimeSpan()
		{
			this.ReadStartElement();
			TimeSpan timeSpan = this.ReadContentAsTimeSpan();
			this.ReadEndElement();
			return timeSpan;
		}

		/// <summary>Converts an element's content to a unique identifier.</summary>
		/// <returns>The node's content represented as a unique identifier.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x06000283 RID: 643 RVA: 0x0000D850 File Offset: 0x0000BA50
		public virtual UniqueId ReadElementContentAsUniqueId()
		{
			this.ReadStartElement();
			UniqueId uniqueId = this.ReadContentAsUniqueId();
			this.ReadEndElement();
			return uniqueId;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.String" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.String" />.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x0000D874 File Offset: 0x0000BA74
		public override string ReadElementContentAsString()
		{
			if (this.IsEmptyElement)
			{
				this.Read();
				return string.Empty;
			}
			this.ReadStartElement();
			string text;
			if (this.NodeType == XmlNodeType.EndElement)
			{
				text = string.Empty;
			}
			else
			{
				text = this.ReadContentAsString();
			}
			this.ReadEndElement();
			return text;
		}

		/// <summary>Checks whether the current node is an element and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns false.</exception>
		// Token: 0x06000285 RID: 645 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public virtual void ReadFullStartElement()
		{
			if (!this.IsStartElement())
			{
				throw new XmlException("Current node is not a start element");
			}
			this.ReadStartElement();
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="name" /> and advances the reader to the next node.</summary>
		/// <param name="name">The qualified name of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns false.</exception>
		// Token: 0x06000286 RID: 646 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public virtual void ReadFullStartElement(string name)
		{
			if (!this.IsStartElement(name))
			{
				throw new XmlException(string.Format("Current node is not a start element '{0}'", name));
			}
			this.ReadStartElement(name);
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns false.</exception>
		// Token: 0x06000287 RID: 647 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public virtual void ReadFullStartElement(string localName, string namespaceUri)
		{
			if (!this.IsStartElement(localName, namespaceUri))
			{
				throw new XmlException(string.Format("Current node is not a start element '{0}' in namesapce '{1}'", localName, namespaceUri));
			}
			this.ReadStartElement(localName, namespaceUri);
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <see cref="M:System.Xml.XmlDictionaryReader.IsStartElement(System.Xml.XmlDictionaryString,System.Xml.XmlDictionaryString)" /> returns false.</exception>
		// Token: 0x06000288 RID: 648 RVA: 0x0000D948 File Offset: 0x0000BB48
		public virtual void ReadFullStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (!this.IsStartElement(localName, namespaceUri))
			{
				throw new XmlException(string.Format("Current node is not a start element '{0}' in namesapce '{1}'", localName, namespaceUri));
			}
			this.ReadStartElement(localName.Value, namespaceUri.Value);
		}

		/// <summary>Checks whether the current node is an element with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> and advances the reader to the next node.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x06000289 RID: 649 RVA: 0x0000D988 File Offset: 0x0000BB88
		public virtual void ReadStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.ReadStartElement(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads the contents of the current node into a string.</summary>
		/// <returns>A string that contains the contents of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">Unable to read the contents of the current node.</exception>
		/// <exception cref="T:System.Xml.XmlException">Maximum allowed string length exceeded.</exception>
		// Token: 0x0600028A RID: 650 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		public override string ReadString()
		{
			return this.ReadString(this.Quotas.MaxStringContentLength);
		}

		/// <summary>Reads the contents of the current node into a string with a given maximum length.</summary>
		/// <returns>A string that contains the contents of the current node.</returns>
		/// <param name="maxStringContentLength">Maximum allowed string length.</param>
		/// <exception cref="T:System.InvalidOperationException">Unable to read the contents of the current node.</exception>
		/// <exception cref="T:System.Xml.XmlException">Maximum allowed string length exceeded.</exception>
		// Token: 0x0600028B RID: 651 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		[MonoTODO]
		protected string ReadString(int maxStringContentLength)
		{
			return base.ReadString();
		}

		/// <summary>Not implemented.</summary>
		/// <returns>Not implemented.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always.</exception>
		// Token: 0x0600028C RID: 652 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public virtual int ReadValueAsBase64(byte[] bytes, int start, int length)
		{
			throw new NotSupportedException();
		}

		/// <summary>Not implemented in this class (it always returns false). May be overridden in derived classes.</summary>
		/// <returns>false, unless overridden in a derived class.</returns>
		/// <param name="value">Returns null, unless overridden in a derived class.</param>
		// Token: 0x0600028D RID: 653 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		public virtual bool TryGetValueAsDictionaryString(out XmlDictionaryString value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		// Token: 0x0600028E RID: 654 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, 0, buffer.Length, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.</exception>
		// Token: 0x0600028F RID: 655 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, new XmlDictionary(), quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.</exception>
		// Token: 0x06000290 RID: 656 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, dictionary, quotas, new XmlBinaryReaderSession(), null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.</exception>
		// Token: 0x06000291 RID: 657 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session)
		{
			return XmlDictionaryReader.CreateBinaryReader(buffer, offset, count, dictionary, quotas, session, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">The <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">The <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to be called when the reader is closed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero or greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero or greater than the buffer length minus the offset.</exception>
		// Token: 0x06000292 RID: 658 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public static XmlDictionaryReader CreateBinaryReader(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			return new XmlBinaryDictionaryReader(buffer, offset, count, dictionary, quotas, session, onClose);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		// Token: 0x06000293 RID: 659 RVA: 0x0000DA50 File Offset: 0x0000BC50
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionary(), quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="quotas" /> is null.</exception>
		// Token: 0x06000294 RID: 660 RVA: 0x0000DA60 File Offset: 0x0000BC60
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, dictionary, quotas, new XmlBinaryReaderSession(), null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">The quotas that apply to this operation.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		// Token: 0x06000295 RID: 661 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session)
		{
			return XmlDictionaryReader.CreateBinaryReader(stream, dictionary, quotas, session, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that can read .NET Binary XML Format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">
		///   <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to be called when the reader is closed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		// Token: 0x06000296 RID: 662 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public static XmlDictionaryReader CreateBinaryReader(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quotas, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose)
		{
			return new XmlBinaryDictionaryReader(stream, dictionary, quotas, session, onClose);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> from an existing <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="reader">An instance of <see cref="T:System.Xml.XmlReader" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null.</exception>
		// Token: 0x06000297 RID: 663 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		public static XmlDictionaryReader CreateDictionaryReader(XmlReader reader)
		{
			return new XmlSimpleDictionaryReader(reader);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encoding">The possible character encoding of the stream.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000298 RID: 664 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas)
		{
			return new XmlMtomDictionaryReader(stream, encoding, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000299 RID: 665 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(stream, encodings, null, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029A RID: 666 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(stream, encodings, contentType, quotas, int.MaxValue, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encodings">The possible character encodings of the stream.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The MIME type of the message.</param>
		/// <param name="maxBufferSize">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply to the reader.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029B RID: 667 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public static XmlDictionaryReader CreateMtomReader(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			return new XmlMtomDictionaryReader(stream, encodings, contentType, quotas, maxBufferSize, onClose);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encoding">The possible character encoding of the input.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encoding" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029C RID: 668 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(new MemoryStream(buffer, offset, count), encoding, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029D RID: 669 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(new MemoryStream(buffer, offset, count), encodings, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The quotas to apply to this reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029E RID: 670 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateMtomReader(new MemoryStream(buffer, offset, count), encodings, contentType, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" /> that reads XML in the MTOM format.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encodings">The possible character encodings of the input.</param>
		/// <param name="contentType">The Content-Type MIME type of the message.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply to the reader.</param>
		/// <param name="maxBufferSize">The maximum allowed size of the buffer.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029F RID: 671 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		public static XmlDictionaryReader CreateMtomReader(byte[] buffer, int offset, int count, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			return XmlDictionaryReader.CreateMtomReader(new MemoryStream(buffer, offset, count), encodings, contentType, quotas, maxBufferSize, onClose);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		// Token: 0x060002A0 RID: 672 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateTextReader(buffer, 0, buffer.Length, quotas);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		// Token: 0x060002A1 RID: 673 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, int offset, int count, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateTextReader(buffer, offset, count, Encoding.UTF8, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">The starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding properties to apply.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		// Token: 0x060002A2 RID: 674 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public static XmlDictionaryReader CreateTextReader(byte[] buffer, int offset, int count, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			return XmlDictionaryReader.CreateTextReader(new MemoryStream(buffer, offset, count), encoding, quotas, onClose);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="quotas">The quotas applied to the reader.</param>
		// Token: 0x060002A3 RID: 675 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public static XmlDictionaryReader CreateTextReader(Stream stream, XmlDictionaryReaderQuotas quotas)
		{
			return XmlDictionaryReader.CreateTextReader(stream, Encoding.UTF8, quotas, null);
		}

		/// <summary>Creates an instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</summary>
		/// <returns>An instance of <see cref="T:System.Xml.XmlDictionaryReader" />.</returns>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding properties to apply.</param>
		/// <param name="quotas">The <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="onClose">The delegate to be called when the reader is closed.</param>
		// Token: 0x060002A4 RID: 676 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public static XmlDictionaryReader CreateTextReader(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas, OnXmlDictionaryReaderClose onClose)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			XmlNameTable xmlNameTable = new NameTable();
			XmlParserContext xmlParserContext = new XmlParserContext(xmlNameTable, new XmlNamespaceManager(xmlNameTable), string.Empty, XmlSpace.None, encoding);
			return new XmlSimpleDictionaryReader(XmlReader.Create(stream, xmlReaderSettings, xmlParserContext), null, onClose)
			{
				quotas = quotas
			};
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		private void CheckReadArrayArguments(Array array, int offset, int length)
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

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DC28 File Offset: 0x0000BE28
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

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002A7 RID: 679 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The local name of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002A8 RID: 680 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public virtual int ReadArray(string localName, string namespaceUri, bool[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToBoolean(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> array of the <see cref="T:System.Boolean" /> nodes.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002A9 RID: 681 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public virtual bool[] ReadBooleanArray(string localName, string namespaceUri)
		{
			List<bool> list = new List<bool>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToBoolean(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Boolean" /> nodes into a typed array.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> array of the <see cref="T:System.Boolean" /> nodes.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002AA RID: 682 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public virtual bool[] ReadBooleanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadBooleanArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.DateTime" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002AB RID: 683 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.DateTime" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002AC RID: 684 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public virtual int ReadArray(string localName, string namespaceUri, DateTime[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToDateTime(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002AD RID: 685 RVA: 0x0000DE18 File Offset: 0x0000C018
		public virtual DateTime[] ReadDateTimeArray(string localName, string namespaceUri)
		{
			List<DateTime> list = new List<DateTime>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToDateTime(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002AE RID: 686 RVA: 0x0000DE88 File Offset: 0x0000C088
		public virtual DateTime[] ReadDateTimeArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadDateTimeArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Decimal" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002AF RID: 687 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Decimal" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002B0 RID: 688 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public virtual int ReadArray(string localName, string namespaceUri, decimal[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToDecimal(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000DF48 File Offset: 0x0000C148
		public virtual decimal[] ReadDecimalArray(string localName, string namespaceUri)
		{
			List<decimal> list = new List<decimal>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToDecimal(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002B2 RID: 690 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		public virtual decimal[] ReadDecimalArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadDecimalArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Double" /> nodes type into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002B3 RID: 691 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Double" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002B4 RID: 692 RVA: 0x0000E00C File Offset: 0x0000C20C
		public virtual int ReadArray(string localName, string namespaceUri, double[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToDouble(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Double" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002B5 RID: 693 RVA: 0x0000E070 File Offset: 0x0000C270
		public virtual double[] ReadDoubleArray(string localName, string namespaceUri)
		{
			List<double> list = new List<double>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToDouble(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Converts a node's content to a <see cref="T:System.DateTime" /> array.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Double" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002B6 RID: 694 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public virtual double[] ReadDoubleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadDoubleArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Guid" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000E108 File Offset: 0x0000C308
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.Guid" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000E134 File Offset: 0x0000C334
		public virtual int ReadArray(string localName, string namespaceUri, Guid[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToGuid(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see cref="T:System.Guid" />.</summary>
		/// <returns>An array of <see cref="T:System.Guid" />.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public virtual Guid[] ReadGuidArray(string localName, string namespaceUri)
		{
			List<Guid> list = new List<Guid>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToGuid(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of <see cref="T:System.Guid" />.</summary>
		/// <returns>An array of <see cref="T:System.Guid" />.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002BA RID: 698 RVA: 0x0000E210 File Offset: 0x0000C410
		public virtual Guid[] ReadGuidArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadGuidArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of short integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002BB RID: 699 RVA: 0x0000E238 File Offset: 0x0000C438
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of short integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002BC RID: 700 RVA: 0x0000E264 File Offset: 0x0000C464
		public virtual int ReadArray(string localName, string namespaceUri, short[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToInt16(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of short integers (<see cref="T:System.Int16" />).</summary>
		/// <returns>An array of short integers (<see cref="T:System.Int16" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002BD RID: 701 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		public virtual short[] ReadInt16Array(string localName, string namespaceUri)
		{
			List<short> list = new List<short>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToInt16(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of short integers (<see cref="T:System.Int16" />).</summary>
		/// <returns>An array of short integers (<see cref="T:System.Int16" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002BE RID: 702 RVA: 0x0000E338 File Offset: 0x0000C538
		public virtual short[] ReadInt16Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadInt16Array(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002BF RID: 703 RVA: 0x0000E360 File Offset: 0x0000C560
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002C0 RID: 704 RVA: 0x0000E38C File Offset: 0x0000C58C
		public virtual int ReadArray(string localName, string namespaceUri, int[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToInt32(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of integers (<see cref="T:System.Int32" />).</summary>
		/// <returns>An array of integers (<see cref="T:System.Int32" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002C1 RID: 705 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public virtual int[] ReadInt32Array(string localName, string namespaceUri)
		{
			List<int> list = new List<int>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToInt32(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of integers (<see cref="T:System.Int32" />).</summary>
		/// <returns>An array of integers (<see cref="T:System.Int32" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000E460 File Offset: 0x0000C660
		public virtual int[] ReadInt32Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadInt32Array(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of long integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002C3 RID: 707 RVA: 0x0000E488 File Offset: 0x0000C688
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of long integers into a typed array.</summary>
		/// <returns>The number of integers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the integers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of integers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public virtual int ReadArray(string localName, string namespaceUri, long[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToInt64(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of long integers (<see cref="T:System.Int64" />).</summary>
		/// <returns>An array of long integers (<see cref="T:System.Int64" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002C5 RID: 709 RVA: 0x0000E518 File Offset: 0x0000C718
		public virtual long[] ReadInt64Array(string localName, string namespaceUri)
		{
			List<long> list = new List<long>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToInt64(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of long integers (<see cref="T:System.Int64" />).</summary>
		/// <returns>An array of long integers (<see cref="T:System.Int64" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002C6 RID: 710 RVA: 0x0000E588 File Offset: 0x0000C788
		public virtual long[] ReadInt64Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadInt64Array(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of float numbers into a typed array.</summary>
		/// <returns>The number of float numbers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the float numbers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of float numbers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002C7 RID: 711 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of float numbers into a typed array.</summary>
		/// <returns>The umber of float numbers put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the float numbers are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of float numbers to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002C8 RID: 712 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public virtual int ReadArray(string localName, string namespaceUri, float[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToSingle(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of float numbers (<see cref="T:System.Single" />).</summary>
		/// <returns>An array of float numbers (<see cref="T:System.Single" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000E640 File Offset: 0x0000C840
		public virtual float[] ReadSingleArray(string localName, string namespaceUri)
		{
			List<float> list = new List<float>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToSingle(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into an array of float numbers (<see cref="T:System.Single" />).</summary>
		/// <returns>An array of float numbers (<see cref="T:System.Single" />).</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002CA RID: 714 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public virtual float[] ReadSingleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadSingleArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.TimeSpan" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002CB RID: 715 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		public virtual int ReadArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadArray(localName.Value, namespaceUri.Value, array, offset, length);
		}

		/// <summary>Reads repeated occurrences of <see cref="T:System.TimeSpan" /> nodes into a typed array.</summary>
		/// <returns>The number of nodes put in the array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		/// <param name="array">The array into which the nodes are put.</param>
		/// <param name="offset">The starting index in the array.</param>
		/// <param name="count">The number of nodes to put in the array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is &lt; 0 or &gt; <paramref name="array" /> length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is &lt; 0 or &gt; <paramref name="array" /> length minus <paramref name="offset" />.</exception>
		// Token: 0x060002CC RID: 716 RVA: 0x0000E704 File Offset: 0x0000C904
		public virtual int ReadArray(string localName, string namespaceUri, TimeSpan[] array, int offset, int length)
		{
			this.CheckReadArrayArguments(array, offset, length);
			for (int i = 0; i < length; i++)
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					return i;
				}
				this.ReadStartElement(localName, namespaceUri);
				array[offset + i] = XmlConvert.ToTimeSpan(this.ReadContentAsString());
				this.ReadEndElement();
			}
			return length;
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002CD RID: 717 RVA: 0x0000E770 File Offset: 0x0000C970
		public virtual TimeSpan[] ReadTimeSpanArray(string localName, string namespaceUri)
		{
			List<TimeSpan> list = new List<TimeSpan>();
			do
			{
				this.MoveToContent();
				if (this.NodeType != XmlNodeType.Element)
				{
					break;
				}
				this.ReadStartElement(localName, namespaceUri);
				list.Add(XmlConvert.ToTimeSpan(this.ReadContentAsString()));
				this.ReadEndElement();
			}
			while (list.Count != this.Quotas.MaxArrayLength);
			return list.ToArray();
		}

		/// <summary>Reads the contents of a series of nodes with the given <paramref name="localName" /> and <paramref name="namespaceUri" /> into a <see cref="T:System.TimeSpan" /> array.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> array.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceUri">The namespace URI of the element.</param>
		// Token: 0x060002CE RID: 718 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public virtual TimeSpan[] ReadTimeSpanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			this.CheckDictionaryStringArgs(localName, namespaceUri);
			return this.ReadTimeSpanArray(localName.Value, namespaceUri.Value);
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Boolean" />.</returns>
		// Token: 0x060002CF RID: 719 RVA: 0x0000E808 File Offset: 0x0000CA08
		public override bool ReadElementContentAsBoolean()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			bool flag = this.ReadContentAsBoolean();
			this.ReadEndElement();
			return flag;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The element is not in valid format.</exception>
		/// <exception cref="T:System.FormatException">The element is not in valid format.</exception>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000E838 File Offset: 0x0000CA38
		public override DateTime ReadElementContentAsDateTime()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			DateTime dateTime = this.ReadContentAsDateTime();
			this.ReadEndElement();
			return dateTime;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x060002D1 RID: 721 RVA: 0x0000E868 File Offset: 0x0000CA68
		public override decimal ReadElementContentAsDecimal()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			decimal num = this.ReadContentAsDecimal();
			this.ReadEndElement();
			return num;
		}

		/// <summary>Converts an element's content to a <see cref="T:System.Double" />.</summary>
		/// <returns>The node's content represented as a <see cref="T:System.Double" />.</returns>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000E898 File Offset: 0x0000CA98
		public override double ReadElementContentAsDouble()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			double num = this.ReadContentAsDouble();
			this.ReadEndElement();
			return num;
		}

		/// <summary>Converts an element's content to a floating point number (<see cref="T:System.Single" />).</summary>
		/// <returns>The node's content represented as a floating point number (<see cref="T:System.Single" />).</returns>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public override float ReadElementContentAsFloat()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			float num = this.ReadContentAsFloat();
			this.ReadEndElement();
			return num;
		}

		/// <summary>Converts an element's content to an integer (<see cref="T:System.Int32" />).</summary>
		/// <returns>The node's content represented as an integer (<see cref="T:System.Int32" />).</returns>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public override int ReadElementContentAsInt()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			int num = this.ReadContentAsInt();
			this.ReadEndElement();
			return num;
		}

		/// <summary>Converts an element's content to a long integer (<see cref="T:System.Int64" />).</summary>
		/// <returns>The node's content represented as a long integer (<see cref="T:System.Int64" />).</returns>
		// Token: 0x060002D5 RID: 725 RVA: 0x0000E928 File Offset: 0x0000CB28
		public override long ReadElementContentAsLong()
		{
			this.ReadStartElement(this.LocalName, this.NamespaceURI);
			long num = this.ReadContentAsLong();
			this.ReadEndElement();
			return num;
		}

		// Token: 0x04000141 RID: 321
		private XmlDictionaryReaderQuotas quotas;

		// Token: 0x04000142 RID: 322
		private MethodInfo xmlconv_from_bin_hex = typeof(XmlConvert).GetMethod("FromBinHexString", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);

		// Token: 0x04000143 RID: 323
		private static readonly char[] wsChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
