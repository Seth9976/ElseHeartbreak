using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D8 RID: 216
	public class GUIElement : Behaviour
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public bool HitTest(Vector3 screenPosition, [DefaultValue("null")] Camera camera)
		{
			return GUIElement.INTERNAL_CALL_HitTest(this, ref screenPosition, camera);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		[ExcludeFromDocs]
		public bool HitTest(Vector3 screenPosition)
		{
			Camera camera = null;
			return GUIElement.INTERNAL_CALL_HitTest(this, ref screenPosition, camera);
		}

		// Token: 0x06000677 RID: 1655
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_HitTest(GUIElement self, ref Vector3 screenPosition, Camera camera);

		// Token: 0x06000678 RID: 1656
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect GetScreenRect([DefaultValue("null")] Camera camera);

		// Token: 0x06000679 RID: 1657 RVA: 0x0000CE0C File Offset: 0x0000B00C
		[ExcludeFromDocs]
		public Rect GetScreenRect()
		{
			Camera camera = null;
			return this.GetScreenRect(camera);
		}
	}
}
