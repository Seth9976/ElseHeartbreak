using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000024 RID: 36
	internal interface ISecurityCallContext
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121
		int Count { get; }

		// Token: 0x0600007A RID: 122
		void GetEnumerator(ref IEnumerator enumerator);

		// Token: 0x0600007B RID: 123
		object GetItem(string user);

		// Token: 0x0600007C RID: 124
		bool IsCallerInRole(string role);

		// Token: 0x0600007D RID: 125
		bool IsSecurityEnabled();

		// Token: 0x0600007E RID: 126
		bool IsUserInRole(ref object user, string role);
	}
}
