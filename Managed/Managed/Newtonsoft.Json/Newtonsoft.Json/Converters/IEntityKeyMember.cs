using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001C RID: 28
	internal interface IEntityKeyMember
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010F RID: 271
		// (set) Token: 0x06000110 RID: 272
		string Key { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000111 RID: 273
		// (set) Token: 0x06000112 RID: 274
		object Value { get; set; }
	}
}
