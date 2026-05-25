using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML element.</summary>
	// Token: 0x02000014 RID: 20
	[XmlSchemaProvider(null, IsAny = true)]
	public class XElement : XContainer, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x00003E38 File Offset: 0x00002038
		public XElement(XName name, object value)
		{
			this.name = name;
			base.Add(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from another <see cref="T:System.Xml.Linq.XElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XElement" /> object to copy from.</param>
		// Token: 0x0600009D RID: 157 RVA: 0x00003E58 File Offset: 0x00002058
		public XElement(XElement source)
		{
			this.name = source.name;
			base.Add(source.Attributes());
			base.Add(source.Nodes());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name. </summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		// Token: 0x0600009E RID: 158 RVA: 0x00003E98 File Offset: 0x00002098
		public XElement(XName name)
		{
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The initial content of the element.</param>
		// Token: 0x0600009F RID: 159 RVA: 0x00003EB0 File Offset: 0x000020B0
		public XElement(XName name, params object[] contents)
		{
			this.name = name;
			base.Add(contents);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from an <see cref="T:System.Xml.Linq.XStreamingElement" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement" /> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement" />.</param>
		// Token: 0x060000A0 RID: 160 RVA: 0x00003ED0 File Offset: 0x000020D0
		public XElement(XStreamingElement source)
		{
			this.name = source.Name;
			base.Add(source.Contents);
		}

		/// <summary>Converts an object into its XML representation.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to which this object is serialized.</param>
		// Token: 0x060000A2 RID: 162 RVA: 0x00003F10 File Offset: 0x00002110
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.Save(writer);
		}

		/// <summary>Generates an object from its XML representation.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which the object is deserialized.</param>
		// Token: 0x060000A3 RID: 163 RVA: 0x00003F1C File Offset: 0x0000211C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			base.ReadContentFrom(reader, LoadOptions.None);
		}

		/// <summary>Gets an XML schema definition that describes the XML representation of this object.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x060000A4 RID: 164 RVA: 0x00003F28 File Offset: 0x00002128
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>Gets an empty collection of elements.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains an empty collection.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003F2C File Offset: 0x0000212C
		public static IEnumerable<XElement> EmptySequence
		{
			get
			{
				return XElement.emptySequence;
			}
		}

		/// <summary>Gets the first attribute of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that contains the first attribute of this element.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003F34 File Offset: 0x00002134
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003F3C File Offset: 0x0000213C
		public XAttribute FirstAttribute
		{
			get
			{
				return this.attr_first;
			}
			internal set
			{
				this.attr_first = value;
			}
		}

		/// <summary>Gets the last attribute of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that contains the last attribute of this element.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003F48 File Offset: 0x00002148
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003F50 File Offset: 0x00002150
		public XAttribute LastAttribute
		{
			get
			{
				return this.attr_last;
			}
			internal set
			{
				this.attr_last = value;
			}
		}

		/// <summary>Gets a value indicating whether this element as at least one attribute.</summary>
		/// <returns>true if this element has at least one attribute; otherwise false.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003F5C File Offset: 0x0000215C
		public bool HasAttributes
		{
			get
			{
				return this.attr_first != null;
			}
		}

		/// <summary>Gets a value indicating whether this element has at least one child element.</summary>
		/// <returns>true if this element has at least one child element; otherwise false.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003F6C File Offset: 0x0000216C
		public bool HasElements
		{
			get
			{
				foreach (object obj in base.Nodes())
				{
					if (obj is XElement)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether this element contains no content.</summary>
		/// <returns>true if this element contains no content; otherwise false.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00004000 File Offset: 0x00002200
		public bool IsEmpty
		{
			get
			{
				return !base.Nodes().GetEnumerator().MoveNext() && this.explicit_is_empty;
			}
			internal set
			{
				this.explicit_is_empty = value;
			}
		}

		/// <summary>Gets the name of this element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this element.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000400C File Offset: 0x0000220C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004014 File Offset: 0x00002214
		public XName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XElement" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Element" />.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000403C File Offset: 0x0000223C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		/// <summary>Gets the concatenated text contents of this element.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains all of the text content of this element. If there are multiple text nodes, they will be concatenated.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004040 File Offset: 0x00002240
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004100 File Offset: 0x00002300
		public string Value
		{
			get
			{
				StringBuilder stringBuilder = null;
				foreach (XNode xnode in base.Nodes())
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					if (xnode is XText)
					{
						stringBuilder.Append(((XText)xnode).Value);
					}
					else if (xnode is XElement)
					{
						stringBuilder.Append(((XElement)xnode).Value);
					}
				}
				return (stringBuilder != null) ? stringBuilder.ToString() : string.Empty;
			}
			set
			{
				base.RemoveNodes();
				base.Add(value);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004110 File Offset: 0x00002310
		private IEnumerable<XElement> GetAncestorList(XName name, bool getMeIn)
		{
			List<XElement> list = new List<XElement>();
			if (getMeIn)
			{
				list.Add(this);
			}
			for (XElement xelement = base.Parent; xelement != null; xelement = xelement.Parent)
			{
				if (name == null || xelement.Name == name)
				{
					list.Add(xelement);
				}
			}
			return list;
		}

		/// <summary>Returns the <see cref="T:System.Xml.Linq.XAttribute" /> of this <see cref="T:System.Xml.Linq.XElement" /> that has the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> that has the specified <see cref="T:System.Xml.Linq.XName" />; null if there is no attribute with the specified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the <see cref="T:System.Xml.Linq.XAttribute" /> to get.</param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00004170 File Offset: 0x00002370
		public XAttribute Attribute(XName name)
		{
			foreach (XAttribute xattribute in this.Attributes())
			{
				if (xattribute.Name == name)
				{
					return xattribute;
				}
			}
			return null;
		}

		/// <summary>Returns a collection of attributes of this element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> of attributes of this element.</returns>
		// Token: 0x060000B5 RID: 181 RVA: 0x000041E8 File Offset: 0x000023E8
		public IEnumerable<XAttribute> Attributes()
		{
			XAttribute next;
			for (XAttribute a = this.attr_first; a != null; a = next)
			{
				next = a.NextAttribute;
				yield return a;
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of attributes of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the attributes of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x060000B6 RID: 182 RVA: 0x0000420C File Offset: 0x0000240C
		public IEnumerable<XAttribute> Attributes(XName name)
		{
			foreach (XAttribute a in this.Attributes())
			{
				if (a.Name == name)
				{
					yield return a;
				}
			}
			yield break;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004240 File Offset: 0x00002440
		private static void DefineDefaultSettings(XmlReaderSettings settings, LoadOptions options)
		{
			settings.ProhibitDtd = false;
			settings.IgnoreWhitespace = (options & LoadOptions.PreserveWhitespace) == LoadOptions.None;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004258 File Offset: 0x00002458
		private static XmlReaderSettings CreateDefaultSettings(LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			XElement.DefineDefaultSettings(xmlReaderSettings, options);
			return xmlReaderSettings;
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a file.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the contents of the specified file.</returns>
		/// <param name="uri">A URI string referencing the file to load into a new <see cref="T:System.Xml.Linq.XElement" />.</param>
		// Token: 0x060000B9 RID: 185 RVA: 0x00004274 File Offset: 0x00002474
		public static XElement Load(string uri)
		{
			return XElement.Load(uri, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a file, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the contents of the specified file.</returns>
		/// <param name="uri">A URI string referencing the file to load into an <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x060000BA RID: 186 RVA: 0x00004280 File Offset: 0x00002480
		public static XElement Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XElement.CreateDefaultSettings(options);
			XElement xelement;
			using (XmlReader xmlReader = XmlReader.Create(uri, xmlReaderSettings))
			{
				xelement = XElement.LoadCore(xmlReader, options);
			}
			return xelement;
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a <see cref="T:System.IO.TextReader" />. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that will be read for the <see cref="T:System.Xml.Linq.XElement" /> content.</param>
		// Token: 0x060000BB RID: 187 RVA: 0x000042D8 File Offset: 0x000024D8
		public static XElement Load(TextReader tr)
		{
			return XElement.Load(tr, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from a <see cref="T:System.IO.TextReader" />, optionally preserving white space and retaining line information. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that will be read for the <see cref="T:System.Xml.Linq.XElement" /> content.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x000042E4 File Offset: 0x000024E4
		public static XElement Load(TextReader tr, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XElement.CreateDefaultSettings(options);
			XElement xelement;
			using (XmlReader xmlReader = XmlReader.Create(tr, xmlReaderSettings))
			{
				xelement = XElement.LoadCore(xmlReader, options);
			}
			return xelement;
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from an <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XElement" />.</param>
		// Token: 0x060000BD RID: 189 RVA: 0x0000433C File Offset: 0x0000253C
		public static XElement Load(XmlReader reader)
		{
			return XElement.Load(reader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from an <see cref="T:System.Xml.XmlReader" />, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XElement" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x00004348 File Offset: 0x00002548
		public static XElement Load(XmlReader reader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = ((reader.Settings == null) ? new XmlReaderSettings() : reader.Settings.Clone());
			XElement.DefineDefaultSettings(xmlReaderSettings, options);
			XElement xelement;
			using (XmlReader xmlReader = XmlReader.Create(reader, xmlReaderSettings))
			{
				xelement = XElement.LoadCore(xmlReader, options);
			}
			return xelement;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000043C4 File Offset: 0x000025C4
		internal static XElement LoadCore(XmlReader r, LoadOptions options)
		{
			r.MoveToContent();
			if (r.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException("The XmlReader must be positioned at an element");
			}
			XName xname = XName.Get(r.LocalName, r.NamespaceURI);
			XElement xelement = new XElement(xname);
			xelement.FillLineInfoAndBaseUri(r, options);
			if (r.MoveToFirstAttribute())
			{
				do
				{
					if (r.LocalName == "xmlns" && r.NamespaceURI == XNamespace.Xmlns.NamespaceName)
					{
						xelement.SetAttributeValue(XNamespace.None.GetName("xmlns"), r.Value);
					}
					else
					{
						xelement.SetAttributeValue(XName.Get(r.LocalName, r.NamespaceURI), r.Value);
					}
					xelement.LastAttribute.FillLineInfoAndBaseUri(r, options);
				}
				while (r.MoveToNextAttribute());
				r.MoveToElement();
			}
			if (!r.IsEmptyElement)
			{
				r.Read();
				xelement.ReadContentFrom(r, options);
				r.ReadEndElement();
				xelement.explicit_is_empty = false;
			}
			else
			{
				xelement.explicit_is_empty = true;
				r.Read();
			}
			return xelement;
		}

		/// <summary>Load an <see cref="T:System.Xml.Linq.XElement" /> from a string that contains XML.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> populated from the string that contains XML.</returns>
		/// <param name="text">A <see cref="T:System.String" /> that contains XML.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x000044E4 File Offset: 0x000026E4
		public static XElement Parse(string s)
		{
			return XElement.Parse(s, LoadOptions.None);
		}

		/// <summary>Load an <see cref="T:System.Xml.Linq.XElement" /> from a string that contains XML, optionally preserving white space and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XElement" /> populated from the string that contains XML.</returns>
		/// <param name="text">A <see cref="T:System.String" /> that contains XML.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x060000C1 RID: 193 RVA: 0x000044F0 File Offset: 0x000026F0
		public static XElement Parse(string s, LoadOptions options)
		{
			return XElement.Load(new StringReader(s), options);
		}

		/// <summary>Removes nodes and attributes from this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		// Token: 0x060000C2 RID: 194 RVA: 0x00004500 File Offset: 0x00002700
		public void RemoveAll()
		{
			this.RemoveAttributes();
			base.RemoveNodes();
		}

		/// <summary>Removes the attributes of this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		// Token: 0x060000C3 RID: 195 RVA: 0x00004510 File Offset: 0x00002710
		public void RemoveAttributes()
		{
			while (this.attr_first != null)
			{
				this.attr_last.Remove();
			}
		}

		/// <summary>Serialize this element to a file.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		// Token: 0x060000C4 RID: 196 RVA: 0x00004530 File Offset: 0x00002730
		public void Save(string filename)
		{
			this.Save(filename, SaveOptions.None);
		}

		/// <summary>Serialize this element to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060000C5 RID: 197 RVA: 0x0000453C File Offset: 0x0000273C
		public void Save(string filename, SaveOptions options)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(filename, new XmlWriterSettings
			{
				Indent = (options != SaveOptions.DisableFormatting)
			}))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this element to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		// Token: 0x060000C6 RID: 198 RVA: 0x0000459C File Offset: 0x0000279C
		public void Save(TextWriter tw)
		{
			this.Save(tw, SaveOptions.None);
		}

		/// <summary>Serialize this element to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x000045A8 File Offset: 0x000027A8
		public void Save(TextWriter tw, SaveOptions options)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(tw, new XmlWriterSettings
			{
				Indent = (options != SaveOptions.DisableFormatting)
			}))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		// Token: 0x060000C8 RID: 200 RVA: 0x00004608 File Offset: 0x00002808
		public void Save(XmlWriter w)
		{
			this.WriteTo(w);
		}

		/// <summary>Returns a collection of elements that contain this element, and the ancestors of this element. </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of elements that contain this element, and the ancestors of this element. </returns>
		// Token: 0x060000C9 RID: 201 RVA: 0x00004614 File Offset: 0x00002814
		public IEnumerable<XElement> AncestorsAndSelf()
		{
			return this.GetAncestorList(null, true);
		}

		/// <summary>Returns a filtered collection of elements that contain this element, and the ancestors of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contain this element, and the ancestors of this element. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00004620 File Offset: 0x00002820
		public IEnumerable<XElement> AncestorsAndSelf(XName name)
		{
			return this.GetAncestorList(name, true);
		}

		/// <summary>Returns a collection of elements that contain this element, and all descendant elements of this element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of elements that contain this element, and all descendant elements of this element, in document order.</returns>
		// Token: 0x060000CB RID: 203 RVA: 0x0000462C File Offset: 0x0000282C
		public IEnumerable<XElement> DescendantsAndSelf()
		{
			List<XElement> list = new List<XElement>();
			list.Add(this);
			list.AddRange(base.Descendants());
			return list;
		}

		/// <summary>Returns a filtered collection of elements that contain this element, and all descendant elements of this element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contain this element, and all descendant elements of this element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x060000CC RID: 204 RVA: 0x00004654 File Offset: 0x00002854
		public IEnumerable<XElement> DescendantsAndSelf(XName name)
		{
			List<XElement> list = new List<XElement>();
			if (name == this.name)
			{
				list.Add(this);
			}
			list.AddRange(base.Descendants(name));
			return list;
		}

		/// <summary>Returns a collection of nodes that contain this element, and all descendant nodes of this element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contain this element, and all descendant nodes of this element, in document order.</returns>
		// Token: 0x060000CD RID: 205 RVA: 0x00004690 File Offset: 0x00002890
		public IEnumerable<XNode> DescendantNodesAndSelf()
		{
			yield return this;
			foreach (XNode node in base.DescendantNodes())
			{
				yield return node;
			}
			yield break;
		}

		/// <summary>Sets the value of an attribute, adds an attribute, or removes an attribute. </summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the attribute to change.</param>
		/// <param name="value">The value to assign to the attribute. The attribute is removed if the value is null. Otherwise, the value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XAttribute.Value" /> property of the attribute.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an instance of <see cref="T:System.Xml.Linq.XObject" />.</exception>
		// Token: 0x060000CE RID: 206 RVA: 0x000046B4 File Offset: 0x000028B4
		public void SetAttributeValue(XName name, object value)
		{
			XAttribute xattribute = this.Attribute(name);
			if (value == null)
			{
				if (xattribute != null)
				{
					xattribute.Remove();
				}
			}
			else if (xattribute == null)
			{
				this.SetAttributeObject(new XAttribute(name, value));
			}
			else
			{
				xattribute.Value = XUtil.ToString(value);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004704 File Offset: 0x00002904
		private void SetAttributeObject(XAttribute a)
		{
			a = (XAttribute)XUtil.GetDetachedObject(a);
			a.SetOwner(this);
			if (this.attr_first == null)
			{
				this.attr_first = a;
				this.attr_last = a;
			}
			else
			{
				this.attr_last.NextAttribute = a;
				a.PreviousAttribute = this.attr_last;
				this.attr_last = a;
			}
		}

		/// <summary>Write this element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000D0 RID: 208 RVA: 0x00004764 File Offset: 0x00002964
		public override void WriteTo(XmlWriter w)
		{
			string text = ((this.name.NamespaceName.Length <= 0) ? string.Empty : w.LookupPrefix(this.name.Namespace.NamespaceName));
			foreach (XAttribute xattribute in this.Attributes())
			{
				if (xattribute.IsNamespaceDeclaration && xattribute.Value == this.name.Namespace.NamespaceName)
				{
					if (xattribute.Name.Namespace == XNamespace.Xmlns)
					{
						text = xattribute.Name.LocalName;
					}
					break;
				}
			}
			w.WriteStartElement(text, this.name.LocalName, this.name.Namespace.NamespaceName);
			foreach (XAttribute xattribute2 in this.Attributes())
			{
				if (xattribute2.IsNamespaceDeclaration)
				{
					if (xattribute2.Name.Namespace == XNamespace.Xmlns)
					{
						w.WriteAttributeString("xmlns", xattribute2.Name.LocalName, XNamespace.Xmlns.NamespaceName, xattribute2.Value);
					}
					else
					{
						w.WriteAttributeString("xmlns", xattribute2.Value);
					}
				}
				else
				{
					w.WriteAttributeString(xattribute2.Name.LocalName, xattribute2.Name.Namespace.NamespaceName, xattribute2.Value);
				}
			}
			foreach (XNode xnode in base.Nodes())
			{
				xnode.WriteTo(w);
			}
			if (this.explicit_is_empty)
			{
				w.WriteEndElement();
			}
			else
			{
				w.WriteFullEndElement();
			}
		}

		/// <summary>Gets the default <see cref="T:System.Xml.Linq.XNamespace" /> of this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the default namespace of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000D1 RID: 209 RVA: 0x000049C4 File Offset: 0x00002BC4
		public XNamespace GetDefaultNamespace()
		{
			for (XElement xelement = this; xelement != null; xelement = xelement.Parent)
			{
				foreach (XAttribute xattribute in xelement.Attributes())
				{
					if (xattribute.IsNamespaceDeclaration && xattribute.Name.Namespace == XNamespace.None)
					{
						return XNamespace.Get(xattribute.Value);
					}
				}
			}
			return XNamespace.None;
		}

		/// <summary>Gets the namespace associated with a particular prefix for this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> for the namespace associated with the prefix for this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="prefix">A string that contains the namespace prefix to look up.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000D2 RID: 210 RVA: 0x00004A74 File Offset: 0x00002C74
		public XNamespace GetNamespaceOfPrefix(string prefix)
		{
			for (XElement xelement = this; xelement != null; xelement = xelement.Parent)
			{
				foreach (XAttribute xattribute in xelement.Attributes())
				{
					if (xattribute.IsNamespaceDeclaration && ((prefix.Length == 0 && xattribute.Name.LocalName == "xmlns") || xattribute.Name.LocalName == prefix))
					{
						return XNamespace.Get(xattribute.Value);
					}
				}
			}
			return XNamespace.None;
		}

		/// <summary>Gets the prefix associated with a namespace for this <see cref="T:System.Xml.Linq.XElement" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace prefix.</returns>
		/// <param name="ns">An <see cref="T:System.Xml.Linq.XNamespace" /> to look up.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000D3 RID: 211 RVA: 0x00004B44 File Offset: 0x00002D44
		public string GetPrefixOfNamespace(XNamespace ns)
		{
			foreach (string text in this.GetPrefixOfNamespaceCore(ns))
			{
				if (this.GetNamespaceOfPrefix(text) == ns)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private IEnumerable<string> GetPrefixOfNamespaceCore(XNamespace ns)
		{
			for (XElement el = this; el != null; el = el.Parent)
			{
				foreach (XAttribute a in el.Attributes())
				{
					if (a.IsNamespaceDeclaration && a.Value == ns.NamespaceName)
					{
						yield return (!(a.Name.Namespace == XNamespace.None)) ? a.Name.LocalName : string.Empty;
					}
				}
			}
			yield break;
		}

		/// <summary>Replaces the child nodes and the attributes of this element with the specified content.</summary>
		/// <param name="content">The content that will replace the child nodes and attributes of this element.</param>
		// Token: 0x060000D5 RID: 213 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public void ReplaceAll(object item)
		{
			base.RemoveNodes();
			base.Add(item);
		}

		/// <summary>Replaces the child nodes and the attributes of this element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		// Token: 0x060000D6 RID: 214 RVA: 0x00004C04 File Offset: 0x00002E04
		public void ReplaceAll(params object[] items)
		{
			base.RemoveNodes();
			base.Add(items);
		}

		/// <summary>Replaces the attributes of this element with the specified content.</summary>
		/// <param name="content">The content that will replace the attributes of this element.</param>
		// Token: 0x060000D7 RID: 215 RVA: 0x00004C14 File Offset: 0x00002E14
		public void ReplaceAttributes(object item)
		{
			this.RemoveAttributes();
			base.Add(item);
		}

		/// <summary>Replaces the attributes of this element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		// Token: 0x060000D8 RID: 216 RVA: 0x00004C24 File Offset: 0x00002E24
		public void ReplaceAttributes(params object[] items)
		{
			this.RemoveAttributes();
			base.Add(items);
		}

		/// <summary>Sets the value of a child element, adds a child element, or removes a child element.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the child element to change.</param>
		/// <param name="value">The value to assign to the child element. The child element is removed if the value is null. Otherwise, the value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XElement.Value" /> property of the child element.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an instance of <see cref="T:System.Xml.Linq.XObject" />.</exception>
		// Token: 0x060000D9 RID: 217 RVA: 0x00004C34 File Offset: 0x00002E34
		public void SetElementValue(XName name, object value)
		{
			XElement xelement = new XElement(name, value);
			base.RemoveNodes();
			base.Add(xelement);
		}

		/// <summary>Sets the value of this element.</summary>
		/// <param name="value">The value to assign to this element. The value is converted to its string representation and assigned to the <see cref="P:System.Xml.Linq.XElement.Value" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an <see cref="T:System.Xml.Linq.XObject" />.</exception>
		// Token: 0x060000DA RID: 218 RVA: 0x00004C58 File Offset: 0x00002E58
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is XAttribute || value is XDocument || value is XDeclaration || value is XDocumentType)
			{
				throw new ArgumentException(string.Format("Node type {0} is not allowed as element value", value.GetType()));
			}
			base.RemoveNodes();
			foreach (object obj in XUtil.ExpandArray(value))
			{
				base.Add(obj);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004D1C File Offset: 0x00002F1C
		internal override bool OnAddingObject(object o, bool rejectAttribute, XNode refNode, bool addFirst)
		{
			if (o is XDocument || o is XDocumentType || o is XDeclaration || (rejectAttribute && o is XAttribute))
			{
				throw new ArgumentException(string.Format("A node of type {0} cannot be added as a content", o.GetType()));
			}
			XAttribute xattribute = o as XAttribute;
			if (xattribute != null)
			{
				foreach (XAttribute xattribute2 in this.Attributes())
				{
					if (xattribute.Name == xattribute2.Name)
					{
						throw new InvalidOperationException(string.Format("Duplicate attribute: {0}", xattribute.Name));
					}
				}
				this.SetAttributeObject(xattribute);
				return true;
			}
			if (o is string && refNode is XText)
			{
				XText xtext = (XText)refNode;
				xtext.Value += o as string;
				return true;
			}
			return false;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Boolean" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000DC RID: 220 RVA: 0x00004E38 File Offset: 0x00003038
		public static explicit operator bool(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XUtil.ConvertToBoolean(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		// Token: 0x060000DD RID: 221 RVA: 0x00004E58 File Offset: 0x00003058
		public static explicit operator bool?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new bool?(XUtil.ConvertToBoolean(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.DateTime" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000DE RID: 222 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static explicit operator DateTime(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XUtil.ToDateTime(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		// Token: 0x060000DF RID: 223 RVA: 0x00004EC0 File Offset: 0x000030C0
		public static explicit operator DateTime?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new DateTime?(XUtil.ToDateTime(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.DateTimeOffset" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00004F08 File Offset: 0x00003108
		public static explicit operator DateTimeOffset(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDateTimeOffset(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to an <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		// Token: 0x060000E1 RID: 225 RVA: 0x00004F28 File Offset: 0x00003128
		public static explicit operator DateTimeOffset?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new DateTimeOffset?(XmlConvert.ToDateTimeOffset(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000E2 RID: 226 RVA: 0x00004F70 File Offset: 0x00003170
		public static explicit operator decimal(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDecimal(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		// Token: 0x060000E3 RID: 227 RVA: 0x00004F90 File Offset: 0x00003190
		public static explicit operator decimal?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new decimal?(XmlConvert.ToDecimal(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Double" />.</summary>
		/// <returns>A <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Double" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Double" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000E4 RID: 228 RVA: 0x00004FD8 File Offset: 0x000031D8
		public static explicit operator double(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToDouble(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Double" /> value.</exception>
		// Token: 0x060000E5 RID: 229 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static explicit operator double?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new double?(XmlConvert.ToDouble(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Single" />.</summary>
		/// <returns>A <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Single" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Single" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000E6 RID: 230 RVA: 0x00005040 File Offset: 0x00003240
		public static explicit operator float(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToSingle(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Single" /> value.</exception>
		// Token: 0x060000E7 RID: 231 RVA: 0x00005060 File Offset: 0x00003260
		public static explicit operator float?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new float?(XmlConvert.ToSingle(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Guid" />.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000E8 RID: 232 RVA: 0x000050A8 File Offset: 0x000032A8
		public static explicit operator Guid(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToGuid(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		// Token: 0x060000E9 RID: 233 RVA: 0x000050C8 File Offset: 0x000032C8
		public static explicit operator Guid?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new Guid?(XmlConvert.ToGuid(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to an <see cref="T:System.Int32" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Int32" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000EA RID: 234 RVA: 0x00005110 File Offset: 0x00003310
		public static explicit operator int(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		// Token: 0x060000EB RID: 235 RVA: 0x00005130 File Offset: 0x00003330
		public static explicit operator int?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new int?(XmlConvert.ToInt32(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to an <see cref="T:System.Int64" />.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Int64" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000EC RID: 236 RVA: 0x00005178 File Offset: 0x00003378
		public static explicit operator long(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		// Token: 0x060000ED RID: 237 RVA: 0x00005198 File Offset: 0x00003398
		public static explicit operator long?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new long?(XmlConvert.ToInt64(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.UInt32" />.</summary>
		/// <returns>A <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.UInt32" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000EE RID: 238 RVA: 0x000051E0 File Offset: 0x000033E0
		[CLSCompliant(false)]
		public static explicit operator uint(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt32(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		// Token: 0x060000EF RID: 239 RVA: 0x00005200 File Offset: 0x00003400
		[CLSCompliant(false)]
		public static explicit operator uint?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new uint?(XmlConvert.ToUInt32(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.UInt64" />.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.UInt64" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000F0 RID: 240 RVA: 0x00005248 File Offset: 0x00003448
		[CLSCompliant(false)]
		public static explicit operator ulong(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToUInt64(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		// Token: 0x060000F1 RID: 241 RVA: 0x00005268 File Offset: 0x00003468
		[CLSCompliant(false)]
		public static explicit operator ulong?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new ulong?(XmlConvert.ToUInt64(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.TimeSpan" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		// Token: 0x060000F2 RID: 242 RVA: 0x000052B0 File Offset: 0x000034B0
		public static explicit operator TimeSpan(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return XmlConvert.ToTimeSpan(element.Value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</param>
		/// <exception cref="T:System.FormatException">The element does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x060000F3 RID: 243 RVA: 0x000052D0 File Offset: 0x000034D0
		public static explicit operator TimeSpan?(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return (element.Value != null) ? new TimeSpan?(XmlConvert.ToTimeSpan(element.Value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XElement" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the content of this <see cref="T:System.Xml.Linq.XElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Linq.XElement" /> to cast to <see cref="T:System.String" />.</param>
		// Token: 0x060000F4 RID: 244 RVA: 0x00005318 File Offset: 0x00003518
		public static explicit operator string(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return element.Value;
		}

		// Token: 0x04000037 RID: 55
		private static IEnumerable<XElement> emptySequence = new List<XElement>();

		// Token: 0x04000038 RID: 56
		private XName name;

		// Token: 0x04000039 RID: 57
		private XAttribute attr_first;

		// Token: 0x0400003A RID: 58
		private XAttribute attr_last;

		// Token: 0x0400003B RID: 59
		private bool explicit_is_empty = true;
	}
}
