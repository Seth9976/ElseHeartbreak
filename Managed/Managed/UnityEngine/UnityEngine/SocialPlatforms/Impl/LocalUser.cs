using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000023 RID: 35
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000024A8 File Offset: 0x000006A8
		public LocalUser()
		{
			this.m_Friends = new UserProfile[0];
			this.m_Authenticated = false;
			this.m_Underage = false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D8 File Offset: 0x000006D8
		public void Authenticate(Action<bool> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E8 File Offset: 0x000006E8
		public void LoadFriends(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadFriends(this, callback);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024F8 File Offset: 0x000006F8
		public void SetFriends(IUserProfile[] friends)
		{
			this.m_Friends = friends;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002504 File Offset: 0x00000704
		public void SetAuthenticated(bool value)
		{
			this.m_Authenticated = value;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002510 File Offset: 0x00000710
		public void SetUnderage(bool value)
		{
			this.m_Underage = value;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000251C File Offset: 0x0000071C
		public IUserProfile[] friends
		{
			get
			{
				return this.m_Friends;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002524 File Offset: 0x00000724
		public bool authenticated
		{
			get
			{
				return this.m_Authenticated;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000252C File Offset: 0x0000072C
		public bool underage
		{
			get
			{
				return this.m_Underage;
			}
		}

		// Token: 0x04000095 RID: 149
		private IUserProfile[] m_Friends;

		// Token: 0x04000096 RID: 150
		private bool m_Authenticated;

		// Token: 0x04000097 RID: 151
		private bool m_Underage;
	}
}
