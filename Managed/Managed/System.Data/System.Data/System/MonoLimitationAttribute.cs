using System;

namespace System
{
	// Token: 0x0200007C RID: 124
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001F404 File Offset: 0x0001D604
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
