using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000039 RID: 57
	public class CanvasUpdateRegistry
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00005660 File Offset: 0x00003860
		protected CanvasUpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdate;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000056A4 File Offset: 0x000038A4
		public static CanvasUpdateRegistry instance
		{
			get
			{
				if (CanvasUpdateRegistry.s_Instance == null)
				{
					CanvasUpdateRegistry.s_Instance = new CanvasUpdateRegistry();
				}
				return CanvasUpdateRegistry.s_Instance;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000056C0 File Offset: 0x000038C0
		private bool ObjectValidForUpdate(ICanvasElement element)
		{
			bool flag = element != null;
			bool flag2 = element is Object;
			if (flag2)
			{
				flag = element as Object != null;
			}
			return flag;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000056F4 File Offset: 0x000038F4
		private void PerformUpdate()
		{
			this.m_LayoutRebuildQueue.RemoveAll((ICanvasElement x) => x == null || x.IsDestroyed());
			this.m_GraphicRebuildQueue.RemoveAll((ICanvasElement x) => x == null || x.IsDestroyed());
			this.m_PerformingLayoutUpdate = true;
			this.m_LayoutRebuildQueue.Sort(CanvasUpdateRegistry.s_SortLayoutFunction);
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
				{
					try
					{
						if (this.ObjectValidForUpdate(CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j]))
						{
							CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j].Rebuild((CanvasUpdate)i);
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j].transform);
					}
				}
			}
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Clear();
			this.m_PerformingLayoutUpdate = false;
			this.m_PerformingGraphicUpdate = true;
			for (int k = 3; k < 5; k++)
			{
				for (int l = 0; l < CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Count; l++)
				{
					try
					{
						ICanvasElement canvasElement = CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[l];
						if (this.ObjectValidForUpdate(canvasElement))
						{
							canvasElement.Rebuild((CanvasUpdate)k);
						}
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2, CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[l].transform);
					}
				}
			}
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Clear();
			this.m_PerformingGraphicUpdate = false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000058DC File Offset: 0x00003ADC
		private static int ParentCount(Transform child)
		{
			if (child == null)
			{
				return 0;
			}
			Transform transform = child.parent;
			int num = 0;
			while (transform != null)
			{
				num++;
				transform = transform.parent;
			}
			return num;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005920 File Offset: 0x00003B20
		private static int SortLayoutList(ICanvasElement x, ICanvasElement y)
		{
			Transform transform = x.transform;
			Transform transform2 = y.transform;
			return CanvasUpdateRegistry.ParentCount(transform) - CanvasUpdateRegistry.ParentCount(transform2);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005948 File Offset: 0x00003B48
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005958 File Offset: 0x00003B58
		private void InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			this.m_LayoutRebuildQueue.Add(element);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005968 File Offset: 0x00003B68
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005978 File Offset: 0x00003B78
		private void InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to add {0} for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", element));
				return;
			}
			this.m_GraphicRebuildQueue.Add(element);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000059C8 File Offset: 0x00003BC8
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000059F8 File Offset: 0x00003BF8
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005A28 File Offset: 0x00003C28
		public static bool IsRebuildingLayout()
		{
			return CanvasUpdateRegistry.instance.m_PerformingLayoutUpdate;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005A34 File Offset: 0x00003C34
		public static bool IsRebuildingGraphics()
		{
			return CanvasUpdateRegistry.instance.m_PerformingGraphicUpdate;
		}

		// Token: 0x040000A7 RID: 167
		private static CanvasUpdateRegistry s_Instance;

		// Token: 0x040000A8 RID: 168
		private bool m_PerformingLayoutUpdate;

		// Token: 0x040000A9 RID: 169
		private bool m_PerformingGraphicUpdate;

		// Token: 0x040000AA RID: 170
		private readonly IndexedSet<ICanvasElement> m_LayoutRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000AB RID: 171
		private readonly IndexedSet<ICanvasElement> m_GraphicRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000AC RID: 172
		private static readonly Comparison<ICanvasElement> s_SortLayoutFunction = new Comparison<ICanvasElement>(CanvasUpdateRegistry.SortLayoutList);
	}
}
