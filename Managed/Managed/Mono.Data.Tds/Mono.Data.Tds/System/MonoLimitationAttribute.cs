using System;

namespace System
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000DF18 File Offset: 0x0000C118
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
