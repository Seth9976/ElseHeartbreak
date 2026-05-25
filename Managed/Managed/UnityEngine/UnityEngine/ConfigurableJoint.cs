using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018F RID: 399
	public sealed class ConfigurableJoint : Joint
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000210B8 File Offset: 0x0001F2B8
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x000210D0 File Offset: 0x0001F2D0
		public Vector3 secondaryAxis
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_secondaryAxis(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_secondaryAxis(ref value);
			}
		}

		// Token: 0x060012A2 RID: 4770
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_secondaryAxis(out Vector3 value);

		// Token: 0x060012A3 RID: 4771
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_secondaryAxis(ref Vector3 value);

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060012A4 RID: 4772
		// (set) Token: 0x060012A5 RID: 4773
		public extern ConfigurableJointMotion xMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060012A6 RID: 4774
		// (set) Token: 0x060012A7 RID: 4775
		public extern ConfigurableJointMotion yMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060012A8 RID: 4776
		// (set) Token: 0x060012A9 RID: 4777
		public extern ConfigurableJointMotion zMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060012AA RID: 4778
		// (set) Token: 0x060012AB RID: 4779
		public extern ConfigurableJointMotion angularXMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060012AC RID: 4780
		// (set) Token: 0x060012AD RID: 4781
		public extern ConfigurableJointMotion angularYMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060012AE RID: 4782
		// (set) Token: 0x060012AF RID: 4783
		public extern ConfigurableJointMotion angularZMotion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000210DC File Offset: 0x0001F2DC
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x000210F4 File Offset: 0x0001F2F4
		public SoftJointLimit linearLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_linearLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_linearLimit(ref value);
			}
		}

		// Token: 0x060012B2 RID: 4786
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_linearLimit(out SoftJointLimit value);

		// Token: 0x060012B3 RID: 4787
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_linearLimit(ref SoftJointLimit value);

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00021100 File Offset: 0x0001F300
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00021118 File Offset: 0x0001F318
		public SoftJointLimit lowAngularXLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_lowAngularXLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_lowAngularXLimit(ref value);
			}
		}

		// Token: 0x060012B6 RID: 4790
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lowAngularXLimit(out SoftJointLimit value);

		// Token: 0x060012B7 RID: 4791
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lowAngularXLimit(ref SoftJointLimit value);

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00021124 File Offset: 0x0001F324
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x0002113C File Offset: 0x0001F33C
		public SoftJointLimit highAngularXLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_highAngularXLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_highAngularXLimit(ref value);
			}
		}

		// Token: 0x060012BA RID: 4794
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_highAngularXLimit(out SoftJointLimit value);

		// Token: 0x060012BB RID: 4795
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_highAngularXLimit(ref SoftJointLimit value);

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00021148 File Offset: 0x0001F348
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00021160 File Offset: 0x0001F360
		public SoftJointLimit angularYLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_angularYLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_angularYLimit(ref value);
			}
		}

		// Token: 0x060012BE RID: 4798
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularYLimit(out SoftJointLimit value);

		// Token: 0x060012BF RID: 4799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularYLimit(ref SoftJointLimit value);

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0002116C File Offset: 0x0001F36C
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00021184 File Offset: 0x0001F384
		public SoftJointLimit angularZLimit
		{
			get
			{
				SoftJointLimit softJointLimit;
				this.INTERNAL_get_angularZLimit(out softJointLimit);
				return softJointLimit;
			}
			set
			{
				this.INTERNAL_set_angularZLimit(ref value);
			}
		}

		// Token: 0x060012C2 RID: 4802
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularZLimit(out SoftJointLimit value);

		// Token: 0x060012C3 RID: 4803
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularZLimit(ref SoftJointLimit value);

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00021190 File Offset: 0x0001F390
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x000211A8 File Offset: 0x0001F3A8
		public Vector3 targetPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_targetPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_targetPosition(ref value);
			}
		}

		// Token: 0x060012C6 RID: 4806
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetPosition(out Vector3 value);

		// Token: 0x060012C7 RID: 4807
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetPosition(ref Vector3 value);

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000211B4 File Offset: 0x0001F3B4
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x000211CC File Offset: 0x0001F3CC
		public Vector3 targetVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_targetVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_targetVelocity(ref value);
			}
		}

		// Token: 0x060012CA RID: 4810
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetVelocity(out Vector3 value);

		// Token: 0x060012CB RID: 4811
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetVelocity(ref Vector3 value);

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000211D8 File Offset: 0x0001F3D8
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000211F0 File Offset: 0x0001F3F0
		public JointDrive xDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_xDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_xDrive(ref value);
			}
		}

		// Token: 0x060012CE RID: 4814
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_xDrive(out JointDrive value);

		// Token: 0x060012CF RID: 4815
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_xDrive(ref JointDrive value);

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000211FC File Offset: 0x0001F3FC
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00021214 File Offset: 0x0001F414
		public JointDrive yDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_yDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_yDrive(ref value);
			}
		}

		// Token: 0x060012D2 RID: 4818
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_yDrive(out JointDrive value);

		// Token: 0x060012D3 RID: 4819
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_yDrive(ref JointDrive value);

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00021220 File Offset: 0x0001F420
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00021238 File Offset: 0x0001F438
		public JointDrive zDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_zDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_zDrive(ref value);
			}
		}

		// Token: 0x060012D6 RID: 4822
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_zDrive(out JointDrive value);

		// Token: 0x060012D7 RID: 4823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_zDrive(ref JointDrive value);

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00021244 File Offset: 0x0001F444
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x0002125C File Offset: 0x0001F45C
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

		// Token: 0x060012DA RID: 4826
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetRotation(out Quaternion value);

		// Token: 0x060012DB RID: 4827
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetRotation(ref Quaternion value);

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00021268 File Offset: 0x0001F468
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00021280 File Offset: 0x0001F480
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

		// Token: 0x060012DE RID: 4830
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetAngularVelocity(out Vector3 value);

		// Token: 0x060012DF RID: 4831
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetAngularVelocity(ref Vector3 value);

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060012E0 RID: 4832
		// (set) Token: 0x060012E1 RID: 4833
		public extern RotationDriveMode rotationDriveMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0002128C File Offset: 0x0001F48C
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x000212A4 File Offset: 0x0001F4A4
		public JointDrive angularXDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_angularXDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_angularXDrive(ref value);
			}
		}

		// Token: 0x060012E4 RID: 4836
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularXDrive(out JointDrive value);

		// Token: 0x060012E5 RID: 4837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularXDrive(ref JointDrive value);

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000212B0 File Offset: 0x0001F4B0
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x000212C8 File Offset: 0x0001F4C8
		public JointDrive angularYZDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_angularYZDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_angularYZDrive(ref value);
			}
		}

		// Token: 0x060012E8 RID: 4840
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularYZDrive(out JointDrive value);

		// Token: 0x060012E9 RID: 4841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularYZDrive(ref JointDrive value);

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x000212D4 File Offset: 0x0001F4D4
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x000212EC File Offset: 0x0001F4EC
		public JointDrive slerpDrive
		{
			get
			{
				JointDrive jointDrive;
				this.INTERNAL_get_slerpDrive(out jointDrive);
				return jointDrive;
			}
			set
			{
				this.INTERNAL_set_slerpDrive(ref value);
			}
		}

		// Token: 0x060012EC RID: 4844
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_slerpDrive(out JointDrive value);

		// Token: 0x060012ED RID: 4845
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_slerpDrive(ref JointDrive value);

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060012EE RID: 4846
		// (set) Token: 0x060012EF RID: 4847
		public extern JointProjectionMode projectionMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060012F0 RID: 4848
		// (set) Token: 0x060012F1 RID: 4849
		public extern float projectionDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060012F2 RID: 4850
		// (set) Token: 0x060012F3 RID: 4851
		public extern float projectionAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060012F4 RID: 4852
		// (set) Token: 0x060012F5 RID: 4853
		public extern bool configuredInWorldSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060012F6 RID: 4854
		// (set) Token: 0x060012F7 RID: 4855
		public extern bool swapBodies
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
