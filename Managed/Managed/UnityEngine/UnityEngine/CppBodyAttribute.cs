using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	internal class CppBodyAttribute : Attribute
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023A0 File Offset: 0x000005A0
		public CppBodyAttribute(string body)
		{
		}
	}
}
