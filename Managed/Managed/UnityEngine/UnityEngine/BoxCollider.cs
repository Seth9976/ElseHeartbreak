using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	public sealed class BoxCollider : Collider
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000213DC File Offset: 0x0001F5DC
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x000213F4 File Offset: 0x0001F5F4
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

		// Token: 0x0600131D RID: 4893
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x0600131E RID: 4894
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00021400 File Offset: 0x0001F600
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x00021418 File Offset: 0x0001F618
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

		// Token: 0x06001321 RID: 4897
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x06001322 RID: 4898
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00021424 File Offset: 0x0001F624
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x00021438 File Offset: 0x0001F638
		[Obsolete("use BoxCollider.size instead.")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}
	}
}
