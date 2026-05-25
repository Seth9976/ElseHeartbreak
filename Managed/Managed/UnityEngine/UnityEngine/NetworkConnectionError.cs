using System;

namespace UnityEngine
{
	// Token: 0x0200011F RID: 287
	public enum NetworkConnectionError
	{
		// Token: 0x0400052A RID: 1322
		NoError,
		// Token: 0x0400052B RID: 1323
		RSAPublicKeyMismatch = 21,
		// Token: 0x0400052C RID: 1324
		InvalidPassword = 23,
		// Token: 0x0400052D RID: 1325
		ConnectionFailed = 15,
		// Token: 0x0400052E RID: 1326
		TooManyConnectedPlayers = 18,
		// Token: 0x0400052F RID: 1327
		ConnectionBanned = 22,
		// Token: 0x04000530 RID: 1328
		AlreadyConnectedToServer = 16,
		// Token: 0x04000531 RID: 1329
		AlreadyConnectedToAnotherServer = -1,
		// Token: 0x04000532 RID: 1330
		CreateSocketOrThreadFailure = -2,
		// Token: 0x04000533 RID: 1331
		IncorrectParameters = -3,
		// Token: 0x04000534 RID: 1332
		EmptyConnectTarget = -4,
		// Token: 0x04000535 RID: 1333
		InternalDirectConnectFailed = -5,
		// Token: 0x04000536 RID: 1334
		NATTargetNotConnected = 69,
		// Token: 0x04000537 RID: 1335
		NATTargetConnectionLost = 71,
		// Token: 0x04000538 RID: 1336
		NATPunchthroughFailed = 73
	}
}
