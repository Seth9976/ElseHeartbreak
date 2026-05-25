using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000010 RID: 16
	internal enum BsonType : sbyte
	{
		// Token: 0x0400004D RID: 77
		Number = 1,
		// Token: 0x0400004E RID: 78
		String,
		// Token: 0x0400004F RID: 79
		Object,
		// Token: 0x04000050 RID: 80
		Array,
		// Token: 0x04000051 RID: 81
		Binary,
		// Token: 0x04000052 RID: 82
		Undefined,
		// Token: 0x04000053 RID: 83
		Oid,
		// Token: 0x04000054 RID: 84
		Boolean,
		// Token: 0x04000055 RID: 85
		Date,
		// Token: 0x04000056 RID: 86
		Null,
		// Token: 0x04000057 RID: 87
		Regex,
		// Token: 0x04000058 RID: 88
		Reference,
		// Token: 0x04000059 RID: 89
		Code,
		// Token: 0x0400005A RID: 90
		Symbol,
		// Token: 0x0400005B RID: 91
		CodeWScope,
		// Token: 0x0400005C RID: 92
		Integer,
		// Token: 0x0400005D RID: 93
		TimeStamp,
		// Token: 0x0400005E RID: 94
		Long,
		// Token: 0x0400005F RID: 95
		MinKey = -1,
		// Token: 0x04000060 RID: 96
		MaxKey = 127
	}
}
