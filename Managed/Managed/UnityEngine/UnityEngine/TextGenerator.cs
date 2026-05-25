using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000052 RID: 82
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00007F74 File Offset: 0x00006174
		public TextGenerator()
			: this(50)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007F80 File Offset: 0x00006180
		public TextGenerator(int initialCapacity)
		{
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
			this.Init();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007FBC File Offset: 0x000061BC
		void IDisposable.Dispose()
		{
			this.Dispose_cpp();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007FC4 File Offset: 0x000061C4
		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008000 File Offset: 0x00006200
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			if (settings.font != null && settings.font.dynamic)
			{
				return settings;
			}
			if (settings.fontSize != 0 || settings.fontStyle != FontStyle.Normal)
			{
				Debug.LogWarning("Font size and style overrides are only supported for dynamic fonts.");
				settings.fontSize = 0;
				settings.fontStyle = FontStyle.Normal;
			}
			if (settings.resizeTextForBestFit)
			{
				Debug.LogWarning("BestFit is only suppoerted for dynamic fonts.");
				settings.resizeTextForBestFit = false;
			}
			return settings;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008084 File Offset: 0x00006284
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008090 File Offset: 0x00006290
		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000809C File Offset: 0x0000629C
		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000080A8 File Offset: 0x000062A8
		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000080B4 File Offset: 0x000062B4
		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000080F0 File Offset: 0x000062F0
		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008124 File Offset: 0x00006324
		public bool Populate(string str, TextGenerationSettings settings)
		{
			if (this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings))
			{
				return this.m_LastValid;
			}
			return this.PopulateAlways(str, settings);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008164 File Offset: 0x00006364
		private bool PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			this.m_LastValid = this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.fontSize, textGenerationSettings.scaleFactor, textGenerationSettings.lineSpacing, textGenerationSettings.fontStyle, textGenerationSettings.richText, textGenerationSettings.resizeTextForBestFit, textGenerationSettings.resizeTextMinSize, textGenerationSettings.resizeTextMaxSize, textGenerationSettings.verticalOverflow, textGenerationSettings.horizontalOverflow, textGenerationSettings.updateBounds, textGenerationSettings.textAnchor, textGenerationSettings.generationExtents, textGenerationSettings.pivot, textGenerationSettings.generateOutOfBounds);
			return this.m_LastValid;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008230 File Offset: 0x00006430
		public IList<UIVertex> verts
		{
			get
			{
				if (!this.m_CachedVerts)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008264 File Offset: 0x00006464
		public IList<UICharInfo> characters
		{
			get
			{
				if (!this.m_CachedCharacters)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008298 File Offset: 0x00006498
		public IList<UILineInfo> lines
		{
			get
			{
				if (!this.m_CachedLines)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x0600019C RID: 412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x0600019D RID: 413
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_cpp();

		// Token: 0x0600019E RID: 414 RVA: 0x000082CC File Offset: 0x000064CC
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds)
		{
			return this.Populate_Internal_cpp(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008318 File Offset: 0x00006518
		internal bool Populate_Internal_cpp(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds)
		{
			return TextGenerator.INTERNAL_CALL_Populate_Internal_cpp(this, str, font, ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds);
		}

		// Token: 0x060001A0 RID: 416
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Populate_Internal_cpp(TextGenerator self, string str, Font font, ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds);

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008354 File Offset: 0x00006554
		public Rect rectExtents
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_rectExtents(out rect);
				return rect;
			}
		}

		// Token: 0x060001A2 RID: 418
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rectExtents(out Rect value);

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A3 RID: 419
		public extern int vertexCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060001A4 RID: 420
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVerticesInternal(object vertices);

		// Token: 0x060001A5 RID: 421
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UIVertex[] GetVerticesArray();

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A6 RID: 422
		public extern int characterCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000836C File Offset: 0x0000656C
		public int characterCountVisible
		{
			get
			{
				return (!string.IsNullOrEmpty(this.m_LastString)) ? Mathf.Min(this.m_LastString.Length, Mathf.Max(0, (this.vertexCount - 4) / 4)) : 0;
			}
		}

		// Token: 0x060001A8 RID: 424
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCharactersInternal(object characters);

		// Token: 0x060001A9 RID: 425
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UICharInfo[] GetCharactersArray();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001AA RID: 426
		public extern int lineCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060001AB RID: 427
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLinesInternal(object lines);

		// Token: 0x060001AC RID: 428
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UILineInfo[] GetLinesArray();

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AD RID: 429
		public extern int fontSizeUsedForBestFit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x04000160 RID: 352
		internal IntPtr m_Ptr;

		// Token: 0x04000161 RID: 353
		private string m_LastString;

		// Token: 0x04000162 RID: 354
		private TextGenerationSettings m_LastSettings;

		// Token: 0x04000163 RID: 355
		private bool m_HasGenerated;

		// Token: 0x04000164 RID: 356
		private bool m_LastValid;

		// Token: 0x04000165 RID: 357
		private readonly List<UIVertex> m_Verts;

		// Token: 0x04000166 RID: 358
		private readonly List<UICharInfo> m_Characters;

		// Token: 0x04000167 RID: 359
		private readonly List<UILineInfo> m_Lines;

		// Token: 0x04000168 RID: 360
		private bool m_CachedVerts;

		// Token: 0x04000169 RID: 361
		private bool m_CachedCharacters;

		// Token: 0x0400016A RID: 362
		private bool m_CachedLines;
	}
}
