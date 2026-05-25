using System;

namespace Newtonsoft.Json
{
	// Token: 0x020000B6 RID: 182
	public enum JsonToken
	{
		// Token: 0x0400027F RID: 639
		None,
		// Token: 0x04000280 RID: 640
		StartObject,
		// Token: 0x04000281 RID: 641
		StartArray,
		// Token: 0x04000282 RID: 642
		StartConstructor,
		// Token: 0x04000283 RID: 643
		PropertyName,
		// Token: 0x04000284 RID: 644
		Comment,
		// Token: 0x04000285 RID: 645
		Raw,
		// Token: 0x04000286 RID: 646
		Integer,
		// Token: 0x04000287 RID: 647
		Float,
		// Token: 0x04000288 RID: 648
		String,
		// Token: 0x04000289 RID: 649
		Boolean,
		// Token: 0x0400028A RID: 650
		Null,
		// Token: 0x0400028B RID: 651
		Undefined,
		// Token: 0x0400028C RID: 652
		EndObject,
		// Token: 0x0400028D RID: 653
		EndArray,
		// Token: 0x0400028E RID: 654
		EndConstructor,
		// Token: 0x0400028F RID: 655
		Date,
		// Token: 0x04000290 RID: 656
		Bytes
	}
}
