using System;

namespace UnityEngine
{
	// Token: 0x020000F4 RID: 244
	internal sealed class GUIGridSizer : GUILayoutEntry
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x00013940 File Offset: 0x00011B40
		private GUIGridSizer(GUIContent[] contents, int _xCount, GUIStyle buttonStyle, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.count = contents.Length;
			this.xCount = _xCount;
			this.ApplyStyleSettings(buttonStyle);
			this.ApplyOptions(options);
			if (_xCount == 0 || contents.Length == 0)
			{
				return;
			}
			float num = (float)(Mathf.Max(buttonStyle.margin.left, buttonStyle.margin.right) * (this.xCount - 1));
			float num2 = (float)(Mathf.Max(buttonStyle.margin.top, buttonStyle.margin.bottom) * (this.rows - 1));
			if (buttonStyle.fixedWidth != 0f)
			{
				this.minButtonWidth = (this.maxButtonWidth = buttonStyle.fixedWidth);
			}
			if (buttonStyle.fixedHeight != 0f)
			{
				this.minButtonHeight = (this.maxButtonHeight = buttonStyle.fixedHeight);
			}
			if (this.minButtonWidth == -1f)
			{
				if (this.minWidth != 0f)
				{
					this.minButtonWidth = (this.minWidth - num) / (float)this.xCount;
				}
				if (this.maxWidth != 0f)
				{
					this.maxButtonWidth = (this.maxWidth - num) / (float)this.xCount;
				}
			}
			if (this.minButtonHeight == -1f)
			{
				if (this.minHeight != 0f)
				{
					this.minButtonHeight = (this.minHeight - num2) / (float)this.rows;
				}
				if (this.maxHeight != 0f)
				{
					this.maxButtonHeight = (this.maxHeight - num2) / (float)this.rows;
				}
			}
			if (this.minButtonHeight == -1f || this.maxButtonHeight == -1f || this.minButtonWidth == -1f || this.maxButtonWidth == -1f)
			{
				float num3 = 0f;
				float num4 = 0f;
				foreach (GUIContent guicontent in contents)
				{
					Vector2 vector = buttonStyle.CalcSize(guicontent);
					num4 = Mathf.Max(num4, vector.x);
					num3 = Mathf.Max(num3, vector.y);
				}
				if (this.minButtonWidth == -1f)
				{
					if (this.maxButtonWidth != -1f)
					{
						this.minButtonWidth = Mathf.Min(num4, this.maxButtonWidth);
					}
					else
					{
						this.minButtonWidth = num4;
					}
				}
				if (this.maxButtonWidth == -1f)
				{
					if (this.minButtonWidth != -1f)
					{
						this.maxButtonWidth = Mathf.Max(num4, this.minButtonWidth);
					}
					else
					{
						this.maxButtonWidth = num4;
					}
				}
				if (this.minButtonHeight == -1f)
				{
					if (this.maxButtonHeight != -1f)
					{
						this.minButtonHeight = Mathf.Min(num3, this.maxButtonHeight);
					}
					else
					{
						this.minButtonHeight = num3;
					}
				}
				if (this.maxButtonHeight == -1f)
				{
					if (this.minButtonHeight != -1f)
					{
						this.maxHeight = Mathf.Max(this.maxHeight, this.minButtonHeight);
					}
					this.maxButtonHeight = this.maxHeight;
				}
			}
			this.minWidth = this.minButtonWidth * (float)this.xCount + num;
			this.maxWidth = this.maxButtonWidth * (float)this.xCount + num;
			this.minHeight = this.minButtonHeight * (float)this.rows + num2;
			this.maxHeight = this.maxButtonHeight * (float)this.rows + num2;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00013D04 File Offset: 0x00011F04
		public static Rect GetRect(GUIContent[] contents, int xCount, GUIStyle style, GUILayoutOption[] options)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type == EventType.Used)
				{
					return GUILayoutEntry.kDummyRect;
				}
				rect = GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIGridSizer(contents, xCount, style, options));
			}
			return rect;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00013D8C File Offset: 0x00011F8C
		private int rows
		{
			get
			{
				int num = this.count / this.xCount;
				if (this.count % this.xCount != 0)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x04000335 RID: 821
		private int count;

		// Token: 0x04000336 RID: 822
		private int xCount;

		// Token: 0x04000337 RID: 823
		private float minButtonWidth = -1f;

		// Token: 0x04000338 RID: 824
		private float maxButtonWidth = -1f;

		// Token: 0x04000339 RID: 825
		private float minButtonHeight = -1f;

		// Token: 0x0400033A RID: 826
		private float maxButtonHeight = -1f;
	}
}
