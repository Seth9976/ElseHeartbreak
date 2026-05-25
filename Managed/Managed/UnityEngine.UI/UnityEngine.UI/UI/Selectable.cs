using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000060 RID: 96
	[SelectionBase]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Selectable", 70)]
	public class Selectable : UIBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, IMoveHandler
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000EB34 File Offset: 0x0000CD34
		protected Selectable()
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000EB94 File Offset: 0x0000CD94
		public static List<Selectable> allSelectables
		{
			get
			{
				return Selectable.s_List;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000EC08 File Offset: 0x0000CE08
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000EC50 File Offset: 0x0000CE50
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000EC74 File Offset: 0x0000CE74
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000EC98 File Offset: 0x0000CE98
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		private bool isPointerInside { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		private bool isPointerDown { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		private bool hasSelection { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000ED18 File Offset: 0x0000CF18
		protected override void OnCanvasGroupChanged()
		{
			bool flag = true;
			Transform transform = base.transform;
			while (transform != null)
			{
				transform.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				bool flag2 = false;
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (!this.m_CanvasGroupCache[i].interactable)
					{
						flag = false;
						flag2 = true;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					break;
				}
				transform = transform.parent;
			}
			if (flag != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = flag;
				this.OnSetProperty();
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		protected override void OnEnable()
		{
			base.OnEnable();
			Selectable.s_List.Add(this);
			Selectable.SelectionState selectionState = Selectable.SelectionState.Normal;
			if (this.hasSelection)
			{
				selectionState = Selectable.SelectionState.Highlighted;
			}
			this.m_CurrentSelectionState = selectionState;
			this.InternalEvaluateAndTransitionToSelectionState(true);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000EE24 File Offset: 0x0000D024
		private void OnSetProperty()
		{
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000EE30 File Offset: 0x0000D030
		protected override void OnDisable()
		{
			Selectable.s_List.Remove(this);
			this.InstantClearState();
			base.OnDisable();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000EE4C File Offset: 0x0000D04C
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				return this.m_CurrentSelectionState;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000EE54 File Offset: 0x0000D054
		protected virtual void InstantClearState()
		{
			string normalTrigger = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				break;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				break;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(normalTrigger);
				break;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000EECC File Offset: 0x0000D0CC
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			Color color;
			Sprite sprite;
			string text;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				color = this.m_Colors.normalColor;
				sprite = null;
				text = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				color = this.m_Colors.highlightedColor;
				sprite = this.m_SpriteState.highlightedSprite;
				text = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				color = this.m_Colors.pressedColor;
				sprite = this.m_SpriteState.pressedSprite;
				text = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				color = this.m_Colors.disabledColor;
				sprite = this.m_SpriteState.disabledSprite;
				text = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				color = Color.black;
				sprite = null;
				text = string.Empty;
				break;
			}
			if (base.gameObject.activeInHierarchy)
			{
				switch (this.m_Transition)
				{
				case Selectable.Transition.ColorTint:
					this.StartColorTween(color * this.m_Colors.colorMultiplier, instant);
					break;
				case Selectable.Transition.SpriteSwap:
					this.DoSpriteSwap(sprite);
					break;
				case Selectable.Transition.Animation:
					this.TriggerAnimation(text);
					break;
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F00C File Offset: 0x0000D20C
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 vector = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 vector2 = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, vector));
			float num = float.NegativeInfinity;
			Selectable selectable = null;
			for (int i = 0; i < Selectable.s_List.Count; i++)
			{
				Selectable selectable2 = Selectable.s_List[i];
				if (!(selectable2 == this) && !(selectable2 == null))
				{
					if (selectable2.IsInteractable() && selectable2.navigation.mode != Navigation.Mode.None)
					{
						RectTransform rectTransform = selectable2.transform as RectTransform;
						Vector3 vector3 = ((!(rectTransform != null)) ? Vector3.zero : rectTransform.rect.center);
						Vector3 vector4 = selectable2.transform.TransformPoint(vector3) - vector2;
						float num2 = Vector3.Dot(dir, vector4);
						if (num2 > 0f)
						{
							float num3 = num2 / vector4.sqrMagnitude;
							if (num3 > num)
							{
								num = num3;
								selectable = selectable2;
							}
						}
					}
				}
			}
			return selectable;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F160 File Offset: 0x0000D360
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F218 File Offset: 0x0000D418
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F274 File Offset: 0x0000D474
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F32C File Offset: 0x0000D52C
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F388 File Offset: 0x0000D588
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				break;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				break;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				break;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				break;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F400 File Offset: 0x0000D600
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, (!instant) ? this.m_Colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F448 File Offset: 0x0000D648
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F468 File Offset: 0x0000D668
		private void TriggerAnimation(string triggername)
		{
			if (this.animator == null || !this.animator.enabled || !this.animator.isActiveAndEnabled || this.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F52C File Offset: 0x0000D72C
		protected bool IsHighlighted(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return false;
			}
			if (this.IsPressed())
			{
				return false;
			}
			bool flag = this.hasSelection;
			if (eventData is PointerEventData)
			{
				PointerEventData pointerEventData = eventData as PointerEventData;
				flag |= (this.isPointerDown && !this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == null);
			}
			else
			{
				flag |= this.isPointerInside;
			}
			return flag;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F600 File Offset: 0x0000D800
		[Obsolete("Is Pressed no longer requires eventData", false)]
		protected bool IsPressed(BaseEventData eventData)
		{
			return this.IsPressed();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F608 File Offset: 0x0000D808
		protected bool IsPressed()
		{
			return this.IsActive() && this.isPointerInside && this.isPointerDown;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F638 File Offset: 0x0000D838
		protected void UpdateSelectionState(BaseEventData eventData)
		{
			if (this.IsPressed())
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Pressed;
				return;
			}
			if (this.IsHighlighted(eventData))
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Highlighted;
				return;
			}
			this.m_CurrentSelectionState = Selectable.SelectionState.Normal;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F674 File Offset: 0x0000D874
		private void EvaluateAndTransitionToSelectionState(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateSelectionState(eventData);
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F690 File Offset: 0x0000D890
		private void InternalEvaluateAndTransitionToSelectionState(bool instant)
		{
			Selectable.SelectionState selectionState = this.m_CurrentSelectionState;
			if (this.IsActive() && !this.IsInteractable())
			{
				selectionState = Selectable.SelectionState.Disabled;
			}
			this.DoStateTransition(selectionState, instant);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F71C File Offset: 0x0000D91C
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F738 File Offset: 0x0000D938
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F748 File Offset: 0x0000D948
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F758 File Offset: 0x0000D958
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F768 File Offset: 0x0000D968
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F778 File Offset: 0x0000D978
		public virtual void Select()
		{
			if (EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x04000197 RID: 407
		private static List<Selectable> s_List = new List<Selectable>();

		// Token: 0x04000198 RID: 408
		[SerializeField]
		[FormerlySerializedAs("navigation")]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x04000199 RID: 409
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x0400019A RID: 410
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x0400019B RID: 411
		[SerializeField]
		[FormerlySerializedAs("spriteState")]
		private SpriteState m_SpriteState;

		// Token: 0x0400019C RID: 412
		[FormerlySerializedAs("animationTriggers")]
		[SerializeField]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x0400019D RID: 413
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x0400019E RID: 414
		[FormerlySerializedAs("highlightGraphic")]
		[SerializeField]
		[FormerlySerializedAs("m_HighlightGraphic")]
		private Graphic m_TargetGraphic;

		// Token: 0x0400019F RID: 415
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x040001A0 RID: 416
		private Selectable.SelectionState m_CurrentSelectionState;

		// Token: 0x040001A1 RID: 417
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x02000061 RID: 97
		public enum Transition
		{
			// Token: 0x040001A6 RID: 422
			None,
			// Token: 0x040001A7 RID: 423
			ColorTint,
			// Token: 0x040001A8 RID: 424
			SpriteSwap,
			// Token: 0x040001A9 RID: 425
			Animation
		}

		// Token: 0x02000062 RID: 98
		protected enum SelectionState
		{
			// Token: 0x040001AB RID: 427
			Normal,
			// Token: 0x040001AC RID: 428
			Highlighted,
			// Token: 0x040001AD RID: 429
			Pressed,
			// Token: 0x040001AE RID: 430
			Disabled
		}
	}
}
