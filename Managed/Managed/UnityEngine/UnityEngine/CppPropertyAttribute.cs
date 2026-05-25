using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal class CppPropertyAttribute : Attribute
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000023C0 File Offset: 0x000005C0
		public CppPropertyAttribute(string getter, string setter)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C8 File Offset: 0x000005C8
		public CppPropertyAttribute(string getter)
		{
		}
	}
}
