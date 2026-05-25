using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000064 RID: 100
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Slider", 34)]
	public class Slider : Selectable, IEventSystemHandler, IInitializePotentialDragHandler, IDragHandler, ICanvasElement
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000F888 File Offset: 0x0000DA88
		protected Slider()
		{
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000F918 File Offset: 0x0000DB18
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000F920 File Offset: 0x0000DB20
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000F93C File Offset: 0x0000DB3C
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000F944 File Offset: 0x0000DB44
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000F96C File Offset: 0x0000DB6C
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000F974 File Offset: 0x0000DB74
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000F99C File Offset: 0x0000DB9C
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		public float value
		{
			get
			{
				if (this.wholeNumbers)
				{
					return Mathf.Round(this.m_Value);
				}
				return this.m_Value;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000FA60 File Offset: 0x0000DC60
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public Slider.SliderEvent onValueChanged
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000FA74 File Offset: 0x0000DC74
		private float stepSize
		{
			get
			{
				return (!this.wholeNumbers) ? ((this.maxValue - this.minValue) * 0.1f) : 1f;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		private void Set(float input, bool sendCallback)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(num);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000FC48 File Offset: 0x0000DE48
		private Slider.Axis axis
		{
			get
			{
				return (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft) ? Slider.Axis.Vertical : Slider.Axis.Horizontal;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000FC68 File Offset: 0x0000DE68
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000FC84 File Offset: 0x0000DE84
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					one[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				int axis = (int)this.axis;
				float num = ((!this.reverseValue) ? this.normalizedValue : (1f - this.normalizedValue));
				one2[(int)this.axis] = num;
				zero2[axis] = num;
				this.m_HandleRect.anchorMin = zero2;
				this.m_HandleRect.anchorMax = one2;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform rectTransform = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)this.axis] > 0f)
			{
				Vector2 vector;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, cam, out vector))
				{
					return;
				}
				vector -= rectTransform.rect.position;
				float num = Mathf.Clamp01((vector - this.m_Offset)[(int)this.axis] / rectTransform.rect.size[(int)this.axis]);
				this.normalizedValue = ((!this.reverseValue) ? num : (1f - num));
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000FECC File Offset: 0x0000E0CC
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out vector))
				{
					this.m_Offset = vector;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000FF8C File Offset: 0x0000E18C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
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
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
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

		// Token: 0x06000370 RID: 880 RVA: 0x0001016C File Offset: 0x0000E36C
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001020C File Offset: 0x0000E40C
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00010244 File Offset: 0x0000E444
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010250 File Offset: 0x0000E450
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis axis = this.axis;
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

		// Token: 0x06000376 RID: 886 RVA: 0x000102BC File Offset: 0x0000E4BC
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000102C4 File Offset: 0x0000E4C4
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001AF RID: 431
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		[Space(6f)]
		private Slider.Direction m_Direction;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		private float m_MinValue;

		// Token: 0x040001B3 RID: 435
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x040001B4 RID: 436
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x040001B5 RID: 437
		[SerializeField]
		private float m_Value = 1f;

		// Token: 0x040001B6 RID: 438
		[Space(6f)]
		[SerializeField]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x040001B7 RID: 439
		private Image m_FillImage;

		// Token: 0x040001B8 RID: 440
		private Transform m_FillTransform;

		// Token: 0x040001B9 RID: 441
		private RectTransform m_FillContainerRect;

		// Token: 0x040001BA RID: 442
		private Transform m_HandleTransform;

		// Token: 0x040001BB RID: 443
		private RectTransform m_HandleContainerRect;

		// Token: 0x040001BC RID: 444
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040001BD RID: 445
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000065 RID: 101
		public enum Direction
		{
			// Token: 0x040001BF RID: 447
			LeftToRight,
			// Token: 0x040001C0 RID: 448
			RightToLeft,
			// Token: 0x040001C1 RID: 449
			BottomToTop,
			// Token: 0x040001C2 RID: 450
			TopToBottom
		}

		// Token: 0x02000066 RID: 102
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}

		// Token: 0x02000067 RID: 103
		private enum Axis
		{
			// Token: 0x040001C4 RID: 452
			Horizontal,
			// Token: 0x040001C5 RID: 453
			Vertical
		}
	}
}
