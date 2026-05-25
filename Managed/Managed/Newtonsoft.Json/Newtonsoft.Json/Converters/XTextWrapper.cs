using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000051 RID: 81
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000AF91 File Offset: 0x00009191
		private XText Text
		{
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000AF9E File Offset: 0x0000919E
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000AFA7 File Offset: 0x000091A7
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000AFB4 File Offset: 0x000091B4
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000AFC2 File Offset: 0x000091C2
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
