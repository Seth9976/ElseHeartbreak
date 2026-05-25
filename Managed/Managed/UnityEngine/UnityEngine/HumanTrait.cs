using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200020F RID: 527
	public sealed class HumanTrait : Object
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001940 RID: 6464
		public static extern int MuscleCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001941 RID: 6465
		public static extern string[] MuscleName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001942 RID: 6466
		public static extern int BoneCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001943 RID: 6467
		public static extern string[] BoneName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001944 RID: 6468
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int MuscleFromBone(int i, int dofIndex);

		// Token: 0x06001945 RID: 6469
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int BoneFromMuscle(int i);

		// Token: 0x06001946 RID: 6470
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequiredBone(int i);

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001947 RID: 6471
		public static extern int RequiredBoneCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001948 RID: 6472
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasCollider(Avatar avatar, int i);

		// Token: 0x06001949 RID: 6473
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMin(int i);

		// Token: 0x0600194A RID: 6474
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMax(int i);
	}
}
