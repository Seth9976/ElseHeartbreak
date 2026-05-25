using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000034 RID: 52
	public interface IScore
	{
		// Token: 0x060000D2 RID: 210
		void ReportScore(Action<bool> callback);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D3 RID: 211
		// (set) Token: 0x060000D4 RID: 212
		string leaderboardID { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D5 RID: 213
		// (set) Token: 0x060000D6 RID: 214
		long value { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D7 RID: 215
		DateTime date { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D8 RID: 216
		string formattedValue { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D9 RID: 217
		string userID { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DA RID: 218
		int rank { get; }
	}
}
