using System;

namespace System
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00002C54 File Offset: 0x00000E54
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00002C5C File Offset: 0x00000E5C
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002C6C File Offset: 0x00000E6C
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x0400008B RID: 139
		private string comment;
	}
}
