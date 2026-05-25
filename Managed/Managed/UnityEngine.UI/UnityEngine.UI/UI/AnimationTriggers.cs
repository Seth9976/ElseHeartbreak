using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class AnimationTriggers
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005540 File Offset: 0x00003740
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005548 File Offset: 0x00003748
		public string normalTrigger
		{
			get
			{
				return this.m_NormalTrigger;
			}
			set
			{
				this.m_NormalTrigger = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005554 File Offset: 0x00003754
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000555C File Offset: 0x0000375C
		public string highlightedTrigger
		{
			get
			{
				return this.m_HighlightedTrigger;
			}
			set
			{
				this.m_HighlightedTrigger = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005568 File Offset: 0x00003768
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005570 File Offset: 0x00003770
		public string pressedTrigger
		{
			get
			{
				return this.m_PressedTrigger;
			}
			set
			{
				this.m_PressedTrigger = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000557C File Offset: 0x0000377C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005584 File Offset: 0x00003784
		public string disabledTrigger
		{
			get
			{
				return this.m_DisabledTrigger;
			}
			set
			{
				this.m_DisabledTrigger = value;
			}
		}

		// Token: 0x04000097 RID: 151
		private const string kDefaultNormalAnimName = "Normal";

		// Token: 0x04000098 RID: 152
		private const string kDefaultSelectedAnimName = "Highlighted";

		// Token: 0x04000099 RID: 153
		private const string kDefaultPressedAnimName = "Pressed";

		// Token: 0x0400009A RID: 154
		private const string kDefaultDisabledAnimName = "Disabled";

		// Token: 0x0400009B RID: 155
		[FormerlySerializedAs("normalTrigger")]
		[SerializeField]
		private string m_NormalTrigger = "Normal";

		// Token: 0x0400009C RID: 156
		[SerializeField]
		[FormerlySerializedAs("highlightedTrigger")]
		[FormerlySerializedAs("m_SelectedTrigger")]
		private string m_HighlightedTrigger = "Highlighted";

		// Token: 0x0400009D RID: 157
		[SerializeField]
		[FormerlySerializedAs("pressedTrigger")]
		private string m_PressedTrigger = "Pressed";

		// Token: 0x0400009E RID: 158
		[SerializeField]
		[FormerlySerializedAs("disabledTrigger")]
		private string m_DisabledTrigger = "Disabled";
	}
}
