using System;

namespace UnityEngine
{
	// Token: 0x0200010D RID: 269
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x040004D4 RID: 1236
		None = 0,
		// Token: 0x040004D5 RID: 1237
		Shift = 1,
		// Token: 0x040004D6 RID: 1238
		Control = 2,
		// Token: 0x040004D7 RID: 1239
		Alt = 4,
		// Token: 0x040004D8 RID: 1240
		Command = 8,
		// Token: 0x040004D9 RID: 1241
		Numeric = 16,
		// Token: 0x040004DA RID: 1242
		CapsLock = 32,
		// Token: 0x040004DB RID: 1243
		FunctionKey = 64
	}
}
