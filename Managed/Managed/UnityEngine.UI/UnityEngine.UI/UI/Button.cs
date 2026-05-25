using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000035 RID: 53
	[AddComponentMenu("UI/Button", 30)]
	public class Button : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00005590 File Offset: 0x00003790
		protected Button()
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000055A4 File Offset: 0x000037A4
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000055AC File Offset: 0x000037AC
		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return this.m_OnClick;
			}
			set
			{
				this.m_OnClick = value;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000055B8 File Offset: 0x000037B8
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.m_OnClick.Invoke();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000055E8 File Offset: 0x000037E8
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000055FC File Offset: 0x000037FC
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000563C File Offset: 0x0000383C
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x0400009F RID: 159
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		// Token: 0x02000036 RID: 54
		[Serializable]
		public class ButtonClickedEvent : UnityEvent
		{
		}
	}
}
