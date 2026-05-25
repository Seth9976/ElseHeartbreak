using System;

namespace System
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000DF24 File Offset: 0x0000C124
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
