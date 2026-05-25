using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200006C RID: 108
	[AddComponentMenu("UI/Toggle", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class Toggle : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		protected Toggle()
		{
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.m_Group = value;
				this.SetToggleGroup(this.m_Group, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010FFC File Offset: 0x0000F1FC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011018 File Offset: 0x0000F218
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011028 File Offset: 0x0000F228
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			ToggleGroup group = this.m_Group;
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (this.m_Group != null && this.IsActive())
			{
				this.m_Group.RegisterToggle(this);
			}
			if (newGroup != null && newGroup != group && this.isOn && this.IsActive())
			{
				this.m_Group.NotifyToggleOn(this);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000110C8 File Offset: 0x0000F2C8
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000110DC File Offset: 0x0000F2DC
		private void Set(bool value)
		{
			this.Set(value, true);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000110E8 File Offset: 0x0000F2E8
		private void Set(bool value, bool sendCallback)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001118C File Offset: 0x0000F38C
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha((!this.m_IsOn) ? 0f : 1f, (!instant) ? 0.1f : 0f, true);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000111E8 File Offset: 0x0000F3E8
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000111F4 File Offset: 0x0000F3F4
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011228 File Offset: 0x0000F428
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001123C File Offset: 0x0000F43C
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011244 File Offset: 0x0000F444
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001124C File Offset: 0x0000F44C
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001D4 RID: 468
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x040001D5 RID: 469
		public Graphic graphic;

		// Token: 0x040001D6 RID: 470
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x040001D7 RID: 471
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x040001D8 RID: 472
		[SerializeField]
		[FormerlySerializedAs("m_IsActive")]
		[Tooltip("Is the toggle currently on or off?")]
		private bool m_IsOn;

		// Token: 0x0200006D RID: 109
		public enum ToggleTransition
		{
			// Token: 0x040001DA RID: 474
			None,
			// Token: 0x040001DB RID: 475
			Fade
		}

		// Token: 0x0200006E RID: 110
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}
	}
}
