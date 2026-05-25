using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public struct FontData : ISerializationCallbackReceiver
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005BA8 File Offset: 0x00003DA8
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005BAC File Offset: 0x00003DAC
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, 300);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, 0, 300);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005C00 File Offset: 0x00003E00
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_RichText = true
				};
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005C4C File Offset: 0x00003E4C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005C54 File Offset: 0x00003E54
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005C60 File Offset: 0x00003E60
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005C68 File Offset: 0x00003E68
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005C74 File Offset: 0x00003E74
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005C7C File Offset: 0x00003E7C
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005C88 File Offset: 0x00003E88
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00005C90 File Offset: 0x00003E90
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005C9C File Offset: 0x00003E9C
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005CB0 File Offset: 0x00003EB0
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005CC4 File Offset: 0x00003EC4
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005CCC File Offset: 0x00003ECC
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005CD8 File Offset: 0x00003ED8
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005D00 File Offset: 0x00003F00
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005D08 File Offset: 0x00003F08
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00005D1C File Offset: 0x00003F1C
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x040000B5 RID: 181
		[FormerlySerializedAs("font")]
		[SerializeField]
		private Font m_Font;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		[FormerlySerializedAs("fontStyle")]
		private FontStyle m_FontStyle;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		private int m_MinSize;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		[FormerlySerializedAs("alignment")]
		private TextAnchor m_Alignment;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		[FormerlySerializedAs("richText")]
		private bool m_RichText;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private float m_LineSpacing;
	}
}
