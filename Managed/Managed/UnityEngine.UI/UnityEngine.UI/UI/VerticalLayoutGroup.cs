using System;

namespace UnityEngine.UI
{
	// Token: 0x02000087 RID: 135
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00013684 File Offset: 0x00011884
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001368C File Offset: 0x0001188C
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001369C File Offset: 0x0001189C
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000136A8 File Offset: 0x000118A8
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000136B4 File Offset: 0x000118B4
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
