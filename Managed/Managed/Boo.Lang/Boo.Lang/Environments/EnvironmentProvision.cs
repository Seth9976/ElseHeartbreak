using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000013 RID: 19
	public struct EnvironmentProvision<T> where T : class
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002E9C File Offset: 0x0000109C
		public T Instance
		{
			get
			{
				if (this._instance != null)
				{
					return this._instance;
				}
				return this._instance = My<T>.Instance;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002ED0 File Offset: 0x000010D0
		public static implicit operator T(EnvironmentProvision<T> provision)
		{
			return provision.Instance;
		}

		// Token: 0x04000011 RID: 17
		private T _instance;
	}
}
