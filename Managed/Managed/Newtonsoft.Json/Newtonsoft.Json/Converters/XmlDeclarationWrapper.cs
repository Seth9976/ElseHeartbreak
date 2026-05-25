using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004C RID: 76
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000AC01 File Offset: 0x00008E01
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000AC11 File Offset: 0x00008E11
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000AC1E File Offset: 0x00008E1E
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000AC2B File Offset: 0x00008E2B
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000AC39 File Offset: 0x00008E39
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000AC46 File Offset: 0x00008E46
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x040000F3 RID: 243
		private XmlDeclaration _declaration;
	}
}
