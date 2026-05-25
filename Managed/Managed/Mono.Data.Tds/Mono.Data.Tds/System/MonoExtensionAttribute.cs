using System;

namespace System
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000DF00 File Offset: 0x0000C100
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
