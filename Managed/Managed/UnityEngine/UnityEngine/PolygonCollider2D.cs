using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001B5 RID: 437
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001551 RID: 5457
		// (set) Token: 0x06001552 RID: 5458
		public extern Vector2[] points
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001553 RID: 5459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector2[] GetPath(int index);

		// Token: 0x06001554 RID: 5460
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPath(int index, Vector2[] points);

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001555 RID: 5461
		// (set) Token: 0x06001556 RID: 5462
		public extern int pathCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001557 RID: 5463
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetTotalPointCount();

		// Token: 0x06001558 RID: 5464 RVA: 0x00022FEC File Offset: 0x000211EC
		public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
		{
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref scale, ref offset);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00022FFC File Offset: 0x000211FC
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides, Vector2 scale)
		{
			Vector2 zero = Vector2.zero;
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref scale, ref zero);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0002301C File Offset: 0x0002121C
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides)
		{
			Vector2 zero = Vector2.zero;
			Vector2 one = Vector2.one;
			PolygonCollider2D.INTERNAL_CALL_CreatePrimitive(this, sides, ref one, ref zero);
		}

		// Token: 0x0600155B RID: 5467
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CreatePrimitive(PolygonCollider2D self, int sides, ref Vector2 scale, ref Vector2 offset);
	}
}
