using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001FE RID: 510
	public sealed class AnimatorUtility
	{
		// Token: 0x0600190F RID: 6415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OptimizeTransformHierarchy(GameObject go, string[] exposedTransforms);

		// Token: 0x06001910 RID: 6416
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeoptimizeTransformHierarchy(GameObject go);
	}
}
