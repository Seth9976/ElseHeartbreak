using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200008C RID: 140
	[Flags]
	public enum JsonSchemaType
	{
		// Token: 0x0400021F RID: 543
		None = 0,
		// Token: 0x04000220 RID: 544
		String = 1,
		// Token: 0x04000221 RID: 545
		Float = 2,
		// Token: 0x04000222 RID: 546
		Integer = 4,
		// Token: 0x04000223 RID: 547
		Boolean = 8,
		// Token: 0x04000224 RID: 548
		Object = 16,
		// Token: 0x04000225 RID: 549
		Array = 32,
		// Token: 0x04000226 RID: 550
		Null = 64,
		// Token: 0x04000227 RID: 551
		Any = 127
	}
}
