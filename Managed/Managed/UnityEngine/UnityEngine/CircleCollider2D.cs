using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B2 RID: 434
	public sealed class CircleCollider2D : Collider2D
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00022F68 File Offset: 0x00021168
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x00022F80 File Offset: 0x00021180
		public Vector2 center
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_center(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x0600153D RID: 5437
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector2 value);

		// Token: 0x0600153E RID: 5438
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector2 value);

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600153F RID: 5439
		// (set) Token: 0x06001540 RID: 5440
		public extern float radius
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
