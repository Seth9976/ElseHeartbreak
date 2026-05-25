using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D1 RID: 465
	public sealed class AudioSettings
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600167C RID: 5756
		public static extern AudioSpeakerMode driverCaps
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600167D RID: 5757
		// (set) Token: 0x0600167E RID: 5758
		public static extern AudioSpeakerMode speakerMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600167F RID: 5759
		public static extern double dspTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001680 RID: 5760
		// (set) Token: 0x06001681 RID: 5761
		public static extern int outputSampleRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001682 RID: 5762
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDSPBufferSize(int bufferLength, int numBuffers);

		// Token: 0x06001683 RID: 5763
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);
	}
}
