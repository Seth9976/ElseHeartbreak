using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000092 RID: 146
	public sealed class Resources
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000B180 File Offset: 0x00009380
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)rawObjects[i]);
			}
			return array;
		}

		// Token: 0x060002FC RID: 764
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x060002FD RID: 765 RVA: 0x0000B1C4 File Offset: 0x000093C4
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B1DC File Offset: 0x000093DC
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06000300 RID: 768
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object Load(string path, Type systemTypeInstance);

		// Token: 0x06000301 RID: 769 RVA: 0x0000B208 File Offset: 0x00009408
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B21C File Offset: 0x0000941C
		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		// Token: 0x06000303 RID: 771
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ResourceRequest LoadAsync(string path, Type type);

		// Token: 0x06000304 RID: 772
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] LoadAll(string path, Type systemTypeInstance);

		// Token: 0x06000305 RID: 773 RVA: 0x0000B230 File Offset: 0x00009430
		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B244 File Offset: 0x00009444
		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		// Token: 0x06000307 RID: 775
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetBuiltinResource(Type type, string path);

		// Token: 0x06000308 RID: 776 RVA: 0x0000B25C File Offset: 0x0000945C
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x06000309 RID: 777
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object LoadAssetAtPath(string assetPath, Type type);

		// Token: 0x0600030A RID: 778 RVA: 0x0000B274 File Offset: 0x00009474
		public static T LoadAssetAtPath<T>(string assetPath) where T : Object
		{
			return (T)((object)Resources.LoadAssetAtPath(assetPath, typeof(T)));
		}

		// Token: 0x0600030B RID: 779
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAsset(Object assetToUnload);

		// Token: 0x0600030C RID: 780
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
