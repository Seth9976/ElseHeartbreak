using System;

namespace UnityEngine
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00003F98 File Offset: 0x00002198
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003FA8 File Offset: 0x000021A8
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x040000E3 RID: 227
		public readonly int lines;
	}
}
