using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x020000F1 RID: 241
	internal class GUILayoutGroup : GUILayoutEntry
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x00011E2C File Offset: 0x0001002C
		public GUILayoutGroup()
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00011EC0 File Offset: 0x000100C0
		public GUILayoutGroup(GUIStyle _style, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, _style)
		{
			if (options != null)
			{
				this.ApplyOptions(options);
			}
			this.m_Margin.left = _style.margin.left;
			this.m_Margin.right = _style.margin.right;
			this.m_Margin.top = _style.margin.top;
			this.m_Margin.bottom = _style.margin.bottom;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00011FB4 File Offset: 0x000101B4
		public override RectOffset margin
		{
			get
			{
				return this.m_Margin;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00011FBC File Offset: 0x000101BC
		public override void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			base.ApplyOptions(options);
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
				case GUILayoutOption.Type.minWidth:
				case GUILayoutOption.Type.maxWidth:
					this.userSpecifiedHeight = true;
					break;
				case GUILayoutOption.Type.fixedHeight:
				case GUILayoutOption.Type.minHeight:
				case GUILayoutOption.Type.maxHeight:
					this.userSpecifiedWidth = true;
					break;
				case GUILayoutOption.Type.spacing:
					this.spacing = (float)((int)guilayoutOption.value);
					break;
				}
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001206C File Offset: 0x0001026C
		protected override void ApplyStyleSettings(GUIStyle style)
		{
			base.ApplyStyleSettings(style);
			RectOffset margin = style.margin;
			this.m_Margin.left = margin.left;
			this.m_Margin.right = margin.right;
			this.m_Margin.top = margin.top;
			this.m_Margin.bottom = margin.bottom;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000120CC File Offset: 0x000102CC
		public void ResetCursor()
		{
			this.cursor = 0;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000120D8 File Offset: 0x000102D8
		public Rect PeekNext()
		{
			if (this.cursor < this.entries.Count)
			{
				GUILayoutEntry guilayoutEntry = this.entries[this.cursor];
				return guilayoutEntry.rect;
			}
			throw new ArgumentException(string.Concat(new object[]
			{
				"Getting control ",
				this.cursor,
				"'s position in a group with only ",
				this.entries.Count,
				" controls when doing ",
				Event.current.rawType,
				"\nAborting"
			}));
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00012178 File Offset: 0x00010378
		public GUILayoutEntry GetNext()
		{
			if (this.cursor < this.entries.Count)
			{
				GUILayoutEntry guilayoutEntry = this.entries[this.cursor];
				this.cursor++;
				return guilayoutEntry;
			}
			throw new ArgumentException(string.Concat(new object[]
			{
				"Getting control ",
				this.cursor,
				"'s position in a group with only ",
				this.entries.Count,
				" controls when doing ",
				Event.current.rawType,
				"\nAborting"
			}));
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00012220 File Offset: 0x00010420
		public Rect GetLast()
		{
			if (this.cursor == 0)
			{
				Debug.LogError("You cannot call GetLast immediately after beginning a group.");
				return GUILayoutEntry.kDummyRect;
			}
			if (this.cursor <= this.entries.Count)
			{
				GUILayoutEntry guilayoutEntry = this.entries[this.cursor - 1];
				return guilayoutEntry.rect;
			}
			Debug.LogError(string.Concat(new object[]
			{
				"Getting control ",
				this.cursor,
				"'s position in a group with only ",
				this.entries.Count,
				" controls when doing ",
				Event.current.type
			}));
			return GUILayoutEntry.kDummyRect;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000122DC File Offset: 0x000104DC
		public void Add(GUILayoutEntry e)
		{
			this.entries.Add(e);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000122EC File Offset: 0x000104EC
		public override void CalcWidth()
		{
			if (this.entries.Count == 0)
			{
				this.maxWidth = (this.minWidth = (float)base.style.padding.horizontal);
				return;
			}
			this.childMinWidth = 0f;
			this.childMaxWidth = 0f;
			int num = 0;
			int num2 = 0;
			this.stretchableCountX = 0;
			bool flag = true;
			if (this.isVertical)
			{
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					guilayoutEntry.CalcWidth();
					RectOffset margin = guilayoutEntry.margin;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						if (!flag)
						{
							num = Mathf.Min(margin.left, num);
							num2 = Mathf.Min(margin.right, num2);
						}
						else
						{
							num = margin.left;
							num2 = margin.right;
							flag = false;
						}
						this.childMinWidth = Mathf.Max(guilayoutEntry.minWidth + (float)margin.horizontal, this.childMinWidth);
						this.childMaxWidth = Mathf.Max(guilayoutEntry.maxWidth + (float)margin.horizontal, this.childMaxWidth);
					}
					this.stretchableCountX += guilayoutEntry.stretchWidth;
				}
				this.childMinWidth -= (float)(num + num2);
				this.childMaxWidth -= (float)(num + num2);
			}
			else
			{
				int num3 = 0;
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					guilayoutEntry2.CalcWidth();
					RectOffset margin2 = guilayoutEntry2.margin;
					if (guilayoutEntry2.style != GUILayoutUtility.spaceStyle)
					{
						int num4;
						if (!flag)
						{
							num4 = ((num3 <= margin2.left) ? margin2.left : num3);
						}
						else
						{
							num4 = 0;
							flag = false;
						}
						this.childMinWidth += guilayoutEntry2.minWidth + this.spacing + (float)num4;
						this.childMaxWidth += guilayoutEntry2.maxWidth + this.spacing + (float)num4;
						num3 = margin2.right;
						this.stretchableCountX += guilayoutEntry2.stretchWidth;
					}
					else
					{
						this.childMinWidth += guilayoutEntry2.minWidth;
						this.childMaxWidth += guilayoutEntry2.maxWidth;
						this.stretchableCountX += guilayoutEntry2.stretchWidth;
					}
				}
				this.childMinWidth -= this.spacing;
				this.childMaxWidth -= this.spacing;
				if (this.entries.Count != 0)
				{
					num = this.entries[0].margin.left;
					num2 = num3;
				}
				else
				{
					num2 = (num = 0);
				}
			}
			float num5;
			float num6;
			if (base.style != GUIStyle.none || this.userSpecifiedWidth)
			{
				num5 = (float)Mathf.Max(base.style.padding.left, num);
				num6 = (float)Mathf.Max(base.style.padding.right, num2);
			}
			else
			{
				this.m_Margin.left = num;
				this.m_Margin.right = num2;
				num6 = (num5 = 0f);
			}
			this.minWidth = Mathf.Max(this.minWidth, this.childMinWidth + num5 + num6);
			if (this.maxWidth == 0f)
			{
				this.stretchWidth += this.stretchableCountX + ((!base.style.stretchWidth) ? 0 : 1);
				this.maxWidth = this.childMaxWidth + num5 + num6;
			}
			else
			{
				this.stretchWidth = 0;
			}
			this.maxWidth = Mathf.Max(this.maxWidth, this.minWidth);
			if (base.style.fixedWidth != 0f)
			{
				this.maxWidth = (this.minWidth = base.style.fixedWidth);
				this.stretchWidth = 0;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00012760 File Offset: 0x00010960
		public override void SetHorizontal(float x, float width)
		{
			base.SetHorizontal(x, width);
			if (this.resetCoords)
			{
				x = 0f;
			}
			RectOffset padding = base.style.padding;
			if (this.isVertical)
			{
				if (base.style != GUIStyle.none)
				{
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						float num = (float)Mathf.Max(guilayoutEntry.margin.left, padding.left);
						float num2 = x + num;
						float num3 = width - (float)Mathf.Max(guilayoutEntry.margin.right, padding.right) - num;
						if (guilayoutEntry.stretchWidth != 0)
						{
							guilayoutEntry.SetHorizontal(num2, num3);
						}
						else
						{
							guilayoutEntry.SetHorizontal(num2, Mathf.Clamp(num3, guilayoutEntry.minWidth, guilayoutEntry.maxWidth));
						}
					}
				}
				else
				{
					float num4 = x - (float)this.margin.left;
					float num5 = width + (float)this.margin.horizontal;
					foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
					{
						if (guilayoutEntry2.stretchWidth != 0)
						{
							guilayoutEntry2.SetHorizontal(num4 + (float)guilayoutEntry2.margin.left, num5 - (float)guilayoutEntry2.margin.horizontal);
						}
						else
						{
							guilayoutEntry2.SetHorizontal(num4 + (float)guilayoutEntry2.margin.left, Mathf.Clamp(num5 - (float)guilayoutEntry2.margin.horizontal, guilayoutEntry2.minWidth, guilayoutEntry2.maxWidth));
						}
					}
				}
			}
			else
			{
				if (base.style != GUIStyle.none)
				{
					float num6 = (float)padding.left;
					float num7 = (float)padding.right;
					if (this.entries.Count != 0)
					{
						num6 = Mathf.Max(num6, (float)this.entries[0].margin.left);
						num7 = Mathf.Max(num7, (float)this.entries[this.entries.Count - 1].margin.right);
					}
					x += num6;
					width -= num7 + num6;
				}
				float num8 = width - this.spacing * (float)(this.entries.Count - 1);
				float num9 = 0f;
				if (this.childMinWidth != this.childMaxWidth)
				{
					num9 = Mathf.Clamp((num8 - this.childMinWidth) / (this.childMaxWidth - this.childMinWidth), 0f, 1f);
				}
				float num10 = 0f;
				if (num8 > this.childMaxWidth && this.stretchableCountX > 0)
				{
					num10 = (num8 - this.childMaxWidth) / (float)this.stretchableCountX;
				}
				int num11 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
				{
					float num12 = Mathf.Lerp(guilayoutEntry3.minWidth, guilayoutEntry3.maxWidth, num9);
					num12 += num10 * (float)guilayoutEntry3.stretchWidth;
					if (guilayoutEntry3.style != GUILayoutUtility.spaceStyle)
					{
						int num13 = guilayoutEntry3.margin.left;
						if (flag)
						{
							num13 = 0;
							flag = false;
						}
						int num14 = ((num11 <= num13) ? num13 : num11);
						x += (float)num14;
						num11 = guilayoutEntry3.margin.right;
					}
					guilayoutEntry3.SetHorizontal(Mathf.Round(x), Mathf.Round(num12));
					x += num12 + this.spacing;
				}
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00012B6C File Offset: 0x00010D6C
		public override void CalcHeight()
		{
			if (this.entries.Count == 0)
			{
				this.maxHeight = (this.minHeight = (float)base.style.padding.vertical);
				return;
			}
			this.childMinHeight = (this.childMaxHeight = 0f);
			int num = 0;
			int num2 = 0;
			this.stretchableCountY = 0;
			if (this.isVertical)
			{
				int num3 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					guilayoutEntry.CalcHeight();
					RectOffset margin = guilayoutEntry.margin;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						int num4;
						if (!flag)
						{
							num4 = Mathf.Max(num3, margin.top);
						}
						else
						{
							num4 = 0;
							flag = false;
						}
						this.childMinHeight += guilayoutEntry.minHeight + this.spacing + (float)num4;
						this.childMaxHeight += guilayoutEntry.maxHeight + this.spacing + (float)num4;
						num3 = margin.bottom;
						this.stretchableCountY += guilayoutEntry.stretchHeight;
					}
					else
					{
						this.childMinHeight += guilayoutEntry.minHeight;
						this.childMaxHeight += guilayoutEntry.maxHeight;
						this.stretchableCountY += guilayoutEntry.stretchHeight;
					}
				}
				this.childMinHeight -= this.spacing;
				this.childMaxHeight -= this.spacing;
				if (this.entries.Count != 0)
				{
					num = this.entries[0].margin.top;
					num2 = num3;
				}
				else
				{
					num = (num2 = 0);
				}
			}
			else
			{
				bool flag2 = true;
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					guilayoutEntry2.CalcHeight();
					RectOffset margin2 = guilayoutEntry2.margin;
					if (guilayoutEntry2.style != GUILayoutUtility.spaceStyle)
					{
						if (!flag2)
						{
							num = Mathf.Min(margin2.top, num);
							num2 = Mathf.Min(margin2.bottom, num2);
						}
						else
						{
							num = margin2.top;
							num2 = margin2.bottom;
							flag2 = false;
						}
						this.childMinHeight = Mathf.Max(guilayoutEntry2.minHeight, this.childMinHeight);
						this.childMaxHeight = Mathf.Max(guilayoutEntry2.maxHeight, this.childMaxHeight);
					}
					this.stretchableCountY += guilayoutEntry2.stretchHeight;
				}
			}
			float num5;
			float num6;
			if (base.style != GUIStyle.none || this.userSpecifiedHeight)
			{
				num5 = (float)Mathf.Max(base.style.padding.top, num);
				num6 = (float)Mathf.Max(base.style.padding.bottom, num2);
			}
			else
			{
				this.m_Margin.top = num;
				this.m_Margin.bottom = num2;
				num6 = (num5 = 0f);
			}
			this.minHeight = Mathf.Max(this.minHeight, this.childMinHeight + num5 + num6);
			if (this.maxHeight == 0f)
			{
				this.stretchHeight += this.stretchableCountY + ((!base.style.stretchHeight) ? 0 : 1);
				this.maxHeight = this.childMaxHeight + num5 + num6;
			}
			else
			{
				this.stretchHeight = 0;
			}
			this.maxHeight = Mathf.Max(this.maxHeight, this.minHeight);
			if (base.style.fixedHeight != 0f)
			{
				this.maxHeight = (this.minHeight = base.style.fixedHeight);
				this.stretchHeight = 0;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00012FA4 File Offset: 0x000111A4
		public override void SetVertical(float y, float height)
		{
			base.SetVertical(y, height);
			if (this.entries.Count == 0)
			{
				return;
			}
			RectOffset padding = base.style.padding;
			if (this.resetCoords)
			{
				y = 0f;
			}
			if (this.isVertical)
			{
				if (base.style != GUIStyle.none)
				{
					float num = (float)padding.top;
					float num2 = (float)padding.bottom;
					if (this.entries.Count != 0)
					{
						num = Mathf.Max(num, (float)this.entries[0].margin.top);
						num2 = Mathf.Max(num2, (float)this.entries[this.entries.Count - 1].margin.bottom);
					}
					y += num;
					height -= num2 + num;
				}
				float num3 = height - this.spacing * (float)(this.entries.Count - 1);
				float num4 = 0f;
				if (this.childMinHeight != this.childMaxHeight)
				{
					num4 = Mathf.Clamp((num3 - this.childMinHeight) / (this.childMaxHeight - this.childMinHeight), 0f, 1f);
				}
				float num5 = 0f;
				if (num3 > this.childMaxHeight && this.stretchableCountY > 0)
				{
					num5 = (num3 - this.childMaxHeight) / (float)this.stretchableCountY;
				}
				int num6 = 0;
				bool flag = true;
				foreach (GUILayoutEntry guilayoutEntry in this.entries)
				{
					float num7 = Mathf.Lerp(guilayoutEntry.minHeight, guilayoutEntry.maxHeight, num4);
					num7 += num5 * (float)guilayoutEntry.stretchHeight;
					if (guilayoutEntry.style != GUILayoutUtility.spaceStyle)
					{
						int num8 = guilayoutEntry.margin.top;
						if (flag)
						{
							num8 = 0;
							flag = false;
						}
						int num9 = ((num6 <= num8) ? num8 : num6);
						y += (float)num9;
						num6 = guilayoutEntry.margin.bottom;
					}
					guilayoutEntry.SetVertical(Mathf.Round(y), Mathf.Round(num7));
					y += num7 + this.spacing;
				}
			}
			else if (base.style != GUIStyle.none)
			{
				foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
				{
					float num10 = (float)Mathf.Max(guilayoutEntry2.margin.top, padding.top);
					float num11 = y + num10;
					float num12 = height - (float)Mathf.Max(guilayoutEntry2.margin.bottom, padding.bottom) - num10;
					if (guilayoutEntry2.stretchHeight != 0)
					{
						guilayoutEntry2.SetVertical(num11, num12);
					}
					else
					{
						guilayoutEntry2.SetVertical(num11, Mathf.Clamp(num12, guilayoutEntry2.minHeight, guilayoutEntry2.maxHeight));
					}
				}
			}
			else
			{
				float num13 = y - (float)this.margin.top;
				float num14 = height + (float)this.margin.vertical;
				foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
				{
					if (guilayoutEntry3.stretchHeight != 0)
					{
						guilayoutEntry3.SetVertical(num13 + (float)guilayoutEntry3.margin.top, num14 - (float)guilayoutEntry3.margin.vertical);
					}
					else
					{
						guilayoutEntry3.SetVertical(num13 + (float)guilayoutEntry3.margin.top, Mathf.Clamp(num14 - (float)guilayoutEntry3.margin.vertical, guilayoutEntry3.minHeight, guilayoutEntry3.maxHeight));
					}
				}
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000133C0 File Offset: 0x000115C0
		public override string ToString()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text2 += " ";
			}
			string text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				base.ToString(),
				" Margins: ",
				this.childMinHeight,
				" {\n"
			});
			GUILayoutEntry.indent += 4;
			foreach (GUILayoutEntry guilayoutEntry in this.entries)
			{
				text = text + guilayoutEntry.ToString() + "\n";
			}
			text = text + text2 + "}";
			GUILayoutEntry.indent -= 4;
			return text;
		}

		// Token: 0x04000317 RID: 791
		public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

		// Token: 0x04000318 RID: 792
		public bool isVertical = true;

		// Token: 0x04000319 RID: 793
		public bool resetCoords;

		// Token: 0x0400031A RID: 794
		public float spacing;

		// Token: 0x0400031B RID: 795
		public bool sameSize = true;

		// Token: 0x0400031C RID: 796
		public bool isWindow;

		// Token: 0x0400031D RID: 797
		public int windowID = -1;

		// Token: 0x0400031E RID: 798
		private int cursor;

		// Token: 0x0400031F RID: 799
		protected int stretchableCountX = 100;

		// Token: 0x04000320 RID: 800
		protected int stretchableCountY = 100;

		// Token: 0x04000321 RID: 801
		protected bool userSpecifiedWidth;

		// Token: 0x04000322 RID: 802
		protected bool userSpecifiedHeight;

		// Token: 0x04000323 RID: 803
		protected float childMinWidth = 100f;

		// Token: 0x04000324 RID: 804
		protected float childMaxWidth = 100f;

		// Token: 0x04000325 RID: 805
		protected float childMinHeight = 100f;

		// Token: 0x04000326 RID: 806
		protected float childMaxHeight = 100f;

		// Token: 0x04000327 RID: 807
		private RectOffset m_Margin = new RectOffset();
	}
}
