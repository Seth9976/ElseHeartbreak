using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000002 RID: 2
	public class SprakAPI : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public SprakAPI(params string[] values)
		{
			this.Values = values;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		public string[] Values { get; set; }
	}
}
