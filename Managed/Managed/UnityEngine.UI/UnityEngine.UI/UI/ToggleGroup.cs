using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200006F RID: 111
	[AddComponentMenu("UI/Toggle Group", 36)]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001125C File Offset: 0x0000F45C
		protected ToggleGroup()
		{
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011270 File Offset: 0x0000F470
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00011278 File Offset: 0x0000F478
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011284 File Offset: 0x0000F484
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", toggle, this));
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public void NotifyToggleOn(Toggle toggle)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					this.m_Toggles[i].isOn = false;
				}
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011318 File Offset: 0x0000F518
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00011338 File Offset: 0x0000F538
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00011358 File Offset: 0x0000F558
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00011394 File Offset: 0x0000F594
		public IEnumerable<Toggle> ActiveToggles()
		{
			return this.m_Toggles.Where((Toggle x) => x.isOn);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000113CC File Offset: 0x0000F5CC
		public void SetAllTogglesOff()
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				this.m_Toggles[i].isOn = false;
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x040001DC RID: 476
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x040001DD RID: 477
		private List<Toggle> m_Toggles = new List<Toggle>();
	}
}
