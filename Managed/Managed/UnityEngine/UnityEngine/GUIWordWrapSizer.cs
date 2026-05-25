using System;

namespace UnityEngine
{
	// Token: 0x020000F5 RID: 245
	internal sealed class GUIWordWrapSizer : GUILayoutEntry
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public GUIWordWrapSizer(GUIStyle _style, GUIContent _content, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, _style)
		{
			this.content = new GUIContent(_content);
			base.ApplyOptions(options);
			this.forcedMinHeight = this.minHeight;
			this.forcedMaxHeight = this.maxHeight;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00013E14 File Offset: 0x00012014
		public override void CalcWidth()
		{
			if (this.minWidth == 0f || this.maxWidth == 0f)
			{
				float num;
				float num2;
				base.style.CalcMinMaxWidth(this.content, out num, out num2);
				if (this.minWidth == 0f)
				{
					this.minWidth = num;
				}
				if (this.maxWidth == 0f)
				{
					this.maxWidth = num2;
				}
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00013E84 File Offset: 0x00012084
		public override void CalcHeight()
		{
			if (this.forcedMinHeight == 0f || this.forcedMaxHeight == 0f)
			{
				float num = base.style.CalcHeight(this.content, this.rect.width);
				if (this.forcedMinHeight == 0f)
				{
					this.minHeight = num;
				}
				else
				{
					this.minHeight = this.forcedMinHeight;
				}
				if (this.forcedMaxHeight == 0f)
				{
					this.maxHeight = num;
				}
				else
				{
					this.maxHeight = this.forcedMaxHeight;
				}
			}
		}

		// Token: 0x0400033B RID: 827
		private GUIContent content;

		// Token: 0x0400033C RID: 828
		private float forcedMinHeight;

		// Token: 0x0400033D RID: 829
		private float forcedMaxHeight;
	}
}
