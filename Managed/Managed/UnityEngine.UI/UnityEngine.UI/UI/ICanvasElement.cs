using System;

namespace UnityEngine.UI
{
	// Token: 0x02000038 RID: 56
	public interface ICanvasElement
	{
		// Token: 0x06000154 RID: 340
		void Rebuild(CanvasUpdate executing);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000155 RID: 341
		Transform transform { get; }

		// Token: 0x06000156 RID: 342
		bool IsDestroyed();
	}
}
