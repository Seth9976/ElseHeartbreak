using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Sprites
{
	// Token: 0x0200014A RID: 330
	public sealed class DataUtility
	{
		// Token: 0x06000DD8 RID: 3544
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Vector4 GetInnerUV(Sprite sprite);

		// Token: 0x06000DD9 RID: 3545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Vector4 GetOuterUV(Sprite sprite);

		// Token: 0x06000DDA RID: 3546
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Vector4 GetPadding(Sprite sprite);

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001D7C4 File Offset: 0x0001B9C4
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 vector;
			DataUtility.Internal_GetMinSize(sprite, out vector);
			return vector;
		}

		// Token: 0x06000DDC RID: 3548
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetMinSize(Sprite sprite, out Vector2 output);
	}
}
