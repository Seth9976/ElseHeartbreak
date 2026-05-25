using System;
using System.Diagnostics;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[Conditional("UNITY_FLASH")]
	internal class CanConvertToFlashAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000022D4 File Offset: 0x000004D4
		public CanConvertToFlashAttribute()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022DC File Offset: 0x000004DC
		public CanConvertToFlashAttribute(params string[] members)
		{
		}
	}
}
