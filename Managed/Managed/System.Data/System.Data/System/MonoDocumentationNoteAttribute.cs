using System;

namespace System
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
