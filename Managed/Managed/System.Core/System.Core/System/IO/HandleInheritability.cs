using System;

namespace System.IO
{
	/// <summary>Specifies whether the underlying handle is inheritable by child processes.</summary>
	// Token: 0x02000087 RID: 135
	public enum HandleInheritability
	{
		/// <summary>Specifies that the handle is not inheritable by child processes.</summary>
		// Token: 0x040001BF RID: 447
		None,
		/// <summary>Specifies that the handle is inheritable by child processes.</summary>
		// Token: 0x040001C0 RID: 448
		Inheritable
	}
}
