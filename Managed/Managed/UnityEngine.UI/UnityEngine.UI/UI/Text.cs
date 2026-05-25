using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200006B RID: 107
	[AddComponentMenu("UI/Text", 11)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00010510 File Offset: 0x0000E710
		protected Text()
		{
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00010534 File Offset: 0x0000E734
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCache) == null)
				{
					textGenerator = (this.m_TextCache = ((this.m_Text.Length == 0) ? new TextGenerator() : new TextGenerator(this.m_Text.Length)));
				}
				return textGenerator;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00010584 File Offset: 0x0000E784
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator textGenerator;
				if ((textGenerator = this.m_TextCacheForLayout) == null)
				{
					textGenerator = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return textGenerator;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000105AC File Offset: 0x0000E7AC
		public override Material defaultMaterial
		{
			get
			{
				if (Text.s_DefaultText == null)
				{
					Text.s_DefaultText = Canvas.GetDefaultCanvasTextMaterial();
				}
				return Text.s_DefaultText;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010654 File Offset: 0x0000E854
		public void FontTextureChanged()
		{
			if (!this)
			{
				FontUpdateTracker.UntrackText(this);
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
			}
			else
			{
				this.SetAllDirty();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000106BC File Offset: 0x0000E8BC
		// (set) Token: 0x0600038B RID: 907 RVA: 0x000106CC File Offset: 0x0000E8CC
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				FontUpdateTracker.UntrackText(this);
				this.m_FontData.font = value;
				FontUpdateTracker.TrackText(this);
				this.SetAllDirty();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00010710 File Offset: 0x0000E910
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00010718 File Offset: 0x0000E918
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					if (string.IsNullOrEmpty(this.m_Text))
					{
						return;
					}
					this.m_Text = string.Empty;
					this.SetVerticesDirty();
				}
				else if (this.m_Text != value)
				{
					this.m_Text = value;
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001077C File Offset: 0x0000E97C
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0001078C File Offset: 0x0000E98C
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000107C4 File Offset: 0x0000E9C4
		// (set) Token: 0x06000391 RID: 913 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001080C File Offset: 0x0000EA0C
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0001081C File Offset: 0x0000EA1C
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00010854 File Offset: 0x0000EA54
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00010864 File Offset: 0x0000EA64
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0001089C File Offset: 0x0000EA9C
		// (set) Token: 0x06000397 RID: 919 RVA: 0x000108AC File Offset: 0x0000EAAC
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000108E4 File Offset: 0x0000EAE4
		// (set) Token: 0x06000399 RID: 921 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001092C File Offset: 0x0000EB2C
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0001093C File Offset: 0x0000EB3C
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00010974 File Offset: 0x0000EB74
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00010984 File Offset: 0x0000EB84
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000109BC File Offset: 0x0000EBBC
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000109CC File Offset: 0x0000EBCC
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010A04 File Offset: 0x0000EC04
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00010A14 File Offset: 0x0000EC14
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00010ADC File Offset: 0x0000ECDC
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010B08 File Offset: 0x0000ED08
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010B24 File Offset: 0x0000ED24
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings textGenerationSettings = default(TextGenerationSettings);
			textGenerationSettings.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				textGenerationSettings.fontSize = this.m_FontData.fontSize;
				textGenerationSettings.resizeTextMinSize = this.m_FontData.minSize;
				textGenerationSettings.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			textGenerationSettings.textAnchor = this.m_FontData.alignment;
			textGenerationSettings.scaleFactor = this.pixelsPerUnit;
			textGenerationSettings.color = base.color;
			textGenerationSettings.font = this.font;
			textGenerationSettings.pivot = base.rectTransform.pivot;
			textGenerationSettings.richText = this.m_FontData.richText;
			textGenerationSettings.lineSpacing = this.m_FontData.lineSpacing;
			textGenerationSettings.fontStyle = this.m_FontData.fontStyle;
			textGenerationSettings.resizeTextForBestFit = this.m_FontData.bestFit;
			textGenerationSettings.updateBounds = false;
			textGenerationSettings.horizontalOverflow = this.m_FontData.horizontalOverflow;
			textGenerationSettings.verticalOverflow = this.m_FontData.verticalOverflow;
			return textGenerationSettings;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010C58 File Offset: 0x0000EE58
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010D2C File Offset: 0x0000EF2C
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 size = base.rectTransform.rect.size;
			TextGenerationSettings generationSettings = this.GetGenerationSettings(size);
			this.cachedTextGenerator.Populate(this.m_Text, generationSettings);
			Rect rect = base.rectTransform.rect;
			Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_FontData.alignment);
			Vector2 zero = Vector2.zero;
			zero.x = ((textAnchorPivot.x != 1f) ? rect.xMin : rect.xMax);
			zero.y = ((textAnchorPivot.y != 0f) ? rect.yMax : rect.yMin);
			Vector2 vector = base.PixelAdjustPoint(zero) - zero;
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float num = 1f / this.pixelsPerUnit;
			if (vector != Vector2.zero)
			{
				for (int i = 0; i < verts.Count; i++)
				{
					UIVertex uivertex = verts[i];
					uivertex.position *= num;
					uivertex.position.x = uivertex.position.x + vector.x;
					uivertex.position.y = uivertex.position.y + vector.y;
					vbo.Add(uivertex);
				}
			}
			else
			{
				for (int j = 0; j < verts.Count; j++)
				{
					UIVertex uivertex2 = verts[j];
					uivertex2.position *= num;
					vbo.Add(uivertex2);
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00010F00 File Offset: 0x0000F100
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010F04 File Offset: 0x0000F104
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00010F08 File Offset: 0x0000F108
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010F10 File Offset: 0x0000F110
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010F44 File Offset: 0x0000F144
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00010F4C File Offset: 0x0000F14C
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00010F54 File Offset: 0x0000F154
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(new Vector2(base.rectTransform.rect.size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040001CE RID: 462
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x040001CF RID: 463
		[TextArea(3, 10)]
		[SerializeField]
		protected string m_Text = string.Empty;

		// Token: 0x040001D0 RID: 464
		private TextGenerator m_TextCache;

		// Token: 0x040001D1 RID: 465
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x040001D2 RID: 466
		protected static Material s_DefaultText;

		// Token: 0x040001D3 RID: 467
		[NonSerialized]
		private bool m_DisableFontTextureRebuiltCallback;
	}
}
