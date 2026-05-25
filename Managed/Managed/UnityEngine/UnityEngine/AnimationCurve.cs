using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001EC RID: 492
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationCurve
	{
		// Token: 0x060017D1 RID: 6097 RVA: 0x00023E08 File Offset: 0x00022008
		public AnimationCurve(params Keyframe[] keys)
		{
			this.Init(keys);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00023E18 File Offset: 0x00022018
		public AnimationCurve()
		{
			this.Init(null);
		}

		// Token: 0x060017D3 RID: 6099
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x060017D4 RID: 6100 RVA: 0x00023E28 File Offset: 0x00022028
		~AnimationCurve()
		{
			this.Cleanup();
		}

		// Token: 0x060017D5 RID: 6101
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float Evaluate(float time);

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00023E64 File Offset: 0x00022064
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x00023E6C File Offset: 0x0002206C
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060017D8 RID: 6104
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddKey(float time, float value);

		// Token: 0x060017D9 RID: 6105 RVA: 0x00023E78 File Offset: 0x00022078
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00023E84 File Offset: 0x00022084
		private int AddKey_Internal(Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_AddKey_Internal(this, ref key);
		}

		// Token: 0x060017DB RID: 6107
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_AddKey_Internal(AnimationCurve self, ref Keyframe key);

		// Token: 0x060017DC RID: 6108 RVA: 0x00023E90 File Offset: 0x00022090
		public int MoveKey(int index, Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_MoveKey(this, index, ref key);
		}

		// Token: 0x060017DD RID: 6109
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_MoveKey(AnimationCurve self, int index, ref Keyframe key);

		// Token: 0x060017DE RID: 6110
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveKey(int index);

		// Token: 0x1700063D RID: 1597
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey_Internal(index);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060017E0 RID: 6112
		public extern int length
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060017E1 RID: 6113
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060017E2 RID: 6114
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe GetKey_Internal(int index);

		// Token: 0x060017E3 RID: 6115
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060017E4 RID: 6116
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060017E5 RID: 6117 RVA: 0x00023EA8 File Offset: 0x000220A8
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			float num = (valueEnd - valueStart) / (timeEnd - timeStart);
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, num),
				new Keyframe(timeEnd, valueEnd, num, 0f)
			};
			return new AnimationCurve(array);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00023EFC File Offset: 0x000220FC
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, 0f),
				new Keyframe(timeEnd, valueEnd, 0f, 0f)
			};
			return new AnimationCurve(array);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060017E7 RID: 6119
		// (set) Token: 0x060017E8 RID: 6120
		public extern WrapMode preWrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060017E9 RID: 6121
		// (set) Token: 0x060017EA RID: 6122
		public extern WrapMode postWrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060017EB RID: 6123
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init(Keyframe[] keys);

		// Token: 0x04000736 RID: 1846
		internal IntPtr m_Ptr;
	}
}
