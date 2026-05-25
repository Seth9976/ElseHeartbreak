using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000223 RID: 547
	public sealed class Canvas : Behaviour
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001A86 RID: 6790 RVA: 0x000262C8 File Offset: 0x000244C8
		// (remove) Token: 0x06001A87 RID: 6791 RVA: 0x000262E0 File Offset: 0x000244E0
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001A88 RID: 6792
		// (set) Token: 0x06001A89 RID: 6793
		public extern RenderMode renderMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001A8A RID: 6794
		public extern bool isRootCanvas
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001A8B RID: 6795
		// (set) Token: 0x06001A8C RID: 6796
		public extern Camera worldCamera
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000262F8 File Offset: 0x000244F8
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_pixelRect(out rect);
				return rect;
			}
		}

		// Token: 0x06001A8E RID: 6798
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001A8F RID: 6799
		// (set) Token: 0x06001A90 RID: 6800
		public extern float scaleFactor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001A91 RID: 6801
		// (set) Token: 0x06001A92 RID: 6802
		public extern float referencePixelsPerUnit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001A93 RID: 6803
		// (set) Token: 0x06001A94 RID: 6804
		public extern bool overridePixelPerfect
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001A95 RID: 6805
		// (set) Token: 0x06001A96 RID: 6806
		public extern bool pixelPerfect
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001A97 RID: 6807
		// (set) Token: 0x06001A98 RID: 6808
		public extern float planeDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001A99 RID: 6809
		public extern int renderOrder
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001A9A RID: 6810
		// (set) Token: 0x06001A9B RID: 6811
		public extern bool overrideSorting
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001A9C RID: 6812
		// (set) Token: 0x06001A9D RID: 6813
		public extern int sortingOrder
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001A9E RID: 6814
		// (set) Token: 0x06001A9F RID: 6815
		public extern int sortingLayerID
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001AA0 RID: 6816
		public extern int cachedSortingLayerValue
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001AA1 RID: 6817
		// (set) Token: 0x06001AA2 RID: 6818
		public extern string sortingLayerName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001AA3 RID: 6819
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasMaterial();

		// Token: 0x06001AA4 RID: 6820
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Material GetDefaultCanvasTextMaterial();

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00026310 File Offset: 0x00024510
		private static void SendWillRenderCanvases()
		{
			if (Canvas.willRenderCanvases != null)
			{
				Canvas.willRenderCanvases();
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00026328 File Offset: 0x00024528
		public static void ForceUpdateCanvases()
		{
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x02000230 RID: 560
		// (Invoke) Token: 0x06001AE5 RID: 6885
		public delegate void WillRenderCanvases();
	}
}
