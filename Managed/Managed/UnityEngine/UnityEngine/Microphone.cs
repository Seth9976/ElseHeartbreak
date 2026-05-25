using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E1 RID: 481
	public sealed class Microphone
	{
		// Token: 0x06001757 RID: 5975
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency);

		// Token: 0x06001758 RID: 5976
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End(string deviceName);

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001759 RID: 5977
		public static extern string[] devices
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600175A RID: 5978
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsRecording(string deviceName);

		// Token: 0x0600175B RID: 5979
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPosition(string deviceName);

		// Token: 0x0600175C RID: 5980
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq);
	}
}
