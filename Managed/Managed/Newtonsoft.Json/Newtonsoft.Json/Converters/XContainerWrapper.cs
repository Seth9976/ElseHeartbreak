using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004F RID: 79
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000ACF3 File Offset: 0x00008EF3
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AD00 File Offset: 0x00008F00
		public XContainerWrapper(XContainer container)
			: base(container)
		{
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000AD11 File Offset: 0x00008F11
		public override IList<IXmlNode> ChildNodes
		{
			get
			{
				return (from n in this.Container.Nodes()
					select XContainerWrapper.WrapNode(n)).ToList<IXmlNode>();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000AD45 File Offset: 0x00008F45
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000AD68 File Offset: 0x00008F68
		internal static IXmlNode WrapNode(XObject node)
		{
			if (node is XDocument)
			{
				return new XDocumentWrapper((XDocument)node);
			}
			if (node is XElement)
			{
				return new XElementWrapper((XElement)node);
			}
			if (node is XContainer)
			{
				return new XContainerWrapper((XContainer)node);
			}
			if (node is XProcessingInstruction)
			{
				return new XProcessingInstructionWrapper((XProcessingInstruction)node);
			}
			if (node is XText)
			{
				return new XTextWrapper((XText)node);
			}
			if (node is XComment)
			{
				return new XCommentWrapper((XComment)node);
			}
			if (node is XAttribute)
			{
				return new XAttributeWrapper((XAttribute)node);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000AE07 File Offset: 0x00009007
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			return newChild;
		}
	}
}
