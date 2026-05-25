using System;

namespace System.Xml
{
	// Token: 0x02000058 RID: 88
	internal class XmlSimpleDictionaryWriter : XmlDictionaryWriter
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00010E58 File Offset: 0x0000F058
		public XmlSimpleDictionaryWriter(XmlWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00010E68 File Offset: 0x0000F068
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00010E78 File Offset: 0x0000F078
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010E88 File Offset: 0x0000F088
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00010E98 File Offset: 0x0000F098
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.writer.WriteBinHex(buffer, index, count);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00010EC8 File Offset: 0x0000F0C8
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00010F0C File Offset: 0x0000F10C
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010F1C File Offset: 0x0000F11C
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010F2C File Offset: 0x0000F12C
		public override void WriteEndElement()
		{
			base.Depth--;
			this.writer.WriteEndElement();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010F48 File Offset: 0x0000F148
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010F58 File Offset: 0x0000F158
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010F68 File Offset: 0x0000F168
		public override void WriteName(string name)
		{
			this.writer.WriteName(name);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010F78 File Offset: 0x0000F178
		public override void WriteNmToken(string name)
		{
			this.writer.WriteNmToken(name);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010F88 File Offset: 0x0000F188
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.writer.WriteNode(reader, defattr);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010F98 File Offset: 0x0000F198
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.writer.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011008 File Offset: 0x0000F208
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			base.Depth++;
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011034 File Offset: 0x0000F234
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011044 File Offset: 0x0000F244
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011054 File Offset: 0x0000F254
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00011064 File Offset: 0x0000F264
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00011074 File Offset: 0x0000F274
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011084 File Offset: 0x0000F284
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x0400017D RID: 381
		private XmlWriter writer;
	}
}
