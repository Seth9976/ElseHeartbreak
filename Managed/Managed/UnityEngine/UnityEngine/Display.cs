using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015E RID: 350
	public sealed class Display
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x0001ED9C File Offset: 0x0001CF9C
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000F31 RID: 3889 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		// (remove) Token: 0x06000F32 RID: 3890 RVA: 0x0001EE00 File Offset: 0x0001D000
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0001EE18 File Offset: 0x0001D018
		public int renderingWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0001EE60 File Offset: 0x0001D060
		public int systemWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0001EE84 File Offset: 0x0001D084
		public int systemHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer2;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0001EF08 File Offset: 0x0001D108
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0001EF10 File Offset: 0x0001D110
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			Display.displays = new Display[nativeDisplay.Length];
			for (int i = 0; i < nativeDisplay.Length; i++)
			{
				Display.displays[i] = new Display(nativeDisplay[i]);
			}
			Display._mainDisplay = Display.displays[0];
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0001EF5C File Offset: 0x0001D15C
		private static void FireDisplaysUpdated()
		{
			if (Display.onDisplaysUpdated != null)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x06000F3E RID: 3902
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000F3F RID: 3903
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000F40 RID: 3904
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000F41 RID: 3905
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x06000F42 RID: 3906
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay);

		// Token: 0x040005E8 RID: 1512
		internal IntPtr nativeDisplay;

		// Token: 0x040005E9 RID: 1513
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x040005EA RID: 1514
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x0200022C RID: 556
		// (Invoke) Token: 0x06001AD5 RID: 6869
		public delegate void DisplaysUpdatedDelegate();
	}
}
