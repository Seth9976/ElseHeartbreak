using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000040 RID: 64
	public class GraphicRegistry
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00006FA4 File Offset: 0x000051A4
		protected GraphicRegistry()
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006FD0 File Offset: 0x000051D0
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006FEC File Offset: 0x000051EC
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.Add(graphic);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, indexedSet);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007048 File Offset: 0x00005248
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007084 File Offset: 0x00005284
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out indexedSet))
			{
				return indexedSet;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x040000DE RID: 222
		private static GraphicRegistry s_Instance;

		// Token: 0x040000DF RID: 223
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x040000E0 RID: 224
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}
