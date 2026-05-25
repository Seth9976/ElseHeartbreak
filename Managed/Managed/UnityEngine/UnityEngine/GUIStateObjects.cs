using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	internal class GUIStateObjects
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002438 File Offset: 0x00000638
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			if (!GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002478 File Offset: 0x00000678
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			if (t.IsInstanceOfType(obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x04000094 RID: 148
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
