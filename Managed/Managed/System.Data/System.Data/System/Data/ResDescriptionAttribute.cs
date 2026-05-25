using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		public ResDescriptionAttribute(string description)
			: base(description)
		{
			this.description = description;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001F2D0 File Offset: 0x0001D4D0
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x040001FF RID: 511
		private string description;
	}
}
