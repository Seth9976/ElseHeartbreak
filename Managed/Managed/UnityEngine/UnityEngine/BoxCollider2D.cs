using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B3 RID: 435
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00022F94 File Offset: 0x00021194
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x00022FAC File Offset: 0x000211AC
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

		// Token: 0x06001544 RID: 5444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector2 value);

		// Token: 0x06001545 RID: 5445
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector2 value);

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00022FB8 File Offset: 0x000211B8
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x00022FD0 File Offset: 0x000211D0
		public Vector2 size
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_size(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x06001548 RID: 5448
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector2 value);

		// Token: 0x06001549 RID: 5449
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector2 value);
	}
}
