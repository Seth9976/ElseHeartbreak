using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200002F RID: 47
	public interface ILocalUser : IUserProfile
	{
		// Token: 0x060000B8 RID: 184
		void Authenticate(Action<bool> callback);

		// Token: 0x060000B9 RID: 185
		void LoadFriends(Action<bool> callback);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BA RID: 186
		IUserProfile[] friends { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BB RID: 187
		bool authenticated { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BC RID: 188
		bool underage { get; }
	}
}
