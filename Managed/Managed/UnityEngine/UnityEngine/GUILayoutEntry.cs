using System;

namespace UnityEngine
{
	// Token: 0x020000F0 RID: 240
	internal class GUILayoutEntry
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			if (_style == null)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00011948 File Offset: 0x0000FB48
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000119E8 File Offset: 0x0000FBE8
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00011A00 File Offset: 0x0000FC00
		public virtual RectOffset margin
		{
			get
			{
				return this.style.margin;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00011A10 File Offset: 0x0000FC10
		public virtual void CalcWidth()
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00011A14 File Offset: 0x0000FC14
		public virtual void CalcHeight()
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00011A18 File Offset: 0x0000FC18
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00011A34 File Offset: 0x0000FC34
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00011A50 File Offset: 0x0000FC50
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth != 0f || !style.stretchWidth) ? 0 : 1);
			this.stretchHeight = ((style.fixedHeight != 0f || !style.stretchHeight) ? 0 : 1);
			this.m_Style = style;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
					this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.fixedHeight:
					this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.minWidth:
					this.minWidth = (float)guilayoutOption.value;
					if (this.maxWidth < this.minWidth)
					{
						this.maxWidth = this.minWidth;
					}
					break;
				case GUILayoutOption.Type.maxWidth:
					this.maxWidth = (float)guilayoutOption.value;
					if (this.minWidth > this.maxWidth)
					{
						this.minWidth = this.maxWidth;
					}
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.minHeight:
					this.minHeight = (float)guilayoutOption.value;
					if (this.maxHeight < this.minHeight)
					{
						this.maxHeight = this.minHeight;
					}
					break;
				case GUILayoutOption.Type.maxHeight:
					this.maxHeight = (float)guilayoutOption.value;
					if (this.minHeight > this.maxHeight)
					{
						this.minHeight = this.maxHeight;
					}
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.stretchWidth:
					this.stretchWidth = (int)guilayoutOption.value;
					break;
				case GUILayoutOption.Type.stretchHeight:
					this.stretchHeight = (int)guilayoutOption.value;
					break;
				}
			}
			if (this.maxWidth != 0f && this.maxWidth < this.minWidth)
			{
				this.maxWidth = this.minWidth;
			}
			if (this.maxHeight != 0f && this.maxHeight < this.minHeight)
			{
				this.maxHeight = this.minHeight;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new object[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style == null) ? "NULL" : this.style.name,
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth,
				"-",
				this.maxWidth,
				(this.stretchWidth == 0) ? string.Empty : "+",
				", H: ",
				this.minHeight,
				"-",
				this.maxHeight,
				(this.stretchHeight == 0) ? string.Empty : "+"
			});
		}

		// Token: 0x0400030D RID: 781
		public float minWidth;

		// Token: 0x0400030E RID: 782
		public float maxWidth;

		// Token: 0x0400030F RID: 783
		public float minHeight;

		// Token: 0x04000310 RID: 784
		public float maxHeight;

		// Token: 0x04000311 RID: 785
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x04000312 RID: 786
		public int stretchWidth;

		// Token: 0x04000313 RID: 787
		public int stretchHeight;

		// Token: 0x04000314 RID: 788
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x04000315 RID: 789
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000316 RID: 790
		protected static int indent = 0;
	}
}
