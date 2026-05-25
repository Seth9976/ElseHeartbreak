using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DF RID: 479
	public sealed class AudioChorusFilter : Behaviour
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001725 RID: 5925
		// (set) Token: 0x06001726 RID: 5926
		public extern float dryMix
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001727 RID: 5927
		// (set) Token: 0x06001728 RID: 5928
		public extern float wetMix1
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001729 RID: 5929
		// (set) Token: 0x0600172A RID: 5930
		public extern float wetMix2
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600172B RID: 5931
		// (set) Token: 0x0600172C RID: 5932
		public extern float wetMix3
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600172D RID: 5933
		// (set) Token: 0x0600172E RID: 5934
		public extern float delay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600172F RID: 5935
		// (set) Token: 0x06001730 RID: 5936
		public extern float rate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001731 RID: 5937
		// (set) Token: 0x06001732 RID: 5938
		public extern float depth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001733 RID: 5939
		// (set) Token: 0x06001734 RID: 5940
		[Obsolete("feedback is deprecated, this property does nothing.")]
		public extern float feedback
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
