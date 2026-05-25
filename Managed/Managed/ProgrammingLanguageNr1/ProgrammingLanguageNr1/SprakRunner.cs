using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000023 RID: 35
	public class SprakRunner
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00008FE4 File Offset: 0x000071E4
		public SprakRunner(TextReader stream, FunctionDefinition[] functionDefinitions)
		{
			this.construct(stream, functionDefinitions, null);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008FF8 File Offset: 0x000071F8
		public SprakRunner(TextReader stream, FunctionDefinition[] functionDefinitions, VariableDefinition[] variableDefinitions)
		{
			this.construct(stream, functionDefinitions, variableDefinitions);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000901C File Offset: 0x0000721C
		private static List<FunctionDefinition> __CreateBuiltInFunctionDefinitions()
		{
			List<FunctionDefinition> list = new List<FunctionDefinition>();
			FunctionDocumentation functionDocumentation = new FunctionDocumentation("Count the number of elements in an array", new string[] { "The array" });
			list.Add(new FunctionDefinition("number", "Count", new string[] { "array" }, new string[] { "a" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_count), functionDocumentation));
			FunctionDocumentation functionDocumentation2 = new FunctionDocumentation("Create a range of numbers from 'min' to (and including) 'max'", new string[] { "The start value of the range", "The end value of the range" });
			list.Add(new FunctionDefinition("number", "Range", new string[] { "number", "number" }, new string[] { "min", "max" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_range), functionDocumentation2));
			FunctionDocumentation functionDocumentation3 = new FunctionDocumentation("Create a new array that contains the indexes of another array", new string[] { "The array with indexes" });
			list.Add(new FunctionDefinition("array", "GetIndexes", new string[] { "array" }, new string[] { "a" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_createArrayOrRangeOfIndexes), functionDocumentation3));
			FunctionDocumentation functionDocumentation4 = new FunctionDocumentation("Remove an element from an array", new string[] { "The array to remove an element from", "The index in the array to remove" });
			list.Add(new FunctionDefinition("void", "Remove", new string[] { "array", "number" }, new string[] { "array", "position" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_removeElement), functionDocumentation4));
			FunctionDocumentation functionDocumentation5 = new FunctionDocumentation("Remove an element from an array", new string[] { "The array to remove an element from", "The index in the array to remove" });
			list.Add(new FunctionDefinition("void", "RemoveAll", new string[] { "array" }, new string[] { "array" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_removeAllElements), functionDocumentation5));
			FunctionDocumentation functionDocumentation6 = new FunctionDocumentation("Check if an index is in the array", new string[] { "The array to check in", "The index to check for in the array" });
			list.Add(new FunctionDefinition("bool", "HasIndex", new string[] { "array", "var" }, new string[] { "array", "key" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_hasKey), functionDocumentation6));
			FunctionDocumentation functionDocumentation7 = new FunctionDocumentation("Add an element to the end of an array", new string[] { "The array to add an element to", "The element to add" });
			list.Add(new FunctionDefinition("void", "Append", new string[] { "array", "var" }, new string[] { "array", "elem" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_append), functionDocumentation7));
			FunctionDocumentation functionDocumentation8 = new FunctionDocumentation("Get the type of something (returns a string)", new string[] { "The value to get the type of" });
			list.Add(new FunctionDefinition("string", "Type", new string[] { "var" }, new string[] { "value" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_type), functionDocumentation8));
			FunctionDocumentation functionDocumentation9 = new FunctionDocumentation("Round a number to the nearest integer", new string[] { "The number to round" });
			list.Add(new FunctionDefinition("number", "Round", new string[] { "var" }, new string[] { "x" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_round), functionDocumentation9));
			FunctionDocumentation functionDocumentation10 = new FunctionDocumentation("Remove the decimals of a float", new string[] { "The number to convert to an integer" });
			list.Add(new FunctionDefinition("number", "Int", new string[] { "var" }, new string[] { "x" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_int), functionDocumentation10));
			FunctionDocumentation functionDocumentation11 = new FunctionDocumentation("Get the remainder of x / y", new string[] { "x", "y" });
			list.Add(new FunctionDefinition("number", "Mod", new string[] { "var", "var" }, new string[] { "x", "y" }, new ExternalFunctionCreator.OnFunctionCall(SprakRunner.API_mod), functionDocumentation11));
			return list;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009480 File Offset: 0x00007680
		public static List<FunctionDefinition> builtInFunctions
		{
			get
			{
				if (SprakRunner.__builtInFunctions == null)
				{
					SprakRunner.__builtInFunctions = SprakRunner.__CreateBuiltInFunctionDefinitions();
				}
				return SprakRunner.__builtInFunctions;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000949C File Offset: 0x0000769C
		~SprakRunner()
		{
			SprakRunner.nrOfSprakRunnersInMemory--;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000094E0 File Offset: 0x000076E0
		private void construct(TextReader stream, FunctionDefinition[] functionDefinitions, VariableDefinition[] variableDefinitions)
		{
			SprakRunner.nrOfSprakRunnersInMemory++;
			Debug.Assert(stream != null);
			Debug.Assert(functionDefinitions != null);
			this.m_compileTimeErrorHandler = new ErrorHandler();
			this.m_runtimeErrorHandler = new ErrorHandler();
			this.m_tokens = this.Tokenize(stream);
			try
			{
				this.m_ast = this.Parse(this.m_tokens);
				if (this.m_compileTimeErrorHandler.getErrors().Count > 0)
				{
					this.m_compileTimeErrorHandler.printErrorsToConsole();
				}
				else
				{
					this.AddLocalVariables(this.m_ast, variableDefinitions);
					ExternalFunctionCreator externalFunctionCreator = this.AddExternalFunctions(functionDefinitions, this.m_ast);
					Scope scope = this.CreateScopeTree(this.m_ast);
					if (this.m_compileTimeErrorHandler.getErrors().Count > 0)
					{
						this.m_compileTimeErrorHandler.printErrorsToConsole();
					}
					else
					{
						this.m_interpreter = new InterpreterTwo(this.m_ast, scope, this.m_runtimeErrorHandler, externalFunctionCreator);
						this.m_started = false;
					}
				}
			}
			catch (Error error)
			{
				this.m_compileTimeErrorHandler.errorOccured(error);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009614 File Offset: 0x00007814
		private List<Token> Tokenize(TextReader stream)
		{
			Tokenizer tokenizer = new Tokenizer(this.m_compileTimeErrorHandler, true);
			return tokenizer.process(stream);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009638 File Offset: 0x00007838
		public void SwapStackTopValueTo(object pValue)
		{
			if (this.m_interpreter == null)
			{
				return;
			}
			if (pValue == null)
			{
				return;
			}
			this.m_interpreter.SwapStackTopValueTo(pValue);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000965C File Offset: 0x0000785C
		private AST Parse(List<Token> tokens)
		{
			Parser parser = new Parser(tokens, this.m_compileTimeErrorHandler);
			parser.process();
			return parser.getAST();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009684 File Offset: 0x00007884
		private void AddLocalVariables(AST ast, VariableDefinition[] variableDefinitions)
		{
			AST child = ast.getChild(0).getChild(0);
			if (variableDefinitions == null)
			{
				return;
			}
			foreach (VariableDefinition variableDefinition in variableDefinitions)
			{
				Token token = new Token(Token.TokenType.VAR_DECLARATION, "<VAR_DECL>", ast.getToken().LineNr, ast.getToken().LinePosition);
				AST_VariableDeclaration ast_VariableDeclaration = new AST_VariableDeclaration(token, ReturnValueConversions.SystemTypeToReturnValueType(variableDefinition.initValue.GetType()), variableDefinition.variableName);
				if (variableDefinition.initValue != null)
				{
					AST ast2 = this.CreateAssignmentTreeFromInitValue(variableDefinition.variableName, variableDefinition.initValue);
					AST ast3 = new AST(new Token(Token.TokenType.STATEMENT_LIST, "<DECLARATION_AND_ASSIGNMENT>", ast_VariableDeclaration.getToken().LineNr, ast_VariableDeclaration.getToken().LinePosition));
					ast3.addChild(ast_VariableDeclaration);
					ast3.addChild(ast2);
					child.addChild(ast3);
				}
				else
				{
					child.addChild(ast_VariableDeclaration);
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009774 File Offset: 0x00007974
		private AST CreateAssignmentTreeFromInitValue(string pVariableName, object pInitValue)
		{
			Token.TokenType tokenType;
			switch (ReturnValueConversions.SystemTypeToReturnValueType(pInitValue.GetType()))
			{
			case ReturnValueType.NUMBER:
				tokenType = Token.TokenType.NUMBER;
				break;
			case ReturnValueType.VOID:
				throw new Error("Can't assign void to variable");
			case ReturnValueType.STRING:
				tokenType = Token.TokenType.QUOTED_STRING;
				break;
			case ReturnValueType.BOOL:
				tokenType = Token.TokenType.BOOLEAN_VALUE;
				break;
			case ReturnValueType.ARRAY:
				tokenType = Token.TokenType.ARRAY;
				break;
			default:
				throw new Exception("Forgot to implement support for a type?");
			}
			Token token = new TokenWithValue(tokenType, pInitValue.ToString(), pInitValue);
			AST ast = new AST_Assignment(new Token(Token.TokenType.ASSIGNMENT, "="), pVariableName);
			ast.addChild(token);
			return ast;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000980C File Offset: 0x00007A0C
		public void ChangeGlobalVariableInitValue(string pName, object pobject)
		{
			bool flag = false;
			AST child = this.m_ast.getChild(0);
			AST child2 = child.getChild(0);
			if (child2.getTokenString() != "<GLOBAL_VARIABLE_DEFINITIONS_LIST>")
			{
				throw new Exception("Wrong node, " + child2.getTokenString());
			}
			if (child2.getChildren() != null && child2.getChildren().Count > 0)
			{
				foreach (AST ast in child2.getChildren())
				{
					AST_Assignment ast_Assignment = (AST_Assignment)ast.getChild(1);
					if (ast_Assignment.VariableName == pName)
					{
						ast.removeChild(1);
						ast.addChild(this.CreateAssignmentTreeFromInitValue(pName, pobject));
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				throw new Exception("Couldn't find and change the variable " + pName);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009924 File Offset: 0x00007B24
		public object GetGlobalVariableValue(string pName)
		{
			return this.m_interpreter.GetGlobalVariableValue(pName);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009934 File Offset: 0x00007B34
		private ExternalFunctionCreator AddExternalFunctions(FunctionDefinition[] functionDefinitions, AST ast)
		{
			List<FunctionDefinition> list = new List<FunctionDefinition>();
			list.AddRange(SprakRunner.builtInFunctions);
			list.AddRange(functionDefinitions);
			FunctionDocumentation functionDocumentation = new FunctionDocumentation("Check if a function exists on the object", new string[] { "The name of the function" });
			list.Add(new FunctionDefinition("bool", "HasFunction", new string[] { "string" }, new string[] { "functionName" }, new ExternalFunctionCreator.OnFunctionCall(this.API_hasFunction), functionDocumentation));
			ExternalFunctionCreator externalFunctionCreator = new ExternalFunctionCreator(list.ToArray());
			AST child = ast.getChild(1);
			foreach (AST ast2 in externalFunctionCreator.FunctionASTs)
			{
				child.addChild(ast2);
			}
			return externalFunctionCreator;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009A24 File Offset: 0x00007C24
		public object RunFunction(string functionName, object[] args)
		{
			this.m_interpreter.SetProgramToExecuteFunction(functionName, args);
			this.run();
			return this.GetFinalReturnValue();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009A40 File Offset: 0x00007C40
		public object GetFinalReturnValue()
		{
			if (!this.m_interpreter.ValueStackIsEmpty())
			{
				return this.m_interpreter.PopValue();
			}
			return VoidType.voidType;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009A78 File Offset: 0x00007C78
		private static object API_type(object[] args)
		{
			return ReturnValueConversions.PrettyObjectType(args[0].GetType());
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009A88 File Offset: 0x00007C88
		private static object API_createArrayOrRangeOfIndexes(object[] args)
		{
			if (args[0].GetType() == typeof(SortedDictionary<KeyWrapper, object>))
			{
				SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
				SortedDictionary<KeyWrapper, object> sortedDictionary2 = new SortedDictionary<KeyWrapper, object>();
				int num = 0;
				foreach (KeyWrapper keyWrapper in sortedDictionary.Keys)
				{
					sortedDictionary2.Add(new KeyWrapper((float)num), keyWrapper.value);
					num++;
				}
				return sortedDictionary2;
			}
			if (args[0].GetType() == typeof(object[]))
			{
				object[] array = (object[])args[0];
				SortedDictionary<KeyWrapper, object> sortedDictionary3 = new SortedDictionary<KeyWrapper, object>();
				for (int i = 0; i < array.Length; i++)
				{
					object obj = array[i];
					sortedDictionary3.Add(new KeyWrapper((float)i), obj);
				}
				return sortedDictionary3;
			}
			if (args[0].GetType() == typeof(Range))
			{
				Range range = (Range)args[0];
				Range range2 = new Range(0, Math.Abs((int)(range.end - range.start)) + 1, 1);
				return range2;
			}
			if (args[0].GetType() == typeof(string))
			{
				string text = (string)args[0];
				Range range3 = new Range(0, text.Length, 1);
				return range3;
			}
			throw new Error("Can't convert " + args[0].ToString() + " to an array in GetIndexes()");
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009C30 File Offset: 0x00007E30
		private static object API_hasKey(object[] args)
		{
			SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
			object obj = args[1];
			return sortedDictionary.ContainsKey(new KeyWrapper(obj));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009C5C File Offset: 0x00007E5C
		private static object API_removeElement(object[] args)
		{
			SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
			object obj = args[1];
			if (sortedDictionary.ContainsKey(new KeyWrapper(obj)))
			{
				sortedDictionary.Remove(new KeyWrapper(obj));
				return new object();
			}
			throw new Error("Can't remove item with key " + obj + " from array");
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009CB0 File Offset: 0x00007EB0
		private static object API_removeAllElements(object[] args)
		{
			SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
			sortedDictionary.Clear();
			return new object();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009CD4 File Offset: 0x00007ED4
		private static object API_append(object[] args)
		{
			SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
			object obj = args[1];
			int num = -1;
			foreach (KeyWrapper keyWrapper in sortedDictionary.Keys)
			{
				object value = keyWrapper.value;
				if (value.GetType() == typeof(float) && (float)num < (float)value)
				{
					num = (int)((float)value);
				}
			}
			sortedDictionary.Add(new KeyWrapper((float)num + 1f), obj);
			return VoidType.voidType;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009DA0 File Offset: 0x00007FA0
		private static object API_count(object[] args)
		{
			if (args[0].GetType() == typeof(SortedDictionary<KeyWrapper, object>))
			{
				SortedDictionary<KeyWrapper, object> sortedDictionary = args[0] as SortedDictionary<KeyWrapper, object>;
				return (float)sortedDictionary.Count;
			}
			if (args[0].GetType() == typeof(object[]))
			{
				return (float)((object[])args[0]).Length;
			}
			if (args[0].GetType() == typeof(Range))
			{
				Range range = (Range)args[0];
				float num = range.end - range.start;
				return num;
			}
			if (args[0].GetType() == typeof(string))
			{
				return (float)((string)args[0]).Length;
			}
			throw new Error("Can't convert " + args[0].ToString() + " to an array in Count()");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009E80 File Offset: 0x00008080
		private static object API_allocate(object[] args)
		{
			int num = (int)((float)args[0]);
			SortedDictionary<KeyWrapper, object> sortedDictionary = new SortedDictionary<KeyWrapper, object>();
			for (int i = 0; i < num; i++)
			{
				sortedDictionary.Add(new KeyWrapper(i), VoidType.voidType);
			}
			return sortedDictionary;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009ECC File Offset: 0x000080CC
		private static object API_range(object[] args)
		{
			int num = (int)((float)args[0]);
			int num2 = (int)((float)args[1]);
			if (Math.Abs(num - num2) > 50)
			{
				int num3 = ((num >= num2) ? (-1) : 1);
				Range range = new Range(num, num2, num3);
				return range;
			}
			SortedDictionary<KeyWrapper, object> sortedDictionary = new SortedDictionary<KeyWrapper, object>();
			int num4;
			if (num < num2)
			{
				num4 = 1;
				num2++;
			}
			else
			{
				num4 = -1;
				num2--;
			}
			int num5 = 0;
			for (int num6 = num; num6 != num2; num6 += num4)
			{
				sortedDictionary[new KeyWrapper((float)num5)] = (float)num6;
				num5++;
			}
			return sortedDictionary;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009F80 File Offset: 0x00008180
		private static object API_toArray(object[] args)
		{
			throw new Error("Conversion not implemented yet.");
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009F8C File Offset: 0x0000818C
		private static object API_toNumber(object[] args)
		{
			throw new Error("Conversion not implemented yet.");
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009F98 File Offset: 0x00008198
		private static object API_toString(object[] args)
		{
			throw new Error("Conversion not implemented yet.");
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009FA4 File Offset: 0x000081A4
		private static object API_toBool(object[] args)
		{
			throw new Error("Conversion not implemented yet.");
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009FB0 File Offset: 0x000081B0
		private static object API_round(object[] args)
		{
			return (float)Math.Round((double)((float)args[0]));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009FC8 File Offset: 0x000081C8
		private static object API_int(object[] args)
		{
			return (float)((int)((float)args[0]));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009FDC File Offset: 0x000081DC
		private static object API_mod(object[] args)
		{
			return (float)((int)((float)args[0])) % (float)((int)((float)args[1]));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009FF8 File Offset: 0x000081F8
		private object API_hasFunction(object[] args)
		{
			return this.HasFunction(args[0] as string);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A010 File Offset: 0x00008210
		private Scope CreateScopeTree(AST ast)
		{
			ScopeBuilder scopeBuilder = new ScopeBuilder(ast, this.m_compileTimeErrorHandler);
			scopeBuilder.process();
			return scopeBuilder.getGlobalScope();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A038 File Offset: 0x00008238
		private void PaintAST(AST ast)
		{
			ASTPainter astpainter = new ASTPainter();
			astpainter.PaintAST(ast);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A054 File Offset: 0x00008254
		private void PrintTokens()
		{
			Console.WriteLine("TOKENS");
			Console.WriteLine("======");
			foreach (Token token in this.m_tokens)
			{
				Console.WriteLine(" " + token.ToString());
			}
			Console.WriteLine("======");
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public void run()
		{
			this.run(100000);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public void run(int pMaxNrOfExecutions)
		{
			Debug.Assert(pMaxNrOfExecutions < int.MaxValue);
			if (this.m_compileTimeErrorHandler.getErrors().Count == 0)
			{
				int num = 0;
				int num2 = 0;
				foreach (InterpreterTwo.Status status in this.m_interpreter)
				{
					num2 = ((this.m_interpreter.stackSize <= num2) ? num2 : this.m_interpreter.stackSize);
					num++;
					if (num >= pMaxNrOfExecutions)
					{
						Console.WriteLine("\nHit maximum execution count limit!");
						break;
					}
					if (this.m_runtimeErrorHandler.getErrors().Count > 0)
					{
						Console.WriteLine("\nRuntime error occured:");
						this.m_runtimeErrorHandler.printErrorsToConsole();
						Console.WriteLine("Stack: " + this.m_interpreter.DumpStack());
						break;
					}
				}
				Console.WriteLine("\nCompleted " + num + " executions");
				Console.WriteLine("Maximum stacksize was " + num2 + "\n");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000A23C File Offset: 0x0000843C
		public InterpreterTwo interpreter
		{
			get
			{
				return this.m_interpreter;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A244 File Offset: 0x00008444
		public void HardReset()
		{
			this.m_ast = null;
			this.m_interpreter = null;
			this.m_programIterator = null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A25C File Offset: 0x0000845C
		public void Reset()
		{
			if (this.m_programIterator != null)
			{
				this.m_programIterator.Dispose();
			}
			this.m_programIterator = null;
			this.m_ast.ClearMemorySpaces();
			this.m_started = false;
			if (this.m_interpreter != null)
			{
				this.m_interpreter.Reset();
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public InterpreterTwo.ProgramFunctionCallStatus ResetAtFunction(string functionName, object[] args)
		{
			if (this.m_interpreter == null)
			{
				Console.WriteLine("Interpreter is null when resetting at function " + functionName);
				return InterpreterTwo.ProgramFunctionCallStatus.NO_FUNCTION;
			}
			this.Reset();
			return this.m_interpreter.SetProgramToExecuteFunction(functionName, args);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public bool HasFunction(string functionName)
		{
			return this.m_interpreter != null && this.m_interpreter.HasFunction(functionName);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A30C File Offset: 0x0000850C
		public bool Start()
		{
			if (this.m_compileTimeErrorHandler.getErrors().Count != 0)
			{
				return this.m_started = false;
			}
			if (this.m_interpreter == null)
			{
				Console.WriteLine("m_interpreter is null");
				return false;
			}
			this.m_programIterator = this.m_interpreter.GetEnumerator();
			return this.m_started = true;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A36C File Offset: 0x0000856C
		public InterpreterTwo.Status Step()
		{
			if (this.m_compileTimeErrorHandler.getErrors().Count != 0 && this.m_runtimeErrorHandler.getErrors().Count != 0)
			{
				Console.WriteLine("Can't continue to run program since it contains errors!");
				return InterpreterTwo.Status.ERROR;
			}
			InterpreterTwo.Status status;
			try
			{
				if (this.m_programIterator.MoveNext())
				{
					if (this.m_programIterator == null)
					{
						Console.WriteLine("m_programIterator is null (happens when code is reset while running?)");
						status = InterpreterTwo.Status.FINISHED;
					}
					else
					{
						status = this.m_programIterator.Current;
					}
				}
				else
				{
					status = InterpreterTwo.Status.FINISHED;
				}
			}
			catch (Error error)
			{
				Console.Write("Caught sprak error in SprakRunner Step(): " + error);
				this.m_runtimeErrorHandler.errorOccured(error);
				status = InterpreterTwo.Status.ERROR;
			}
			return status;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000A440 File Offset: 0x00008640
		public bool isStarted
		{
			get
			{
				return this.m_started;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000A448 File Offset: 0x00008648
		public List<Token> Tokens
		{
			get
			{
				return this.m_tokens;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A450 File Offset: 0x00008650
		public void printTree(bool printExecutionCounters)
		{
			if (this.m_compileTimeErrorHandler.getErrors().Count == 0 && this.m_runtimeErrorHandler.getErrors().Count == 0)
			{
				new ASTPainter
				{
					PrintExecutions = printExecutionCounters
				}.PaintAST(this.m_ast);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A4A0 File Offset: 0x000086A0
		public Dictionary<string, ProfileData> GetProfileData()
		{
			return new Dictionary<string, ProfileData>();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public ErrorHandler getCompileTimeErrorHandler()
		{
			return this.m_compileTimeErrorHandler;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public ErrorHandler getRuntimeErrorHandler()
		{
			return this.m_runtimeErrorHandler;
		}

		// Token: 0x040000A4 RID: 164
		private static List<FunctionDefinition> __builtInFunctions = null;

		// Token: 0x040000A5 RID: 165
		public static int nrOfSprakRunnersInMemory = 0;

		// Token: 0x040000A6 RID: 166
		private AST m_ast;

		// Token: 0x040000A7 RID: 167
		private bool m_started;

		// Token: 0x040000A8 RID: 168
		private List<Token> m_tokens;

		// Token: 0x040000A9 RID: 169
		private InterpreterTwo m_interpreter;

		// Token: 0x040000AA RID: 170
		private ErrorHandler m_compileTimeErrorHandler;

		// Token: 0x040000AB RID: 171
		private ErrorHandler m_runtimeErrorHandler;

		// Token: 0x040000AC RID: 172
		private IEnumerator<InterpreterTwo.Status> m_programIterator;

		// Token: 0x040000AD RID: 173
		public bool returnFromExternalFunctionCall;
	}
}
