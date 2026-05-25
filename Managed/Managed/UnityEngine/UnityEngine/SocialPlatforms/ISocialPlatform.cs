using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200002E RID: 46
	public interface ISocialPlatform
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169
		ILocalUser localUser { get; }

		// Token: 0x060000AA RID: 170
		void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback);

		// Token: 0x060000AB RID: 171
		void ReportProgress(string achievementID, double progress, Action<bool> callback);

		// Token: 0x060000AC RID: 172
		void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback);

		// Token: 0x060000AD RID: 173
		void LoadAchievements(Action<IAchievement[]> callback);

		// Token: 0x060000AE RID: 174
		IAchievement CreateAchievement();

		// Token: 0x060000AF RID: 175
		void ReportScore(long score, string board, Action<bool> callback);

		// Token: 0x060000B0 RID: 176
		void LoadScores(string leaderboardID, Action<IScore[]> callback);

		// Token: 0x060000B1 RID: 177
		ILeaderboard CreateLeaderboard();

		// Token: 0x060000B2 RID: 178
		void ShowAchievementsUI();

		// Token: 0x060000B3 RID: 179
		void ShowLeaderboardUI();

		// Token: 0x060000B4 RID: 180
		void Authenticate(ILocalUser user, Action<bool> callback);

		// Token: 0x060000B5 RID: 181
		void LoadFriends(ILocalUser user, Action<bool> callback);

		// Token: 0x060000B6 RID: 182
		void LoadScores(ILeaderboard board, Action<bool> callback);

		// Token: 0x060000B7 RID: 183
		bool GetLoading(ILeaderboard board);
	}
}
