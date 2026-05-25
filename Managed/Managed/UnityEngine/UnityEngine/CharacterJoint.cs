using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018C RID: 396
	public sealed class CharacterJoint : Joint
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00020F90 File Offset: 0x0001F190
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		public Vector3 swingAxis
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_swingAxis(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_swingAxis(ref value);
			}
		}

		// Token: 0x06001281 RID: 4737
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swingAxis(out Vector3 value);

		// Token: 0x06001282 RID: 4738
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swingAxis(ref Vector3 value);

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x00020FCC File Offset: 0x0001F1CC
		public SoftJointLimit lowTwistLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_lowTwistLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_lowTwistLimit(ref value);
			}
		}

		// Token: 0x06001285 RID: 4741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lowTwistLimit(out SoftJointLimit value);

		// Token: 0x06001286 RID: 4742
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lowTwistLimit(ref SoftJointLimit value);

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		public SoftJointLimit highTwistLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_highTwistLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_highTwistLimit(ref value);
			}
		}

		// Token: 0x06001289 RID: 4745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_highTwistLimit(out SoftJointLimit value);

		// Token: 0x0600128A RID: 4746
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_highTwistLimit(ref SoftJointLimit value);

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00020FFC File Offset: 0x0001F1FC
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00021014 File Offset: 0x0001F214
		public SoftJointLimit swing1Limit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_swing1Limit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_swing1Limit(ref value);
			}
		}

		// Token: 0x0600128D RID: 4749
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swing1Limit(out SoftJointLimit value);

		// Token: 0x0600128E RID: 4750
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swing1Limit(ref SoftJointLimit value);

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00021020 File Offset: 0x0001F220
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x00021038 File Offset: 0x0001F238
		public SoftJointLimit swing2Limit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_swing2Limit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_swing2Limit(ref value);
			}
		}

		// Token: 0x06001291 RID: 4753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_swing2Limit(out SoftJointLimit value);

		// Token: 0x06001292 RID: 4754
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_swing2Limit(ref SoftJointLimit value);

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00021044 File Offset: 0x0001F244
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x0002105C File Offset: 0x0001F25C
		public Quaternion targetRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_targetRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_targetRotation(ref value);
			}
		}

		// Token: 0x06001295 RID: 4757
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetRotation(out Quaternion value);

		// Token: 0x06001296 RID: 4758
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetRotation(ref Quaternion value);

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00021068 File Offset: 0x0001F268
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x00021080 File Offset: 0x0001F280
		public Vector3 targetAngularVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_targetAngularVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_targetAngularVelocity(ref value);
			}
		}

		// Token: 0x06001299 RID: 4761
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetAngularVelocity(out Vector3 value);

		// Token: 0x0600129A RID: 4762
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetAngularVelocity(ref Vector3 value);

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0002108C File Offset: 0x0001F28C
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x000210A4 File Offset: 0x0001F2A4
		public JointDrive rotationDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_rotationDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_rotationDrive(ref value);
			}
		}

		// Token: 0x0600129D RID: 4765
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rotationDrive(out JointDrive value);

		// Token: 0x0600129E RID: 4766
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rotationDrive(ref JointDrive value);
	}
}
