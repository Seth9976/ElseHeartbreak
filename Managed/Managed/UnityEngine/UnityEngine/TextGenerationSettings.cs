using System;

namespace UnityEngine
{
	// Token: 0x02000051 RID: 81
	public struct TextGenerationSettings
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007D88 File Offset: 0x00005F88
		private bool CompareColors(Color left, Color right)
		{
			Color32 color = left;
			Color32 color2 = right;
			return color.Equals(color2);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007DB4 File Offset: 0x00005FB4
		private bool CompareVector2(Vector2 left, Vector2 right)
		{
			return Mathf.Approximately(left.x, right.x) && Mathf.Approximately(left.y, right.y);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public bool Equals(TextGenerationSettings other)
		{
			return this.CompareColors(this.color, other.color) && this.fontSize == other.fontSize && Mathf.Approximately(this.scaleFactor, other.scaleFactor) && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && Mathf.Approximately(this.lineSpacing, other.lineSpacing) && this.fontStyle == other.fontStyle && this.richText == other.richText && this.textAnchor == other.textAnchor && this.resizeTextForBestFit == other.resizeTextForBestFit && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && this.resizeTextForBestFit == other.resizeTextForBestFit && this.updateBounds == other.updateBounds && this.horizontalOverflow == other.horizontalOverflow && this.verticalOverflow == other.verticalOverflow && this.CompareVector2(this.generationExtents, other.generationExtents) && this.CompareVector2(this.pivot, other.pivot) && this.font == other.font;
		}

		// Token: 0x0400014F RID: 335
		public Font font;

		// Token: 0x04000150 RID: 336
		public Color color;

		// Token: 0x04000151 RID: 337
		public int fontSize;

		// Token: 0x04000152 RID: 338
		public float lineSpacing;

		// Token: 0x04000153 RID: 339
		public bool richText;

		// Token: 0x04000154 RID: 340
		public float scaleFactor;

		// Token: 0x04000155 RID: 341
		public FontStyle fontStyle;

		// Token: 0x04000156 RID: 342
		public TextAnchor textAnchor;

		// Token: 0x04000157 RID: 343
		public bool resizeTextForBestFit;

		// Token: 0x04000158 RID: 344
		public int resizeTextMinSize;

		// Token: 0x04000159 RID: 345
		public int resizeTextMaxSize;

		// Token: 0x0400015A RID: 346
		public bool updateBounds;

		// Token: 0x0400015B RID: 347
		public VerticalWrapMode verticalOverflow;

		// Token: 0x0400015C RID: 348
		public HorizontalWrapMode horizontalOverflow;

		// Token: 0x0400015D RID: 349
		public Vector2 generationExtents;

		// Token: 0x0400015E RID: 350
		public Vector2 pivot;

		// Token: 0x0400015F RID: 351
		public bool generateOutOfBounds;
	}
}
