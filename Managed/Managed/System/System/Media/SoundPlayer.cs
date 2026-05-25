using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Audio;

namespace System.Media
{
	/// <summary>Controls playback of a sound from a .wav file.</summary>
	// Token: 0x020002B4 RID: 692
	[global::System.ComponentModel.ToolboxItem(false)]
	[Serializable]
	public class SoundPlayer : global::System.ComponentModel.Component, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		// Token: 0x060017EE RID: 6126 RVA: 0x00041C7C File Offset: 0x0003FE7C
		public SoundPlayer()
		{
			this.sound_location = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the .wav file within the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> to a .wav file.</param>
		// Token: 0x060017EF RID: 6127 RVA: 0x00041CA8 File Offset: 0x0003FEA8
		public SoundPlayer(Stream stream)
			: this()
		{
			this.audiostream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the specified .wav file.</summary>
		/// <param name="soundLocation">The location of a .wav file to load.</param>
		/// <exception cref="T:System.UriFormatException">The URL value specified by <paramref name="soundLocation" /> cannot be resolved.</exception>
		// Token: 0x060017F0 RID: 6128 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public SoundPlayer(string soundLocation)
			: this()
		{
			if (soundLocation == null)
			{
				throw new ArgumentNullException("soundLocation");
			}
			this.sound_location = soundLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.UriFormatException">The <see cref="P:System.Media.SoundPlayer.SoundLocation" /> specified in <paramref name="serializationInfo" /> cannot be resolved.</exception>
		// Token: 0x060017F1 RID: 6129 RVA: 0x00041CD8 File Offset: 0x0003FED8
		protected SoundPlayer(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a .wav file has been successfully or unsuccessfully loaded.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060017F3 RID: 6131 RVA: 0x00041D00 File Offset: 0x0003FF00
		// (remove) Token: 0x060017F4 RID: 6132 RVA: 0x00041D1C File Offset: 0x0003FF1C
		public event global::System.ComponentModel.AsyncCompletedEventHandler LoadCompleted;

		/// <summary>Occurs when a new audio source path for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060017F5 RID: 6133 RVA: 0x00041D38 File Offset: 0x0003FF38
		// (remove) Token: 0x060017F6 RID: 6134 RVA: 0x00041D54 File Offset: 0x0003FF54
		public event EventHandler SoundLocationChanged;

		/// <summary>Occurs when a new <see cref="T:System.IO.Stream" /> audio source for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060017F7 RID: 6135 RVA: 0x00041D70 File Offset: 0x0003FF70
		// (remove) Token: 0x060017F8 RID: 6136 RVA: 0x00041D8C File Offset: 0x0003FF8C
		public event EventHandler StreamChanged;

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" />  to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060017F9 RID: 6137 RVA: 0x00041DA8 File Offset: 0x0003FFA8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00041DAC File Offset: 0x0003FFAC
		private void LoadFromStream(Stream s)
		{
			this.mstream = new MemoryStream();
			byte[] array = new byte[4096];
			int num;
			while ((num = s.Read(array, 0, 4096)) > 0)
			{
				this.mstream.Write(array, 0, num);
			}
			this.mstream.Position = 0L;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00041E04 File Offset: 0x00040004
		private void LoadFromUri(string location)
		{
			this.mstream = null;
			if (string.IsNullOrEmpty(location))
			{
				return;
			}
			Stream stream;
			if (File.Exists(location))
			{
				stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			else
			{
				global::System.Net.WebRequest webRequest = global::System.Net.WebRequest.Create(location);
				stream = webRequest.GetResponse().GetResponseStream();
			}
			using (stream)
			{
				this.LoadFromStream(stream);
			}
		}

		/// <summary>Loads a sound synchronously.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060017FC RID: 6140 RVA: 0x00041E8C File Offset: 0x0004008C
		public void Load()
		{
			if (this.load_completed)
			{
				return;
			}
			if (this.audiostream != null)
			{
				this.LoadFromStream(this.audiostream);
			}
			else
			{
				this.LoadFromUri(this.sound_location);
			}
			this.adata = null;
			this.adev = null;
			this.load_completed = true;
			global::System.ComponentModel.AsyncCompletedEventArgs asyncCompletedEventArgs = new global::System.ComponentModel.AsyncCompletedEventArgs(null, false, this);
			this.OnLoadCompleted(asyncCompletedEventArgs);
			if (this.LoadCompleted != null)
			{
				this.LoadCompleted(this, asyncCompletedEventArgs);
			}
			if (SoundPlayer.use_win32_player)
			{
				if (this.win32_player == null)
				{
					this.win32_player = new Mono.Audio.Win32SoundPlayer(this.mstream);
				}
				else
				{
					this.win32_player.Stream = this.mstream;
				}
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00041F48 File Offset: 0x00040148
		private void AsyncFinished(IAsyncResult ar)
		{
			ThreadStart threadStart = ar.AsyncState as ThreadStart;
			threadStart.EndInvoke(ar);
		}

		/// <summary>Loads a .wav file from a stream or a Web resource using a new thread.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060017FE RID: 6142 RVA: 0x00041F68 File Offset: 0x00040168
		public void LoadAsync()
		{
			if (this.load_completed)
			{
				return;
			}
			ThreadStart threadStart = new ThreadStart(this.Load);
			threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" />  that contains the event data. </param>
		// Token: 0x060017FF RID: 6143 RVA: 0x00041FA4 File Offset: 0x000401A4
		protected virtual void OnLoadCompleted(global::System.ComponentModel.AsyncCompletedEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.SoundLocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001800 RID: 6144 RVA: 0x00041FA8 File Offset: 0x000401A8
		protected virtual void OnSoundLocationChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.StreamChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001801 RID: 6145 RVA: 0x00041FAC File Offset: 0x000401AC
		protected virtual void OnStreamChanged(EventArgs e)
		{
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00041FB0 File Offset: 0x000401B0
		private void Start()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = false;
				if (this.adata != null)
				{
					this.adata.IsStopped = false;
				}
			}
			if (!this.load_completed)
			{
				this.Load();
			}
		}

		/// <summary>Plays the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06001803 RID: 6147 RVA: 0x00041FEC File Offset: 0x000401EC
		public void Play()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlaySync);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
			}
			else
			{
				this.Start();
				if (this.mstream == null)
				{
					SystemSounds.Beep.Play();
					return;
				}
				this.win32_player.Play();
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00042050 File Offset: 0x00040250
		private void PlayLoop()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			while (!this.stopped)
			{
				this.PlaySync();
			}
		}

		/// <summary>Plays and loops the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06001805 RID: 6149 RVA: 0x00042090 File Offset: 0x00040290
		public void PlayLooping()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlayLoop);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
			}
			else
			{
				this.Start();
				if (this.mstream == null)
				{
					SystemSounds.Beep.Play();
					return;
				}
				this.win32_player.PlayLooping();
			}
		}

		/// <summary>Plays the .wav file and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06001806 RID: 6150 RVA: 0x000420F4 File Offset: 0x000402F4
		public void PlaySync()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			if (!SoundPlayer.use_win32_player)
			{
				try
				{
					if (this.adata == null)
					{
						this.adata = new Mono.Audio.WavData(this.mstream);
					}
					if (this.adev == null)
					{
						this.adev = Mono.Audio.AudioDevice.CreateDevice(null);
					}
					if (this.adata != null)
					{
						this.adata.Setup(this.adev);
						this.adata.Play(this.adev);
					}
				}
				catch
				{
				}
			}
			else
			{
				this.win32_player.PlaySync();
			}
		}

		/// <summary>Stops playback of the sound if playback is occurring.</summary>
		// Token: 0x06001807 RID: 6151 RVA: 0x000421C0 File Offset: 0x000403C0
		public void Stop()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = true;
				if (this.adata != null)
				{
					this.adata.IsStopped = true;
				}
			}
			else
			{
				this.win32_player.Stop();
			}
		}

		/// <summary>Gets a value indicating whether loading of a .wav file has successfully completed.</summary>
		/// <returns>true if a .wav file is loaded; false if a .wav file has not yet been loaded.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00042208 File Offset: 0x00040408
		public bool IsLoadCompleted
		{
			get
			{
				return this.load_completed;
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, in which the .wav file must load.</summary>
		/// <returns>The number of milliseconds to wait. The default is 10000 (10 seconds).</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00042210 File Offset: 0x00040410
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x00042218 File Offset: 0x00040418
		public int LoadTimeout
		{
			get
			{
				return this.load_timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("timeout must be >= 0");
				}
				this.load_timeout = value;
			}
		}

		/// <summary>Gets or sets the file path or URL of the .wav file to load.</summary>
		/// <returns>The file path or URL from which to load a .wav file, or <see cref="F:System.String.Empty" /> if no file path is present. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00042234 File Offset: 0x00040434
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0004223C File Offset: 0x0004043C
		public string SoundLocation
		{
			get
			{
				return this.sound_location;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.sound_location = value;
				this.load_completed = false;
				this.OnSoundLocationChanged(EventArgs.Empty);
				if (this.SoundLocationChanged != null)
				{
					this.SoundLocationChanged(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IO.Stream" /> from which to load the .wav file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> from which to load the .wav file, or null if no stream is available. The default is null.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00042290 File Offset: 0x00040490
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x00042298 File Offset: 0x00040498
		public Stream Stream
		{
			get
			{
				return this.audiostream;
			}
			set
			{
				if (this.audiostream != value)
				{
					this.audiostream = value;
					this.load_completed = false;
					this.OnStreamChanged(EventArgs.Empty);
					if (this.StreamChanged != null)
					{
						this.StreamChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000422E8 File Offset: 0x000404E8
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x000422F0 File Offset: 0x000404F0
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x04000F51 RID: 3921
		private string sound_location;

		// Token: 0x04000F52 RID: 3922
		private Stream audiostream;

		// Token: 0x04000F53 RID: 3923
		private object tag = string.Empty;

		// Token: 0x04000F54 RID: 3924
		private MemoryStream mstream;

		// Token: 0x04000F55 RID: 3925
		private bool load_completed;

		// Token: 0x04000F56 RID: 3926
		private int load_timeout = 10000;

		// Token: 0x04000F57 RID: 3927
		private Mono.Audio.AudioDevice adev;

		// Token: 0x04000F58 RID: 3928
		private Mono.Audio.AudioData adata;

		// Token: 0x04000F59 RID: 3929
		private bool stopped;

		// Token: 0x04000F5A RID: 3930
		private Mono.Audio.Win32SoundPlayer win32_player;

		// Token: 0x04000F5B RID: 3931
		private static readonly bool use_win32_player = Environment.OSVersion.Platform != PlatformID.Unix;
	}
}
