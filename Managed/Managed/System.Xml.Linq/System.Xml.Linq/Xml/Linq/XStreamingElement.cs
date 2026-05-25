using System;
using System.Collections.Generic;
using System.IO;

namespace System.Xml.Linq
{
	/// <summary>Represents elements in an XML tree that supports deferred streaming output.</summary>
	// Token: 0x02000023 RID: 35
	public class XStreamingElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		// Token: 0x060001D0 RID: 464 RVA: 0x00008C3C File Offset: 0x00006E3C
		public XStreamingElement(XName name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		// Token: 0x060001D1 RID: 465 RVA: 0x00008C4C File Offset: 0x00006E4C
		public XStreamingElement(XName name, object content)
			: this(name)
		{
			this.Add(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		// Token: 0x060001D2 RID: 466 RVA: 0x00008C5C File Offset: 0x00006E5C
		public XStreamingElement(XName name, params object[] content)
			: this(name)
		{
			this.Add(content);
		}

		/// <summary>Gets or sets the name of this streaming element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this streaming element.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008C6C File Offset: 0x00006E6C
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008C74 File Offset: 0x00006E74
		public XName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008C80 File Offset: 0x00006E80
		internal IEnumerable<object> Contents
		{
			get
			{
				return this.contents;
			}
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		// Token: 0x060001D6 RID: 470 RVA: 0x00008C88 File Offset: 0x00006E88
		public void Add(object content)
		{
			if (this.contents == null)
			{
				this.contents = new List<object>();
			}
			this.contents.Add(content);
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		// Token: 0x060001D7 RID: 471 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public void Add(params object[] content)
		{
			if (this.contents == null)
			{
				this.contents = new List<object>();
			}
			this.contents.Add(content);
		}

		/// <summary>Serialize this streaming element to a file.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		// Token: 0x060001D8 RID: 472 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public void Save(string fileName)
		{
			using (TextWriter textWriter = File.CreateText(fileName))
			{
				this.Save(textWriter);
			}
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XStreamingElement" /> will be written to.</param>
		// Token: 0x060001D9 RID: 473 RVA: 0x00008D34 File Offset: 0x00006F34
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, SaveOptions.None);
		}

		/// <summary>Serialize this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		// Token: 0x060001DA RID: 474 RVA: 0x00008D40 File Offset: 0x00006F40
		public void Save(XmlWriter writer)
		{
			this.WriteTo(writer);
		}

		/// <summary>Serialize this streaming element to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060001DB RID: 475 RVA: 0x00008D4C File Offset: 0x00006F4C
		public void Save(string fileName, SaveOptions options)
		{
			using (TextWriter textWriter = File.CreateText(fileName))
			{
				this.Save(textWriter, options);
			}
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060001DC RID: 476 RVA: 0x00008D98 File Offset: 0x00006F98
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = (options != SaveOptions.DisableFormatting)
			}))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Returns the formatted (indented) XML for this streaming element.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		// Token: 0x060001DD RID: 477 RVA: 0x00008DFC File Offset: 0x00006FFC
		public override string ToString()
		{
			return this.ToString(SaveOptions.None);
		}

		/// <summary>Returns the XML for this streaming element, optionally disabling formatting.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060001DE RID: 478 RVA: 0x00008E08 File Offset: 0x00007008
		public string ToString(SaveOptions options)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, options);
			return stringWriter.ToString();
		}

		/// <summary>Writes this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001DF RID: 479 RVA: 0x00008E2C File Offset: 0x0000702C
		public void WriteTo(XmlWriter writer)
		{
			writer.WriteStartElement(this.name.LocalName, this.name.Namespace.NamespaceName);
			this.WriteContents(this.contents, writer);
			writer.WriteEndElement();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008E70 File Offset: 0x00007070
		private void WriteContents(IEnumerable<object> items, XmlWriter w)
		{
			foreach (object obj in XUtil.ExpandArray(items))
			{
				if (obj != null)
				{
					if (obj is XNode)
					{
						((XNode)obj).WriteTo(w);
					}
					else if (obj is object[])
					{
						this.WriteContents((object[])obj, w);
					}
					else if (obj is XAttribute)
					{
						this.WriteAttribute((XAttribute)obj, w);
					}
					else
					{
						new XText(obj.ToString()).WriteTo(w);
					}
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008F48 File Offset: 0x00007148
		private void WriteAttribute(XAttribute a, XmlWriter w)
		{
			if (a.IsNamespaceDeclaration)
			{
				if (a.Name.Namespace == XNamespace.Xmlns)
				{
					w.WriteAttributeString("xmlns", a.Name.LocalName, XNamespace.Xmlns.NamespaceName, a.Value);
				}
				else
				{
					w.WriteAttributeString("xmlns", a.Value);
				}
			}
			else
			{
				w.WriteAttributeString(a.Name.LocalName, a.Name.Namespace.NamespaceName, a.Value);
			}
		}

		// Token: 0x04000078 RID: 120
		private XName name;

		// Token: 0x04000079 RID: 121
		private List<object> contents;
	}
}
