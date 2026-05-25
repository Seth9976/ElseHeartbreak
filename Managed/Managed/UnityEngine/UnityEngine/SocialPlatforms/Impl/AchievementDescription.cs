using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000026 RID: 38
	public class AchievementDescription : IAchievementDescription
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000027F4 File Offset: 0x000009F4
		public AchievementDescription(string id, string title, Texture2D image, string achievedDescription, string unachievedDescription, bool hidden, int points)
		{
			this.id = id;
			this.m_Title = title;
			this.m_Image = image;
			this.m_AchievedDescription = achievedDescription;
			this.m_UnachievedDescription = unachievedDescription;
			this.m_Hidden = hidden;
			this.m_Points = points;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002834 File Offset: 0x00000A34
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id, " - ", this.title, " - ", this.achievedDescription, " - ", this.unachievedDescription, " - ", this.points, " - ",
				this.hidden
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000028CC File Offset: 0x00000ACC
		public string id { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028D8 File Offset: 0x00000AD8
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000028E0 File Offset: 0x00000AE0
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028E8 File Offset: 0x00000AE8
		public string achievedDescription
		{
			get
			{
				return this.m_AchievedDescription;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000028F0 File Offset: 0x00000AF0
		public string unachievedDescription
		{
			get
			{
				return this.m_UnachievedDescription;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000028F8 File Offset: 0x00000AF8
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002900 File Offset: 0x00000B00
		public int points
		{
			get
			{
				return this.m_Points;
			}
		}

		// Token: 0x040000A2 RID: 162
		private string m_Title;

		// Token: 0x040000A3 RID: 163
		private Texture2D m_Image;

		// Token: 0x040000A4 RID: 164
		private string m_AchievedDescription;

		// Token: 0x040000A5 RID: 165
		private string m_UnachievedDescription;

		// Token: 0x040000A6 RID: 166
		private bool m_Hidden;

		// Token: 0x040000A7 RID: 167
		private int m_Points;
	}
}
