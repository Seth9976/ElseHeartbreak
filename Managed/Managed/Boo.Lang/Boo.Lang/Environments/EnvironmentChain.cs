using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000012 RID: 18
	public class EnvironmentChain : IEnvironment
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002E44 File Offset: 0x00001044
		public EnvironmentChain(params IEnvironment[] chain)
		{
			this._chain = chain;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E54 File Offset: 0x00001054
		public TNeed Provide<TNeed>() where TNeed : class
		{
			foreach (IEnvironment environment in this._chain)
			{
				TNeed tneed = environment.Provide<TNeed>();
				if (tneed != null)
				{
					return tneed;
				}
			}
			return (TNeed)((object)null);
		}

		// Token: 0x04000010 RID: 16
		private readonly IEnvironment[] _chain;
	}
}
