using System;
using UnityEngine.SocialPlatforms;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	public static class Social
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003E34 File Offset: 0x00002034
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003E3C File Offset: 0x0000203C
		public static ISocialPlatform Active
		{
			get
			{
				return ActivePlatform.Instance;
			}
			set
			{
				ActivePlatform.Instance = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003E44 File Offset: 0x00002044
		public static ILocalUser localUser
		{
			get
			{
				return Social.Active.localUser;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003E50 File Offset: 0x00002050
		public static void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			Social.Active.LoadUsers(userIDs, callback);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003E60 File Offset: 0x00002060
		public static void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			Social.Active.ReportProgress(achievementID, progress, callback);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E70 File Offset: 0x00002070
		public static void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			Social.Active.LoadAchievementDescriptions(callback);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E80 File Offset: 0x00002080
		public static void LoadAchievements(Action<IAchievement[]> callback)
		{
			Social.Active.LoadAchievements(callback);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E90 File Offset: 0x00002090
		public static void ReportScore(long score, string board, Action<bool> callback)
		{
			Social.Active.ReportScore(score, board, callback);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003EA0 File Offset: 0x000020A0
		public static void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			Social.Active.LoadScores(leaderboardID, callback);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static ILeaderboard CreateLeaderboard()
		{
			return Social.Active.CreateLeaderboard();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003EBC File Offset: 0x000020BC
		public static IAchievement CreateAchievement()
		{
			return Social.Active.CreateAchievement();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003EC8 File Offset: 0x000020C8
		public static void ShowAchievementsUI()
		{
			Social.Active.ShowAchievementsUI();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003ED4 File Offset: 0x000020D4
		public static void ShowLeaderboardUI()
		{
			Social.Active.ShowLeaderboardUI();
		}
	}
}
