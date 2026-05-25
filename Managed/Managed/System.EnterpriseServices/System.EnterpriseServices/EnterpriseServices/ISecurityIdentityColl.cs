using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000026 RID: 38
	internal interface ISecurityIdentityColl
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000082 RID: 130
		int Count { get; }

		// Token: 0x06000083 RID: 131
		void GetEnumerator(out IEnumerator enumerator);

		// Token: 0x06000084 RID: 132
		SecurityIdentity GetItem(int idx);
	}
}
