using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000085 RID: 133
	public sealed class SystemInfo
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BC RID: 700
		public static extern string operatingSystem
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BD RID: 701
		public static extern string processorType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BE RID: 702
		public static extern int processorCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BF RID: 703
		public static extern int systemMemorySize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002C0 RID: 704
		public static extern int graphicsMemorySize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C1 RID: 705
		public static extern string graphicsDeviceName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C2 RID: 706
		public static extern string graphicsDeviceVendor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002C3 RID: 707
		public static extern int graphicsDeviceID
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002C4 RID: 708
		public static extern int graphicsDeviceVendorID
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C5 RID: 709
		public static extern string graphicsDeviceVersion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C6 RID: 710
		public static extern int graphicsShaderLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C7 RID: 711
		public static extern int graphicsPixelFillrate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C8 RID: 712
		public static extern bool supportsShadows
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C9 RID: 713
		public static extern bool supportsRenderTextures
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002CA RID: 714
		public static extern bool supportsRenderToCubemap
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002CB RID: 715
		public static extern bool supportsImageEffects
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002CC RID: 716
		public static extern bool supports3DTextures
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002CD RID: 717
		public static extern bool supportsComputeShaders
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002CE RID: 718
		public static extern bool supportsInstancing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CF RID: 719
		public static extern bool supportsSparseTextures
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002D0 RID: 720
		public static extern int supportedRenderTargetCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D1 RID: 721
		public static extern int supportsStencil
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002D2 RID: 722
		public static extern bool supportsVertexPrograms
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002D3 RID: 723
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsRenderTextureFormat(RenderTextureFormat format);

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002D4 RID: 724
		public static extern NPOTSupport npotSupport
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D5 RID: 725
		public static extern string deviceUniqueIdentifier
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D6 RID: 726
		public static extern string deviceName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D7 RID: 727
		public static extern string deviceModel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D8 RID: 728
		public static extern bool supportsAccelerometer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D9 RID: 729
		public static extern bool supportsGyroscope
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002DA RID: 730
		public static extern bool supportsLocationService
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002DB RID: 731
		public static extern bool supportsVibration
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002DC RID: 732
		public static extern DeviceType deviceType
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DD RID: 733
		public static extern int maxTextureSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
