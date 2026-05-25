using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000190 RID: 400
	public sealed class ConstantForce : Behaviour
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00021300 File Offset: 0x0001F500
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00021318 File Offset: 0x0001F518
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

		// Token: 0x060012FB RID: 4859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_force(out Vector3 value);

		// Token: 0x060012FC RID: 4860
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_force(ref Vector3 value);

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00021324 File Offset: 0x0001F524
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x0002133C File Offset: 0x0001F53C
		public Vector3 relativeForce
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_relativeForce(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_relativeForce(ref value);
			}
		}

		// Token: 0x060012FF RID: 4863
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_relativeForce(out Vector3 value);

		// Token: 0x06001300 RID: 4864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_relativeForce(ref Vector3 value);

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00021348 File Offset: 0x0001F548
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00021360 File Offset: 0x0001F560
		public Vector3 torque
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_torque(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_torque(ref value);
			}
		}

		// Token: 0x06001303 RID: 4867
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_torque(out Vector3 value);

		// Token: 0x06001304 RID: 4868
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_torque(ref Vector3 value);

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0002136C File Offset: 0x0001F56C
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00021384 File Offset: 0x0001F584
		public Vector3 relativeTorque
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_relativeTorque(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_relativeTorque(ref value);
			}
		}

		// Token: 0x06001307 RID: 4871
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_relativeTorque(out Vector3 value);

		// Token: 0x06001308 RID: 4872
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_relativeTorque(ref Vector3 value);
	}
}
