using System;
using System.ComponentModel;

namespace System.Data.Odbc
{
	// Token: 0x0200011C RID: 284
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class OdbcDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003D718 File Offset: 0x0003B918
		public OdbcDescriptionAttribute(string description)
			: base(description)
		{
			this.description = description;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003D728 File Offset: 0x0003B928
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x0400053E RID: 1342
		private string description;
	}
}
