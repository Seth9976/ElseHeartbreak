using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public struct ColorBlock
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005A70 File Offset: 0x00003C70
		public Color normalColor
		{
			get
			{
				return this.m_NormalColor;
			}
			set
			{
				this.m_NormalColor = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005A7C File Offset: 0x00003C7C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005A84 File Offset: 0x00003C84
		public Color highlightedColor
		{
			get
			{
				return this.m_HighlightedColor;
			}
			set
			{
				this.m_HighlightedColor = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005A90 File Offset: 0x00003C90
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005A98 File Offset: 0x00003C98
		public Color pressedColor
		{
			get
			{
				return this.m_PressedColor;
			}
			set
			{
				this.m_PressedColor = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005AA4 File Offset: 0x00003CA4
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005AAC File Offset: 0x00003CAC
		public Color disabledColor
		{
			get
			{
				return this.m_DisabledColor;
			}
			set
			{
				this.m_DisabledColor = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005AB8 File Offset: 0x00003CB8
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public float colorMultiplier
		{
			get
			{
				return this.m_ColorMultiplier;
			}
			set
			{
				this.m_ColorMultiplier = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005ACC File Offset: 0x00003CCC
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public float fadeDuration
		{
			get
			{
				return this.m_FadeDuration;
			}
			set
			{
				this.m_FadeDuration = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public static ColorBlock defaultColorBlock
		{
			get
			{
				return new ColorBlock
				{
					m_NormalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
					m_HighlightedColor = new Color32(245, 245, 245, byte.MaxValue),
					m_PressedColor = new Color32(200, 200, 200, byte.MaxValue),
					m_DisabledColor = new Color32(200, 200, 200, 128),
					colorMultiplier = 1f,
					fadeDuration = 0.1f
				};
			}
		}

		// Token: 0x040000AF RID: 175
		[SerializeField]
		[FormerlySerializedAs("normalColor")]
		private Color m_NormalColor;

		// Token: 0x040000B0 RID: 176
		[FormerlySerializedAs("highlightedColor")]
		[FormerlySerializedAs("m_SelectedColor")]
		[SerializeField]
		private Color m_HighlightedColor;

		// Token: 0x040000B1 RID: 177
		[FormerlySerializedAs("pressedColor")]
		[SerializeField]
		private Color m_PressedColor;

		// Token: 0x040000B2 RID: 178
		[FormerlySerializedAs("disabledColor")]
		[SerializeField]
		private Color m_DisabledColor;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		[Range(1f, 5f)]
		private float m_ColorMultiplier;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		[FormerlySerializedAs("fadeDuration")]
		private float m_FadeDuration;
	}
}
