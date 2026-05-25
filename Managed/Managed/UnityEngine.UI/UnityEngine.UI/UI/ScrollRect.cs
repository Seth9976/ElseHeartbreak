using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200005D RID: 93
	[SelectionBase]
	[AddComponentMenu("UI/Scroll Rect", 33)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IEventSystemHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IScrollHandler, ICanvasElement
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000D89C File Offset: 0x0000BA9C
		protected ScrollRect()
		{
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000D924 File Offset: 0x0000BB24
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000D92C File Offset: 0x0000BB2C
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000D938 File Offset: 0x0000BB38
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000D940 File Offset: 0x0000BB40
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000D94C File Offset: 0x0000BB4C
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000D954 File Offset: 0x0000BB54
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000D960 File Offset: 0x0000BB60
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000D968 File Offset: 0x0000BB68
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000D974 File Offset: 0x0000BB74
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000D988 File Offset: 0x0000BB88
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000D990 File Offset: 0x0000BB90
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000D99C File Offset: 0x0000BB9C
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000DA38 File Offset: 0x0000BC38
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public ScrollRect.ScrollRectEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000DB00 File Offset: 0x0000BD00
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing != CanvasUpdate.PostLayout)
			{
				return;
			}
			this.UpdateBounds();
			this.UpdateScrollbars(Vector2.zero);
			this.UpdatePrevData();
			this.m_HasRebuiltLayout = true;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000DB40 File Offset: 0x0000BD40
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_HasRebuiltLayout = false;
			base.OnDisable();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000DC48 File Offset: 0x0000BE48
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000DC74 File Offset: 0x0000BE74
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DE20 File Offset: 0x0000C020
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000DE38 File Offset: 0x0000C038
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 vector2 = vector - this.m_PointerStartLocalCursor;
			Vector2 vector3 = this.m_ContentStartPosition + vector2;
			Vector2 vector4 = this.CalculateOffset(vector3 - this.m_Content.anchoredPosition);
			vector3 += vector4;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (vector4.x != 0f)
				{
					vector3.x -= ScrollRect.RubberDelta(vector4.x, this.m_ViewBounds.size.x);
				}
				if (vector4.y != 0f)
				{
					vector3.y -= ScrollRect.RubberDelta(vector4.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector3);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000DF50 File Offset: 0x0000C150
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
			{
				Vector2 vector2 = this.m_Content.anchoredPosition;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
					{
						float num = this.m_Velocity[i];
						vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref num, this.m_Elasticity, float.PositiveInfinity, unscaledDeltaTime);
						this.m_Velocity[i] = num;
					}
					else if (this.m_Inertia)
					{
						ref Vector2 ptr = ref this.m_Velocity;
						int num3;
						int num2 = (num3 = i);
						float num4 = ptr[num3];
						this.m_Velocity[num2] = num4 * Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
						if (Mathf.Abs(this.m_Velocity[i]) < 1f)
						{
							this.m_Velocity[i] = 0f;
						}
						ref Vector2 ptr2 = ref vector2;
						int num5 = (num3 = i);
						num4 = ptr2[num3];
						vector2[num5] = num4 + this.m_Velocity[i] * unscaledDeltaTime;
					}
					else
					{
						this.m_Velocity[i] = 0f;
					}
				}
				if (this.m_Velocity != Vector2.zero)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
				}
			}
			if (this.m_Dragging && this.m_Inertia)
			{
				Vector3 vector3 = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
				this.m_Velocity = Vector3.Lerp(this.m_Velocity, vector3, unscaledDeltaTime * 10f);
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E294 File Offset: 0x0000C494
		private void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E42C File Offset: 0x0000C62C
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000E440 File Offset: 0x0000C640
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000E460 File Offset: 0x0000C660
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000E528 File Offset: 0x0000C728
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x)
				{
					return (float)((this.m_ViewBounds.min.x <= this.m_ContentBounds.min.x) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000E534 File Offset: 0x0000C734
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y)
				{
					return (float)((this.m_ViewBounds.min.y <= this.m_ContentBounds.min.y) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E608 File Offset: 0x0000C808
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E614 File Offset: 0x0000C814
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E620 File Offset: 0x0000C820
		private void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.localPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 localPosition = this.m_Content.localPosition;
			if (Mathf.Abs(localPosition[axis] - num3) > 0.01f)
			{
				localPosition[axis] = num3;
				this.m_Content.localPosition = localPosition;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E700 File Offset: 0x0000C900
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E72C File Offset: 0x0000C92C
		private void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 center = this.m_ContentBounds.center;
			Vector3 vector = this.m_ViewBounds.size - size;
			if (vector.x > 0f)
			{
				center.x -= vector.x * (this.m_Content.pivot.x - 0.5f);
				size.x = this.m_ViewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				center.y -= vector.y * (this.m_Content.pivot.y - 0.5f);
				size.y = this.m_ViewBounds.size.y;
			}
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = center;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E890 File Offset: 0x0000CA90
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			this.m_Content.GetWorldCorners(this.m_Corners);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector3 = worldToLocalMatrix.MultiplyPoint3x4(this.m_Corners[i]);
				vector = Vector3.Min(vector3, vector);
				vector2 = Vector3.Max(vector3, vector2);
			}
			Bounds bounds = new Bounds(vector, Vector3.zero);
			bounds.Encapsulate(vector2);
			return bounds;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E958 File Offset: 0x0000CB58
		private Vector2 CalculateOffset(Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (this.m_MovementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = this.m_ContentBounds.min;
			Vector2 vector2 = this.m_ContentBounds.max;
			if (this.m_Horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				if (vector.x > this.m_ViewBounds.min.x)
				{
					zero.x = this.m_ViewBounds.min.x - vector.x;
				}
				else if (vector2.x < this.m_ViewBounds.max.x)
				{
					zero.x = this.m_ViewBounds.max.x - vector2.x;
				}
			}
			if (this.m_Vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				if (vector2.y < this.m_ViewBounds.max.y)
				{
					zero.y = this.m_ViewBounds.max.y - vector2.y;
				}
				else if (vector.y > this.m_ViewBounds.min.y)
				{
					zero.y = this.m_ViewBounds.min.y - vector.y;
				}
			}
			return zero;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EB24 File Offset: 0x0000CD24
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400017C RID: 380
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x0400017F RID: 383
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x04000182 RID: 386
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x04000183 RID: 387
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x04000184 RID: 388
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x04000185 RID: 389
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x04000186 RID: 390
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x04000187 RID: 391
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x04000188 RID: 392
		private Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x04000189 RID: 393
		private RectTransform m_ViewRect;

		// Token: 0x0400018A RID: 394
		private Bounds m_ContentBounds;

		// Token: 0x0400018B RID: 395
		private Bounds m_ViewBounds;

		// Token: 0x0400018C RID: 396
		private Vector2 m_Velocity;

		// Token: 0x0400018D RID: 397
		private bool m_Dragging;

		// Token: 0x0400018E RID: 398
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x0400018F RID: 399
		private Bounds m_PrevContentBounds;

		// Token: 0x04000190 RID: 400
		private Bounds m_PrevViewBounds;

		// Token: 0x04000191 RID: 401
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x04000192 RID: 402
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x0200005E RID: 94
		public enum MovementType
		{
			// Token: 0x04000194 RID: 404
			Unrestricted,
			// Token: 0x04000195 RID: 405
			Elastic,
			// Token: 0x04000196 RID: 406
			Clamped
		}

		// Token: 0x0200005F RID: 95
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
