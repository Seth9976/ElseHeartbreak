using System;
using System.Collections.Generic;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200000E RID: 14
	public class ExternalFunctionCreator
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000047A8 File Offset: 0x000029A8
		public ExternalFunctionCreator(FunctionDefinition[] functionDefinitions)
		{
			if (functionDefinitions != null)
			{
				foreach (FunctionDefinition functionDefinition in functionDefinitions)
				{
					this.defineFunction(functionDefinition);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004804 File Offset: 0x00002A04
		private void defineFunction(FunctionDefinition f)
		{
			if (this.externalFunctions.ContainsKey(f.functionName))
			{
				throw new Error("There is already a function called '" + f.functionName + "'", Error.ErrorType.UNDEFINED, 0, 0);
			}
			AST ast = new AST(new Token(Token.TokenType.NODE_GROUP, "<PARAMETER_LIST>"));
			for (int i = 0; i < f.parameterTypes.Length; i++)
			{
				ast.addChild(this.createParameterDefinition(f.parameterTypes[i], f.parameterNames[i]));
			}
			AST ast2 = this.createFunctionDefinitionNode(f.returnType, f.functionName, ast);
			this.m_builtInFunctions.Add(ast2);
			this.externalFunctions.Add(f.functionName, f.callback);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000048CC File Offset: 0x00002ACC
		private AST_FunctionDefinitionNode createFunctionDefinitionNode(string returnTypeName, string functionName, AST parameterList)
		{
			AST_FunctionDefinitionNode ast_FunctionDefinitionNode = new AST_FunctionDefinitionNode(new Token(Token.TokenType.FUNC_DECLARATION, "<EXTERNAL_FUNC_DECLARATION>"));
			ast_FunctionDefinitionNode.addChild(new Token(Token.TokenType.BUILT_IN_TYPE_NAME, returnTypeName));
			ast_FunctionDefinitionNode.addChild(new Token(Token.TokenType.NAME, functionName));
			ast_FunctionDefinitionNode.addChild(parameterList);
			ast_FunctionDefinitionNode.addChild(new Token(Token.TokenType.STATEMENT_LIST, "<EMPTY_STATEMENT_LIST>"));
			return ast_FunctionDefinitionNode;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004920 File Offset: 0x00002B20
		public static ReturnValueType GetReturnTypeFromString(string name)
		{
			string text = name.ToLower();
			switch (text)
			{
			case "number":
				return ReturnValueType.NUMBER;
			case "string":
				return ReturnValueType.STRING;
			case "void":
				return ReturnValueType.VOID;
			case "bool":
				return ReturnValueType.BOOL;
			case "array":
				return ReturnValueType.ARRAY;
			case "range":
				return ReturnValueType.RANGE;
			case "var":
				return ReturnValueType.UNKNOWN_TYPE;
			case "unknown_type":
				return ReturnValueType.UNKNOWN_TYPE;
			}
			throw new Exception("GetReturnTypeFromString can't handle built in type with name " + name);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004A10 File Offset: 0x00002C10
		private AST createParameterDefinition(string typeName, string variableName)
		{
			AST ast = new AST(new Token(Token.TokenType.PARAMETER, "<PARAMETER>"));
			AST ast2 = new AST_VariableDeclaration(new Token(Token.TokenType.VAR_DECLARATION, "<PARAMETER_DECLARATION>"), ExternalFunctionCreator.GetReturnTypeFromString(typeName), variableName);
			AST ast3 = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), variableName);
			ast.addChild(ast2);
			ast.addChild(ast3);
			return ast;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004A6C File Offset: 0x00002C6C
		public List<AST> FunctionASTs
		{
			get
			{
				return this.m_builtInFunctions;
			}
		}

		// Token: 0x0400002A RID: 42
		public Dictionary<string, ExternalFunctionCreator.OnFunctionCall> externalFunctions = new Dictionary<string, ExternalFunctionCreator.OnFunctionCall>();

		// Token: 0x0400002B RID: 43
		private List<AST> m_builtInFunctions = new List<AST>();

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000074 RID: 116
		public delegate object OnFunctionCall(object[] pParameters);
	}
}
