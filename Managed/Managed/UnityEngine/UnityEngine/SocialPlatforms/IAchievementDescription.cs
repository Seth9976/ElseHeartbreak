using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000033 RID: 51
	public interface IAchievementDescription
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CA RID: 202
		// (set) Token: 0x060000CB RID: 203
		string id { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CC RID: 204
		string title { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CD RID: 205
		Texture2D image { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CE RID: 206
		string achievedDescription { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CF RID: 207
		string unachievedDescription { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D0 RID: 208
		bool hidden { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D1 RID: 209
		int points { get; }
	}
}
