using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000038 RID: 56
	public interface ILeaderboard
	{
		// Token: 0x060000DC RID: 220
		void SetUserFilter(string[] userIDs);

		// Token: 0x060000DD RID: 221
		void LoadScores(Action<bool> callback);

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DE RID: 222
		bool loading { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		string id { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E1 RID: 225
		// (set) Token: 0x060000E2 RID: 226
		UserScope userScope { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E3 RID: 227
		// (set) Token: 0x060000E4 RID: 228
		Range range { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E5 RID: 229
		// (set) Token: 0x060000E6 RID: 230
		TimeScope timeScope { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E7 RID: 231
		IScore localUserScore { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E8 RID: 232
		uint maxRange { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E9 RID: 233
		IScore[] scores { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EA RID: 234
		string title { get; }
	}
}
