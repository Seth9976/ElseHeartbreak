using System;

namespace Pathfinding
{
	// Token: 0x02000004 RID: 4
	public enum PathStatus
	{
		// Token: 0x04000003 RID: 3
		NOT_CALCULATED_YET,
		// Token: 0x04000004 RID: 4
		DESTINATION_UNREACHABLE,
		// Token: 0x04000005 RID: 5
		FOUND_GOAL,
		// Token: 0x04000006 RID: 6
		ALREADY_THERE
	}
}
