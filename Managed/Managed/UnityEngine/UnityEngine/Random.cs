using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000177 RID: 375
	public sealed class Random
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001138 RID: 4408
		// (set) Token: 0x06001139 RID: 4409
		public static extern int seed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600113A RID: 4410
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float min, float max);

		// Token: 0x0600113B RID: 4411 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public static int Range(int min, int max)
		{
			return Random.RandomRangeInt(min, max);
		}

		// Token: 0x0600113C RID: 4412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int min, int max);

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600113D RID: 4413
		public static extern float value
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000201DC File Offset: 0x0001E3DC
		public static Vector3 insideUnitSphere
		{
			get
			{
				Vector3 vector;
				Random.INTERNAL_get_insideUnitSphere(out vector);
				return vector;
			}
		}

		// Token: 0x0600113F RID: 4415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_insideUnitSphere(out Vector3 value);

		// Token: 0x06001140 RID: 4416
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000201F4 File Offset: 0x0001E3F4
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 vector;
				Random.GetRandomUnitCircle(out vector);
				return vector;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0002020C File Offset: 0x0001E40C
		public static Vector3 onUnitSphere
		{
			get
			{
				Vector3 vector;
				Random.INTERNAL_get_onUnitSphere(out vector);
				return vector;
			}
		}

		// Token: 0x06001143 RID: 4419
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_onUnitSphere(out Vector3 value);

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00020224 File Offset: 0x0001E424
		public static Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				Random.INTERNAL_get_rotation(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x06001145 RID: 4421
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0002023C File Offset: 0x0001E43C
		public static Quaternion rotationUniform
		{
			get
			{
				Quaternion quaternion;
				Random.INTERNAL_get_rotationUniform(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x06001147 RID: 4423
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_rotationUniform(out Quaternion value);

		// Token: 0x06001148 RID: 4424 RVA: 0x00020254 File Offset: 0x0001E454
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00020260 File Offset: 0x0001E460
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}
	}
}
