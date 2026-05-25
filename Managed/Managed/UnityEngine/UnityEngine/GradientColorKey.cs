using System;

namespace UnityEngine
{
	// Token: 0x020000E6 RID: 230
	public struct GradientColorKey
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000D06C File Offset: 0x0000B26C
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x040002E4 RID: 740
		public Color color;

		// Token: 0x040002E5 RID: 741
		public float time;
	}
}
