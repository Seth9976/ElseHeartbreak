using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000086 RID: 134
	public static class LayoutUtility
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00013344 File Offset: 0x00011544
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetMinWidth(rect);
			}
			return LayoutUtility.GetMinHeight(rect);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001335C File Offset: 0x0001155C
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetPreferredWidth(rect);
			}
			return LayoutUtility.GetPreferredHeight(rect);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013374 File Offset: 0x00011574
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetFlexibleWidth(rect);
			}
			return LayoutUtility.GetFlexibleHeight(rect);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001338C File Offset: 0x0001158C
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000133C4 File Offset: 0x000115C4
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00013428 File Offset: 0x00011628
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013460 File Offset: 0x00011660
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013498 File Offset: 0x00011698
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000134FC File Offset: 0x000116FC
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013534 File Offset: 0x00011734
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001354C File Offset: 0x0001174C
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			for (int i = 0; i < list.Count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((layoutElement as Behaviour).enabled && (layoutElement as Behaviour).isActiveAndEnabled))
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			ComponentListPool.Release(list);
			return num;
		}
	}
}
