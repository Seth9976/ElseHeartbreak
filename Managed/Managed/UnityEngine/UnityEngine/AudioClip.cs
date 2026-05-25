using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001D3 RID: 467
	public sealed class AudioClip : Object
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001685 RID: 5765 RVA: 0x0002372C File Offset: 0x0002192C
		// (remove) Token: 0x06001686 RID: 5766 RVA: 0x00023748 File Offset: 0x00021948
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001687 RID: 5767 RVA: 0x00023764 File Offset: 0x00021964
		// (remove) Token: 0x06001688 RID: 5768 RVA: 0x00023780 File Offset: 0x00021980
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback;

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001689 RID: 5769
		public extern float length
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600168A RID: 5770
		public extern int samples
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600168B RID: 5771
		public extern int channels
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600168C RID: 5772
		public extern int frequency
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600168D RID: 5773
		public extern bool isReadyToPlay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600168E RID: 5774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetData(float[] data, int offsetSamples);

		// Token: 0x0600168F RID: 5775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetData(float[] data, int offsetSamples);

		// Token: 0x06001690 RID: 5776 RVA: 0x0002379C File Offset: 0x0002199C
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, null, null);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000237BC File Offset: 0x000219BC
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, pcmreadercallback, null);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000237DC File Offset: 0x000219DC
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null)
			{
				throw new NullReferenceException();
			}
			if (lengthSamples <= 0)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			if (channels <= 0)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			if (pcmreadercallback != null)
			{
				AudioClip audioClip2 = audioClip;
				audioClip2.m_PCMReaderCallback = (AudioClip.PCMReaderCallback)Delegate.Combine(audioClip2.m_PCMReaderCallback, pcmreadercallback);
			}
			if (pcmsetpositioncallback != null)
			{
				AudioClip audioClip3 = audioClip;
				audioClip3.m_PCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)Delegate.Combine(audioClip3.m_PCMSetPositionCallback, pcmsetpositioncallback);
			}
			audioClip.Init_Internal(name, lengthSamples, channels, frequency, _3D, stream);
			return audioClip;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00023880 File Offset: 0x00021A80
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			if (this.m_PCMReaderCallback != null)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0002389C File Offset: 0x00021A9C
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			if (this.m_PCMSetPositionCallback != null)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x06001695 RID: 5781
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip Construct_Internal();

		// Token: 0x06001696 RID: 5782
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init_Internal(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream);

		// Token: 0x0200022D RID: 557
		// (Invoke) Token: 0x06001AD9 RID: 6873
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x0200022E RID: 558
		// (Invoke) Token: 0x06001ADD RID: 6877
		public delegate void PCMSetPositionCallback(int position);
	}
}
