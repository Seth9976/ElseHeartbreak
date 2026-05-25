using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationClipPair
	{
		// Token: 0x04000727 RID: 1831
		public AnimationClip originalClip;

		// Token: 0x04000728 RID: 1832
		public AnimationClip overrideClip;
	}
}
