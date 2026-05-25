using System;

namespace Mono.Audio
{
	// Token: 0x020002B2 RID: 690
	internal class AudioDevice
	{
		// Token: 0x060017DD RID: 6109 RVA: 0x00041AD4 File Offset: 0x0003FCD4
		private static AudioDevice TryAlsa(string name)
		{
			AudioDevice audioDevice2;
			try
			{
				AudioDevice audioDevice = new AlsaDevice(name);
				audioDevice2 = audioDevice;
			}
			catch
			{
				audioDevice2 = null;
			}
			return audioDevice2;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00041B20 File Offset: 0x0003FD20
		public static AudioDevice CreateDevice(string name)
		{
			AudioDevice audioDevice = AudioDevice.TryAlsa(name);
			if (audioDevice == null)
			{
				audioDevice = new AudioDevice();
			}
			return audioDevice;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00041B44 File Offset: 0x0003FD44
		public virtual bool SetFormat(AudioFormat format, int channels, int rate)
		{
			return true;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00041B48 File Offset: 0x0003FD48
		public virtual int PlaySample(byte[] buffer, int num_frames)
		{
			return num_frames;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00041B4C File Offset: 0x0003FD4C
		public virtual void Wait()
		{
		}
	}
}
