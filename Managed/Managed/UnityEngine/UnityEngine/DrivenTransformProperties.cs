using System;

namespace UnityEngine
{
	// Token: 0x0200021C RID: 540
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x04000848 RID: 2120
		None = 0,
		// Token: 0x04000849 RID: 2121
		All = -1,
		// Token: 0x0400084A RID: 2122
		AnchoredPositionX = 2,
		// Token: 0x0400084B RID: 2123
		AnchoredPositionY = 4,
		// Token: 0x0400084C RID: 2124
		AnchoredPositionZ = 8,
		// Token: 0x0400084D RID: 2125
		Rotation = 16,
		// Token: 0x0400084E RID: 2126
		ScaleX = 32,
		// Token: 0x0400084F RID: 2127
		ScaleY = 64,
		// Token: 0x04000850 RID: 2128
		ScaleZ = 128,
		// Token: 0x04000851 RID: 2129
		AnchorMinX = 256,
		// Token: 0x04000852 RID: 2130
		AnchorMinY = 512,
		// Token: 0x04000853 RID: 2131
		AnchorMaxX = 1024,
		// Token: 0x04000854 RID: 2132
		AnchorMaxY = 2048,
		// Token: 0x04000855 RID: 2133
		SizeDeltaX = 4096,
		// Token: 0x04000856 RID: 2134
		SizeDeltaY = 8192,
		// Token: 0x04000857 RID: 2135
		PivotX = 16384,
		// Token: 0x04000858 RID: 2136
		PivotY = 32768,
		// Token: 0x04000859 RID: 2137
		AnchoredPosition = 6,
		// Token: 0x0400085A RID: 2138
		AnchoredPosition3D = 14,
		// Token: 0x0400085B RID: 2139
		Scale = 224,
		// Token: 0x0400085C RID: 2140
		AnchorMin = 768,
		// Token: 0x0400085D RID: 2141
		AnchorMax = 3072,
		// Token: 0x0400085E RID: 2142
		Anchors = 3840,
		// Token: 0x0400085F RID: 2143
		SizeDelta = 12288,
		// Token: 0x04000860 RID: 2144
		Pivot = 49152
	}
}
