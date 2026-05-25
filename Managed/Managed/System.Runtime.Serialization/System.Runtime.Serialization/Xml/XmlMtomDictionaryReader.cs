using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000051 RID: 81
	internal class XmlMtomDictionaryReader : XmlDictionaryReader
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000F78C File Offset: 0x0000D98C
		public XmlMtomDictionaryReader(Stream stream, Encoding encoding, XmlDictionaryReaderQuotas quotas)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.quotas = quotas;
			this.Initialize();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public XmlMtomDictionaryReader(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			this.stream = stream;
			this.encodings = encodings;
			this.content_type = ((contentType == null) ? null : this.CreateContentType(contentType));
			this.quotas = quotas;
			this.max_buffer_size = maxBufferSize;
			this.on_close = onClose;
			this.Initialize();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F82C File Offset: 0x0000DA2C
		private void Initialize()
		{
			NameTable nameTable = new NameTable();
			this.initial_reader = new NonInteractiveStateXmlReader(string.Empty, nameTable, ReadState.Initial);
			this.eof_reader = new NonInteractiveStateXmlReader(string.Empty, nameTable, ReadState.EndOfFile);
			this.xml_reader = this.initial_reader;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F870 File Offset: 0x0000DA70
		private ContentType CreateContentType(string contentTypeString)
		{
			ContentType contentType = null;
			foreach (string text in contentTypeString.Split(new char[] { ';' }))
			{
				string text2 = text.Trim();
				if (contentType == null)
				{
					contentType = new ContentType(text2);
				}
				else
				{
					int num = text2.IndexOf('=');
					if (num < 0)
					{
						throw new XmlException("Invalid content type header");
					}
					string text3 = this.StripBraces(text2.Substring(num + 1));
					contentType.Parameters[text2.Substring(0, num)] = text3;
				}
			}
			return contentType;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F910 File Offset: 0x0000DB10
		private XmlReader Reader
		{
			get
			{
				return this.part_reader ?? this.xml_reader;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F928 File Offset: 0x0000DB28
		public override bool EOF
		{
			get
			{
				return this.Reader == this.eof_reader;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F938 File Offset: 0x0000DB38
		public override void Close()
		{
			if (!this.EOF && this.on_close != null)
			{
				this.on_close(this);
			}
			this.xml_reader = this.eof_reader;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F974 File Offset: 0x0000DB74
		public override bool Read()
		{
			if (this.EOF)
			{
				return false;
			}
			if (this.Reader == this.initial_reader)
			{
				this.SetupPrimaryReader();
			}
			if (this.part_reader != null)
			{
				this.part_reader = null;
			}
			if (!this.Reader.Read())
			{
				this.xml_reader = this.eof_reader;
				return false;
			}
			if (this.Reader.LocalName == "Include" && this.Reader.NamespaceURI == "http://www.w3.org/2004/08/xop/include")
			{
				string text = this.Reader.GetAttribute("href");
				if (!text.StartsWith("cid:"))
				{
					throw new XmlException("Cannot resolve non-cid href attribute value in XOP Include element");
				}
				text = text.Substring(4);
				if (!this.readers.ContainsKey(text))
				{
					this.ReadToIdentifiedStream(text);
				}
				this.part_reader = new MultiPartedXmlReader(this.Reader, this.readers[text]);
			}
			return true;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000FA74 File Offset: 0x0000DC74
		private void SetupPrimaryReader()
		{
			this.ReadOptionalMimeHeaders();
			if (this.current_content_type != null)
			{
				this.content_type = this.current_content_type;
			}
			if (this.content_type == null)
			{
				throw new XmlException("Content-Type header for the MTOM message was not found");
			}
			if (this.content_type.Boundary == null)
			{
				throw new XmlException("Content-Type header for the MTOM message must contain 'boundary' parameter");
			}
			if (this.encoding == null && this.content_type.CharSet != null)
			{
				this.encoding = Encoding.GetEncoding(this.content_type.CharSet);
			}
			if (this.encoding == null && this.encodings == null)
			{
				throw new XmlException("Encoding specification is required either in the constructor argument or the content-type header");
			}
			string text = "--" + this.content_type.Boundary;
			string text2;
			for (;;)
			{
				text2 = this.ReadAsciiLine().Trim();
				if (text2 == null)
				{
					break;
				}
				if (text2.Length != 0)
				{
					goto Block_9;
				}
			}
			return;
			Block_9:
			if (!text2.StartsWith(text, StringComparison.Ordinal))
			{
				throw new XmlException(string.Format("Unexpected boundary line was found. Expected boundary is '{0}' but it was '{1}'", this.content_type.Boundary, text2));
			}
			string text3 = this.content_type.Parameters["start"];
			this.ReadToIdentifiedStream(text3);
			this.xml_reader = XmlReader.Create(this.readers[text3].CreateTextReader());
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		private void ReadToIdentifiedStream(string id)
		{
			while (this.ReadNextStream())
			{
				if (this.current_content_id == id || id == null)
				{
					return;
				}
			}
			throw new XmlException(string.Format("The stream '{0}' did not appear", id));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000FC04 File Offset: 0x0000DE04
		private bool ReadNextStream()
		{
			this.ReadOptionalMimeHeaders();
			string text = "--" + this.content_type.Boundary;
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				string text2 = this.ReadAsciiLine();
				if (text2 == null && stringBuilder.Length == 0)
				{
					break;
				}
				if (text2 == null || text2.StartsWith(text, StringComparison.Ordinal))
				{
					goto IL_004F;
				}
				stringBuilder.Append(text2);
			}
			return false;
			IL_004F:
			this.readers.Add(this.current_content_id, new MimeEncodedStream(this.current_content_id, this.current_content_encoding, stringBuilder.ToString()));
			return true;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		private void ReadOptionalMimeHeaders()
		{
			this.peek_char = this.stream.ReadByte();
			if (this.peek_char == 45)
			{
				return;
			}
			this.ReadMimeHeaders();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		private string ReadAllHeaderLines()
		{
			string text = string.Empty;
			for (;;)
			{
				string text2 = this.ReadAsciiLine();
				if (text2.Length == 0)
				{
					break;
				}
				text2 = text2.TrimEnd(new char[0]);
				text += text2;
				if (text2[text2.Length - 1] != ';')
				{
					text += '\n';
				}
			}
			return text;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000FD28 File Offset: 0x0000DF28
		private void ReadMimeHeaders()
		{
			foreach (string text in this.ReadAllHeaderLines().Split(new char[] { '\n' }))
			{
				if (text.Length != 0)
				{
					int num = text.IndexOf(':');
					if (num < 0)
					{
						throw new XmlException(string.Format("Unexpected header string: {0}", text));
					}
					string text2 = this.StripBraces(text.Substring(num + 1).Trim());
					string text3 = text.Substring(0, num).ToLower();
					switch (text3)
					{
					case "content-type":
						this.current_content_type = this.CreateContentType(text2);
						break;
					case "content-id":
						this.current_content_id = text2;
						break;
					case "content-transfer-encoding":
						this.current_content_encoding = text2;
						break;
					}
				}
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000FE5C File Offset: 0x0000E05C
		private string StripBraces(string s)
		{
			if (s.Length >= 2 && s[0] == '"' && s[s.Length - 1] == '"')
			{
				s = s.Substring(1, s.Length - 2);
			}
			if (s.Length >= 2 && s[0] == '<' && s[s.Length - 1] == '>')
			{
				s = s.Substring(1, s.Length - 2);
			}
			return s;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		private string ReadAsciiLine()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[1024];
			}
			int num = 0;
			int num2 = this.peek_char;
			bool flag = num2 >= 0;
			this.peek_char = -1;
			for (;;)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					num2 = this.stream.ReadByte();
				}
				if (num2 < 0)
				{
					break;
				}
				if (num2 == 13)
				{
					num2 = this.stream.ReadByte();
					if (num2 < 0)
					{
						goto Block_6;
					}
					if (num2 == 10)
					{
						goto Block_7;
					}
					this.buffer[num++] = 13;
					flag = true;
				}
				else
				{
					this.buffer[num++] = (byte)num2;
				}
				if (num == this.buffer.Length)
				{
					byte[] array = new byte[this.buffer.Length << 1];
					Array.Copy(this.buffer, 0, array, 0, this.buffer.Length);
					this.buffer = array;
				}
			}
			if (num > 0)
			{
				throw new XmlException("The stream ends without end of line");
			}
			return null;
			Block_6:
			this.buffer[num++] = 13;
			Block_7:
			return Encoding.ASCII.GetString(this.buffer, 0, num);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010010 File Offset: 0x0000E210
		public override int AttributeCount
		{
			get
			{
				return this.Reader.AttributeCount;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00010020 File Offset: 0x0000E220
		public override string BaseURI
		{
			get
			{
				return this.Reader.BaseURI;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010030 File Offset: 0x0000E230
		public override int Depth
		{
			get
			{
				return this.Reader.Depth;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00010040 File Offset: 0x0000E240
		public override bool HasValue
		{
			get
			{
				return this.Reader.HasValue;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00010050 File Offset: 0x0000E250
		public override bool IsEmptyElement
		{
			get
			{
				return this.Reader.IsEmptyElement;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00010060 File Offset: 0x0000E260
		public override string LocalName
		{
			get
			{
				return this.Reader.LocalName;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010070 File Offset: 0x0000E270
		public override string NamespaceURI
		{
			get
			{
				return this.Reader.NamespaceURI;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00010080 File Offset: 0x0000E280
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Reader.NameTable;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010090 File Offset: 0x0000E290
		public override XmlNodeType NodeType
		{
			get
			{
				return this.Reader.NodeType;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public override string Prefix
		{
			get
			{
				return this.Reader.Prefix;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000100B0 File Offset: 0x0000E2B0
		public override ReadState ReadState
		{
			get
			{
				return this.Reader.ReadState;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public override string Value
		{
			get
			{
				return this.Reader.Value;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public override bool MoveToElement()
		{
			return this.Reader.MoveToElement();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public override string GetAttribute(int index)
		{
			return this.Reader.GetAttribute(index);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public override string GetAttribute(string name)
		{
			return this.Reader.GetAttribute(name);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010100 File Offset: 0x0000E300
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.Reader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010110 File Offset: 0x0000E310
		public override void MoveToAttribute(int index)
		{
			this.Reader.MoveToAttribute(index);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010120 File Offset: 0x0000E320
		public override bool MoveToAttribute(string name)
		{
			return this.Reader.MoveToAttribute(name);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010130 File Offset: 0x0000E330
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.Reader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010140 File Offset: 0x0000E340
		public override bool MoveToFirstAttribute()
		{
			return this.Reader.MoveToFirstAttribute();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010150 File Offset: 0x0000E350
		public override bool MoveToNextAttribute()
		{
			return this.Reader.MoveToNextAttribute();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010160 File Offset: 0x0000E360
		public override string LookupNamespace(string prefix)
		{
			return this.Reader.LookupNamespace(prefix);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010170 File Offset: 0x0000E370
		public override bool ReadAttributeValue()
		{
			return this.Reader.ReadAttributeValue();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010180 File Offset: 0x0000E380
		public override void ResolveEntity()
		{
			this.Reader.ResolveEntity();
		}

		// Token: 0x04000151 RID: 337
		private Stream stream;

		// Token: 0x04000152 RID: 338
		private Encoding encoding;

		// Token: 0x04000153 RID: 339
		private Encoding[] encodings;

		// Token: 0x04000154 RID: 340
		private ContentType content_type;

		// Token: 0x04000155 RID: 341
		private XmlDictionaryReaderQuotas quotas;

		// Token: 0x04000156 RID: 342
		private int max_buffer_size;

		// Token: 0x04000157 RID: 343
		private OnXmlDictionaryReaderClose on_close;

		// Token: 0x04000158 RID: 344
		private Dictionary<string, MimeEncodedStream> readers = new Dictionary<string, MimeEncodedStream>();

		// Token: 0x04000159 RID: 345
		private XmlReader xml_reader;

		// Token: 0x0400015A RID: 346
		private XmlReader initial_reader;

		// Token: 0x0400015B RID: 347
		private XmlReader eof_reader;

		// Token: 0x0400015C RID: 348
		private XmlReader part_reader;

		// Token: 0x0400015D RID: 349
		private int buffer_length;

		// Token: 0x0400015E RID: 350
		private byte[] buffer;

		// Token: 0x0400015F RID: 351
		private int peek_char;

		// Token: 0x04000160 RID: 352
		private ContentType current_content_type;

		// Token: 0x04000161 RID: 353
		private int content_index;

		// Token: 0x04000162 RID: 354
		private string current_content_id;

		// Token: 0x04000163 RID: 355
		private string current_content_encoding;
	}
}
