using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000025 RID: 37
	internal interface ISecurityCallersColl
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007F RID: 127
		int Count { get; }

		// Token: 0x06000080 RID: 128
		void GetEnumerator(out IEnumerator enumerator);

		// Token: 0x06000081 RID: 129
		ISecurityIdentityColl GetItem(int idx);
	}
}
