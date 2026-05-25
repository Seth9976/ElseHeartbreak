using System;

namespace UnityEngine
{
	// Token: 0x0200007C RID: 124
	[Flags]
	public enum HideFlags
	{
		// Token: 0x040001B4 RID: 436
		None = 0,
		// Token: 0x040001B5 RID: 437
		HideInHierarchy = 1,
		// Token: 0x040001B6 RID: 438
		HideInInspector = 2,
		// Token: 0x040001B7 RID: 439
		DontSave = 4,
		// Token: 0x040001B8 RID: 440
		NotEditable = 8,
		// Token: 0x040001B9 RID: 441
		HideAndDontSave = 13
	}
}
