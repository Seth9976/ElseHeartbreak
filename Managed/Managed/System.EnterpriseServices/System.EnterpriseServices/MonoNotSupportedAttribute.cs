using System;

namespace System
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
