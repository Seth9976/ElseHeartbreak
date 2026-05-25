using System;
using System.Diagnostics;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002A RID: 42
	public class FunctionSymbol : Scope, Symbol
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000B138 File Offset: 0x00009338
		public FunctionSymbol(Scope enclosingScope, string name, ReturnValueType type, AST functionDefinitionNode)
			: base(Scope.ScopeType.FUNCTION_SCOPE, name, enclosingScope)
		{
			Debug.Assert(enclosingScope != null);
			Debug.Assert(functionDefinitionNode != null);
			this.m_enclosingScope = enclosingScope;
			this.m_functionDefinitionNode = functionDefinitionNode;
			this.m_returnValueType = type;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B180 File Offset: 0x00009380
		public ReturnValueType getReturnValueType()
		{
			return this.m_returnValueType;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B188 File Offset: 0x00009388
		public AST getFunctionDefinitionNode()
		{
			return this.m_functionDefinitionNode;
		}

		// Token: 0x040000C1 RID: 193
		private AST m_functionDefinitionNode;

		// Token: 0x040000C2 RID: 194
		private ReturnValueType m_returnValueType;
	}
}
