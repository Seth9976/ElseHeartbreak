using System;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003FB8 File Offset: 0x000021B8
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FD0 File Offset: 0x000021D0
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x040000E4 RID: 228
		public readonly int minLines;

		// Token: 0x040000E5 RID: 229
		public readonly int maxLines;
	}
}
