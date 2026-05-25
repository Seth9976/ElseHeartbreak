using System;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	public enum TextureFormat
	{
		// Token: 0x0400000D RID: 13
		Alpha8 = 1,
		// Token: 0x0400000E RID: 14
		ARGB4444,
		// Token: 0x0400000F RID: 15
		RGB24,
		// Token: 0x04000010 RID: 16
		RGBA32,
		// Token: 0x04000011 RID: 17
		ARGB32,
		// Token: 0x04000012 RID: 18
		RGB565 = 7,
		// Token: 0x04000013 RID: 19
		DXT1 = 10,
		// Token: 0x04000014 RID: 20
		DXT5 = 12,
		// Token: 0x04000015 RID: 21
		RGBA4444,
		// Token: 0x04000016 RID: 22
		BGRA32,
		// Token: 0x04000017 RID: 23
		PVRTC_RGB2 = 30,
		// Token: 0x04000018 RID: 24
		PVRTC_RGBA2,
		// Token: 0x04000019 RID: 25
		PVRTC_RGB4,
		// Token: 0x0400001A RID: 26
		PVRTC_RGBA4,
		// Token: 0x0400001B RID: 27
		ETC_RGB4,
		// Token: 0x0400001C RID: 28
		ATC_RGB4,
		// Token: 0x0400001D RID: 29
		ATC_RGBA8,
		// Token: 0x0400001E RID: 30
		ATF_RGB_DXT1 = 38,
		// Token: 0x0400001F RID: 31
		ATF_RGBA_JPG,
		// Token: 0x04000020 RID: 32
		ATF_RGB_JPG,
		// Token: 0x04000021 RID: 33
		EAC_R,
		// Token: 0x04000022 RID: 34
		EAC_R_SIGNED,
		// Token: 0x04000023 RID: 35
		EAC_RG,
		// Token: 0x04000024 RID: 36
		EAC_RG_SIGNED,
		// Token: 0x04000025 RID: 37
		ETC2_RGB,
		// Token: 0x04000026 RID: 38
		ETC2_RGBA1,
		// Token: 0x04000027 RID: 39
		ETC2_RGBA8,
		// Token: 0x04000028 RID: 40
		ASTC_RGB_4x4,
		// Token: 0x04000029 RID: 41
		ASTC_RGB_5x5,
		// Token: 0x0400002A RID: 42
		ASTC_RGB_6x6,
		// Token: 0x0400002B RID: 43
		ASTC_RGB_8x8,
		// Token: 0x0400002C RID: 44
		ASTC_RGB_10x10,
		// Token: 0x0400002D RID: 45
		ASTC_RGB_12x12,
		// Token: 0x0400002E RID: 46
		ASTC_RGBA_4x4,
		// Token: 0x0400002F RID: 47
		ASTC_RGBA_5x5,
		// Token: 0x04000030 RID: 48
		ASTC_RGBA_6x6,
		// Token: 0x04000031 RID: 49
		ASTC_RGBA_8x8,
		// Token: 0x04000032 RID: 50
		ASTC_RGBA_10x10,
		// Token: 0x04000033 RID: 51
		ASTC_RGBA_12x12,
		// Token: 0x04000034 RID: 52
		[Obsolete("Use PVRTC_RGB2")]
		PVRTC_2BPP_RGB = 30,
		// Token: 0x04000035 RID: 53
		[Obsolete("Use PVRTC_RGBA2")]
		PVRTC_2BPP_RGBA,
		// Token: 0x04000036 RID: 54
		[Obsolete("Use PVRTC_RGB4")]
		PVRTC_4BPP_RGB,
		// Token: 0x04000037 RID: 55
		[Obsolete("Use PVRTC_RGBA4")]
		PVRTC_4BPP_RGBA
	}
}
