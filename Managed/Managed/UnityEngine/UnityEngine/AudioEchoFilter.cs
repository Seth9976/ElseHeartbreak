using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	public sealed class AudioEchoFilter : Behaviour
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600171C RID: 5916
		// (set) Token: 0x0600171D RID: 5917
		public extern float delay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600171E RID: 5918
		// (set) Token: 0x0600171F RID: 5919
		public extern float decayRatio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001720 RID: 5920
		// (set) Token: 0x06001721 RID: 5921
		public extern float dryMix
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001722 RID: 5922
		// (set) Token: 0x06001723 RID: 5923
		public extern float wetMix
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
