using System;

namespace Mono.Data.SqlExpressions.yydebug
{
	// Token: 0x02000005 RID: 5
	internal class yyDebugSimple : yyDebug
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000036A4 File Offset: 0x000018A4
		private void println(string s)
		{
			Console.Error.WriteLine(s);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000036B4 File Offset: 0x000018B4
		public void push(int state, object value)
		{
			this.println(string.Concat(new object[] { "push\tstate ", state, "\tvalue ", value }));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000036F0 File Offset: 0x000018F0
		public void lex(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[] { "lex\tstate ", state, "\treading ", name, "\tvalue ", value }));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003730 File Offset: 0x00001930
		public void shift(int from, int to, int errorFlag)
		{
			switch (errorFlag)
			{
			case 0:
			case 1:
			case 2:
				this.println(string.Concat(new object[] { "shift\tfrom state ", from, " to ", to, "\t", errorFlag, " left to recover" }));
				break;
			case 3:
				this.println(string.Concat(new object[] { "shift\tfrom state ", from, " to ", to, "\ton error" }));
				break;
			default:
				this.println(string.Concat(new object[] { "shift\tfrom state ", from, " to ", to }));
				break;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003824 File Offset: 0x00001A24
		public void pop(int state)
		{
			this.println("pop\tstate " + state + "\ton error");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003844 File Offset: 0x00001A44
		public void discard(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[] { "discard\tstate ", state, "\ttoken ", name, "\tvalue ", value }));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003884 File Offset: 0x00001A84
		public void reduce(int from, int to, int rule, string text, int len)
		{
			this.println(string.Concat(new object[] { "reduce\tstate ", from, "\tuncover ", to, "\trule (", rule, ") ", text }));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void shift(int from, int to)
		{
			this.println(string.Concat(new object[] { "goto\tfrom state ", from, " to ", to }));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000391C File Offset: 0x00001B1C
		public void accept(object value)
		{
			this.println("accept\tvalue " + value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003930 File Offset: 0x00001B30
		public void error(string message)
		{
			this.println("error\t" + message);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003944 File Offset: 0x00001B44
		public void reject()
		{
			this.println("reject");
		}
	}
}
