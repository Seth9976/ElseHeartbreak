using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009C RID: 156
	public sealed class OcclusionArea : Component
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B570 File Offset: 0x00009770
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000B588 File Offset: 0x00009788
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_center(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x06000335 RID: 821
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06000336 RID: 822
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000B594 File Offset: 0x00009794
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000B5AC File Offset: 0x000097AC
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_size(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x06000339 RID: 825
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x0600033A RID: 826
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);
	}
}
