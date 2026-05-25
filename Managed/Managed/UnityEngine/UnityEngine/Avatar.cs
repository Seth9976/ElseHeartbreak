using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200020E RID: 526
	public sealed class Avatar : Object
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001933 RID: 6451
		public extern bool isValid
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001934 RID: 6452
		public extern bool isHuman
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001935 RID: 6453
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		// Token: 0x06001936 RID: 6454
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetParameter(int parameterId, float value);

		// Token: 0x06001937 RID: 6455
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetAxisLength(int humanId);

		// Token: 0x06001938 RID: 6456
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Quaternion GetPreRotation(int humanId);

		// Token: 0x06001939 RID: 6457
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Quaternion GetPostRotation(int humanId);

		// Token: 0x0600193A RID: 6458 RVA: 0x00024908 File Offset: 0x00022B08
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return Avatar.INTERNAL_CALL_GetZYPostQ(this, humanId, ref parentQ, ref q);
		}

		// Token: 0x0600193B RID: 6459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_GetZYPostQ(Avatar self, int humanId, ref Quaternion parentQ, ref Quaternion q);

		// Token: 0x0600193C RID: 6460 RVA: 0x00024918 File Offset: 0x00022B18
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return Avatar.INTERNAL_CALL_GetZYRoll(this, humanId, ref uvw);
		}

		// Token: 0x0600193D RID: 6461
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_GetZYRoll(Avatar self, int humanId, ref Vector3 uvw);

		// Token: 0x0600193E RID: 6462
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Vector3 GetLimitSign(int humanId);
	}
}
