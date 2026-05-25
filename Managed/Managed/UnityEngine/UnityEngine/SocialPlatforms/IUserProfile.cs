using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000031 RID: 49
	public interface IUserProfile
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BD RID: 189
		string userName { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BE RID: 190
		string id { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BF RID: 191
		bool isFriend { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C0 RID: 192
		UserState state { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C1 RID: 193
		Texture2D image { get; }
	}
}
