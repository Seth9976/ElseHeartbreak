using System;

namespace System
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public MonoTODOAttribute()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400013F RID: 319
		private string comment;
	}
}
