using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000010 RID: 16
	public static class EnvironmentBoundValue
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public static EnvironmentBoundValue<T> Capture<T>() where T : class
		{
			return EnvironmentBoundValue.Return<T>(My<T>.Instance);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static EnvironmentBoundValue<T> Return<T>(T value)
		{
			return EnvironmentBoundValue.Create<T>(ActiveEnvironment.Instance, value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public static EnvironmentBoundValue<T> Create<T>(IEnvironment environment, T value)
		{
			return new EnvironmentBoundValue<T>(environment, value);
		}
	}
}
