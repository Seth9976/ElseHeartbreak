using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000059 RID: 89
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Scrollbar", 32)]
	public class Scrollbar : Selectable, IEventSystemHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, ICanvasElement
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000CF00 File Offset: 0x0000B100
		protected Scrollbar()
		{
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CF40 File Offset: 0x0000B140
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000CF48 File Offset: 0x0000B148
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CF68 File Offset: 0x0000B168
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000CF70 File Offset: 0x0000B170
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000CF8C File Offset: 0x0000B18C
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000D004 File Offset: 0x0000B204
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000D02C File Offset: 0x0000B22C
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000D034 File Offset: 0x0000B234
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000D040 File Offset: 0x0000B240
		private float stepSize
		{
			get
			{
				return (this.m_NumberOfSteps <= 1) ? 0.1f : (1f / (float)(this.m_NumberOfSteps - 1));
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D068 File Offset: 0x0000B268
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D06C File Offset: 0x0000B26C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D090 File Offset: 0x0000B290
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
			}
			else
			{
				this.m_ContainerRect = null;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D108 File Offset: 0x0000B308
		private void Set(float input, bool sendCallback)
		{
			float value = this.m_Value;
			this.m_Value = Mathf.Clamp01(input);
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D154 File Offset: 0x0000B354
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000D170 File Offset: 0x0000B370
		private Scrollbar.Axis axis
		{
			get
			{
				return (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft) ? Scrollbar.Axis.Vertical : Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000D190 File Offset: 0x0000B390
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = this.value * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D288 File Offset: 0x0000B488
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			Vector2 vector2 = vector - this.m_Offset - this.m_ContainerRect.rect.position;
			Vector2 vector3 = vector2 - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = ((this.axis != Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.height : this.m_ContainerRect.rect.width);
			float num2 = num * (1f - this.size);
			if (num2 <= 0f)
			{
				return;
			}
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(vector3.x / num2);
				break;
			case Scrollbar.Direction.RightToLeft:
				this.Set(1f - vector3.x / num2);
				break;
			case Scrollbar.Direction.BottomToTop:
				this.Set(vector3.y / num2);
				break;
			case Scrollbar.Direction.TopToBottom:
				this.Set(1f - vector3.y / num2);
				break;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D408 File Offset: 0x0000B608
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D438 File Offset: 0x0000B638
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 vector;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out vector))
			{
				this.m_Offset = vector - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D534 File Offset: 0x0000B734
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 localMousePos;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out localMousePos))
				{
					float axisCoordinate = ((this.axis != Scrollbar.Axis.Horizontal) ? localMousePos.y : localMousePos.x);
					if (axisCoordinate < 0f)
					{
						this.value -= this.size;
					}
					else
					{
						this.value += this.size;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D560 File Offset: 0x0000B760
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D570 File Offset: 0x0000B770
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D734 File Offset: 0x0000B934
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D768 File Offset: 0x0000B968
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D79C File Offset: 0x0000B99C
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D818 File Offset: 0x0000BA18
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D884 File Offset: 0x0000BA84
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D88C File Offset: 0x0000BA8C
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Value = 1f;

		// Token: 0x0400016C RID: 364
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x0400016D RID: 365
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x0400016E RID: 366
		[SerializeField]
		[Space(6f)]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x0400016F RID: 367
		private RectTransform m_ContainerRect;

		// Token: 0x04000170 RID: 368
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000171 RID: 369
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000172 RID: 370
		private Coroutine m_PointerDownRepeat;

		// Token: 0x04000173 RID: 371
		private bool isPointerDownAndNotDragging;

		// Token: 0x0200005A RID: 90
		public enum Direction
		{
			// Token: 0x04000175 RID: 373
			LeftToRight,
			// Token: 0x04000176 RID: 374
			RightToLeft,
			// Token: 0x04000177 RID: 375
			BottomToTop,
			// Token: 0x04000178 RID: 376
			TopToBottom
		}

		// Token: 0x0200005B RID: 91
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x0200005C RID: 92
		private enum Axis
		{
			// Token: 0x0400017A RID: 378
			Horizontal,
			// Token: 0x0400017B RID: 379
			Vertical
		}
	}
}
