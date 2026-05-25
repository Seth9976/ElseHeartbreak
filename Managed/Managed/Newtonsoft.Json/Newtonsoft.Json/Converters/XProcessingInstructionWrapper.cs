using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000053 RID: 83
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B035 File Offset: 0x00009235
		private XProcessingInstruction ProcessingInstruction
		{
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B042 File Offset: 0x00009242
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B04B File Offset: 0x0000924B
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B058 File Offset: 0x00009258
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000B065 File Offset: 0x00009265
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
