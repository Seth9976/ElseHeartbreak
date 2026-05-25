using System;

namespace System
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00002C8C File Offset: 0x00000E8C
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
