using System;

namespace Boo.Lang.Environments
{
	// Token: 0x0200000E RID: 14
	public class ClosedEnvironment : IEnvironment
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C80 File Offset: 0x00000E80
		public ClosedEnvironment(params object[] bindings)
		{
			this._bindings = bindings;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C90 File Offset: 0x00000E90
		public TNeed Provide<TNeed>() where TNeed : class
		{
			foreach (object obj in this._bindings)
			{
				if (obj is TNeed)
				{
					return (TNeed)((object)obj);
				}
			}
			return (TNeed)((object)null);
		}

		// Token: 0x0400000C RID: 12
		private readonly object[] _bindings;
	}
}
