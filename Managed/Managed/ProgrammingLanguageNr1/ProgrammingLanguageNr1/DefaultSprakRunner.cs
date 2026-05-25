using System;
using System.Collections.Generic;
using System.IO;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200000D RID: 13
	public class DefaultSprakRunner
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00004570 File Offset: 0x00002770
		public DefaultSprakRunner(TextReader stream)
		{
			FunctionDefinition[] array = new FunctionDefinition[]
			{
				new FunctionDefinition("void", "print", new string[] { "var" }, new string[] { "the thing to print" }, new ExternalFunctionCreator.OnFunctionCall(this.print), FunctionDocumentation.Default()),
				new FunctionDefinition("number", "sqrt", new string[] { "number" }, new string[] { "f" }, new ExternalFunctionCreator.OnFunctionCall(this.sqrt), FunctionDocumentation.Default())
			};
			this.m_sprakRunner = new SprakRunner(stream, array);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004634 File Offset: 0x00002834
		public void printOutputToConsole()
		{
			Console.WriteLine("PROGRAM OUTPUT:");
			foreach (string text in this.m_output)
			{
				Console.WriteLine(text);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000046A8 File Offset: 0x000028A8
		private object print(object[] parameters)
		{
			object obj = parameters[0];
			if (obj == null)
			{
				throw new Exception("Parameter0 is null!");
			}
			this.m_output.Add(ReturnValueConversions.PrettyStringRepresenation(obj));
			return VoidType.voidType;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000046E8 File Offset: 0x000028E8
		private object sqrt(object[] parameters)
		{
			object obj = parameters[0];
			if (obj.GetType() == typeof(float))
			{
				return (float)Math.Sqrt((double)((float)obj));
			}
			throw new Error("Can't use sqrt on something that's not a number", Error.ErrorType.SYNTAX, 0, 0);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004730 File Offset: 0x00002930
		private object f(object[] parameters)
		{
			return new object();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004738 File Offset: 0x00002938
		public ErrorHandler getCompileTimeErrorHandler()
		{
			return this.m_sprakRunner.getCompileTimeErrorHandler();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004748 File Offset: 0x00002948
		public ErrorHandler getRuntimeErrorHandler()
		{
			return this.m_sprakRunner.getRuntimeErrorHandler();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004758 File Offset: 0x00002958
		public void run()
		{
			this.m_sprakRunner.run();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004768 File Offset: 0x00002968
		public void run(int n)
		{
			this.m_sprakRunner.run(n);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004778 File Offset: 0x00002978
		public List<string> Output
		{
			get
			{
				return this.m_output;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004780 File Offset: 0x00002980
		public void printTree(bool printExecutionCounters)
		{
			this.m_sprakRunner.printTree(printExecutionCounters);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004790 File Offset: 0x00002990
		public object RunFunction(string functionName, object[] args)
		{
			return this.m_sprakRunner.RunFunction(functionName, args);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000047A0 File Offset: 0x000029A0
		public SprakRunner sprakRunner
		{
			get
			{
				return this.m_sprakRunner;
			}
		}

		// Token: 0x04000028 RID: 40
		private List<string> m_output = new List<string>();

		// Token: 0x04000029 RID: 41
		private SprakRunner m_sprakRunner;
	}
}
