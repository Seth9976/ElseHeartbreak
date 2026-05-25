using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000203 RID: 515
	public sealed class AvatarBuilder
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x000248EC File Offset: 0x00022AEC
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription monoHumanDescription)
		{
			return AvatarBuilder.INTERNAL_CALL_BuildHumanAvatar(go, ref monoHumanDescription);
		}

		// Token: 0x0600192F RID: 6447
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Avatar INTERNAL_CALL_BuildHumanAvatar(GameObject go, ref HumanDescription monoHumanDescription);

		// Token: 0x06001930 RID: 6448
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Avatar BuildGenericAvatar(GameObject go, string rootMotionTransformName);
	}
}
