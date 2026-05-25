using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200002D RID: 45
	internal static class ActivePlatform
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003EE0 File Offset: 0x000020E0
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003EFC File Offset: 0x000020FC
		internal static ISocialPlatform Instance
		{
			get
			{
				if (ActivePlatform._active == null)
				{
					ActivePlatform._active = ActivePlatform.SelectSocialPlatform();
				}
				return ActivePlatform._active;
			}
			set
			{
				ActivePlatform._active = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F04 File Offset: 0x00002104
		private static ISocialPlatform SelectSocialPlatform()
		{
			return new Local();
		}

		// Token: 0x040000CB RID: 203
		private static ISocialPlatform _active;
	}
}
