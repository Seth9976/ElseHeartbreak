using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001E5 RID: 485
	public sealed class WebCamTexture : Texture
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x00023AA4 File Offset: 0x00021CA4
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00023AB8 File Offset: 0x00021CB8
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00023ACC File Offset: 0x00021CCC
		public WebCamTexture(string deviceName)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00023AE0 File Offset: 0x00021CE0
		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00023AF8 File Offset: 0x00021CF8
		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00023B10 File Offset: 0x00021D10
		public WebCamTexture()
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, string.Empty, 0, 0, 0);
		}

		// Token: 0x06001772 RID: 6002
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);

		// Token: 0x06001773 RID: 6003 RVA: 0x00023B28 File Offset: 0x00021D28
		public void Play()
		{
			WebCamTexture.INTERNAL_CALL_Play(this);
		}

		// Token: 0x06001774 RID: 6004
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Play(WebCamTexture self);

		// Token: 0x06001775 RID: 6005 RVA: 0x00023B30 File Offset: 0x00021D30
		public void Pause()
		{
			WebCamTexture.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x06001776 RID: 6006
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(WebCamTexture self);

		// Token: 0x06001777 RID: 6007 RVA: 0x00023B38 File Offset: 0x00021D38
		public void Stop()
		{
			WebCamTexture.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x06001778 RID: 6008
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(WebCamTexture self);

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001779 RID: 6009
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600177A RID: 6010
		// (set) Token: 0x0600177B RID: 6011
		public extern string deviceName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600177C RID: 6012
		// (set) Token: 0x0600177D RID: 6013
		public extern float requestedFPS
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600177E RID: 6014
		// (set) Token: 0x0600177F RID: 6015
		public extern int requestedWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001780 RID: 6016
		// (set) Token: 0x06001781 RID: 6017
		public extern int requestedHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001782 RID: 6018
		public static extern WebCamDevice[] devices
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001783 RID: 6019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixel(int x, int y);

		// Token: 0x06001784 RID: 6020 RVA: 0x00023B40 File Offset: 0x00021D40
		public Color[] GetPixels()
		{
			return this.GetPixels(0, 0, this.width, this.height);
		}

		// Token: 0x06001785 RID: 6021
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight);

		// Token: 0x06001786 RID: 6022
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("null")] Color32[] colors);

		// Token: 0x06001787 RID: 6023 RVA: 0x00023B64 File Offset: 0x00021D64
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			Color32[] array = null;
			return this.GetPixels32(array);
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001788 RID: 6024
		public extern int videoRotationAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001789 RID: 6025
		public extern bool videoVerticallyMirrored
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600178A RID: 6026
		public extern bool didUpdateThisFrame
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
