using System;

namespace System
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000DF0C File Offset: 0x0000C10C
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
