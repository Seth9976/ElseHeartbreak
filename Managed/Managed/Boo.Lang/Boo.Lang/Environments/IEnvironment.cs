using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000014 RID: 20
	public interface IEnvironment
	{
		// Token: 0x06000055 RID: 85
		TNeed Provide<TNeed>() where TNeed : class;
	}
}
