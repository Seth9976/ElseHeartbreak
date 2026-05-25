using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200001A RID: 26
	public class VariableSymbol : Symbol
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00007F68 File Offset: 0x00006168
		public VariableSymbol(string name, ReturnValueType type)
		{
			this.m_name = name;
			this.m_returnValueType = type;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007F80 File Offset: 0x00006180
		public string getName()
		{
			return this.m_name;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007F88 File Offset: 0x00006188
		public ReturnValueType getReturnValueType()
		{
			return this.m_returnValueType;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007F90 File Offset: 0x00006190
		public override string ToString()
		{
			return this.getReturnValueType() + " " + this.getName();
		}

		// Token: 0x04000084 RID: 132
		private string m_name;

		// Token: 0x04000085 RID: 133
		private ReturnValueType m_returnValueType;
	}
}
