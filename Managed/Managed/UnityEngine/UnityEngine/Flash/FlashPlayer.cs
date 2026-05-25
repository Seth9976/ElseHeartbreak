using System;

namespace UnityEngine.Flash
{
	// Token: 0x02000160 RID: 352
	public sealed class FlashPlayer
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0001EF94 File Offset: 0x0001D194
		public static string TargetVersion
		{
			get
			{
				return FlashPlayer.GetUnityAppConstants("TargetFlashPlayerVersion");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		public static string TargetSwfVersion
		{
			get
			{
				return FlashPlayer.GetUnityAppConstants("TargetSwfVersion");
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0001EFAC File Offset: 0x0001D1AC
		internal static string GetUnityAppConstants(string name)
		{
			return ActionScript.Expression<string>("UnityNative.getUnityAppConstants()[{0}]", new object[] { name });
		}
	}
}
