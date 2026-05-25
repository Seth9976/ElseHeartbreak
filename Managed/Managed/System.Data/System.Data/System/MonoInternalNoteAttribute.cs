using System;

namespace System
{
	// Token: 0x0200007B RID: 123
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
