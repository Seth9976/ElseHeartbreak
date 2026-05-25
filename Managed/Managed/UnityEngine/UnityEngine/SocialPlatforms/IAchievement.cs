using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000032 RID: 50
	public interface IAchievement
	{
		// Token: 0x060000C2 RID: 194
		void ReportProgress(Action<bool> callback);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		string id { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		double percentCompleted { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C7 RID: 199
		bool completed { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C8 RID: 200
		bool hidden { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C9 RID: 201
		DateTime lastReportedDate { get; }
	}
}
