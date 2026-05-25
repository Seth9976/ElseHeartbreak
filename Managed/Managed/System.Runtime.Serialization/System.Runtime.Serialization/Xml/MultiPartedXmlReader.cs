using System;

namespace System.Xml
{
	// Token: 0x02000053 RID: 83
	internal class MultiPartedXmlReader : DummyStateXmlReader
	{
		// Token: 0x06000358 RID: 856 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public MultiPartedXmlReader(XmlReader reader, MimeEncodedStream value)
			: base(reader.BaseURI, reader.NameTable, reader.ReadState)
		{
			this.owner = reader;
			this.value = value.CreateTextReader().ReadToEnd();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public override int Depth
		{
			get
			{
				return this.owner.Depth;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00010200 File Offset: 0x0000E400
		public override bool HasValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00010204 File Offset: 0x0000E404
		public override string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001020C File Offset: 0x0000E40C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x04000165 RID: 357
		private XmlReader owner;

		// Token: 0x04000166 RID: 358
		private string value;
	}
}
