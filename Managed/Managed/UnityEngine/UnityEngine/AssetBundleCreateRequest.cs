using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000078 RID: 120
	public sealed class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A2 RID: 674
		public extern AssetBundle assetBundle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002A3 RID: 675
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DisableCompatibilityChecks();
	}
}
