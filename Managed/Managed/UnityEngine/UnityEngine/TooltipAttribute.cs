using System;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003F50 File Offset: 0x00002150
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x040000DE RID: 222
		public readonly string tooltip;
	}
}
