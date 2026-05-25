using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000028 RID: 40
	public class Leaderboard : ILeaderboard
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002A94 File Offset: 0x00000C94
		public Leaderboard()
		{
			this.id = "Invalid";
			this.range = new Range(1, 10);
			this.userScope = UserScope.Global;
			this.timeScope = TimeScope.AllTime;
			this.m_Loading = false;
			this.m_LocalUserScore = new Score("Invalid", 0L);
			this.m_MaxRange = 0U;
			this.m_Scores = new Score[0];
			this.m_Title = "Invalid";
			this.m_UserIDs = new string[0];
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002B14 File Offset: 0x00000D14
		public void SetUserFilter(string[] userIDs)
		{
			this.m_UserIDs = userIDs;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002B20 File Offset: 0x00000D20
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ID: '",
				this.id,
				"' Title: '",
				this.m_Title,
				"' Loading: '",
				this.m_Loading,
				"' Range: [",
				this.range.from,
				",",
				this.range.count,
				"] MaxRange: '",
				this.m_MaxRange,
				"' Scores: '",
				this.m_Scores.Length,
				"' UserScope: '",
				this.userScope,
				"' TimeScope: '",
				this.timeScope,
				"' UserFilter: '",
				this.m_UserIDs.Length
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C3C File Offset: 0x00000E3C
		public bool loading
		{
			get
			{
				return ActivePlatform.Instance.GetLoading(this);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void SetLocalUserScore(IScore score)
		{
			this.m_LocalUserScore = score;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C58 File Offset: 0x00000E58
		public void SetMaxRange(uint maxRange)
		{
			this.m_MaxRange = maxRange;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C64 File Offset: 0x00000E64
		public void SetScores(IScore[] scores)
		{
			this.m_Scores = scores;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C70 File Offset: 0x00000E70
		public void SetTitle(string title)
		{
			this.m_Title = title;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C7C File Offset: 0x00000E7C
		public string[] GetUserFilter()
		{
			return this.m_UserIDs;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002C84 File Offset: 0x00000E84
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002C8C File Offset: 0x00000E8C
		public string id { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public UserScope userScope { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002CAC File Offset: 0x00000EAC
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public Range range { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public TimeScope timeScope { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public IScore localUserScore
		{
			get
			{
				return this.m_LocalUserScore;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CDC File Offset: 0x00000EDC
		public uint maxRange
		{
			get
			{
				return this.m_MaxRange;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public IScore[] scores
		{
			get
			{
				return this.m_Scores;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002CEC File Offset: 0x00000EEC
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x040000AF RID: 175
		private bool m_Loading;

		// Token: 0x040000B0 RID: 176
		private IScore m_LocalUserScore;

		// Token: 0x040000B1 RID: 177
		private uint m_MaxRange;

		// Token: 0x040000B2 RID: 178
		private IScore[] m_Scores;

		// Token: 0x040000B3 RID: 179
		private string m_Title;

		// Token: 0x040000B4 RID: 180
		private string[] m_UserIDs;
	}
}
