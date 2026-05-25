using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000011 RID: 17
	public struct FunctionDocumentation
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public FunctionDocumentation(string pFunctionDescription, string[] pArgumentDescriptions)
		{
			this._functionDescription = pFunctionDescription;
			this._argumentDescriptions = pArgumentDescriptions;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public static FunctionDocumentation Default()
		{
			return new FunctionDocumentation("no function description", new string[0]);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004ADC File Offset: 0x00002CDC
		public string GetFunctionDescription()
		{
			return this._functionDescription;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public string GetArgumentDescription(int nr)
		{
			if (nr < 0 || nr > this._argumentDescriptions.Length - 1)
			{
				return "No description";
			}
			return this._argumentDescriptions[nr];
		}

		// Token: 0x04000034 RID: 52
		private string _functionDescription;

		// Token: 0x04000035 RID: 53
		private string[] _argumentDescriptions;
	}
}
