using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000009 RID: 9
	internal abstract class BsonToken
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79
		public abstract BsonType Type { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003AF1 File Offset: 0x00001CF1
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003AF9 File Offset: 0x00001CF9
		public BsonToken Parent { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003B02 File Offset: 0x00001D02
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003B0A File Offset: 0x00001D0A
		public int CalculatedSize { get; set; }
	}
}
