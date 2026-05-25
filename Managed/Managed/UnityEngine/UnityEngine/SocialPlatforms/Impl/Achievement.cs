using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000025 RID: 37
	public class Achievement : IAchievement
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002678 File Offset: 0x00000878
		public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
		{
			this.id = id;
			this.percentCompleted = percentCompleted;
			this.m_Completed = completed;
			this.m_Hidden = hidden;
			this.m_LastReportedDate = lastReportedDate;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026A8 File Offset: 0x000008A8
		public Achievement(string id, double percent)
		{
			this.id = id;
			this.percentCompleted = percent;
			this.m_Hidden = false;
			this.m_Completed = false;
			this.m_LastReportedDate = DateTime.MinValue;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026D8 File Offset: 0x000008D8
		public Achievement()
			: this("unknown", 0.0)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026F0 File Offset: 0x000008F0
		public override string ToString()
		{
			return string.Concat(new object[] { this.id, " - ", this.percentCompleted, " - ", this.completed, " - ", this.hidden, " - ", this.lastReportedDate });
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000276C File Offset: 0x0000096C
		public void ReportProgress(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportProgress(this.id, this.percentCompleted, callback);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002790 File Offset: 0x00000990
		public void SetCompleted(bool value)
		{
			this.m_Completed = value;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000279C File Offset: 0x0000099C
		public void SetHidden(bool value)
		{
			this.m_Hidden = value;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027A8 File Offset: 0x000009A8
		public void SetLastReportedDate(DateTime date)
		{
			this.m_LastReportedDate = date;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027B4 File Offset: 0x000009B4
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000027BC File Offset: 0x000009BC
		public string id { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027C8 File Offset: 0x000009C8
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000027D0 File Offset: 0x000009D0
		public double percentCompleted { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000027DC File Offset: 0x000009DC
		public bool completed
		{
			get
			{
				return this.m_Completed;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027EC File Offset: 0x000009EC
		public DateTime lastReportedDate
		{
			get
			{
				return this.m_LastReportedDate;
			}
		}

		// Token: 0x0400009D RID: 157
		private bool m_Completed;

		// Token: 0x0400009E RID: 158
		private bool m_Hidden;

		// Token: 0x0400009F RID: 159
		private DateTime m_LastReportedDate;
	}
}
