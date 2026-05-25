using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002D RID: 45
	public class AST_LoopBlockNode : AST
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public AST_LoopBlockNode(Token token)
			: base(token)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B1FC File Offset: 0x000093FC
		public override void ClearMemorySpaces()
		{
			base.ClearMemorySpaces();
			if (this.m_scope != null)
			{
				this.m_scope.ClearMemorySpaces();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B21C File Offset: 0x0000941C
		public void setScope(Scope scope)
		{
			this.m_scope = scope;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B228 File Offset: 0x00009428
		public Scope getScope()
		{
			return this.m_scope;
		}

		// Token: 0x040000C6 RID: 198
		private Scope m_scope;
	}
}
