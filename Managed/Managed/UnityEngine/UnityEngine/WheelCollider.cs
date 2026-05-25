using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200019A RID: 410
	public sealed class WheelCollider : Collider
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x000215FC File Offset: 0x0001F7FC
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x00021614 File Offset: 0x0001F814
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

		// Token: 0x06001364 RID: 4964
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06001365 RID: 4965
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001366 RID: 4966
		// (set) Token: 0x06001367 RID: 4967
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001368 RID: 4968
		// (set) Token: 0x06001369 RID: 4969
		public extern float suspensionDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00021620 File Offset: 0x0001F820
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x00021638 File Offset: 0x0001F838
		public JointSpring suspensionSpring
		{
			get
			{
				JointSpring jointSpring;
				this.INTERNAL_get_suspensionSpring(out jointSpring);
				return jointSpring;
			}
			set
			{
				this.INTERNAL_set_suspensionSpring(ref value);
			}
		}

		// Token: 0x0600136C RID: 4972
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_suspensionSpring(out JointSpring value);

		// Token: 0x0600136D RID: 4973
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_suspensionSpring(ref JointSpring value);

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600136E RID: 4974
		// (set) Token: 0x0600136F RID: 4975
		public extern float mass
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00021644 File Offset: 0x0001F844
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x0002165C File Offset: 0x0001F85C
		public WheelFrictionCurve forwardFriction
		{
			get
			{
				WheelFrictionCurve wheelFrictionCurve;
				this.INTERNAL_get_forwardFriction(out wheelFrictionCurve);
				return wheelFrictionCurve;
			}
			set
			{
				this.INTERNAL_set_forwardFriction(ref value);
			}
		}

		// Token: 0x06001372 RID: 4978
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_forwardFriction(out WheelFrictionCurve value);

		// Token: 0x06001373 RID: 4979
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_forwardFriction(ref WheelFrictionCurve value);

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00021668 File Offset: 0x0001F868
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00021680 File Offset: 0x0001F880
		public WheelFrictionCurve sidewaysFriction
		{
			get
			{
				WheelFrictionCurve wheelFrictionCurve;
				this.INTERNAL_get_sidewaysFriction(out wheelFrictionCurve);
				return wheelFrictionCurve;
			}
			set
			{
				this.INTERNAL_set_sidewaysFriction(ref value);
			}
		}

		// Token: 0x06001376 RID: 4982
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_sidewaysFriction(out WheelFrictionCurve value);

		// Token: 0x06001377 RID: 4983
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_sidewaysFriction(ref WheelFrictionCurve value);

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001378 RID: 4984
		// (set) Token: 0x06001379 RID: 4985
		public extern float motorTorque
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600137A RID: 4986
		// (set) Token: 0x0600137B RID: 4987
		public extern float brakeTorque
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600137C RID: 4988
		// (set) Token: 0x0600137D RID: 4989
		public extern float steerAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600137E RID: 4990
		public extern bool isGrounded
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600137F RID: 4991
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetGroundHit(out WheelHit hit);

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001380 RID: 4992
		public extern float rpm
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
