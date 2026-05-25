using System;

namespace UnityEngine.UI
{
	// Token: 0x0200007D RID: 125
	public abstract class HorizontalOrVerticalLayoutGroup : LayoutGroup
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000124C0 File Offset: 0x000106C0
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x000124C8 File Offset: 0x000106C8
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000124D8 File Offset: 0x000106D8
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x000124E0 File Offset: 0x000106E0
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000124F0 File Offset: 0x000106F0
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000124F8 File Offset: 0x000106F8
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00012508 File Offset: 0x00010708
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float num = (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal);
			float num2 = num;
			float num3 = num;
			float num4 = 0f;
			bool flag = isVertical ^ (axis == 1);
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				RectTransform rectTransform = base.rectChildren[i];
				float minSize = LayoutUtility.GetMinSize(rectTransform, axis);
				float preferredSize = LayoutUtility.GetPreferredSize(rectTransform, axis);
				float num5 = LayoutUtility.GetFlexibleSize(rectTransform, axis);
				if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
				{
					num5 = Mathf.Max(num5, 1f);
				}
				if (flag)
				{
					num2 = Mathf.Max(minSize + num, num2);
					num3 = Mathf.Max(preferredSize + num, num3);
					num4 = Mathf.Max(num5, num4);
				}
				else
				{
					num2 += minSize + this.spacing;
					num3 += preferredSize + this.spacing;
					num4 += num5;
				}
			}
			if (!flag && base.rectChildren.Count > 0)
			{
				num2 -= this.spacing;
				num3 -= this.spacing;
			}
			num3 = Mathf.Max(num2, num3);
			base.SetLayoutInputForAxis(num2, num3, num4, axis);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00012648 File Offset: 0x00010848
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float num = base.rectTransform.rect.size[axis];
			bool flag = isVertical ^ (axis == 1);
			if (flag)
			{
				float num2 = num - (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal);
				for (int i = 0; i < base.rectChildren.Count; i++)
				{
					RectTransform rectTransform = base.rectChildren[i];
					float minSize = LayoutUtility.GetMinSize(rectTransform, axis);
					float preferredSize = LayoutUtility.GetPreferredSize(rectTransform, axis);
					float num3 = LayoutUtility.GetFlexibleSize(rectTransform, axis);
					if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
					{
						num3 = Mathf.Max(num3, 1f);
					}
					float num4 = Mathf.Clamp(num2, minSize, (num3 <= 0f) ? preferredSize : num);
					float startOffset = base.GetStartOffset(axis, num4);
					base.SetChildAlongAxis(rectTransform, axis, startOffset, num4);
				}
			}
			else
			{
				float num5 = (float)((axis != 0) ? base.padding.top : base.padding.left);
				if (base.GetTotalFlexibleSize(axis) == 0f && base.GetTotalPreferredSize(axis) < num)
				{
					num5 = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis != 0) ? base.padding.vertical : base.padding.horizontal));
				}
				float num6 = 0f;
				if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
				{
					num6 = Mathf.Clamp01((num - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
				}
				float num7 = 0f;
				if (num > base.GetTotalPreferredSize(axis) && base.GetTotalFlexibleSize(axis) > 0f)
				{
					num7 = (num - base.GetTotalPreferredSize(axis)) / base.GetTotalFlexibleSize(axis);
				}
				for (int j = 0; j < base.rectChildren.Count; j++)
				{
					RectTransform rectTransform2 = base.rectChildren[j];
					float minSize2 = LayoutUtility.GetMinSize(rectTransform2, axis);
					float preferredSize2 = LayoutUtility.GetPreferredSize(rectTransform2, axis);
					float num8 = LayoutUtility.GetFlexibleSize(rectTransform2, axis);
					if ((axis != 0) ? this.childForceExpandHeight : this.childForceExpandWidth)
					{
						num8 = Mathf.Max(num8, 1f);
					}
					float num9 = Mathf.Lerp(minSize2, preferredSize2, num6);
					num9 += num8 * num7;
					base.SetChildAlongAxis(rectTransform2, axis, num5, num9);
					num5 += num9 + this.spacing;
				}
			}
		}

		// Token: 0x04000220 RID: 544
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x04000221 RID: 545
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x04000222 RID: 546
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;
	}
}
