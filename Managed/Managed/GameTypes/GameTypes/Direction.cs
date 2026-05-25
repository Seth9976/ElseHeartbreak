using System;

namespace GameTypes
{
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum Direction
	{
		// Token: 0x0400000E RID: 14
		ZERO = 0,
		// Token: 0x0400000F RID: 15
		RIGHT = 1,
		// Token: 0x04000010 RID: 16
		UP_RIGHT = 2,
		// Token: 0x04000011 RID: 17
		UP = 4,
		// Token: 0x04000012 RID: 18
		UP_LEFT = 8,
		// Token: 0x04000013 RID: 19
		LEFT = 16,
		// Token: 0x04000014 RID: 20
		DOWN_LEFT = 32,
		// Token: 0x04000015 RID: 21
		DOWN = 64,
		// Token: 0x04000016 RID: 22
		DOWN_RIGHT = 128
	}
}
