using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000008 RID: 8
	public class InterpreterTwo : IEnumerable<InterpreterTwo.Status>, IEnumerable
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002514 File Offset: 0x00000714
		public InterpreterTwo(AST ast, Scope globalScope, ErrorHandler errorHandler, ExternalFunctionCreator externalFunctionCreator)
		{
			this.m_ast = ast;
			this.m_errorHandler = errorHandler;
			this.m_globalScope = globalScope;
			this.m_currentScope = this.m_globalScope;
			this.m_externalFunctionCreator = externalFunctionCreator;
			this.Reset();
			InterpreterTwo.nrOfInterpreters++;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025AC File Offset: 0x000007AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025B4 File Offset: 0x000007B4
		~InterpreterTwo()
		{
			InterpreterTwo.nrOfInterpreters--;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Reset()
		{
			if (this.m_globalMemorySpace != null)
			{
				this.m_globalMemorySpace.Delete();
				this.m_globalMemorySpace = null;
			}
			if (this.m_currentMemorySpace != null)
			{
				this.m_currentMemorySpace.Delete();
				this.m_currentMemorySpace = null;
			}
			if (this.m_globalScope != null)
			{
				this.m_globalScope.ClearMemorySpaces();
			}
			if (this.m_currentScope != null)
			{
				this.m_currentScope.ClearMemorySpaces();
			}
			this.m_valueStack.Clear();
			this.m_globalMemorySpace = new MemorySpace("globals", this.m_ast.getChild(0), this.m_globalScope, this.m_memorySpaceNodeListCache);
			this.m_currentMemorySpace = this.m_globalMemorySpace;
			this.m_memorySpaceStack.Clear();
			this.m_currentScope = this.m_globalScope;
			this.m_currentScope.ClearMemorySpaces();
			this.m_currentScope.PushMemorySpace(this.m_currentMemorySpace);
			this.m_memorySpaceNodeListCache.clear();
			this.m_topLevelDepth = 0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026F0 File Offset: 0x000008F0
		public IEnumerator<InterpreterTwo.Status> GetEnumerator()
		{
			while (this.ExecuteNextStatement())
			{
				yield return InterpreterTwo.Status.OK;
			}
			if (this.m_errorHandler.getErrors().Count > 0)
			{
				yield return InterpreterTwo.Status.ERROR;
			}
			else
			{
				yield return InterpreterTwo.Status.FINISHED;
			}
			yield break;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000270C File Offset: 0x0000090C
		private void PushNewScope(Scope newScope, string nameOfNewMemorySpace, AST startNode)
		{
			Debug.Assert(newScope != null);
			Debug.Assert(startNode != null);
			if (this.m_memorySpaceStack.Count > 100)
			{
				Token token = startNode.getToken();
				throw new Error("Stack overflow!", Error.ErrorType.RUNTIME, token.LineNr, token.LinePosition);
			}
			this.m_currentScope = newScope;
			this.m_memorySpaceStack.Push(this.m_currentMemorySpace);
			this.m_currentMemorySpace = new MemorySpace(nameOfNewMemorySpace, startNode, this.m_currentScope, this.m_memorySpaceNodeListCache);
			this.m_currentScope.PushMemorySpace(this.m_currentMemorySpace);
			InterpreterTwo.nrOfScopes++;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027B0 File Offset: 0x000009B0
		private void PopCurrentScope()
		{
			Scope currentScope = this.m_currentScope;
			MemorySpace memorySpace = this.m_currentScope.PopMemorySpace();
			this.m_currentMemorySpace = this.m_memorySpaceStack.Pop();
			this.m_currentScope = this.m_currentMemorySpace.Scope;
			this.m_currentScope.PushMemorySpace(this.m_currentMemorySpace);
			InterpreterTwo.nrOfScopes--;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002810 File Offset: 0x00000A10
		private void PrintValueStack()
		{
			Console.Write("VALUE_STACK: ");
			foreach (object obj in this.m_valueStack)
			{
				Console.Write(obj.ToString() + ", ");
			}
			Console.Write("\n");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000289C File Offset: 0x00000A9C
		public void PrintMemoryStack()
		{
			Console.WriteLine("MEMORY_STACK:");
			Console.WriteLine("\t" + this.m_currentMemorySpace.getName() + ":");
			this.m_currentMemorySpace.PrintValues();
			foreach (MemorySpace memorySpace in this.m_memorySpaceStack)
			{
				Console.WriteLine("\t" + memorySpace.getName() + ":");
				memorySpace.PrintValues();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002954 File Offset: 0x00000B54
		public void SwapStackTopValueTo(object pValue)
		{
			if (this.m_valueStack.Count > 0)
			{
				this.m_valueStack.Pop();
				this.m_valueStack.Push(pValue);
				return;
			}
			StackTrace stackTrace = new StackTrace();
			Console.WriteLine(string.Concat(new object[]
			{
				"SwapStackTopValueTo '",
				pValue,
				"' but stack is empty, stacktrace: ",
				stackTrace.ToString()
			}));
			throw new Error("Can't return value (stack empty)");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029D0 File Offset: 0x00000BD0
		public bool HasFunction(string functionName)
		{
			return this.m_globalScope.resolve(functionName) != null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029E4 File Offset: 0x00000BE4
		public InterpreterTwo.ProgramFunctionCallStatus SetProgramToExecuteFunction(string functionName, object[] args)
		{
			FunctionSymbol functionSymbol = (FunctionSymbol)this.m_globalScope.resolve(functionName);
			if (functionSymbol == null)
			{
				return InterpreterTwo.ProgramFunctionCallStatus.NO_FUNCTION;
			}
			if (this.IsFunctionExternal(functionName))
			{
				this.CallExternalFunction(functionName, args);
				return InterpreterTwo.ProgramFunctionCallStatus.EXTERNAL_FUNCTION;
			}
			AST_FunctionDefinitionNode ast_FunctionDefinitionNode = (AST_FunctionDefinitionNode)functionSymbol.getFunctionDefinitionNode();
			if (ast_FunctionDefinitionNode == null)
			{
				throw new Error(functionName + " has got no function definition node!");
			}
			List<AST> children = ast_FunctionDefinitionNode.getChild(2).getChildren();
			int count = children.Count;
			if (count != args.Length)
			{
				throw new Error(string.Concat(new object[] { "The function ", functionName, " takes ", count, " arguments, not ", args.Length }));
			}
			this.Reset();
			this.m_topLevelDepth = 1;
			this.m_currentScope = ast_FunctionDefinitionNode.getScope();
			this.m_currentScope.ClearMemorySpaces();
			string text = functionName + "_memorySpace" + InterpreterTwo.functionCounter++;
			this.PushNewScope(ast_FunctionDefinitionNode.getScope(), text, ast_FunctionDefinitionNode);
			for (int i = args.Length - 1; i >= 0; i--)
			{
				AST_VariableDeclaration ast_VariableDeclaration = children[i].getChild(0) as AST_VariableDeclaration;
				object obj = ReturnValueConversions.ChangeTypeBasedOnReturnValueType(args[i], ast_VariableDeclaration.Type);
				this.PushValue(obj);
			}
			return InterpreterTwo.ProgramFunctionCallStatus.NORMAL_FUNCTION;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B44 File Offset: 0x00000D44
		public object GetGlobalVariableValue(string pName)
		{
			return this.m_globalMemorySpace.getValue(pName);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B54 File Offset: 0x00000D54
		private bool ExecuteNextStatement()
		{
			Debug.Assert(this.m_currentMemorySpace != null);
			while (!this.m_currentMemorySpace.Next())
			{
				if (this.m_memorySpaceStack.Count == this.m_topLevelDepth)
				{
					return false;
				}
				this.PopCurrentScope();
			}
			try
			{
				this.SwitchOnStatement();
			}
			catch (Error error)
			{
				if (error.getLineNr() < 0)
				{
					this.m_errorHandler.errorOccured(new Error(error.getMessage(), error.getErrorType(), this.CurrentNode.getToken().LineNr, this.CurrentNode.getToken().LinePosition));
				}
				else
				{
					this.m_errorHandler.errorOccured(error);
				}
			}
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C2C File Offset: 0x00000E2C
		private void SwitchOnStatement()
		{
			this.CurrentNode.Executions++;
			switch (this.CurrentNode.getTokenType())
			{
			case Token.TokenType.NAME:
				this.ResolveVariableName();
				return;
			case Token.TokenType.ARRAY_LOOKUP:
				this.ArrayLookup();
				return;
			case Token.TokenType.OPERATOR:
				this.Operator();
				return;
			case Token.TokenType.NUMBER:
				this.PushValueFromToken();
				return;
			case Token.TokenType.QUOTED_STRING:
				this.PushValueFromToken();
				return;
			case Token.TokenType.BOOLEAN_VALUE:
				this.PushValueFromToken();
				return;
			case Token.TokenType.ARRAY:
				this.PushValueFromToken();
				return;
			case Token.TokenType.ARRAY_END_SIGNAL:
				this.ArrayEndSignal();
				return;
			case Token.TokenType.BUILT_IN_TYPE_NAME:
			case Token.TokenType.STATEMENT_LIST:
			case Token.TokenType.NODE_GROUP:
				return;
			case Token.TokenType.VAR_DECLARATION:
				this.VariableDeclaration();
				return;
			case Token.TokenType.FUNC_DECLARATION:
				throw new Exception("Can't happen");
			case Token.TokenType.PARAMETER:
				return;
			case Token.TokenType.FUNCTION_CALL:
				this.JumpToFunction();
				return;
			case Token.TokenType.ASSIGNMENT:
				this.AssignmentSignal();
				return;
			case Token.TokenType.ASSIGNMENT_TO_ARRAY:
				this.AssignmentToArrayElementSignal();
				return;
			case Token.TokenType.IF:
				this.EvaluateIf();
				return;
			case Token.TokenType.LOOP:
				this.Loop();
				return;
			case Token.TokenType.LOOP_BLOCK:
				this.LoopBlock();
				return;
			case Token.TokenType.GOTO_BEGINNING_OF_LOOP:
				this.GotoBeginningOfLoop();
				return;
			case Token.TokenType.BREAK:
				this.BreakStatement();
				return;
			case Token.TokenType.RETURN:
				this.ReturnSignal();
				return;
			case Token.TokenType.NOT:
				this.Not();
				return;
			}
			throw new Exception("Hasn't implemented support for token type " + this.m_currentMemorySpace.CurrentNode.getTokenType() + " yet!");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E0C File Offset: 0x0000100C
		private void EvaluateIf()
		{
			AST_IfNode ast_IfNode = this.CurrentNode as AST_IfNode;
			Debug.Assert(ast_IfNode != null);
			object obj = this.PopValue();
			Debug.Assert(obj != null);
			AST ast = null;
			if (obj.GetType() != typeof(bool) && obj.GetType() != typeof(float))
			{
				Token token = ast_IfNode.getToken();
				throw new Error(string.Concat(new object[]
				{
					"Can't use value ",
					obj,
					" of type ",
					ReturnValueConversions.PrettyObjectType(obj.GetType()),
					" in if-statement"
				}), Error.ErrorType.RUNTIME, token.LineNr, token.LinePosition);
			}
			if (this.ConvertToBool(obj))
			{
				ast = ast_IfNode.getChild(1);
			}
			else if (ast_IfNode.getChildren().Count == 3)
			{
				ast = ast_IfNode.getChild(2);
			}
			if (ast != null)
			{
				this.PushNewScope(ast_IfNode.getScope(), "IF_memorySpace" + InterpreterTwo.ifCounter++, ast);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002F2C File Offset: 0x0000112C
		private AST CurrentNode
		{
			get
			{
				Debug.Assert(this.m_currentMemorySpace != null);
				return this.m_currentMemorySpace.CurrentNode;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F4C File Offset: 0x0000114C
		private bool IsFunctionExternal(string pFunctionName)
		{
			return this.m_externalFunctionCreator.externalFunctions.ContainsKey(pFunctionName);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F60 File Offset: 0x00001160
		private void CallExternalFunction(string pFunctionName, object[] pParameters)
		{
			ExternalFunctionCreator.OnFunctionCall onFunctionCall = this.m_externalFunctionCreator.externalFunctions[pFunctionName];
			object obj = onFunctionCall(pParameters);
			if (!(obj is VoidType))
			{
				this.PushValue(obj);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F9C File Offset: 0x0000119C
		private void JumpToFunction()
		{
			AST_FunctionDefinitionNode functionDefinitionRef = (this.CurrentNode as AST_FunctionCall).FunctionDefinitionRef;
			string tokenString = functionDefinitionRef.getChild(1).getTokenString();
			List<AST> children = functionDefinitionRef.getChild(2).getChildren();
			int count = children.Count;
			object[] array = new object[count];
			for (int i = count - 1; i >= 0; i--)
			{
				AST ast = children[i];
				AST_VariableDeclaration ast_VariableDeclaration = ast.getChild(0) as AST_VariableDeclaration;
				array[i] = ReturnValueConversions.ChangeTypeBasedOnReturnValueType(this.PopValue(), ast_VariableDeclaration.Type);
			}
			if (this.IsFunctionExternal(tokenString))
			{
				this.CallExternalFunction(tokenString, array);
			}
			else
			{
				this.PushNewScope(functionDefinitionRef.getScope(), tokenString + "_memorySpace" + InterpreterTwo.functionCounter++, functionDefinitionRef);
				for (int j = count - 1; j >= 0; j--)
				{
					this.PushValue(array[j]);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003094 File Offset: 0x00001294
		private float ConvertToNumber(object o)
		{
			if (o.GetType() == typeof(float))
			{
				return (float)o;
			}
			if (o.GetType() == typeof(int))
			{
				return (float)((int)o);
			}
			if (o.GetType() == typeof(string))
			{
				float num = 0f;
				if (float.TryParse((string)o, out num))
				{
					return num;
				}
			}
			throw new Error(string.Concat(new object[]
			{
				"Can't convert value ",
				o,
				" of type ",
				ReturnValueConversions.PrettyObjectType(o.GetType()),
				" to number"
			}));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003144 File Offset: 0x00001344
		private bool ConvertToBool(object o)
		{
			if (o.GetType() == typeof(bool))
			{
				return (bool)o;
			}
			if (o.GetType() == typeof(float))
			{
				return (float)o != 0f;
			}
			if (o.GetType() == typeof(int))
			{
				return (int)o != 0;
			}
			throw new Error(string.Concat(new object[]
			{
				"Can't convert value ",
				o,
				" of type ",
				ReturnValueConversions.PrettyObjectType(o.GetType()),
				" to bool"
			}));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031FC File Offset: 0x000013FC
		private void Operator()
		{
			string tokenString = this.CurrentNode.getTokenString();
			if (tokenString != null)
			{
				if (InterpreterTwo.<>f__switch$map0 == null)
				{
					InterpreterTwo.<>f__switch$map0 = new Dictionary<string, int>(12)
					{
						{ "+", 0 },
						{ "-", 1 },
						{ "*", 2 },
						{ "/", 3 },
						{ "<", 4 },
						{ ">", 5 },
						{ ">=", 6 },
						{ "<=", 7 },
						{ "==", 8 },
						{ "!=", 9 },
						{ "&&", 10 },
						{ "||", 11 }
					};
				}
				int num;
				if (InterpreterTwo.<>f__switch$map0.TryGetValue(tokenString, out num))
				{
					object obj;
					switch (num)
					{
					case 0:
						obj = this.AddStuffTogetherHack();
						break;
					case 1:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 - num2;
						break;
					}
					case 2:
						obj = this.ConvertToNumber(this.PopValue()) * this.ConvertToNumber(this.PopValue());
						break;
					case 3:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 / num2;
						break;
					}
					case 4:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 < num2;
						break;
					}
					case 5:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 > num2;
						break;
					}
					case 6:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 >= num2;
						break;
					}
					case 7:
					{
						float num2 = this.ConvertToNumber(this.PopValue());
						float num3 = this.ConvertToNumber(this.PopValue());
						obj = num3 <= num2;
						break;
					}
					case 8:
						obj = this.equalityTest();
						break;
					case 9:
						obj = !this.ConvertToBool(this.equalityTest());
						break;
					case 10:
					{
						object obj2 = this.PopValue();
						bool flag = this.ConvertToBool(obj2);
						object obj3 = this.PopValue();
						bool flag2 = this.ConvertToBool(obj3);
						obj = flag && flag2;
						break;
					}
					case 11:
					{
						object obj4 = this.PopValue();
						bool flag3 = this.ConvertToBool(obj4);
						object obj5 = this.PopValue();
						bool flag4 = this.ConvertToBool(obj5);
						obj = flag3 || flag4;
						break;
					}
					default:
						goto IL_02DF;
					}
					this.PushValue(obj);
					return;
				}
			}
			IL_02DF:
			throw new Exception("Operator " + this.CurrentNode.getTokenString() + " is not implemented yet!");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003510 File Offset: 0x00001710
		private object equalityTest()
		{
			object obj = this.PopValue();
			object obj2 = this.PopValue();
			if (obj2 == obj)
			{
				return true;
			}
			if (obj2.GetType() == typeof(float) && obj2.GetType() == obj.GetType())
			{
				return (float)obj == (float)obj2;
			}
			if (obj2.GetType() == typeof(int) && obj2.GetType() == obj.GetType())
			{
				return (int)obj == (int)obj2;
			}
			if (obj2.GetType() == obj.GetType() && obj is IComparable && obj2 is IComparable)
			{
				bool flag = (obj as IComparable).CompareTo(obj2 as IComparable) == 0;
				return flag;
			}
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000035F8 File Offset: 0x000017F8
		private object AddStuffTogetherHack()
		{
			object obj = this.PopValue();
			object obj2 = this.PopValue();
			Type type = obj.GetType();
			Type type2 = obj2.GetType();
			if (type == typeof(float) && type2 == typeof(float))
			{
				return (float)obj + (float)obj2;
			}
			if (type == typeof(int) && type2 == typeof(int))
			{
				return (float)((int)obj + (int)obj2);
			}
			if (type == typeof(string) || type2 == typeof(string))
			{
				return ReturnValueConversions.PrettyStringRepresenation(obj2) + ReturnValueConversions.PrettyStringRepresenation(obj);
			}
			if (type == typeof(object[]) && type2 == typeof(object[]))
			{
				throw new Error("Primitive array concatenation is temporarily disabled.");
			}
			if (type == typeof(SortedDictionary<KeyWrapper, object>) && type2 == typeof(SortedDictionary<KeyWrapper, object>))
			{
				SortedDictionary<KeyWrapper, object> sortedDictionary = obj2 as SortedDictionary<KeyWrapper, object>;
				SortedDictionary<KeyWrapper, object> sortedDictionary2 = obj as SortedDictionary<KeyWrapper, object>;
				SortedDictionary<KeyWrapper, object> sortedDictionary3 = new SortedDictionary<KeyWrapper, object>();
				for (int i = 0; i < sortedDictionary.Count; i++)
				{
					sortedDictionary3.Add(new KeyWrapper(i), sortedDictionary[new KeyWrapper(i)]);
				}
				for (int j = 0; j < sortedDictionary2.Count; j++)
				{
					sortedDictionary3.Add(new KeyWrapper(j + sortedDictionary.Count), sortedDictionary2[new KeyWrapper(j)]);
				}
				Console.WriteLine("Created new array by concatenation: " + ReturnValueConversions.PrettyStringRepresenation(sortedDictionary3));
				return sortedDictionary3;
			}
			throw new Error(string.Concat(new object[] { "Can't add ", obj2, " to ", obj }));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000037EC File Offset: 0x000019EC
		private void ResolveVariableName()
		{
			object value = this.m_currentScope.getValue(this.CurrentNode.getTokenString());
			this.PushValue(value);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003818 File Offset: 0x00001A18
		private void Not()
		{
			object obj = this.PopValue();
			bool flag = this.ConvertToBool(obj);
			this.PushValue(!flag);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003844 File Offset: 0x00001A44
		private void ArrayLookup()
		{
			object obj = this.PopValue();
			object value = this.m_currentScope.getValue(this.CurrentNode.getTokenString());
			object obj2 = null;
			if (value is Range)
			{
				if (obj.GetType() != typeof(float))
				{
					throw new Error("Can't look up " + obj.ToString() + " in the range " + value.ToString());
				}
				Range range = (Range)value;
				float num = range.step * (float)((int)((float)obj));
				float num2 = range.start + num;
				float num3;
				float num4;
				if (range.step > 0f)
				{
					num3 = range.start;
					num4 = range.end;
				}
				else
				{
					num3 = range.end;
					num4 = range.start;
				}
				if (num2 < num3)
				{
					throw new Error("Index " + obj.ToString() + " is outside the range " + value.ToString());
				}
				if (num2 > num4)
				{
					throw new Error("Index " + obj.ToString() + " is outside the range " + value.ToString());
				}
				obj2 = num2;
			}
			else if (value.GetType() == typeof(SortedDictionary<KeyWrapper, object>))
			{
				SortedDictionary<KeyWrapper, object> sortedDictionary = value as SortedDictionary<KeyWrapper, object>;
				if (!sortedDictionary.TryGetValue(new KeyWrapper(obj), out obj2))
				{
					throw new Error(string.Concat(new object[]
					{
						"Can't find the index '",
						obj,
						"' (",
						ReturnValueConversions.PrettyObjectType(obj.GetType()),
						") in the array '",
						this.CurrentNode.getTokenString(),
						"'"
					}), Error.ErrorType.RUNTIME, this.CurrentNode.getToken().LineNr, this.CurrentNode.getToken().LinePosition);
				}
			}
			else
			{
				if (value.GetType() == typeof(object[]))
				{
					throw new Error("Illegal object[] array: " + ReturnValueConversions.PrettyStringRepresenation(value));
				}
				if (value.GetType() != typeof(string))
				{
					throw new Error("Can't convert " + value.ToString() + " to an array (for lookup)");
				}
				int num5;
				if (obj.GetType() == typeof(float))
				{
					num5 = (int)((float)obj);
				}
				else
				{
					if (obj.GetType() != typeof(int))
					{
						throw new Error("Must use nr when looking up index in string");
					}
					num5 = (int)obj;
				}
				string text = (string)value;
				if (num5 < 0 || num5 >= text.Length)
				{
					throw new Error(string.Concat(new object[]
					{
						"The index '",
						num5,
						"' (",
						obj.GetType(),
						") is outside the bounds of the string '",
						this.CurrentNode.getTokenString(),
						"'"
					}), Error.ErrorType.RUNTIME, this.CurrentNode.getToken().LineNr, this.CurrentNode.getToken().LinePosition);
				}
				obj2 = text[num5].ToString();
			}
			this.PushValue(obj2);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003B98 File Offset: 0x00001D98
		private void PushValueFromToken()
		{
			TokenWithValue tokenWithValue = this.CurrentNode.getToken() as TokenWithValue;
			if (tokenWithValue == null)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Can't convert current node to TokenWithValue: ",
					this.CurrentNode,
					", it's of type ",
					this.CurrentNode.getTokenType()
				}));
			}
			this.PushValue(tokenWithValue.getValue());
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003C08 File Offset: 0x00001E08
		private void VariableDeclaration()
		{
			ReturnValueType type = (this.CurrentNode as AST_VariableDeclaration).Type;
			string name = (this.CurrentNode as AST_VariableDeclaration).Name;
			object obj = this.DefaultValue(type);
			this.m_currentScope.setValue(name, obj);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003C4C File Offset: 0x00001E4C
		private object DefaultValue(ReturnValueType type)
		{
			if (type == ReturnValueType.STRING)
			{
				return "";
			}
			if (type == ReturnValueType.BOOL)
			{
				return false;
			}
			if (type == ReturnValueType.NUMBER)
			{
				return 0f;
			}
			if (type == ReturnValueType.RANGE)
			{
				return new Range(0, 0, 0);
			}
			if (type == ReturnValueType.ARRAY)
			{
				return new SortedDictionary<KeyWrapper, object>();
			}
			if (type == ReturnValueType.VOID)
			{
				return VoidType.voidType;
			}
			if (type == ReturnValueType.UNKNOWN_TYPE)
			{
				return UnknownType.unknownType;
			}
			throw new Error("No default value for " + type);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003CE0 File Offset: 0x00001EE0
		private object ConvertToType(object valueToConvert, Type type)
		{
			ReturnValueType returnValueType = ReturnValueConversions.SystemTypeToReturnValueType(type);
			return ReturnValueConversions.ChangeTypeBasedOnReturnValueType(valueToConvert, returnValueType);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003D00 File Offset: 0x00001F00
		private void AssignmentSignal()
		{
			string variableName = (this.CurrentNode as AST_Assignment).VariableName;
			object obj = this.PopValue();
			Type type = this.m_currentScope.getValue(variableName).GetType();
			object obj2 = this.ConvertToType(obj, type);
			this.m_currentScope.setValue(variableName, obj2);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003D50 File Offset: 0x00001F50
		private void AssignmentToArrayElementSignal()
		{
			string variableName = (this.CurrentNode as AST_Assignment).VariableName;
			object obj = this.PopValue();
			object obj2 = this.PopValue();
			object value = this.m_currentScope.getValue(variableName);
			if (value.GetType() == typeof(SortedDictionary<KeyWrapper, object>))
			{
				SortedDictionary<KeyWrapper, object> sortedDictionary = value as SortedDictionary<KeyWrapper, object>;
				if (sortedDictionary.ContainsKey(new KeyWrapper(obj2)))
				{
					sortedDictionary[new KeyWrapper(obj2)] = obj;
				}
				else
				{
					sortedDictionary.Add(new KeyWrapper(obj2), obj);
				}
				return;
			}
			Token token = (this.CurrentNode as AST_Assignment).getToken();
			throw new Error("Can't assign to the variable '" + variableName + "' since it's of the type " + ReturnValueConversions.PrettyObjectType(value.GetType()), Error.ErrorType.RUNTIME, token.LineNr, token.LinePosition);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003E20 File Offset: 0x00002020
		private void ArrayEndSignal()
		{
			AST_ArrayEndSignal ast_ArrayEndSignal = this.CurrentNode as AST_ArrayEndSignal;
			SortedDictionary<KeyWrapper, object> sortedDictionary = new SortedDictionary<KeyWrapper, object>();
			object[] array = new object[ast_ArrayEndSignal.ArraySize];
			for (int i = 0; i < ast_ArrayEndSignal.ArraySize; i++)
			{
				array[i] = this.PopValue();
			}
			for (int j = ast_ArrayEndSignal.ArraySize - 1; j >= 0; j--)
			{
				sortedDictionary.Add(new KeyWrapper(ast_ArrayEndSignal.ArraySize - j - 1), array[j]);
			}
			this.PushValue(sortedDictionary);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003EB0 File Offset: 0x000020B0
		private void ReturnSignal()
		{
			while (this.m_currentScope.scopeType != Scope.ScopeType.FUNCTION_SCOPE && this.m_currentScope.scopeType != Scope.ScopeType.MAIN_SCOPE)
			{
				this.PopCurrentScope();
			}
			this.m_currentMemorySpace.MoveToEnd();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003EEC File Offset: 0x000020EC
		private void LoopBlock()
		{
			AST_LoopBlockNode ast_LoopBlockNode = this.CurrentNode as AST_LoopBlockNode;
			Debug.Assert(ast_LoopBlockNode != null);
			this.PushNewScope(ast_LoopBlockNode.getScope(), "LoopBlock_memorySpace" + InterpreterTwo.loopBlockCounter++, ast_LoopBlockNode.getChild(0));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003F40 File Offset: 0x00002140
		private void Loop()
		{
			AST_LoopNode ast_LoopNode = this.CurrentNode as AST_LoopNode;
			Debug.Assert(ast_LoopNode != null);
			this.PushNewScope(ast_LoopNode.getScope(), "Loop_memorySpace_" + InterpreterTwo.loopCounter++, ast_LoopNode.getChild(0));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003F94 File Offset: 0x00002194
		private void BreakStatement()
		{
			while (this.m_currentScope.scopeType != Scope.ScopeType.LOOP_SCOPE && this.m_currentScope.scopeType != Scope.ScopeType.MAIN_SCOPE)
			{
				this.PopCurrentScope();
			}
			this.m_currentMemorySpace.MoveToEnd();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003FD0 File Offset: 0x000021D0
		private void GotoBeginningOfLoop()
		{
			this.PopCurrentScope();
			this.m_currentMemorySpace.Jump(-1);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003FE4 File Offset: 0x000021E4
		public int stackSize
		{
			get
			{
				return this.m_memorySpaceStack.Count;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003FF4 File Offset: 0x000021F4
		public string DumpStack()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			foreach (MemorySpace memorySpace in this.m_memorySpaceStack)
			{
				stringBuilder.Append(" " + memorySpace.getName());
			}
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004090 File Offset: 0x00002290
		public object PopValue()
		{
			if (this.m_valueStack.Count == 0)
			{
				throw new Error("Can't access value (have you forgotten to return a value from a function?)");
			}
			return this.m_valueStack.Pop();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000040C8 File Offset: 0x000022C8
		public void PushValue(object value)
		{
			this.m_valueStack.Push(value);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000040D8 File Offset: 0x000022D8
		public bool ValueStackIsEmpty()
		{
			return this.m_valueStack.Count == 0;
		}

		// Token: 0x04000008 RID: 8
		public static int nrOfInterpreters = 0;

		// Token: 0x04000009 RID: 9
		public static int nrOfScopes;

		// Token: 0x0400000A RID: 10
		private static int ifCounter = 0;

		// Token: 0x0400000B RID: 11
		private static int functionCounter = 0;

		// Token: 0x0400000C RID: 12
		private static int loopBlockCounter = 0;

		// Token: 0x0400000D RID: 13
		private static int loopCounter = 0;

		// Token: 0x0400000E RID: 14
		private AST m_ast;

		// Token: 0x0400000F RID: 15
		private ExternalFunctionCreator m_externalFunctionCreator;

		// Token: 0x04000010 RID: 16
		private ErrorHandler m_errorHandler;

		// Token: 0x04000011 RID: 17
		private Scope m_globalScope;

		// Token: 0x04000012 RID: 18
		private Scope m_currentScope;

		// Token: 0x04000013 RID: 19
		private MemorySpace m_globalMemorySpace;

		// Token: 0x04000014 RID: 20
		private MemorySpace m_currentMemorySpace;

		// Token: 0x04000015 RID: 21
		private Stack<MemorySpace> m_memorySpaceStack = new Stack<MemorySpace>();

		// Token: 0x04000016 RID: 22
		private Stack<object> m_valueStack = new Stack<object>();

		// Token: 0x04000017 RID: 23
		private MemorySpaceNodeListCache m_memorySpaceNodeListCache = new MemorySpaceNodeListCache();

		// Token: 0x04000018 RID: 24
		private int m_topLevelDepth = 0;

		// Token: 0x02000009 RID: 9
		public enum Status
		{
			// Token: 0x0400001B RID: 27
			OK,
			// Token: 0x0400001C RID: 28
			ERROR,
			// Token: 0x0400001D RID: 29
			FINISHED
		}

		// Token: 0x0200000A RID: 10
		public enum ProgramFunctionCallStatus
		{
			// Token: 0x0400001F RID: 31
			NO_FUNCTION,
			// Token: 0x04000020 RID: 32
			EXTERNAL_FUNCTION,
			// Token: 0x04000021 RID: 33
			NORMAL_FUNCTION
		}
	}
}
