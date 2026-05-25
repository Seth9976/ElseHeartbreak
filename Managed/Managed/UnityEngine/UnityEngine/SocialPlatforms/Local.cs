using System;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000029 RID: 41
	public class Local : ISocialPlatform
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002D38 File Offset: 0x00000F38
		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			LocalUser localUser = (LocalUser)user;
			this.m_DefaultTexture = this.CreateDummyTexture(32, 32);
			this.PopulateStaticData();
			localUser.SetAuthenticated(true);
			localUser.SetUnderage(false);
			localUser.SetUserID("1000");
			localUser.SetUserName("Lerpz");
			localUser.SetImage(this.m_DefaultTexture);
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002DA0 File Offset: 0x00000FA0
		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			((LocalUser)user).SetFriends(this.m_Friends.ToArray());
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002DDC File Offset: 0x00000FDC
		void ISocialPlatform.LoadScores(ILeaderboard board, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			Leaderboard leaderboard = (Leaderboard)board;
			foreach (Leaderboard leaderboard2 in this.m_Leaderboards)
			{
				if (leaderboard2.id == leaderboard.id)
				{
					leaderboard.SetTitle(leaderboard2.title);
					leaderboard.SetScores(leaderboard2.scores);
					leaderboard.SetMaxRange((uint)leaderboard2.scores.Length);
				}
			}
			this.SortScores(leaderboard);
			this.SetLocalPlayerScore(leaderboard);
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002EA8 File Offset: 0x000010A8
		bool ISocialPlatform.GetLoading(ILeaderboard board)
		{
			return this.VerifyUser() && ((Leaderboard)board).loading;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002EC4 File Offset: 0x000010C4
		public ILocalUser localUser
		{
			get
			{
				if (Local.m_LocalUser == null)
				{
					Local.m_LocalUser = new LocalUser();
				}
				return Local.m_LocalUser;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			List<UserProfile> list = new List<UserProfile>();
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (string text in userIDs)
			{
				foreach (UserProfile userProfile in this.m_Users)
				{
					if (userProfile.id == text)
					{
						list.Add(userProfile);
					}
				}
				foreach (UserProfile userProfile2 in this.m_Friends)
				{
					if (userProfile2.id == text)
					{
						list.Add(userProfile2);
					}
				}
			}
			callback(list.ToArray());
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003000 File Offset: 0x00001200
		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Achievement achievement in this.m_Achievements)
			{
				if (achievement.id == id && achievement.percentCompleted <= progress)
				{
					if (progress >= 100.0)
					{
						achievement.SetCompleted(true);
					}
					achievement.SetHidden(false);
					achievement.SetLastReportedDate(DateTime.Now);
					achievement.percentCompleted = progress;
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			foreach (AchievementDescription achievementDescription in this.m_AchievementDescriptions)
			{
				if (achievementDescription.id == id)
				{
					bool flag = progress >= 100.0;
					Achievement achievement2 = new Achievement(id, progress, flag, false, DateTime.Now);
					this.m_Achievements.Add(achievement2);
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			Debug.LogError("Achievement ID not found");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003188 File Offset: 0x00001388
		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			if (callback != null)
			{
				callback(this.m_AchievementDescriptions.ToArray());
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031B0 File Offset: 0x000013B0
		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			if (callback != null)
			{
				callback(this.m_Achievements.ToArray());
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031D8 File Offset: 0x000013D8
		public void ReportScore(long score, string board, Action<bool> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in this.m_Leaderboards)
			{
				if (leaderboard.id == board)
				{
					leaderboard.SetScores(new List<Score>((Score[])leaderboard.scores)
					{
						new Score(board, score, this.localUser.id, DateTime.Now, score + " points", 0)
					}.ToArray());
					if (callback != null)
					{
						callback(true);
					}
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			if (callback != null)
			{
				callback(false);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032C8 File Offset: 0x000014C8
		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			if (!this.VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in this.m_Leaderboards)
			{
				if (leaderboard.id == leaderboardID)
				{
					this.SortScores(leaderboard);
					if (callback != null)
					{
						callback(leaderboard.scores);
					}
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			if (callback != null)
			{
				callback(new Score[0]);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003380 File Offset: 0x00001580
		private void SortScores(Leaderboard board)
		{
			List<Score> list = new List<Score>((Score[])board.scores);
			list.Sort((Score s1, Score s2) => s2.value.CompareTo(s1.value));
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetRank(i + 1);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033E8 File Offset: 0x000015E8
		private void SetLocalPlayerScore(Leaderboard board)
		{
			foreach (Score score in board.scores)
			{
				if (score.userID == this.localUser.id)
				{
					board.SetLocalUserScore(score);
					break;
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003440 File Offset: 0x00001640
		public void ShowAchievementsUI()
		{
			Debug.Log("ShowAchievementsUI not implemented");
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000344C File Offset: 0x0000164C
		public void ShowLeaderboardUI()
		{
			Debug.Log("ShowLeaderboardUI not implemented");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003458 File Offset: 0x00001658
		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000346C File Offset: 0x0000166C
		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003480 File Offset: 0x00001680
		private bool VerifyUser()
		{
			if (!this.localUser.authenticated)
			{
				Debug.LogError("Must authenticate first");
				return false;
			}
			return true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034A0 File Offset: 0x000016A0
		private void PopulateStaticData()
		{
			this.m_Friends.Add(new UserProfile("Fred", "1001", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Julia", "1002", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Jeff", "1003", true, UserState.Online, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Sam", "1004", false, UserState.Offline, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Max", "1005", false, UserState.Offline, this.m_DefaultTexture));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement01", "First achievement", this.m_DefaultTexture, "Get first achievement", "Received first achievement", false, 10));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement02", "Second achievement", this.m_DefaultTexture, "Get second achievement", "Received second achievement", false, 20));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement03", "Third achievement", this.m_DefaultTexture, "Get third achievement", "Received third achievement", false, 15));
			Leaderboard leaderboard = new Leaderboard();
			leaderboard.SetTitle("High Scores");
			leaderboard.id = "Leaderboard01";
			leaderboard.SetScores(new List<Score>
			{
				new Score("Leaderboard01", 300L, "1001", DateTime.Now.AddDays(-1.0), "300 points", 1),
				new Score("Leaderboard01", 255L, "1002", DateTime.Now.AddDays(-1.0), "255 points", 2),
				new Score("Leaderboard01", 55L, "1003", DateTime.Now.AddDays(-1.0), "55 points", 3),
				new Score("Leaderboard01", 10L, "1004", DateTime.Now.AddDays(-1.0), "10 points", 4)
			}.ToArray());
			this.m_Leaderboards.Add(leaderboard);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000036F0 File Offset: 0x000018F0
		private Texture2D CreateDummyTexture(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = (((j & i) <= 0) ? Color.gray : Color.white);
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x040000B9 RID: 185
		private static LocalUser m_LocalUser;

		// Token: 0x040000BA RID: 186
		private List<UserProfile> m_Friends = new List<UserProfile>();

		// Token: 0x040000BB RID: 187
		private List<UserProfile> m_Users = new List<UserProfile>();

		// Token: 0x040000BC RID: 188
		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		// Token: 0x040000BD RID: 189
		private List<Achievement> m_Achievements = new List<Achievement>();

		// Token: 0x040000BE RID: 190
		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		// Token: 0x040000BF RID: 191
		private Texture2D m_DefaultTexture;
	}
}
