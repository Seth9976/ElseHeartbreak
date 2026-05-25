using System;

namespace System
{
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00002C80 File Offset: 0x00000E80
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
