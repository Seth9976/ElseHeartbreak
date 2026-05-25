using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C8 RID: 200
	public sealed class LightmapSettings : Object
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600056A RID: 1386
		// (set) Token: 0x0600056B RID: 1387
		public static extern LightmapData[] lightmaps
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600056C RID: 1388
		// (set) Token: 0x0600056D RID: 1389
		public static extern LightmapsMode lightmapsMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600056E RID: 1390
		// (set) Token: 0x0600056F RID: 1391
		public static extern ColorSpace bakedColorSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000570 RID: 1392
		// (set) Token: 0x06000571 RID: 1393
		public static extern LightProbes lightProbes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
