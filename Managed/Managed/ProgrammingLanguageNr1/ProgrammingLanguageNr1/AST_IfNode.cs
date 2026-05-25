using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000022 RID: 34
	public class AST_IfNode : AST
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00008F54 File Offset: 0x00007154
		public AST_IfNode(Token token)
			: base(token)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008F60 File Offset: 0x00007160
		public void setScope(Scope scope)
		{
			if (scope == null)
			{
				throw new Exception("can't set m_scope to null for IfNode at line " + base.getToken().LineNr);
			}
			this.m_scope = scope;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008F90 File Offset: 0x00007190
		public Scope getScope()
		{
			if (this.m_scope == null)
			{
				throw new Exception("m_scope is null for IfNode at line " + base.getToken().LineNr);
			}
			return this.m_scope;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008FC4 File Offset: 0x000071C4
		public override void ClearMemorySpaces()
		{
			base.ClearMemorySpaces();
			if (this.m_scope != null)
			{
				this.m_scope.ClearMemorySpaces();
			}
		}

		// Token: 0x040000A3 RID: 163
		private Scope m_scope;
	}
}
