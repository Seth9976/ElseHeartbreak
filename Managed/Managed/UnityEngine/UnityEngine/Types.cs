using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	public static class Types
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000040F8 File Offset: 0x000022F8
		public static Type GetType(string typeName, string assemblyName)
		{
			Type type;
			try
			{
				type = Assembly.Load(assemblyName).GetType(typeName);
			}
			catch (Exception)
			{
				type = null;
			}
			return type;
		}
	}
}
