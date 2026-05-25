using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000005 RID: 5
	public class AST_Assignment : AST
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002444 File Offset: 0x00000644
		public AST_Assignment(Token token, string variableName)
			: base(token)
		{
			this.m_variableName = variableName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002454 File Offset: 0x00000654
		public string VariableName
		{
			get
			{
				return this.m_variableName;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000245C File Offset: 0x0000065C
		public override string ToString()
		{
			return "Assign to " + this.m_variableName + "";
		}

		// Token: 0x04000004 RID: 4
		private string m_variableName;
	}
}
