using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000185 RID: 389
	public sealed class HingeJoint : Joint
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00020E58 File Offset: 0x0001F058
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00020E70 File Offset: 0x0001F070
		public JointMotor motor
		{
			get
			{
				JointMotor jointMotor;
				this.INTERNAL_get_motor(out jointMotor);
				return jointMotor;
			}
			set
			{
				this.INTERNAL_set_motor(ref value);
			}
		}

		// Token: 0x06001250 RID: 4688
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_motor(out JointMotor value);

		// Token: 0x06001251 RID: 4689
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_motor(ref JointMotor value);

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00020E7C File Offset: 0x0001F07C
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00020E94 File Offset: 0x0001F094
		public JointLimits limits
		{
			get
			{
				JointLimits jointLimits;
				this.INTERNAL_get_limits(out jointLimits);
				return jointLimits;
			}
			set
			{
				this.INTERNAL_set_limits(ref value);
			}
		}

		// Token: 0x06001254 RID: 4692
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_limits(out JointLimits value);

		// Token: 0x06001255 RID: 4693
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_limits(ref JointLimits value);

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		public JointSpring spring
		{
			get
			{
				JointSpring jointSpring;
				this.INTERNAL_get_spring(out jointSpring);
				return jointSpring;
			}
			set
			{
				this.INTERNAL_set_spring(ref value);
			}
		}

		// Token: 0x06001258 RID: 4696
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_spring(out JointSpring value);

		// Token: 0x06001259 RID: 4697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_spring(ref JointSpring value);

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600125A RID: 4698
		// (set) Token: 0x0600125B RID: 4699
		public extern bool useMotor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600125C RID: 4700
		// (set) Token: 0x0600125D RID: 4701
		public extern bool useLimits
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600125E RID: 4702
		// (set) Token: 0x0600125F RID: 4703
		public extern bool useSpring
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001260 RID: 4704
		public extern float velocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001261 RID: 4705
		public extern float angle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
