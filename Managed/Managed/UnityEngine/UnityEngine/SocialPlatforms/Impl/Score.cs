using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000027 RID: 39
	public class Score : IScore
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002908 File Offset: 0x00000B08
		public Score()
			: this("unkown", -1L)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002918 File Offset: 0x00000B18
		public Score(string leaderboardID, long value)
			: this(leaderboardID, value, "0", DateTime.Now, string.Empty, -1)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002940 File Offset: 0x00000B40
		public Score(string leaderboardID, long value, string userID, DateTime date, string formattedValue, int rank)
		{
			this.leaderboardID = leaderboardID;
			this.value = value;
			this.m_UserID = userID;
			this.m_Date = date;
			this.m_FormattedValue = formattedValue;
			this.m_Rank = rank;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002978 File Offset: 0x00000B78
		public override string ToString()
		{
			return string.Concat(new object[] { "Rank: '", this.m_Rank, "' Value: '", this.value, "' Category: '", this.leaderboardID, "' PlayerID: '", this.m_UserID, "' Date: '", this.m_Date });
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000029F8 File Offset: 0x00000BF8
		public void ReportScore(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportScore(this.value, this.leaderboardID, callback);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void SetDate(DateTime date)
		{
			this.m_Date = date;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A28 File Offset: 0x00000C28
		public void SetFormattedValue(string value)
		{
			this.m_FormattedValue = value;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A34 File Offset: 0x00000C34
		public void SetUserID(string userID)
		{
			this.m_UserID = userID;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A40 File Offset: 0x00000C40
		public void SetRank(int rank)
		{
			this.m_Rank = rank;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002A54 File Offset: 0x00000C54
		public string leaderboardID { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002A68 File Offset: 0x00000C68
		public long value { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A74 File Offset: 0x00000C74
		public DateTime date
		{
			get
			{
				return this.m_Date;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002A7C File Offset: 0x00000C7C
		public string formattedValue
		{
			get
			{
				return this.m_FormattedValue;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002A84 File Offset: 0x00000C84
		public string userID
		{
			get
			{
				return this.m_UserID;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A8C File Offset: 0x00000C8C
		public int rank
		{
			get
			{
				return this.m_Rank;
			}
		}

		// Token: 0x040000A9 RID: 169
		private DateTime m_Date;

		// Token: 0x040000AA RID: 170
		private string m_FormattedValue;

		// Token: 0x040000AB RID: 171
		private string m_UserID;

		// Token: 0x040000AC RID: 172
		private int m_Rank;
	}
}
