using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class VariableDefinition
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public VariableDefinition()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public VariableDefinition(string pVariableName, object pInitValue)
		{
			this.variableName = pVariableName;
			this.initValue = pInitValue;
		}

		// Token: 0x040000C4 RID: 196
		public string variableName;

		// Token: 0x040000C5 RID: 197
		public object initValue;
	}
}
