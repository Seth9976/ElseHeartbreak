using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class DataCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00009878 File Offset: 0x00007A78
		public DataCategoryAttribute(string category)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009880 File Offset: 0x00007A80
		[MonoTODO]
		protected override string GetLocalizedString(string value)
		{
			throw new NotImplementedException();
		}
	}
}
