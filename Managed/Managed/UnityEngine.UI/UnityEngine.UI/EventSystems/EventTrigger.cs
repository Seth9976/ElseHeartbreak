using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000016 RID: 22
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000027C0 File Offset: 0x000009C0
		protected EventTrigger()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027C8 File Offset: 0x000009C8
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			if (this.delegates != null)
			{
				int i = 0;
				int count = this.delegates.Count;
				while (i < count)
				{
					EventTrigger.Entry entry = this.delegates[i];
					if (entry.eventID == id && entry.callback != null)
					{
						entry.callback.Invoke(eventData);
					}
					i++;
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002830 File Offset: 0x00000A30
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000283C File Offset: 0x00000A3C
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002848 File Offset: 0x00000A48
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002854 File Offset: 0x00000A54
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002860 File Offset: 0x00000A60
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000286C File Offset: 0x00000A6C
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002878 File Offset: 0x00000A78
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002884 File Offset: 0x00000A84
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002890 File Offset: 0x00000A90
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000289C File Offset: 0x00000A9C
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028A8 File Offset: 0x00000AA8
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028B4 File Offset: 0x00000AB4
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028C0 File Offset: 0x00000AC0
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028CC File Offset: 0x00000ACC
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028D8 File Offset: 0x00000AD8
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028E4 File Offset: 0x00000AE4
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028F0 File Offset: 0x00000AF0
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x0400000E RID: 14
		public List<EventTrigger.Entry> delegates;

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class Entry
		{
			// Token: 0x0400000F RID: 15
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000010 RID: 16
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
