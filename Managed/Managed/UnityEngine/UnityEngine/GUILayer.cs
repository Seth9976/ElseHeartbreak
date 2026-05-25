using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E3 RID: 227
	public sealed class GUILayer : Behaviour
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x0000D024 File Offset: 0x0000B224
		public GUIElement HitTest(Vector3 screenPosition)
		{
			return GUILayer.INTERNAL_CALL_HitTest(this, ref screenPosition);
		}

		// Token: 0x060006C1 RID: 1729
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GUIElement INTERNAL_CALL_HitTest(GUILayer self, ref Vector3 screenPosition);
	}
}
