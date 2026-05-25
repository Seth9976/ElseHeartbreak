using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000D RID: 13
	internal class BsonString : BsonValue
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003BF1 File Offset: 0x00001DF1
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003BF9 File Offset: 0x00001DF9
		public int ByteCount { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003C02 File Offset: 0x00001E02
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003C0A File Offset: 0x00001E0A
		public bool IncludeLength { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x00003C13 File Offset: 0x00001E13
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
