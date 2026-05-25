using System;

namespace System
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
