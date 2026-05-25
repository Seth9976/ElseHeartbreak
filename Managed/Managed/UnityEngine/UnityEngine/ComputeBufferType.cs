using System;

namespace UnityEngine
{
	// Token: 0x0200015B RID: 347
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x040005E2 RID: 1506
		Default = 0,
		// Token: 0x040005E3 RID: 1507
		Raw = 1,
		// Token: 0x040005E4 RID: 1508
		Append = 2,
		// Token: 0x040005E5 RID: 1509
		Counter = 4,
		// Token: 0x040005E6 RID: 1510
		DrawIndirect = 256
	}
}
