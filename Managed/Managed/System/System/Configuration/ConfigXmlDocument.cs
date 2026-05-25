using System;
using System.Configuration.Internal;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Wraps the corresponding <see cref="T:System.Xml.XmlDocument" /> type and also carries the necessary information for reporting file-name and line numbers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D4 RID: 468
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigErrorInfo, IConfigXmlNode
	{
		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The file name.</returns>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002BD5C File Offset: 0x00029F5C
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		/// <summary>Gets the configuration line number.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002BD64 File Offset: 0x00029F64
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002BD6C File Offset: 0x00029F6C
		string IConfigXmlNode.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0002BD74 File Offset: 0x00029F74
		int IConfigXmlNode.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		/// <summary>Creates a configuration element attribute.</summary>
		/// <returns>The <see cref="P:System.Xml.Serialization.XmlAttributes.XmlAttribute" /> attribute.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name that is used locally.</param>
		/// <param name="namespaceUri">The URL that is assigned to the namespace.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001058 RID: 4184 RVA: 0x0002BD7C File Offset: 0x00029F7C
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlAttribute(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates an XML CData section.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlCDataSection" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001059 RID: 4185 RVA: 0x0002BD88 File Offset: 0x00029F88
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlDocument.ConfigXmlCDataSection(this, data);
		}

		/// <summary>Create an XML comment.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlComment" /> value.</returns>
		/// <param name="data">The comment data.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105A RID: 4186 RVA: 0x0002BD94 File Offset: 0x00029F94
		public override XmlComment CreateComment(string comment)
		{
			return new ConfigXmlDocument.ConfigXmlComment(this, comment);
		}

		/// <summary>Creates a configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> value.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name used locally.</param>
		/// <param name="namespaceUri">The namespace for the URL.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105B RID: 4187 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlElement(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates white spaces.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlSignificantWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105C RID: 4188 RVA: 0x0002BDAC File Offset: 0x00029FAC
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return base.CreateSignificantWhitespace(data);
		}

		/// <summary>Create a text node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlText" /> value.</returns>
		/// <param name="text">The text to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105D RID: 4189 RVA: 0x0002BDB8 File Offset: 0x00029FB8
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlDocument.ConfigXmlText(this, text);
		}

		/// <summary>Creates white space.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105E RID: 4190 RVA: 0x0002BDC4 File Offset: 0x00029FC4
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return base.CreateWhitespace(data);
		}

		/// <summary>Loads the configuration file.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600105F RID: 4191 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		public override void Load(string filename)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(filename);
			try
			{
				xmlTextReader.MoveToContent();
				this.LoadSingleElement(filename, xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads a single configuration element.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <param name="sourceReader">The source for the reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001060 RID: 4192 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this.fileName = filename;
			this.lineNumber = sourceReader.LineNumber;
			string text = sourceReader.ReadOuterXml();
			this.reader = new XmlTextReader(new StringReader(text), sourceReader.NameTable);
			this.Load(this.reader);
			this.reader.Close();
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The configuration file name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0002BE74 File Offset: 0x0002A074
		public string Filename
		{
			get
			{
				if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				}
				return this.fileName;
			}
		}

		/// <summary>Gets the current node line number.</summary>
		/// <returns>The line number for the current node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0002BEB4 File Offset: 0x0002A0B4
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x04000496 RID: 1174
		private XmlTextReader reader;

		// Token: 0x04000497 RID: 1175
		private string fileName;

		// Token: 0x04000498 RID: 1176
		private int lineNumber;

		// Token: 0x020001D5 RID: 469
		private class ConfigXmlAttribute : XmlAttribute, IConfigErrorInfo, IConfigXmlNode
		{
			// Token: 0x06001063 RID: 4195 RVA: 0x0002BEBC File Offset: 0x0002A0BC
			public ConfigXmlAttribute(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x06001064 RID: 4196 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x06001065 RID: 4197 RVA: 0x0002BF24 File Offset: 0x0002A124
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000499 RID: 1177
			private string fileName;

			// Token: 0x0400049A RID: 1178
			private int lineNumber;
		}

		// Token: 0x020001D6 RID: 470
		private class ConfigXmlCDataSection : XmlCDataSection, IConfigErrorInfo, IConfigXmlNode
		{
			// Token: 0x06001066 RID: 4198 RVA: 0x0002BF2C File Offset: 0x0002A12C
			public ConfigXmlCDataSection(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x06001067 RID: 4199 RVA: 0x0002BF5C File Offset: 0x0002A15C
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06001068 RID: 4200 RVA: 0x0002BF9C File Offset: 0x0002A19C
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400049B RID: 1179
			private string fileName;

			// Token: 0x0400049C RID: 1180
			private int lineNumber;
		}

		// Token: 0x020001D7 RID: 471
		private class ConfigXmlComment : XmlComment, IConfigXmlNode
		{
			// Token: 0x06001069 RID: 4201 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
			public ConfigXmlComment(ConfigXmlDocument document, string comment)
				: base(comment, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x0600106A RID: 4202 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x0600106B RID: 4203 RVA: 0x0002C014 File Offset: 0x0002A214
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400049D RID: 1181
			private string fileName;

			// Token: 0x0400049E RID: 1182
			private int lineNumber;
		}

		// Token: 0x020001D8 RID: 472
		private class ConfigXmlElement : XmlElement, IConfigErrorInfo, IConfigXmlNode
		{
			// Token: 0x0600106C RID: 4204 RVA: 0x0002C01C File Offset: 0x0002A21C
			public ConfigXmlElement(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x0600106D RID: 4205 RVA: 0x0002C044 File Offset: 0x0002A244
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x0600106E RID: 4206 RVA: 0x0002C084 File Offset: 0x0002A284
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400049F RID: 1183
			private string fileName;

			// Token: 0x040004A0 RID: 1184
			private int lineNumber;
		}

		// Token: 0x020001D9 RID: 473
		private class ConfigXmlText : XmlText, IConfigErrorInfo, IConfigXmlNode
		{
			// Token: 0x0600106F RID: 4207 RVA: 0x0002C08C File Offset: 0x0002A28C
			public ConfigXmlText(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06001070 RID: 4208 RVA: 0x0002C0BC File Offset: 0x0002A2BC
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06001071 RID: 4209 RVA: 0x0002C0FC File Offset: 0x0002A2FC
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x040004A1 RID: 1185
			private string fileName;

			// Token: 0x040004A2 RID: 1186
			private int lineNumber;
		}
	}
}
