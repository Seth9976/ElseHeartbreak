using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000052 RID: 82
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000AFE3 File Offset: 0x000091E3
		private XComment Text
		{
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000AFF9 File Offset: 0x000091F9
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000B006 File Offset: 0x00009206
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B014 File Offset: 0x00009214
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
