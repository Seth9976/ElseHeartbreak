using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001A3 RID: 419
	public class Cloth : Component
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060013DF RID: 5087
		// (set) Token: 0x060013E0 RID: 5088
		public extern float bendingStiffness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060013E1 RID: 5089
		// (set) Token: 0x060013E2 RID: 5090
		public extern float stretchingStiffness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060013E3 RID: 5091
		// (set) Token: 0x060013E4 RID: 5092
		public extern float damping
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060013E5 RID: 5093
		// (set) Token: 0x060013E6 RID: 5094
		public extern float thickness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00021B50 File Offset: 0x0001FD50
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00021B68 File Offset: 0x0001FD68
		public Vector3 externalAcceleration
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_externalAcceleration(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_externalAcceleration(ref value);
			}
		}

		// Token: 0x060013E9 RID: 5097
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_externalAcceleration(out Vector3 value);

		// Token: 0x060013EA RID: 5098
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_externalAcceleration(ref Vector3 value);

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00021B74 File Offset: 0x0001FD74
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x00021B8C File Offset: 0x0001FD8C
		public Vector3 randomAcceleration
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_randomAcceleration(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_randomAcceleration(ref value);
			}
		}

		// Token: 0x060013ED RID: 5101
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_randomAcceleration(out Vector3 value);

		// Token: 0x060013EE RID: 5102
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_randomAcceleration(ref Vector3 value);

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060013EF RID: 5103
		// (set) Token: 0x060013F0 RID: 5104
		public extern bool useGravity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060013F1 RID: 5105
		// (set) Token: 0x060013F2 RID: 5106
		public extern bool selfCollision
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060013F3 RID: 5107
		// (set) Token: 0x060013F4 RID: 5108
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060013F5 RID: 5109
		public extern Vector3[] vertices
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060013F6 RID: 5110
		public extern Vector3[] normals
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
