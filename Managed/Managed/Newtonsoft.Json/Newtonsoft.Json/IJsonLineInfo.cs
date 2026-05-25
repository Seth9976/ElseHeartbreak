using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000027 RID: 39
	public interface IJsonLineInfo
	{
		// Token: 0x06000146 RID: 326
		bool HasLineInfo();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000147 RID: 327
		int LineNumber { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000148 RID: 328
		int LinePosition { get; }
	}
}
