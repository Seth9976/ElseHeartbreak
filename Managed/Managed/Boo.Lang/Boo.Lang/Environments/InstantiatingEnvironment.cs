using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000015 RID: 21
	public class InstantiatingEnvironment : IEnvironment
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002EE4 File Offset: 0x000010E4
		public TNeed Provide<TNeed>() where TNeed : class
		{
			return Activator.CreateInstance<TNeed>();
		}
	}
}
