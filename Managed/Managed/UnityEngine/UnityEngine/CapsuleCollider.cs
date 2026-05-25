using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000196 RID: 406
	public sealed class CapsuleCollider : Collider
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0002149C File Offset: 0x0001F69C
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x000214B4 File Offset: 0x0001F6B4
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

		// Token: 0x06001338 RID: 4920
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001339 RID: 4921
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600133A RID: 4922
		// (set) Token: 0x0600133B RID: 4923
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600133C RID: 4924
		// (set) Token: 0x0600133D RID: 4925
		public extern float height
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600133E RID: 4926
		// (set) Token: 0x0600133F RID: 4927
		public extern int direction
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
