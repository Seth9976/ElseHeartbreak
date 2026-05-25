using System;

namespace System
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
