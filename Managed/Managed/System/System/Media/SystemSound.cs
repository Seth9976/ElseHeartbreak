using System;
using System.IO;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	/// <filterpriority>2</filterpriority>
	/// <completionlist cref="T:System.Media.SystemSounds" />
	// Token: 0x020002B5 RID: 693
	public class SystemSound
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x000422FC File Offset: 0x000404FC
		internal SystemSound(string tag)
		{
			this.resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x06001812 RID: 6162 RVA: 0x00042334 File Offset: 0x00040534
		public void Play()
		{
			SoundPlayer soundPlayer = new SoundPlayer(this.resource);
			soundPlayer.Play();
		}

		// Token: 0x04000F5F RID: 3935
		private Stream resource;
	}
}
