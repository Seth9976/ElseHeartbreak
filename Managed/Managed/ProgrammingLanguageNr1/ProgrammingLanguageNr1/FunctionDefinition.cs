using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000010 RID: 16
	public struct FunctionDefinition
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00004A74 File Offset: 0x00002C74
		public FunctionDefinition(string pReturnType, string pFunctionName, string[] pParameterTypes, string[] pParameterNames, ExternalFunctionCreator.OnFunctionCall pCallback, FunctionDocumentation pFunctionDocumentation)
		{
			this.returnType = pReturnType;
			this.functionName = pFunctionName;
			this.parameterTypes = pParameterTypes;
			this.parameterNames = pParameterNames;
			this.callback = pCallback;
			this.functionDocumentation = pFunctionDocumentation;
			this.hideInModifier = false;
		}

		// Token: 0x0400002D RID: 45
		public string returnType;

		// Token: 0x0400002E RID: 46
		public string functionName;

		// Token: 0x0400002F RID: 47
		public string[] parameterTypes;

		// Token: 0x04000030 RID: 48
		public string[] parameterNames;

		// Token: 0x04000031 RID: 49
		public ExternalFunctionCreator.OnFunctionCall callback;

		// Token: 0x04000032 RID: 50
		public FunctionDocumentation functionDocumentation;

		// Token: 0x04000033 RID: 51
		public bool hideInModifier;
	}
}
