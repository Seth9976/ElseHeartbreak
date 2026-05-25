using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000037 RID: 55
	public struct Range
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003F0C File Offset: 0x0000210C
		public Range(int fromValue, int valueCount)
		{
			this.from = fromValue;
			this.count = valueCount;
		}

		// Token: 0x040000D9 RID: 217
		public int from;

		// Token: 0x040000DA RID: 218
		public int count;
	}
}
