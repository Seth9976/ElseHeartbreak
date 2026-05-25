using System;

namespace UnityEngine
{
	// Token: 0x020000F3 RID: 243
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x000138E0 File Offset: 0x00011AE0
		public GUIAspectSizer(float aspect, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00013910 File Offset: 0x00011B10
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x04000334 RID: 820
		private float aspect;
	}
}
