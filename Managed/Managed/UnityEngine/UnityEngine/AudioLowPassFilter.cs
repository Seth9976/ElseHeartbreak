using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DB RID: 475
	public sealed class AudioLowPassFilter : Behaviour
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600170F RID: 5903
		// (set) Token: 0x06001710 RID: 5904
		public extern float cutoffFrequency
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001711 RID: 5905
		// (set) Token: 0x06001712 RID: 5906
		public extern float lowpassResonaceQ
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
