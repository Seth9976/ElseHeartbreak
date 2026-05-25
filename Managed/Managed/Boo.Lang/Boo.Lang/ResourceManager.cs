using System;
using Boo.Lang.Resources;

namespace Boo.Lang
{
	// Token: 0x02000025 RID: 37
	public static class ResourceManager
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00004164 File Offset: 0x00002364
		public static string Format(string name, params object[] args)
		{
			return string.Format(ResourceManager.GetString(name), args);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004174 File Offset: 0x00002374
		private static string GetString(string name)
		{
			return (string)typeof(StringResources).GetField(name).GetValue(null);
		}
	}
}
