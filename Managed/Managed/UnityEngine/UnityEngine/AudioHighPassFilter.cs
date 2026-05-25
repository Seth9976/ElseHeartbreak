using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DC RID: 476
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001714 RID: 5908
		// (set) Token: 0x06001715 RID: 5909
		public extern float cutoffFrequency
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001716 RID: 5910
		// (set) Token: 0x06001717 RID: 5911
		public extern float highpassResonaceQ
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
