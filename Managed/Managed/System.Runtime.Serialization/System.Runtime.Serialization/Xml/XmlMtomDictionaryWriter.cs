using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000056 RID: 86
	internal class XmlMtomDictionaryWriter : XmlDictionaryWriter
	{
		// Token: 0x0600037E RID: 894 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public XmlMtomDictionaryWriter(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream)
		{
			this.writer = new StreamWriter(stream, encoding);
			this.max_bytes = maxSizeInBytes;
			this.write_headers = writeMessageHeaders;
			this.owns_stream = ownsStream;
			this.xml_writer_settings = new XmlWriterSettings
			{
				Encoding = encoding,
				OmitXmlDeclaration = true
			};
			ContentType contentType = new ContentType("multipart/related");
			contentType.Parameters["type"] = "application/xop+xml";
			contentType.Boundary = boundary;
			contentType.Parameters["start"] = "<" + startUri + ">";
			contentType.Parameters["start-info"] = startInfo;
			this.content_type = contentType;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010464 File Offset: 0x0000E664
		private XmlWriter CreateWriter()
		{
			return XmlWriter.Create(this.writer, this.xml_writer_settings);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010478 File Offset: 0x0000E678
		public override void Close()
		{
			this.w.Close();
			if (this.owns_stream)
			{
				this.writer.Close();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001049C File Offset: 0x0000E69C
		public override void Flush()
		{
			this.w.Flush();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000104AC File Offset: 0x0000E6AC
		public override string LookupPrefix(string namespaceUri)
		{
			return this.w.LookupPrefix(namespaceUri);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000104BC File Offset: 0x0000E6BC
		public override void WriteBase64(byte[] bytes, int start, int length)
		{
			this.CheckState();
			this.w.WriteBase64(bytes, start, length);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public override void WriteCData(string text)
		{
			this.CheckState();
			this.w.WriteCData(text);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000104E8 File Offset: 0x0000E6E8
		public override void WriteCharEntity(char c)
		{
			this.CheckState();
			this.w.WriteCharEntity(c);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000104FC File Offset: 0x0000E6FC
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckState();
			this.w.WriteChars(buffer, index, count);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00010514 File Offset: 0x0000E714
		public override void WriteComment(string comment)
		{
			this.CheckState();
			this.w.WriteComment(comment);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010528 File Offset: 0x0000E728
		public override void WriteDocType(string name, string pubid, string sysid, string intSubset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010530 File Offset: 0x0000E730
		public override void WriteEndAttribute()
		{
			this.w.WriteEndAttribute();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010540 File Offset: 0x0000E740
		public override void WriteEndDocument()
		{
			this.w.WriteEndDocument();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00010550 File Offset: 0x0000E750
		public override void WriteEndElement()
		{
			this.w.WriteEndElement();
			if (--this.depth == 0)
			{
				this.WriteEndOfMimeSection();
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00010584 File Offset: 0x0000E784
		public override void WriteEntityRef(string name)
		{
			this.w.WriteEntityRef(name);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00010594 File Offset: 0x0000E794
		public override void WriteFullEndElement()
		{
			this.w.WriteFullEndElement();
			if (--this.depth == 0)
			{
				this.WriteEndOfMimeSection();
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000105C8 File Offset: 0x0000E7C8
		public override void WriteProcessingInstruction(string name, string data)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public override void WriteRaw(string raw)
		{
			this.CheckState();
			this.w.WriteRaw(raw);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public override void WriteRaw(char[] chars, int index, int count)
		{
			this.CheckState();
			this.w.WriteRaw(chars, index, count);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000105FC File Offset: 0x0000E7FC
		public override void WriteStartAttribute(string prefix, string localName, string namespaceURI)
		{
			this.CheckState();
			this.w.WriteStartAttribute(prefix, localName, namespaceURI);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00010614 File Offset: 0x0000E814
		public override void WriteStartDocument()
		{
			this.CheckState();
			this.w.WriteStartDocument();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010628 File Offset: 0x0000E828
		public override void WriteStartDocument(bool standalone)
		{
			this.CheckState();
			this.w.WriteStartDocument(standalone);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001063C File Offset: 0x0000E83C
		public override void WriteStartElement(string prefix, string localName, string namespaceURI)
		{
			this.CheckState();
			if (this.depth == 0)
			{
				this.WriteStartOfMimeSection();
			}
			this.w.WriteStartElement(prefix, localName, namespaceURI);
			this.depth++;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001067C File Offset: 0x0000E87C
		public override WriteState WriteState
		{
			get
			{
				return this.w.WriteState;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001068C File Offset: 0x0000E88C
		public override void WriteString(string text)
		{
			this.CheckState();
			int num = 0;
			for (;;)
			{
				int num2 = text.IndexOf('\r', num);
				if (num2 < 0)
				{
					break;
				}
				this.w.WriteString(text.Substring(num, num2 - num));
				this.WriteCharEntity('\r');
				num = num2 + 1;
			}
			this.w.WriteString(text.Substring(num));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public override void WriteSurrogateCharEntity(char low, char high)
		{
			this.CheckState();
			this.w.WriteSurrogateCharEntity(low, high);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001070C File Offset: 0x0000E90C
		public override void WriteWhitespace(string text)
		{
			this.CheckState();
			this.w.WriteWhitespace(text);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00010720 File Offset: 0x0000E920
		public override string XmlLang
		{
			get
			{
				return this.w.XmlLang;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00010730 File Offset: 0x0000E930
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.w.XmlSpace;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00010740 File Offset: 0x0000E940
		private void CheckState()
		{
			if (this.w == null && this.write_headers)
			{
				this.WriteMimeHeaders();
			}
			if (this.w == null || this.w.WriteState == WriteState.Closed || this.w.WriteState == WriteState.Error)
			{
				this.w = this.CreateWriter();
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000107A4 File Offset: 0x0000E9A4
		private void WriteMimeHeaders()
		{
			this.writer.Write("MIME-Version: 1.0\r\n");
			this.writer.Write("Content-Type: ");
			this.writer.Write(this.content_type.ToString());
			this.writer.Write("\r\n\r\n\r\n");
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000107F8 File Offset: 0x0000E9F8
		private void WriteStartOfMimeSection()
		{
			this.section_count++;
			if (this.section_count > 1)
			{
				return;
			}
			this.writer.Write("\r\n");
			this.writer.Write("--");
			this.writer.Write(this.content_type.Boundary);
			this.writer.Write("\r\n");
			this.writer.Write("Content-ID: ");
			this.writer.Write(this.content_type.Parameters["start"]);
			this.writer.Write("\r\n");
			this.writer.Write("Content-Transfer-Encoding: 8bit\r\n");
			this.writer.Write("Content-Type: application/xop+xml;charset=");
			this.writer.Write(this.xml_writer_settings.Encoding.HeaderName);
			this.writer.Write(";type=\"");
			this.writer.Write(this.content_type.Parameters["start-info"].Replace("\"", "\\\""));
			this.writer.Write("\"\r\n\r\n");
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010930 File Offset: 0x0000EB30
		private void WriteEndOfMimeSection()
		{
			if (this.section_count > 1)
			{
				return;
			}
			this.writer.Write("\r\n");
			this.writer.Write("--");
			this.writer.Write(this.content_type.Boundary);
			this.writer.Write("--\r\n");
		}

		// Token: 0x0400016E RID: 366
		private TextWriter writer;

		// Token: 0x0400016F RID: 367
		private XmlWriterSettings xml_writer_settings;

		// Token: 0x04000170 RID: 368
		private Encoding encoding;

		// Token: 0x04000171 RID: 369
		private int max_bytes;

		// Token: 0x04000172 RID: 370
		private bool write_headers;

		// Token: 0x04000173 RID: 371
		private bool owns_stream;

		// Token: 0x04000174 RID: 372
		private ContentType content_type;

		// Token: 0x04000175 RID: 373
		private XmlWriter w;

		// Token: 0x04000176 RID: 374
		private int depth;

		// Token: 0x04000177 RID: 375
		private int section_count;
	}
}
