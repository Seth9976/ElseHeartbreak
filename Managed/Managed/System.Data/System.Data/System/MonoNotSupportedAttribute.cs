using System;

namespace System
{
	// Token: 0x0200007D RID: 125
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0001F410 File Offset: 0x0001D610
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
