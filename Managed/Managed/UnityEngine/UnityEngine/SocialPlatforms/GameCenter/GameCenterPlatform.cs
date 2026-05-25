using System;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	// Token: 0x02000013 RID: 19
	public class GameCenterPlatform : Local
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000023F8 File Offset: 0x000005F8
		public static void ResetAllAchievements(Action<bool> callback)
		{
			Debug.Log("ResetAllAchievements - no effect in editor");
			callback(true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000240C File Offset: 0x0000060C
		public static void ShowDefaultAchievementCompletionBanner(bool value)
		{
			Debug.Log("ShowDefaultAchievementCompletionBanner - no effect in editor");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002418 File Offset: 0x00000618
		public static void ShowLeaderboardUI(string leaderboardID, TimeScope timeScope)
		{
			Debug.Log("ShowLeaderboardUI - no effect in editor");
		}
	}
}
