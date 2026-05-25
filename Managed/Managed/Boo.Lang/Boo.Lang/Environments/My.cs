using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000016 RID: 22
	public static class My<TNeed> where TNeed : class
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EEC File Offset: 0x000010EC
		public static TNeed Instance
		{
			get
			{
				IEnvironment instance = ActiveEnvironment.Instance;
				if (instance == null)
				{
					throw new InvalidOperationException("Environment is not available!");
				}
				TNeed tneed = instance.Provide<TNeed>();
				if (tneed == null)
				{
					throw new InvalidOperationException(string.Format("Environment could not provide '{0}'.", typeof(TNeed)));
				}
				return tneed;
			}
		}
	}
}
