using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000006 RID: 6
	public class AST_FunctionCall : AST
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002474 File Offset: 0x00000674
		public AST_FunctionCall(Token token)
			: base(token)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002480 File Offset: 0x00000680
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002488 File Offset: 0x00000688
		public AST_FunctionDefinitionNode FunctionDefinitionRef
		{
			get
			{
				return this.m_functionDefinitionRef;
			}
			set
			{
				this.m_functionDefinitionRef = value;
			}
		}

		// Token: 0x04000005 RID: 5
		private AST_FunctionDefinitionNode m_functionDefinitionRef;
	}
}
