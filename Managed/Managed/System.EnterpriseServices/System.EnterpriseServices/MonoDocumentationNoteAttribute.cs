using System;

namespace System
{
	// Token: 0x0200004B RID: 75
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00002C74 File Offset: 0x00000E74
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
