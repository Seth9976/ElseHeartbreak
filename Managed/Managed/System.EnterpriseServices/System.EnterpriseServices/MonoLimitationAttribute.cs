using System;

namespace System
{
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00002C98 File Offset: 0x00000E98
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
