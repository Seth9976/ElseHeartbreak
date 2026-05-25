using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML Document Type Definition (DTD). </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000013 RID: 19
	public class XDocumentType : XNode
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class. </summary>
		/// <param name="name">A <see cref="T:System.String" /> that contains the qualified name of the DTD, which is the same as the qualified name of the root element of the XML document.</param>
		/// <param name="publicId">A <see cref="T:System.String" /> that contains the public identifier of an external public DTD.</param>
		/// <param name="systemId">A <see cref="T:System.String" /> that contains the system identifier of an external private DTD.</param>
		/// <param name="internalSubset">A <see cref="T:System.String" /> that contains the internal subset for an internal DTD.</param>
		// Token: 0x06000090 RID: 144 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public XDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			this.name = name;
			this.pubid = publicId;
			this.sysid = systemId;
			this.intSubset = internalSubset;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class from another <see cref="T:System.Xml.Linq.XDocumentType" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XDocumentType" /> object to copy from.</param>
		// Token: 0x06000091 RID: 145 RVA: 0x00003D0C File Offset: 0x00001F0C
		public XDocumentType(XDocumentType other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.pubid = other.pubid;
			this.sysid = other.sysid;
			this.intSubset = other.intSubset;
		}

		/// <summary>Gets or sets the name for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name for this Document Type Definition (DTD).</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003D60 File Offset: 0x00001F60
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003D68 File Offset: 0x00001F68
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the public identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003D84 File Offset: 0x00001F84
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003D8C File Offset: 0x00001F8C
		public string PublicId
		{
			get
			{
				return this.pubid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.pubid = value;
			}
		}

		/// <summary>Gets or sets the system identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the system identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003DA8 File Offset: 0x00001FA8
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public string SystemId
		{
			get
			{
				return this.sysid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.sysid = value;
			}
		}

		/// <summary>Gets or sets the internal subset for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the internal subset for this Document Type Definition (DTD).</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public string InternalSubset
		{
			get
			{
				return this.intSubset;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.intSubset = value;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocumentType" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.DocumentType" />.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Write this <see cref="T:System.Xml.Linq.XDocumentType" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009B RID: 155 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public override void WriteTo(XmlWriter w)
		{
			XDocument document = base.Document;
			XElement root = document.Root;
			if (root != null)
			{
				w.WriteDocType(root.Name.LocalName, this.pubid, this.sysid, this.intSubset);
			}
		}

		// Token: 0x04000033 RID: 51
		private string name;

		// Token: 0x04000034 RID: 52
		private string pubid;

		// Token: 0x04000035 RID: 53
		private string sysid;

		// Token: 0x04000036 RID: 54
		private string intSubset;
	}
}
