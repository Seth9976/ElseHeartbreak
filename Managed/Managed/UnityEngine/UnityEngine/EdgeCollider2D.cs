using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B4 RID: 436
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x0600154B RID: 5451
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600154C RID: 5452
		public extern int edgeCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600154D RID: 5453
		public extern int pointCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600154E RID: 5454
		// (set) Token: 0x0600154F RID: 5455
		public extern Vector2[] points
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
