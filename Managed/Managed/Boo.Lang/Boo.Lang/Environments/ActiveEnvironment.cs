using System;

namespace Boo.Lang.Environments
{
	// Token: 0x0200000C RID: 12
	public static class ActiveEnvironment
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static IEnvironment Instance
		{
			get
			{
				return ActiveEnvironment._instance;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AAC File Offset: 0x00000CAC
		public static void With(IEnvironment environment, Procedure code)
		{
			IEnvironment instance = ActiveEnvironment._instance;
			try
			{
				ActiveEnvironment._instance = environment;
				code();
			}
			finally
			{
				ActiveEnvironment._instance = instance;
			}
		}

		// Token: 0x04000008 RID: 8
		[ThreadStatic]
		private static IEnvironment _instance;
	}
}
