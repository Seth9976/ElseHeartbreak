using System;

namespace System.Xml
{
	// Token: 0x02000052 RID: 82
	internal class NonInteractiveStateXmlReader : DummyStateXmlReader
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00010190 File Offset: 0x0000E390
		public NonInteractiveStateXmlReader(string baseUri, XmlNameTable nameTable, ReadState readState)
			: base(baseUri, nameTable, readState)
		{
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001019C File Offset: 0x0000E39C
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public override bool HasValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public override string Value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000101AC File Offset: 0x0000E3AC
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.None;
			}
		}
	}
}
