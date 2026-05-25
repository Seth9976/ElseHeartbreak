using System;

namespace UnityEngine
{
	// Token: 0x020000F6 RID: 246
	public sealed class GUILayoutOption
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x00013F20 File Offset: 0x00012120
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x0400033E RID: 830
		internal GUILayoutOption.Type type;

		// Token: 0x0400033F RID: 831
		internal object value;

		// Token: 0x020000F7 RID: 247
		internal enum Type
		{
			// Token: 0x04000341 RID: 833
			fixedWidth,
			// Token: 0x04000342 RID: 834
			fixedHeight,
			// Token: 0x04000343 RID: 835
			minWidth,
			// Token: 0x04000344 RID: 836
			maxWidth,
			// Token: 0x04000345 RID: 837
			minHeight,
			// Token: 0x04000346 RID: 838
			maxHeight,
			// Token: 0x04000347 RID: 839
			stretchWidth,
			// Token: 0x04000348 RID: 840
			stretchHeight,
			// Token: 0x04000349 RID: 841
			alignStart,
			// Token: 0x0400034A RID: 842
			alignMiddle,
			// Token: 0x0400034B RID: 843
			alignEnd,
			// Token: 0x0400034C RID: 844
			alignJustify,
			// Token: 0x0400034D RID: 845
			equalSize,
			// Token: 0x0400034E RID: 846
			spacing
		}
	}
}
