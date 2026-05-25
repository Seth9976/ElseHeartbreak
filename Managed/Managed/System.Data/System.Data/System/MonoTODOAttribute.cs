using System;

namespace System
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public MonoTODOAttribute()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x04000234 RID: 564
		private string comment;
	}
}
