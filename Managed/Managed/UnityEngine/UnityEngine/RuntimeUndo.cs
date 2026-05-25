using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000135 RID: 309
	internal sealed class RuntimeUndo
	{
		// Token: 0x06000D1C RID: 3356
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetTransformParent(Transform transform, Transform newParent, string name);

		// Token: 0x06000D1D RID: 3357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RecordObject(Object objectToUndo, string name);

		// Token: 0x06000D1E RID: 3358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RecordObjects(Object[] objectsToUndo, string name);
	}
}
