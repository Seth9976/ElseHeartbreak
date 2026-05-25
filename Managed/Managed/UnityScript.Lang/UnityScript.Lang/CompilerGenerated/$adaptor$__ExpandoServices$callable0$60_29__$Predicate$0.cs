using System;
using UnityScript.Lang;

namespace CompilerGenerated
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	internal sealed class $adaptor$__ExpandoServices$callable0$60_29__$Predicate$0
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002FC8 File Offset: 0x000011C8
		public $adaptor$__ExpandoServices$callable0$60_29__$Predicate$0(__ExpandoServices$callable0$60_29__ from)
		{
			this.$from = from;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FD8 File Offset: 0x000011D8
		public bool Invoke(Expando obj)
		{
			return this.$from(obj);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static Predicate<Expando> Adapt(__ExpandoServices$callable0$60_29__ from)
		{
			return new Predicate<Expando>(new $adaptor$__ExpandoServices$callable0$60_29__$Predicate$0(from).Invoke);
		}

		// Token: 0x04000010 RID: 16
		protected __ExpandoServices$callable0$60_29__ $from;
	}
}
