using System;

namespace Boo.Lang.Environments
{
	// Token: 0x02000011 RID: 17
	public struct EnvironmentBoundValue<T>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public EnvironmentBoundValue(IEnvironment environment, T value)
		{
			this.Environment = environment;
			this.Value = value;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public EnvironmentBoundValue<TResult> Select<TResult>(Function<T, TResult> selector)
		{
			T v = this.Value;
			EnvironmentBoundValue<TResult> r = default(EnvironmentBoundValue<TResult>);
			ActiveEnvironment.With(this.Environment, delegate
			{
				r = EnvironmentBoundValue.Return<TResult>(selector(v));
			});
			return r;
		}

		// Token: 0x0400000E RID: 14
		public readonly T Value;

		// Token: 0x0400000F RID: 15
		public readonly IEnvironment Environment;
	}
}
