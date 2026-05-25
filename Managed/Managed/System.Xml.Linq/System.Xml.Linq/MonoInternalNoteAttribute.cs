using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
