using System;

namespace Newtonsoft.Json
{
	// Token: 0x020000B7 RID: 183
	public enum WriteState
	{
		// Token: 0x04000292 RID: 658
		Error,
		// Token: 0x04000293 RID: 659
		Closed,
		// Token: 0x04000294 RID: 660
		Object,
		// Token: 0x04000295 RID: 661
		Array,
		// Token: 0x04000296 RID: 662
		Constructor,
		// Token: 0x04000297 RID: 663
		Property,
		// Token: 0x04000298 RID: 664
		Start
	}
}
