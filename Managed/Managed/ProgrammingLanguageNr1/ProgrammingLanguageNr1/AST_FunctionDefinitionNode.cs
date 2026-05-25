using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000021 RID: 33
	public class AST_FunctionDefinitionNode : AST
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00008F14 File Offset: 0x00007114
		public AST_FunctionDefinitionNode(Token token)
			: base(token)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008F20 File Offset: 0x00007120
		public override void ClearMemorySpaces()
		{
			base.ClearMemorySpaces();
			if (this.m_scope != null)
			{
				this.m_scope.ClearMemorySpaces();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008F40 File Offset: 0x00007140
		public void setScope(Scope scope)
		{
			this.m_scope = scope;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008F4C File Offset: 0x0000714C
		public Scope getScope()
		{
			return this.m_scope;
		}

		// Token: 0x040000A2 RID: 162
		private Scope m_scope;
	}
}
