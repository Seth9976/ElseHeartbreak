using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Serialization;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	internal static class ClassLibraryInitializer
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022EC File Offset: 0x000004EC
		private static void Init()
		{
			UnityLogWriter.Init();
			if (Application.platform.ToString().Contains("WebPlayer"))
			{
				BinaryFormatter.DefaultSurrogateSelector = new UnitySurrogateSelector();
			}
		}
	}
}
