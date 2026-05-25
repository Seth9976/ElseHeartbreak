using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000024 RID: 36
	public class UserProfile : IUserProfile
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002534 File Offset: 0x00000734
		public UserProfile()
		{
			this.m_UserName = "Uninitialized";
			this.m_ID = "0";
			this.m_IsFriend = false;
			this.m_State = UserState.Offline;
			this.m_Image = new Texture2D(32, 32);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002570 File Offset: 0x00000770
		public UserProfile(string name, string id, bool friend)
			: this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002584 File Offset: 0x00000784
		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image)
		{
			this.m_UserName = name;
			this.m_ID = id;
			this.m_IsFriend = friend;
			this.m_State = state;
			this.m_Image = image;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025B4 File Offset: 0x000007B4
		public override string ToString()
		{
			return string.Concat(new object[] { this.id, " - ", this.userName, " - ", this.isFriend, " - ", this.state });
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002614 File Offset: 0x00000814
		public void SetUserName(string name)
		{
			this.m_UserName = name;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002620 File Offset: 0x00000820
		public void SetUserID(string id)
		{
			this.m_ID = id;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000262C File Offset: 0x0000082C
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002638 File Offset: 0x00000838
		public void SetIsFriend(bool value)
		{
			this.m_IsFriend = value;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002644 File Offset: 0x00000844
		public void SetState(UserState state)
		{
			this.m_State = state;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002650 File Offset: 0x00000850
		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002658 File Offset: 0x00000858
		public string id
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002660 File Offset: 0x00000860
		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002668 File Offset: 0x00000868
		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002670 File Offset: 0x00000870
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x04000098 RID: 152
		protected string m_UserName;

		// Token: 0x04000099 RID: 153
		protected string m_ID;

		// Token: 0x0400009A RID: 154
		protected bool m_IsFriend;

		// Token: 0x0400009B RID: 155
		protected UserState m_State;

		// Token: 0x0400009C RID: 156
		protected Texture2D m_Image;
	}
}
