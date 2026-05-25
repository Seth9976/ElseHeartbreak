using System;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	internal class CppDefineAttribute : Attribute
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002398 File Offset: 0x00000598
		public CppDefineAttribute(string symbol, string value)
		{
		}
	}
}
