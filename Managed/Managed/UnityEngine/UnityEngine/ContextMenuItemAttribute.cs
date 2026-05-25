using System;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00003F38 File Offset: 0x00002138
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x040000DC RID: 220
		public readonly string name;

		// Token: 0x040000DD RID: 221
		public readonly string function;
	}
}
