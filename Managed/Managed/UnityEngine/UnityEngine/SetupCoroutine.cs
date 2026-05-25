using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	internal class SetupCoroutine
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002324 File Offset: 0x00000524
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] array = null;
			if (variable != null)
			{
				array = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, array, null, null, null);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000235C File Offset: 0x0000055C
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] array = null;
			if (variable != null)
			{
				array = new object[] { variable };
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, array, null, null, null);
		}
	}
}
