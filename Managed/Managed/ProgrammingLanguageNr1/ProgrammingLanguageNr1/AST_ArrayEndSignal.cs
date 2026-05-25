using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002E RID: 46
	public class AST_ArrayEndSignal : AST
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000B230 File Offset: 0x00009430
		public AST_ArrayEndSignal(Token token)
			: base(token)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000B23C File Offset: 0x0000943C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000B244 File Offset: 0x00009444
		public int ArraySize
		{
			get
			{
				return this.m_arraySize;
			}
			set
			{
				this.m_arraySize = value;
			}
		}

		// Token: 0x040000C7 RID: 199
		private int m_arraySize;
	}
}
