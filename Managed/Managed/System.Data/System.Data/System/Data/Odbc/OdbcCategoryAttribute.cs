using System;
using System.ComponentModel;

namespace System.Data.Odbc
{
	// Token: 0x0200011B RID: 283
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class OdbcCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0003D6F8 File Offset: 0x0003B8F8
		public OdbcCategoryAttribute(string category)
		{
			this.category = category;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003D708 File Offset: 0x0003B908
		public new string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003D710 File Offset: 0x0003B910
		[MonoTODO]
		protected override string GetLocalizedString(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400053D RID: 1341
		private string category;
	}
}
