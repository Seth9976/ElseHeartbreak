using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200001B RID: 27
	public class ScopeBuilder
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00007FB0 File Offset: 0x000061B0
		public ScopeBuilder(AST ast, ErrorHandler errorHandler)
		{
			Debug.Assert(ast != null);
			Debug.Assert(errorHandler != null);
			this.m_errorHandler = errorHandler;
			this.m_ast = ast;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007FFC File Offset: 0x000061FC
		public void process()
		{
			this.m_globalScope = new Scope(Scope.ScopeType.MAIN_SCOPE, "global scope");
			this.m_currentScope = this.m_globalScope;
			this.evaluateScopeDeclarations(this.m_ast);
			this.evaluateReferences(this.m_ast);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008034 File Offset: 0x00006234
		private void evaluateScopeDeclarations(AST tree)
		{
			Debug.Assert(tree != null);
			if (tree.getTokenType() == Token.TokenType.FUNC_DECLARATION)
			{
				this.evaluateFunctionScope(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.IF)
			{
				this.evaluateIfScope(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.LOOP)
			{
				this.evaluateLoopScope(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.LOOP_BLOCK)
			{
				this.evaluateLoopBlockScope(tree);
			}
			else if (tree.getChildren() != null)
			{
				this.evaluateScopeDeclarationsInAllChildren(tree);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000080C4 File Offset: 0x000062C4
		private void evaluateScopeDeclarationsInAllChildren(AST tree)
		{
			foreach (AST ast in tree.getChildren())
			{
				this.evaluateScopeDeclarations(ast);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000812C File Offset: 0x0000632C
		private void evaluateFunctionScope(AST tree)
		{
			ReturnValueType returnTypeFromString = ExternalFunctionCreator.GetReturnTypeFromString(tree.getChild(0).getTokenString());
			string tokenString = tree.getChild(1).getTokenString();
			Symbol symbol = new FunctionSymbol(this.m_currentScope, tokenString, returnTypeFromString, tree);
			this.m_globalScope.define(symbol);
			this.m_currentScope = (Scope)symbol;
			AST_FunctionDefinitionNode ast_FunctionDefinitionNode = (AST_FunctionDefinitionNode)tree;
			ast_FunctionDefinitionNode.setScope((Scope)symbol);
			this.evaluateScopeDeclarations(tree.getChild(3));
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000081B0 File Offset: 0x000063B0
		private void evaluateIfScope(AST tree)
		{
			Scope scope = new Scope(Scope.ScopeType.IF_SCOPE, "<IF-SUBSCOPE>", this.m_currentScope);
			this.m_currentScope = scope;
			AST_IfNode ast_IfNode = tree as AST_IfNode;
			Debug.Assert(ast_IfNode != null);
			ast_IfNode.setScope(scope);
			this.evaluateScopeDeclarationsInAllChildren(tree.getChild(0));
			AST child = ast_IfNode.getChild(1);
			AST ast = null;
			if (ast_IfNode.getChildren().Count == 3)
			{
				ast = ast_IfNode.getChild(2);
			}
			this.evaluateScopeDeclarationsInAllChildren(child);
			if (ast != null)
			{
				this.evaluateScopeDeclarationsInAllChildren(ast);
			}
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008248 File Offset: 0x00006448
		private void evaluateLoopScope(AST tree)
		{
			Scope scope = new Scope(Scope.ScopeType.LOOP_SCOPE, "<LOOP-SUBSCOPE " + ScopeBuilder.loopSubscopes++ + ">", this.m_currentScope);
			this.m_currentScope = scope;
			AST_LoopNode ast_LoopNode = tree as AST_LoopNode;
			Debug.Assert(ast_LoopNode != null);
			this.evaluateScopeDeclarationsInAllChildren(ast_LoopNode);
			ast_LoopNode.setScope(this.m_currentScope);
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000082C4 File Offset: 0x000064C4
		private void evaluateLoopBlockScope(AST tree)
		{
			Scope scope = new Scope(Scope.ScopeType.LOOP_BLOCK_SCOPE, "<LOOP-BLOCK-SUBSCOPE " + ScopeBuilder.loopBlockSubscopes++ + ">", this.m_currentScope);
			this.m_currentScope = scope;
			AST_LoopBlockNode ast_LoopBlockNode = tree as AST_LoopBlockNode;
			Debug.Assert(ast_LoopBlockNode != null);
			this.evaluateScopeDeclarationsInAllChildren(ast_LoopBlockNode);
			ast_LoopBlockNode.setScope(this.m_currentScope);
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008340 File Offset: 0x00006540
		private void evaluateReferences(AST tree)
		{
			Debug.Assert(tree != null);
			if (tree.getTokenType() == Token.TokenType.VAR_DECLARATION)
			{
				this.evaluateReferencesForVAR_DECLARATION(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.ASSIGNMENT)
			{
				this.evaluateReferencesForASSIGNMENT(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.ASSIGNMENT_TO_ARRAY)
			{
				this.evaluateReferencesForASSIGNMENT_TO_ARRAY(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.ARRAY_LOOKUP)
			{
				this.evaluateReferencesForARRAY_LOOKUP(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.FUNC_DECLARATION)
			{
				this.evaluateReferencesForFUNC_DECLARATION(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.FUNCTION_CALL)
			{
				this.evaluateReferencesForFUNCTION_CALL(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.IF)
			{
				this.evaluateReferencesForIF(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.NAME)
			{
				this.evaluateReferencesForNAME(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.LOOP_BLOCK)
			{
				this.evaluateReferencesForLOOP_BLOCK(tree);
			}
			else if (tree.getTokenType() == Token.TokenType.LOOP)
			{
				this.evaluateReferencesForLOOP(tree);
			}
			else
			{
				this.evaluateReferencesInAllChildren(tree);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008458 File Offset: 0x00006658
		private void evaluateReferencesInAllChildren(AST tree)
		{
			if (tree.getChildren() != null)
			{
				foreach (AST ast in tree.getChildren())
				{
					this.evaluateReferences(ast);
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000084CC File Offset: 0x000066CC
		private void evaluateReferencesForASSIGNMENT(AST tree)
		{
			AST_Assignment ast_Assignment = tree as AST_Assignment;
			if (this.m_currentScope.resolve(ast_Assignment.VariableName) == null)
			{
				this.m_errorHandler.errorOccured("Can't assign to undefined variable " + ast_Assignment.VariableName, Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition);
			}
			this.evaluateReferencesInAllChildren(tree);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008534 File Offset: 0x00006734
		private void evaluateReferencesForASSIGNMENT_TO_ARRAY(AST tree)
		{
			AST_Assignment ast_Assignment = tree as AST_Assignment;
			if (this.m_currentScope.resolve(ast_Assignment.VariableName) == null)
			{
				this.m_errorHandler.errorOccured("Can't assign to undefined array " + ast_Assignment.VariableName, Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition);
			}
			this.evaluateReferencesInAllChildren(tree);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000859C File Offset: 0x0000679C
		private void evaluateReferencesForARRAY_LOOKUP(AST tree)
		{
			if (this.m_currentScope.resolve(tree.getTokenString()) == null)
			{
				this.m_errorHandler.errorOccured("Can't lookup in undefined array " + tree.getTokenString(), Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition);
			}
			this.evaluateReferencesInAllChildren(tree);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000085FC File Offset: 0x000067FC
		private void evaluateReferencesForVAR_DECLARATION(AST tree)
		{
			AST_VariableDeclaration ast_VariableDeclaration = tree as AST_VariableDeclaration;
			ReturnValueType type = ast_VariableDeclaration.Type;
			string name = ast_VariableDeclaration.Name;
			if (this.m_currentScope.isDefined(name))
			{
				this.m_errorHandler.errorOccured(new Error("There is already a variable called '" + name + "'", Error.ErrorType.LOGIC, tree.getToken().LineNr, tree.getToken().LinePosition));
			}
			else
			{
				this.m_currentScope.define(new VariableSymbol(name, type));
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008680 File Offset: 0x00006880
		private void evaluateReferencesForFUNCTION_CALL(AST tree)
		{
			string tokenString = tree.getTokenString();
			Symbol symbol = this.m_currentScope.resolve(tokenString);
			FunctionSymbol functionSymbol = symbol as FunctionSymbol;
			if (functionSymbol == null)
			{
				this.m_errorHandler.errorOccured("Can't find function with name " + tokenString, Error.ErrorType.SCOPE, tree.getToken().LineNr, tree.getToken().LinePosition);
			}
			else
			{
				this.evaluateReferencesInAllChildren(tree);
				AST functionDefinitionNode = functionSymbol.getFunctionDefinitionNode();
				AST_FunctionDefinitionNode ast_FunctionDefinitionNode = (AST_FunctionDefinitionNode)functionDefinitionNode;
				AST_FunctionCall ast_FunctionCall = tree as AST_FunctionCall;
				Debug.Assert(ast_FunctionCall != null);
				ast_FunctionCall.FunctionDefinitionRef = ast_FunctionDefinitionNode;
				List<AST> children = ast_FunctionDefinitionNode.getChild(2).getChildren();
				AST child = tree.getChild(0);
				List<AST> children2 = child.getChildren();
				if (children2.Count != children.Count)
				{
					this.m_errorHandler.errorOccured(string.Concat(new object[]
					{
						"Wrong nr of arguments to  '",
						ast_FunctionDefinitionNode.getChild(1).getTokenString(),
						"' , expected ",
						children.Count,
						" but got ",
						children2.Count
					}), Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000087BC File Offset: 0x000069BC
		private void evaluateReferencesForIF(AST tree)
		{
			AST_IfNode ast_IfNode = (AST_IfNode)tree;
			this.m_currentScope = ast_IfNode.getScope();
			this.evaluateReferencesInAllChildren(tree);
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000087F4 File Offset: 0x000069F4
		private void evaluateReferencesForNAME(AST tree)
		{
			if (this.m_currentScope == null)
			{
				throw new Exception("m_currentScope is null");
			}
			if (tree == null)
			{
				throw new Exception("tree is null");
			}
			Symbol symbol = this.m_currentScope.resolve(tree.getTokenString());
			if (symbol == null)
			{
				this.m_errorHandler.errorOccured(new Error("Can't find variable or function '" + tree.getTokenString() + "' (forgot quotes?)", Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition));
			}
			else if (symbol is FunctionSymbol)
			{
				this.m_errorHandler.errorOccured(new Error("'" + tree.getTokenString() + "' is a function and must be called with ()", Error.ErrorType.SYNTAX, tree.getToken().LineNr, tree.getToken().LinePosition));
			}
			this.evaluateReferencesInAllChildren(tree);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000088D0 File Offset: 0x00006AD0
		private void evaluateReferencesForFUNC_DECLARATION(AST tree)
		{
			string tokenString = tree.getChild(1).getTokenString();
			this.m_currentScope = (Scope)this.m_currentScope.resolve(tokenString);
			this.evaluateReferencesInAllChildren(tree.getChild(2));
			this.evaluateReferencesInAllChildren(tree.getChild(3));
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000892C File Offset: 0x00006B2C
		private void evaluateReferencesForLOOP_BLOCK(AST tree)
		{
			AST_LoopBlockNode ast_LoopBlockNode = tree as AST_LoopBlockNode;
			this.m_currentScope = ast_LoopBlockNode.getScope();
			this.evaluateReferencesInAllChildren(tree);
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008964 File Offset: 0x00006B64
		private void evaluateReferencesForLOOP(AST tree)
		{
			AST_LoopNode ast_LoopNode = tree as AST_LoopNode;
			this.m_currentScope = ast_LoopNode.getScope();
			this.evaluateReferencesInAllChildren(tree);
			this.m_currentScope = this.m_currentScope.getEnclosingScope();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000899C File Offset: 0x00006B9C
		public Scope getGlobalScope()
		{
			Debug.Assert(this.m_globalScope != null, "The global scope is null, this probably means that you haven't called process() on ScopeBuilder");
			return this.m_globalScope;
		}

		// Token: 0x04000086 RID: 134
		private static int loopSubscopes = 0;

		// Token: 0x04000087 RID: 135
		private static int loopBlockSubscopes = 0;

		// Token: 0x04000088 RID: 136
		private AST m_ast;

		// Token: 0x04000089 RID: 137
		private Scope m_globalScope;

		// Token: 0x0400008A RID: 138
		private Scope m_currentScope;

		// Token: 0x0400008B RID: 139
		private ErrorHandler m_errorHandler;
	}
}
