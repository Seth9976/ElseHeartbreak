using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x0200002F RID: 47
	internal interface ITweenValue
	{
		// Token: 0x0600012B RID: 299
		void TweenValue(float floatPercentage);

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012C RID: 300
		bool ignoreTimeScale { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012D RID: 301
		float duration { get; }

		// Token: 0x0600012E RID: 302
		bool ValidTarget();
	}
}
