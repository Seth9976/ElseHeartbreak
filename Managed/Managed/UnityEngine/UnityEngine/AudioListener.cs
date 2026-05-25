using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D5 RID: 469
	public sealed class AudioListener : Behaviour
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001698 RID: 5784
		// (set) Token: 0x06001699 RID: 5785
		public static extern float volume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600169A RID: 5786
		// (set) Token: 0x0600169B RID: 5787
		public static extern bool pause
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600169C RID: 5788
		// (set) Token: 0x0600169D RID: 5789
		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600169E RID: 5790
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper(float[] samples, int channel);

		// Token: 0x0600169F RID: 5791
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper(float[] samples, int channel, FFTWindow window);

		// Token: 0x060016A0 RID: 5792 RVA: 0x000238C0 File Offset: 0x00021AC0
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000238DC File Offset: 0x00021ADC
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000238E8 File Offset: 0x00021AE8
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00023908 File Offset: 0x00021B08
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}
	}
}
