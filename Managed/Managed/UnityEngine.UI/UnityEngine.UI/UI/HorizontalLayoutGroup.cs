using System;

namespace UnityEngine.UI
{
	// Token: 0x0200007C RID: 124
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0001246C File Offset: 0x0001066C
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012474 File Offset: 0x00010674
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012484 File Offset: 0x00010684
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012490 File Offset: 0x00010690
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001249C File Offset: 0x0001069C
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
