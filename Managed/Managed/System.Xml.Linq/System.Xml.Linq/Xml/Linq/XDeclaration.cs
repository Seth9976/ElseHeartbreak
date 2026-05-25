using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML declaration.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000011 RID: 17
	public class XDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class with the specified version, encoding, and standalone status.</summary>
		/// <param name="version">The version of the XML, usually "1.0".</param>
		/// <param name="encoding">The encoding for the XML document.</param>
		/// <param name="standalone">A string containing "yes" or "no" that specifies whether the XML is standalone or requires external entities to be resolved.</param>
		// Token: 0x06000069 RID: 105 RVA: 0x0000347C File Offset: 0x0000167C
		public XDeclaration(string version, string encoding, string standalone)
		{
			this.version = version;
			this.encoding = encoding;
			this.standalone = standalone;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDeclaration" /> class from another <see cref="T:System.Xml.Linq.XDeclaration" /> object. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDeclaration" /> used to initialize this <see cref="T:System.Xml.Linq.XDeclaration" /> object.</param>
		// Token: 0x0600006A RID: 106 RVA: 0x0000349C File Offset: 0x0000169C
		public XDeclaration(XDeclaration other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.version = other.version;
			this.encoding = other.encoding;
			this.standalone = other.standalone;
		}

		/// <summary>Gets or sets the encoding for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the code page name for this document.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000034DC File Offset: 0x000016DC
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000034E4 File Offset: 0x000016E4
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the standalone property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the standalone property for this document.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000034F8 File Offset: 0x000016F8
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				this.standalone = value;
			}
		}

		/// <summary>Gets or sets the version property for this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the version property for this document.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003504 File Offset: 0x00001704
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000350C File Offset: 0x0000170C
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Provides the declaration as a formatted string.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the formatted XML string.</returns>
		// Token: 0x06000071 RID: 113 RVA: 0x00003518 File Offset: 0x00001718
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"<?xml",
				(this.version == null) ? null : " version=\"",
				(this.version == null) ? null : this.version,
				(this.version == null) ? null : "\"",
				(this.encoding == null) ? null : " encoding=\"",
				(this.encoding == null) ? null : this.encoding,
				(this.encoding == null) ? null : "\"",
				(this.standalone == null) ? null : " standalone=\"",
				(this.standalone == null) ? null : this.standalone,
				(this.standalone == null) ? null : "\"",
				"?>"
			});
		}

		// Token: 0x0400002F RID: 47
		private string encoding;

		// Token: 0x04000030 RID: 48
		private string standalone;

		// Token: 0x04000031 RID: 49
		private string version;
	}
}
