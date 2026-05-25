using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal class CppPropertyBodyAttribute : Attribute
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000023B0 File Offset: 0x000005B0
		public CppPropertyBodyAttribute(string getterBody, string setterBody)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B8 File Offset: 0x000005B8
		public CppPropertyBodyAttribute(string getterBody)
		{
		}
	}
}
