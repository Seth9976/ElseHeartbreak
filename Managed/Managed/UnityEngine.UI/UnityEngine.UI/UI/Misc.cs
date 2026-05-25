using System;

namespace UnityEngine.UI
{
	// Token: 0x02000055 RID: 85
	internal static class Misc
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						GameObject gameObject = obj as GameObject;
						gameObject.transform.parent = null;
					}
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
				}
				else
				{
					Object.Destroy(obj);
				}
			}
		}
	}
}
