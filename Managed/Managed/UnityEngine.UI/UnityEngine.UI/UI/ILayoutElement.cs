using System;

namespace UnityEngine.UI
{
	// Token: 0x0200007E RID: 126
	public interface ILayoutElement
	{
		// Token: 0x0600042C RID: 1068
		void CalculateLayoutInputHorizontal();

		// Token: 0x0600042D RID: 1069
		void CalculateLayoutInputVertical();

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042E RID: 1070
		float minWidth { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042F RID: 1071
		float preferredWidth { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000430 RID: 1072
		float flexibleWidth { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000431 RID: 1073
		float minHeight { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000432 RID: 1074
		float preferredHeight { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000433 RID: 1075
		float flexibleHeight { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000434 RID: 1076
		int layoutPriority { get; }
	}
}
