using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	internal class CppIncludeAttribute : Attribute
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002390 File Offset: 0x00000590
		public CppIncludeAttribute(string header)
		{
		}
	}
}
