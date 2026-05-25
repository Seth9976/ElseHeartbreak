using System;

namespace UnityEngine
{
	// Token: 0x0200011E RID: 286
	public enum ConnectionTesterStatus
	{
		// Token: 0x0400051E RID: 1310
		Error = -2,
		// Token: 0x0400051F RID: 1311
		Undetermined,
		// Token: 0x04000520 RID: 1312
		[Obsolete("No longer returned, use newer connection tester enums instead.")]
		PrivateIPNoNATPunchthrough,
		// Token: 0x04000521 RID: 1313
		[Obsolete("No longer returned, use newer connection tester enums instead.")]
		PrivateIPHasNATPunchThrough,
		// Token: 0x04000522 RID: 1314
		PublicIPIsConnectable,
		// Token: 0x04000523 RID: 1315
		PublicIPPortBlocked,
		// Token: 0x04000524 RID: 1316
		PublicIPNoServerStarted,
		// Token: 0x04000525 RID: 1317
		LimitedNATPunchthroughPortRestricted,
		// Token: 0x04000526 RID: 1318
		LimitedNATPunchthroughSymmetric,
		// Token: 0x04000527 RID: 1319
		NATpunchthroughFullCone,
		// Token: 0x04000528 RID: 1320
		NATpunchthroughAddressRestrictedCone
	}
}
