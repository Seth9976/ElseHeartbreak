using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000079 RID: 121
	public interface IContractResolver
	{
		// Token: 0x060005D0 RID: 1488
		JsonContract ResolveContract(Type type);
	}
}
