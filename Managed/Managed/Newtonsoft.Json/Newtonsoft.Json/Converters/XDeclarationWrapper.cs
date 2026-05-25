using System;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004E RID: 78
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this._declaration = declaration;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000ACBD File Offset: 0x00008EBD
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000ACCA File Offset: 0x00008ECA
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000ACE5 File Offset: 0x00008EE5
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

		// Token: 0x040000F5 RID: 245
		internal readonly XDeclaration _declaration;
	}
}
