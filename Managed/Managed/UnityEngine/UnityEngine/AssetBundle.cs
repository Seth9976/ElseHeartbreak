using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200007A RID: 122
	public sealed class AssetBundle : Object
	{
		// Token: 0x060002A7 RID: 679
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundleCreateRequest CreateFromMemory(byte[] binary);

		// Token: 0x060002A8 RID: 680
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AssetBundle CreateFromMemoryImmediate(byte[] binary);

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AF40 File Offset: 0x00009140
		public static AssetBundle CreateFromFile(string path)
		{
			return AssetBundle.Internal_CreateFromFile(path, 0L);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AF4C File Offset: 0x0000914C
		public static AssetBundle CreateFromFile(string path, int offset)
		{
			return AssetBundle.Internal_CreateFromFile(path, (long)offset);
		}

		// Token: 0x060002AB RID: 683
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssetBundle Internal_CreateFromFile(string path, long offset);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002AC RID: 684
		public extern Object mainAsset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002AD RID: 685
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Contains(string name);

		// Token: 0x060002AE RID: 686 RVA: 0x0000AF58 File Offset: 0x00009158
		public Object Load(string name)
		{
			return this.Load(name, typeof(Object));
		}

		// Token: 0x060002AF RID: 687
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object Load(string name, Type type);

		// Token: 0x060002B0 RID: 688
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AssetBundleRequest LoadAsync(string name, Type type);

		// Token: 0x060002B1 RID: 689
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object[] LoadAll(Type type);

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AF6C File Offset: 0x0000916C
		public Object[] LoadAll()
		{
			return this.LoadAll(typeof(Object));
		}

		// Token: 0x060002B3 RID: 691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Unload(bool unloadAllLoadedObjects);
	}
}
