using System;

namespace UnityEngine
{
	// Token: 0x020000E4 RID: 228
	public struct LOD
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0000D030 File Offset: 0x0000B230
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.renderers = renderers;
		}

		// Token: 0x040002E2 RID: 738
		public float screenRelativeTransitionHeight;

		// Token: 0x040002E3 RID: 739
		public Renderer[] renderers;
	}
}
