using System;

namespace System.EnterpriseServices
{
	// Token: 0x0200002A RID: 42
	internal interface ISharedPropertyGroup
	{
		// Token: 0x06000089 RID: 137
		ISharedProperty CreateProperty(string name, out bool fExists);

		// Token: 0x0600008A RID: 138
		ISharedProperty CreatePropertyByPosition(int position, out bool fExists);

		// Token: 0x0600008B RID: 139
		ISharedProperty Property(string name);

		// Token: 0x0600008C RID: 140
		ISharedProperty PropertyByPosition(int position);
	}
}
