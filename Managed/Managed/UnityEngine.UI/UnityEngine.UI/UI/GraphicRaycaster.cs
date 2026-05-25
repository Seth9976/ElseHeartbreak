using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200003E RID: 62
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("Event/Graphic Raycaster")]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000695C File Offset: 0x00004B5C
		protected GraphicRaycaster()
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000699C File Offset: 0x00004B9C
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000069CC File Offset: 0x00004BCC
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000069FC File Offset: 0x00004BFC
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006A04 File Offset: 0x00004C04
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006A10 File Offset: 0x00004C10
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00006A18 File Offset: 0x00004C18
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006A24 File Offset: 0x00004C24
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006A5C File Offset: 0x00004C5C
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			Vector2 vector;
			if (this.eventCamera == null)
			{
				vector = new Vector2(eventData.position.x / (float)Screen.width, eventData.position.y / (float)Screen.height);
			}
			else
			{
				vector = this.eventCamera.ScreenToViewportPoint(eventData.position);
			}
			if (vector.x < 0f || vector.x > 1f || vector.y < 0f || vector.y > 1f)
			{
				return;
			}
			float num = float.MaxValue;
			Ray ray = default(Ray);
			if (this.eventCamera != null)
			{
				ray = this.eventCamera.ScreenPointToRay(eventData.position);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float num2 = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
				RaycastHit raycastHit;
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && Physics.Raycast(ray, out raycastHit, num2, this.m_BlockingMask))
				{
					num = raycastHit.distance;
				}
				if (this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All)
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, num2, this.m_BlockingMask);
					if (raycastHit2D.collider != null)
					{
						num = raycastHit2D.fraction * num2;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, this.eventCamera, eventData.position, this.m_RaycastResults);
			for (int i = 0; i < this.m_RaycastResults.Count; i++)
			{
				GameObject gameObject = this.m_RaycastResults[i].gameObject;
				bool flag = true;
				if (this.ignoreReversedGraphics)
				{
					if (this.eventCamera == null)
					{
						Vector3 vector2 = gameObject.transform.rotation * Vector3.forward;
						flag = Vector3.Dot(Vector3.forward, vector2) > 0f;
					}
					else
					{
						Vector3 vector3 = this.eventCamera.transform.rotation * Vector3.forward;
						Vector3 vector4 = gameObject.transform.rotation * Vector3.forward;
						flag = Vector3.Dot(vector3, vector4) > 0f;
					}
				}
				if (flag)
				{
					float num3;
					if (this.eventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						num3 = 0f;
					}
					else
					{
						Transform transform = gameObject.transform;
						Vector3 forward = transform.forward;
						num3 = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
						if (num3 < 0f)
						{
							goto IL_03B8;
						}
					}
					if (num3 < num)
					{
						RaycastResult raycastResult = new RaycastResult
						{
							gameObject = gameObject,
							module = this,
							distance = num3,
							screenPosition = eventData.position,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[i].depth,
							sortingLayer = this.canvas.cachedSortingLayerValue,
							sortingOrder = this.canvas.sortingOrder
						};
						resultAppendList.Add(raycastResult);
					}
				}
				IL_03B8:;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006E3C File Offset: 0x0000503C
		public override Camera eventCamera
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || (this.canvas.renderMode == RenderMode.ScreenSpaceCamera && this.canvas.worldCamera == null))
				{
					return null;
				}
				return (!(this.canvas.worldCamera != null)) ? Camera.main : this.canvas.worldCamera;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006EB0 File Offset: 0x000050B0
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, List<Graphic> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			GraphicRaycaster.s_SortedGraphics.Clear();
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1)
				{
					if (RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera))
					{
						if (graphic.Raycast(pointerPosition, eventCamera))
						{
							GraphicRaycaster.s_SortedGraphics.Add(graphic);
						}
					}
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			for (int j = 0; j < GraphicRaycaster.s_SortedGraphics.Count; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
		}

		// Token: 0x040000D1 RID: 209
		protected const int kNoEventMaskSet = -1;

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		[FormerlySerializedAs("ignoreReversedGraphics")]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x040000D3 RID: 211
		[FormerlySerializedAs("blockingObjects")]
		[SerializeField]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x040000D5 RID: 213
		private Canvas m_Canvas;

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x0200003F RID: 63
		public enum BlockingObjects
		{
			// Token: 0x040000DA RID: 218
			None,
			// Token: 0x040000DB RID: 219
			TwoD,
			// Token: 0x040000DC RID: 220
			ThreeD,
			// Token: 0x040000DD RID: 221
			All
		}
	}
}
