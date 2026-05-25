using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E2 RID: 482
	public sealed class MovieTexture : Texture
	{
		// Token: 0x0600175E RID: 5982 RVA: 0x00023A74 File Offset: 0x00021C74
		public void Play()
		{
			MovieTexture.INTERNAL_CALL_Play(this);
		}

		// Token: 0x0600175F RID: 5983
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Play(MovieTexture self);

		// Token: 0x06001760 RID: 5984 RVA: 0x00023A7C File Offset: 0x00021C7C
		public void Stop()
		{
			MovieTexture.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x06001761 RID: 5985
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(MovieTexture self);

		// Token: 0x06001762 RID: 5986 RVA: 0x00023A84 File Offset: 0x00021C84
		public void Pause()
		{
			MovieTexture.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x06001763 RID: 5987
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(MovieTexture self);

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001764 RID: 5988
		public extern AudioClip audioClip
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001765 RID: 5989
		// (set) Token: 0x06001766 RID: 5990
		public extern bool loop
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001767 RID: 5991
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001768 RID: 5992
		public extern bool isReadyToPlay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001769 RID: 5993
		public extern float duration
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
