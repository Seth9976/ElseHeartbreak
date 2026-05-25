using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000F RID: 15
	internal class BsonProperty
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003C6C File Offset: 0x00001E6C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003C74 File Offset: 0x00001E74
		public BsonString Name { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003C7D File Offset: 0x00001E7D
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003C85 File Offset: 0x00001E85
		public BsonToken Value { get; set; }
	}
}
