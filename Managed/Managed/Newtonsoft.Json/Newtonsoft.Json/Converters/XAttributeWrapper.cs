using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000054 RID: 84
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B073 File Offset: 0x00009273
		private XAttribute Attribute
		{
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B080 File Offset: 0x00009280
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B089 File Offset: 0x00009289
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B096 File Offset: 0x00009296
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B0B6 File Offset: 0x000092B6
		public override string NamespaceURI
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
