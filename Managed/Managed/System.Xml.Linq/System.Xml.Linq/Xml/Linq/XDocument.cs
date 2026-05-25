using System;
using System.IO;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML document. </summary>
	// Token: 0x02000012 RID: 18
	public class XDocument : XContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class. </summary>
		// Token: 0x06000072 RID: 114 RVA: 0x00003628 File Offset: 0x00001828
		public XDocument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified content.</summary>
		/// <param name="content">A parameter list of content objects to add to this document.</param>
		// Token: 0x06000073 RID: 115 RVA: 0x00003630 File Offset: 0x00001830
		public XDocument(params object[] content)
		{
			base.Add(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified <see cref="T:System.Xml.Linq.XDeclaration" /> and content.</summary>
		/// <param name="declaration">An <see cref="T:System.Xml.Linq.XDeclaration" /> for the document.</param>
		/// <param name="content">The content of the document.</param>
		// Token: 0x06000074 RID: 116 RVA: 0x00003640 File Offset: 0x00001840
		public XDocument(XDeclaration xmldecl, params object[] content)
		{
			this.Declaration = xmldecl;
			base.Add(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class from an existing <see cref="T:System.Xml.Linq.XDocument" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDocument" /> object that will be copied.</param>
		// Token: 0x06000075 RID: 117 RVA: 0x00003658 File Offset: 0x00001858
		public XDocument(XDocument other)
		{
			foreach (object obj in other.Nodes())
			{
				base.Add(XUtil.Clone(obj));
			}
		}

		/// <summary>Gets or sets the XML declaration for this document.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDeclaration" /> that contains the XML declaration for this document.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036C8 File Offset: 0x000018C8
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000036D0 File Offset: 0x000018D0
		public XDeclaration Declaration
		{
			get
			{
				return this.xmldecl;
			}
			set
			{
				this.xmldecl = value;
			}
		}

		/// <summary>Gets the Document Type Definition (DTD) for this document.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XDocumentType" /> that contains the DTD for this document.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000036DC File Offset: 0x000018DC
		public XDocumentType DocumentType
		{
			get
			{
				foreach (object obj in base.Nodes())
				{
					if (obj is XDocumentType)
					{
						return (XDocumentType)obj;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocument" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Document" />.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003754 File Offset: 0x00001954
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		/// <summary>Gets the root element of the XML Tree for this document.</summary>
		/// <returns>The root <see cref="T:System.Xml.Linq.XElement" /> of the XML tree.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003758 File Offset: 0x00001958
		public XElement Root
		{
			get
			{
				foreach (object obj in base.Nodes())
				{
					if (obj is XElement)
					{
						return (XElement)obj;
					}
				}
				return null;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		// Token: 0x0600007B RID: 123 RVA: 0x000037D0 File Offset: 0x000019D0
		public static XDocument Load(string uri)
		{
			return XDocument.Load(uri, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x0600007C RID: 124 RVA: 0x000037DC File Offset: 0x000019DC
		public static XDocument Load(string uri, LoadOptions options)
		{
			XDocument xdocument;
			using (XmlReader xmlReader = XmlReader.Create(uri, new XmlReaderSettings
			{
				IgnoreWhitespace = ((options & LoadOptions.PreserveWhitespace) == LoadOptions.None)
			}))
			{
				xdocument = XDocument.LoadCore(xmlReader, options);
			}
			return xdocument;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003840 File Offset: 0x00001A40
		public static XDocument Load(Stream stream)
		{
			return XDocument.Load(new StreamReader(stream), LoadOptions.None);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003850 File Offset: 0x00001A50
		public static XDocument Load(Stream stream, LoadOptions options)
		{
			return XDocument.Load(new StreamReader(stream), options);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		// Token: 0x0600007F RID: 127 RVA: 0x00003860 File Offset: 0x00001A60
		public static XDocument Load(TextReader reader)
		{
			return XDocument.Load(reader, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x06000080 RID: 128 RVA: 0x0000386C File Offset: 0x00001A6C
		public static XDocument Load(TextReader reader, LoadOptions options)
		{
			XDocument xdocument;
			using (XmlReader xmlReader = XmlReader.Create(reader, new XmlReaderSettings
			{
				IgnoreWhitespace = ((options & LoadOptions.PreserveWhitespace) == LoadOptions.None)
			}))
			{
				xdocument = XDocument.LoadCore(xmlReader, options);
			}
			return xdocument;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from an <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		// Token: 0x06000081 RID: 129 RVA: 0x000038D0 File Offset: 0x00001AD0
		public static XDocument Load(XmlReader reader)
		{
			return XDocument.Load(reader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XElement" /> from an <see cref="T:System.Xml.XmlReader" />, optionally setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies whether to load base URI and line information.</param>
		// Token: 0x06000082 RID: 130 RVA: 0x000038DC File Offset: 0x00001ADC
		public static XDocument Load(XmlReader reader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = ((reader.Settings == null) ? new XmlReaderSettings() : reader.Settings.Clone());
			xmlReaderSettings.IgnoreWhitespace = (options & LoadOptions.PreserveWhitespace) == LoadOptions.None;
			XDocument xdocument;
			using (XmlReader xmlReader = XmlReader.Create(reader, xmlReaderSettings))
			{
				xdocument = XDocument.LoadCore(xmlReader, options);
			}
			return xdocument;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000395C File Offset: 0x00001B5C
		private static XDocument LoadCore(XmlReader reader, LoadOptions options)
		{
			XDocument xdocument = new XDocument();
			xdocument.ReadContent(reader, options);
			return xdocument;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003978 File Offset: 0x00001B78
		private void ReadContent(XmlReader reader, LoadOptions options)
		{
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				this.Declaration = new XDeclaration(reader.GetAttribute("version"), reader.GetAttribute("encoding"), reader.GetAttribute("standalone"));
				reader.Read();
			}
			base.ReadContentFrom(reader, options);
			if (this.Root == null)
			{
				throw new InvalidOperationException("The document element is missing.");
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000039F8 File Offset: 0x00001BF8
		private static void ValidateWhitespace(string s)
		{
			foreach (char c in s)
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
						throw new ArgumentException("Non-whitespace text appears directly in the document.");
					}
					break;
				}
			}
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		/// <param name="text">A string that contains XML.</param>
		// Token: 0x06000086 RID: 134 RVA: 0x00003A60 File Offset: 0x00001C60
		public static XDocument Parse(string s)
		{
			return XDocument.Parse(s, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		/// <param name="text">A string that contains XML.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		// Token: 0x06000087 RID: 135 RVA: 0x00003A6C File Offset: 0x00001C6C
		public static XDocument Parse(string s, LoadOptions options)
		{
			return XDocument.Load(new StringReader(s), options);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		// Token: 0x06000088 RID: 136 RVA: 0x00003A7C File Offset: 0x00001C7C
		public void Save(string filename)
		{
			this.Save(filename, SaveOptions.None);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x06000089 RID: 137 RVA: 0x00003A88 File Offset: 0x00001C88
		public void Save(string filename, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			if ((options & SaveOptions.DisableFormatting) == SaveOptions.None)
			{
				xmlWriterSettings.Indent = true;
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(filename, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		// Token: 0x0600008A RID: 138 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public void Save(TextWriter tw)
		{
			this.Save(tw, SaveOptions.None);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x0600008B RID: 139 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public void Save(TextWriter tw, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			if ((options & SaveOptions.DisableFormatting) == SaveOptions.None)
			{
				xmlWriterSettings.Indent = true;
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(tw, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		// Token: 0x0600008C RID: 140 RVA: 0x00003B54 File Offset: 0x00001D54
		public void Save(XmlWriter w)
		{
			this.WriteTo(w);
		}

		/// <summary>Write this document to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600008D RID: 141 RVA: 0x00003B60 File Offset: 0x00001D60
		public override void WriteTo(XmlWriter w)
		{
			if (this.xmldecl != null)
			{
				if (this.xmldecl.Standalone != null)
				{
					w.WriteStartDocument(this.xmldecl.Standalone == "yes");
				}
				else
				{
					w.WriteStartDocument();
				}
			}
			foreach (XNode xnode in base.Nodes())
			{
				xnode.WriteTo(w);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C04 File Offset: 0x00001E04
		internal override bool OnAddingObject(object obj, bool rejectAttribute, XNode refNode, bool addFirst)
		{
			this.VerifyAddedNode(obj, addFirst);
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C10 File Offset: 0x00001E10
		private void VerifyAddedNode(object node, bool addFirst)
		{
			if (node == null)
			{
				throw new InvalidOperationException("Only a node is allowed here");
			}
			if (node is string)
			{
				XDocument.ValidateWhitespace((string)node);
			}
			if (node is XText)
			{
				XDocument.ValidateWhitespace(((XText)node).Value);
			}
			else if (node is XDocumentType)
			{
				if (this.DocumentType != null)
				{
					throw new InvalidOperationException("There already is another document type declaration");
				}
				if (this.Root != null && !addFirst)
				{
					throw new InvalidOperationException("A document type cannot be added after the document element");
				}
			}
			else if (node is XElement)
			{
				if (this.Root != null)
				{
					throw new InvalidOperationException("There already is another document element");
				}
				if (this.DocumentType != null && addFirst)
				{
					throw new InvalidOperationException("An element cannot be added before the document type declaration");
				}
			}
		}

		// Token: 0x04000032 RID: 50
		private XDeclaration xmldecl;
	}
}
