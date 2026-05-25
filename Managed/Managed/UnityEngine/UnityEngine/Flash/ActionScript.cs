using System;
using System.Diagnostics;

namespace UnityEngine.Flash
{
	// Token: 0x0200015F RID: 351
	[NotConverted]
	public sealed class ActionScript
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0001EF7C File Offset: 0x0001D17C
		[Conditional("UNITY_FLASH")]
		public static void Import(string package)
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0001EF80 File Offset: 0x0001D180
		[Conditional("UNITY_FLASH")]
		public static void Statement(string formatString, params object[] arguments)
		{
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0001EF84 File Offset: 0x0001D184
		public static T Expression<T>(string formatString, params object[] arguments)
		{
			throw new InvalidOperationException();
		}
	}
}
