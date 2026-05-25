using System;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003F70 File Offset: 0x00002170
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x040000E0 RID: 224
		public readonly string header;
	}
}
