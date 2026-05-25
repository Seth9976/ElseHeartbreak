using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000020 RID: 32
	public class BaseEventData
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003258 File Offset: 0x00001458
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003268 File Offset: 0x00001468
		public void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003274 File Offset: 0x00001474
		public void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003280 File Offset: 0x00001480
		public bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003288 File Offset: 0x00001488
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_EventSystem.currentInputModule;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000032A8 File Offset: 0x000014A8
		public GameObject selectedObject
		{
			get
			{
				return this.m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly EventSystem m_EventSystem;

		// Token: 0x0400004B RID: 75
		private bool m_Used;
	}
}
