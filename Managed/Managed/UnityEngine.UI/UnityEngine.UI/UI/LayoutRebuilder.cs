using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x02000085 RID: 133
	public struct LayoutRebuilder : ICanvasElement, IEquatable<LayoutRebuilder>
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00012EC8 File Offset: 0x000110C8
		private LayoutRebuilder(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = this.m_ToRebuild.GetHashCode();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012EE4 File Offset: 0x000110E4
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00012EF8 File Offset: 0x000110F8
		void ICanvasElement.Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012FBC File Offset: 0x000111BC
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00012FC4 File Offset: 0x000111C4
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012FCC File Offset: 0x000111CC
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012FDC File Offset: 0x000111DC
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && (!(e as Behaviour).enabled || !(e as Behaviour).isActiveAndEnabled));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013010 File Offset: 0x00011210
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						action(list[j]);
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000130F0 File Offset: 0x000112F0
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013188 File Offset: 0x00011388
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null)
			{
				return;
			}
			RectTransform rectTransform = rect;
			for (;;)
			{
				RectTransform rectTransform2 = rectTransform.parent as RectTransform;
				if (!LayoutRebuilder.ValidLayoutGroup(rectTransform2))
				{
					break;
				}
				rectTransform = rectTransform2;
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform))
			{
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000131E8 File Offset: 0x000113E8
		private static bool ValidLayoutGroup(RectTransform parent)
		{
			if (parent == null)
			{
				return false;
			}
			List<Component> list = ComponentListPool.Get();
			parent.GetComponents(typeof(ILayoutGroup), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			bool flag = list.Count > 0;
			ComponentListPool.Release(list);
			return flag;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013234 File Offset: 0x00011434
		private static bool ValidController(RectTransform layoutRoot)
		{
			if (layoutRoot == null)
			{
				return false;
			}
			List<Component> list = ComponentListPool.Get();
			layoutRoot.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			bool flag = list.Count > 0;
			ComponentListPool.Release(list);
			return flag;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00013280 File Offset: 0x00011480
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(new LayoutRebuilder(controller));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000132A0 File Offset: 0x000114A0
		public bool Equals(LayoutRebuilder other)
		{
			return this.m_ToRebuild == other.m_ToRebuild;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000132B4 File Offset: 0x000114B4
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000132BC File Offset: 0x000114BC
		public override string ToString()
		{
			return "(Layout Rebuilder for) " + this.m_ToRebuild;
		}

		// Token: 0x04000232 RID: 562
		private readonly RectTransform m_ToRebuild;

		// Token: 0x04000233 RID: 563
		private readonly int m_CachedHashFromTransform;
	}
}
