using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	public sealed class CharacterController : Collider
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		public bool SimpleMove(Vector3 speed)
		{
			return CharacterController.INTERNAL_CALL_SimpleMove(this, ref speed);
		}

		// Token: 0x060013C9 RID: 5065
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SimpleMove(CharacterController self, ref Vector3 speed);

		// Token: 0x060013CA RID: 5066 RVA: 0x00021B00 File Offset: 0x0001FD00
		public CollisionFlags Move(Vector3 motion)
		{
			return CharacterController.INTERNAL_CALL_Move(this, ref motion);
		}

		// Token: 0x060013CB RID: 5067
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionFlags INTERNAL_CALL_Move(CharacterController self, ref Vector3 motion);

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060013CC RID: 5068
		public extern bool isGrounded
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00021B0C File Offset: 0x0001FD0C
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
		}

		// Token: 0x060013CE RID: 5070
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060013CF RID: 5071
		public extern CollisionFlags collisionFlags
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060013D0 RID: 5072
		// (set) Token: 0x060013D1 RID: 5073
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060013D2 RID: 5074
		// (set) Token: 0x060013D3 RID: 5075
		public extern float height
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00021B24 File Offset: 0x0001FD24
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00021B3C File Offset: 0x0001FD3C
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

		// Token: 0x060013D6 RID: 5078
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x060013D7 RID: 5079
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060013D8 RID: 5080
		// (set) Token: 0x060013D9 RID: 5081
		public extern float slopeLimit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060013DA RID: 5082
		// (set) Token: 0x060013DB RID: 5083
		public extern float stepOffset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060013DC RID: 5084
		// (set) Token: 0x060013DD RID: 5085
		public extern bool detectCollisions
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
