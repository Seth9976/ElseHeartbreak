using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000019 RID: 25
	internal interface IConfigurationAttribute
	{
		// Token: 0x06000064 RID: 100
		bool AfterSaveChanges(Hashtable info);

		// Token: 0x06000065 RID: 101
		bool Apply(Hashtable info);

		// Token: 0x06000066 RID: 102
		bool IsValidTarget(string s);
	}
}
