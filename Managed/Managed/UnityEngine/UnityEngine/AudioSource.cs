using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001D8 RID: 472
	public sealed class AudioSource : Behaviour
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016A5 RID: 5797
		// (set) Token: 0x060016A6 RID: 5798
		public extern float volume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016A7 RID: 5799
		// (set) Token: 0x060016A8 RID: 5800
		public extern float pitch
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060016A9 RID: 5801
		// (set) Token: 0x060016AA RID: 5802
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060016AB RID: 5803
		// (set) Token: 0x060016AC RID: 5804
		public extern int timeSamples
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060016AD RID: 5805
		// (set) Token: 0x060016AE RID: 5806
		public extern AudioClip clip
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060016AF RID: 5807
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play([DefaultValue("0")] ulong delay);

		// Token: 0x060016B0 RID: 5808 RVA: 0x0002391C File Offset: 0x00021B1C
		[ExcludeFromDocs]
		public void Play()
		{
			ulong num = 0UL;
			this.Play(num);
		}

		// Token: 0x060016B1 RID: 5809
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayDelayed(float delay);

		// Token: 0x060016B2 RID: 5810
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayScheduled(double time);

		// Token: 0x060016B3 RID: 5811
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledStartTime(double time);

		// Token: 0x060016B4 RID: 5812
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledEndTime(double time);

		// Token: 0x060016B5 RID: 5813
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x060016B6 RID: 5814 RVA: 0x00023934 File Offset: 0x00021B34
		public void Pause()
		{
			AudioSource.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x060016B7 RID: 5815
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(AudioSource self);

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060016B8 RID: 5816
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060016B9 RID: 5817
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale);

		// Token: 0x060016BA RID: 5818 RVA: 0x0002393C File Offset: 0x00021B3C
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			float num = 1f;
			this.PlayOneShot(clip, num);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00023958 File Offset: 0x00021B58
		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			float num = 1f;
			AudioSource.PlayClipAtPoint(clip, position, num);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00023974 File Offset: 0x00021B74
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * Time.timeScale);
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060016BD RID: 5821
		// (set) Token: 0x060016BE RID: 5822
		public extern bool loop
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060016BF RID: 5823
		// (set) Token: 0x060016C0 RID: 5824
		public extern bool ignoreListenerVolume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060016C1 RID: 5825
		// (set) Token: 0x060016C2 RID: 5826
		public extern bool playOnAwake
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060016C3 RID: 5827
		// (set) Token: 0x060016C4 RID: 5828
		public extern bool ignoreListenerPause
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060016C5 RID: 5829
		// (set) Token: 0x060016C6 RID: 5830
		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060016C7 RID: 5831
		// (set) Token: 0x060016C8 RID: 5832
		public extern float panLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060016C9 RID: 5833
		// (set) Token: 0x060016CA RID: 5834
		public extern bool bypassEffects
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060016CB RID: 5835
		// (set) Token: 0x060016CC RID: 5836
		public extern bool bypassListenerEffects
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060016CD RID: 5837
		// (set) Token: 0x060016CE RID: 5838
		public extern bool bypassReverbZones
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060016CF RID: 5839
		// (set) Token: 0x060016D0 RID: 5840
		public extern float dopplerLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060016D1 RID: 5841
		// (set) Token: 0x060016D2 RID: 5842
		public extern float spread
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060016D3 RID: 5843
		// (set) Token: 0x060016D4 RID: 5844
		public extern int priority
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060016D5 RID: 5845
		// (set) Token: 0x060016D6 RID: 5846
		public extern bool mute
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060016D7 RID: 5847
		// (set) Token: 0x060016D8 RID: 5848
		public extern float minDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060016D9 RID: 5849
		// (set) Token: 0x060016DA RID: 5850
		public extern float maxDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060016DB RID: 5851
		// (set) Token: 0x060016DC RID: 5852
		public extern float pan
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060016DD RID: 5853
		// (set) Token: 0x060016DE RID: 5854
		public extern AudioRolloffMode rolloffMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060016DF RID: 5855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetOutputDataHelper(float[] samples, int channel);

		// Token: 0x060016E0 RID: 5856 RVA: 0x000239D4 File Offset: 0x00021BD4
		[Obsolete("GetOutputData return a float[] is deprecated, use GetOutputData passing a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			this.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000239F4 File Offset: 0x00021BF4
		public void GetOutputData(float[] samples, int channel)
		{
			this.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x060016E2 RID: 5858
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSpectrumDataHelper(float[] samples, int channel, FFTWindow window);

		// Token: 0x060016E3 RID: 5859 RVA: 0x00023A00 File Offset: 0x00021C00
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData passing a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			this.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00023A20 File Offset: 0x00021C20
		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			this.GetSpectrumDataHelper(samples, channel, window);
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060016E5 RID: 5861
		// (set) Token: 0x060016E6 RID: 5862
		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float minVolume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060016E7 RID: 5863
		// (set) Token: 0x060016E8 RID: 5864
		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float maxVolume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016E9 RID: 5865
		// (set) Token: 0x060016EA RID: 5866
		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float rolloffFactor
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
