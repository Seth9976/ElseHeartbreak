using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002B RID: 43
	public class AST_LoopNode : AST
	{
		// Token: 0x0600017B RID: 379 RVA: 0x0000B190 File Offset: 0x00009390
		public AST_LoopNode(Token token)
			: base(token)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B19C File Offset: 0x0000939C
		public override void ClearMemorySpaces()
		{
			base.ClearMemorySpaces();
			if (this.m_scope != null)
			{
				this.m_scope.ClearMemorySpaces();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B1BC File Offset: 0x000093BC
		public void setScope(Scope scope)
		{
			this.m_scope = scope;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public Scope getScope()
		{
			return this.m_scope;
		}

		// Token: 0x040000C3 RID: 195
		private Scope m_scope;
	}
}
