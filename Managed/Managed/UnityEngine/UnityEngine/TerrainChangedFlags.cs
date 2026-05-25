using System;

namespace UnityEngine
{
	// Token: 0x02000216 RID: 534
	[Flags]
	internal enum TerrainChangedFlags
	{
		// Token: 0x04000827 RID: 2087
		NoChange = 0,
		// Token: 0x04000828 RID: 2088
		Heightmap = 1,
		// Token: 0x04000829 RID: 2089
		TreeInstances = 2,
		// Token: 0x0400082A RID: 2090
		DelayedHeightmapUpdate = 4,
		// Token: 0x0400082B RID: 2091
		FlushEverythingImmediately = 8,
		// Token: 0x0400082C RID: 2092
		RemoveDirtyDetailsImmediately = 16,
		// Token: 0x0400082D RID: 2093
		WillBeDestroyed = 256
	}
}
