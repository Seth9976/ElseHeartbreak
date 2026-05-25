using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B8 RID: 184
	public sealed class ParticleAnimator : Component
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049C RID: 1180
		// (set) Token: 0x0600049D RID: 1181
		public extern bool doesAnimateColor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000BD60 File Offset: 0x00009F60
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000BD78 File Offset: 0x00009F78
		public Vector3 worldRotationAxis
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_worldRotationAxis(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_worldRotationAxis(ref value);
			}
		}

		// Token: 0x060004A0 RID: 1184
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldRotationAxis(out Vector3 value);

		// Token: 0x060004A1 RID: 1185
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldRotationAxis(ref Vector3 value);

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000BD84 File Offset: 0x00009F84
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public Vector3 localRotationAxis
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localRotationAxis(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localRotationAxis(ref value);
			}
		}

		// Token: 0x060004A4 RID: 1188
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localRotationAxis(out Vector3 value);

		// Token: 0x060004A5 RID: 1189
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localRotationAxis(ref Vector3 value);

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004A6 RID: 1190
		// (set) Token: 0x060004A7 RID: 1191
		public extern float sizeGrow
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public Vector3 rndForce
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_rndForce(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_rndForce(ref value);
			}
		}

		// Token: 0x060004AA RID: 1194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rndForce(out Vector3 value);

		// Token: 0x060004AB RID: 1195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rndForce(ref Vector3 value);

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000BDCC File Offset: 0x00009FCC
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public Vector3 force
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_force(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_force(ref value);
			}
		}

		// Token: 0x060004AE RID: 1198
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_force(out Vector3 value);

		// Token: 0x060004AF RID: 1199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_force(ref Vector3 value);

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B0 RID: 1200
		// (set) Token: 0x060004B1 RID: 1201
		public extern float damping
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B2 RID: 1202
		// (set) Token: 0x060004B3 RID: 1203
		public extern bool autodestruct
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004B4 RID: 1204
		// (set) Token: 0x060004B5 RID: 1205
		public extern Color[] colorAnimation
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
