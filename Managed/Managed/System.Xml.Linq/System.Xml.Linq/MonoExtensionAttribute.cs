using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
