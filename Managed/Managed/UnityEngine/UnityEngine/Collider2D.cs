using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B1 RID: 433
	public class Collider2D : Behaviour
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600152F RID: 5423
		// (set) Token: 0x06001530 RID: 5424
		public extern bool isTrigger
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001531 RID: 5425
		public extern Rigidbody2D attachedRigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001532 RID: 5426
		public extern int shapeCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00022F3C File Offset: 0x0002113C
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
		}

		// Token: 0x06001534 RID: 5428
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001535 RID: 5429
		internal extern ColliderErrorState2D errorState
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00022F54 File Offset: 0x00021154
		public bool OverlapPoint(Vector2 point)
		{
			return Collider2D.INTERNAL_CALL_OverlapPoint(this, ref point);
		}

		// Token: 0x06001537 RID: 5431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_OverlapPoint(Collider2D self, ref Vector2 point);

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001538 RID: 5432
		// (set) Token: 0x06001539 RID: 5433
		public extern PhysicsMaterial2D sharedMaterial
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
