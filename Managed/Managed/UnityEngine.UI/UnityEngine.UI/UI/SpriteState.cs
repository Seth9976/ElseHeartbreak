using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	public struct SpriteState
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000102D4 File Offset: 0x0000E4D4
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000102DC File Offset: 0x0000E4DC
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000102E8 File Offset: 0x0000E4E8
		// (set) Token: 0x0600037C RID: 892 RVA: 0x000102F0 File Offset: 0x0000E4F0
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000102FC File Offset: 0x0000E4FC
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00010304 File Offset: 0x0000E504
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x040001C6 RID: 454
		[FormerlySerializedAs("highlightedSprite")]
		[FormerlySerializedAs("m_SelectedSprite")]
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x040001C7 RID: 455
		[FormerlySerializedAs("pressedSprite")]
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x040001C8 RID: 456
		[SerializeField]
		[FormerlySerializedAs("disabledSprite")]
		private Sprite m_DisabledSprite;
	}
}
