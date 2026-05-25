using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001EA RID: 490
	public sealed class AnimationClip : Motion
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x00023D18 File Offset: 0x00021F18
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x060017B3 RID: 6067
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060017B4 RID: 6068
		public extern float length
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060017B5 RID: 6069
		internal extern float startTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060017B6 RID: 6070
		internal extern float stopTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060017B7 RID: 6071
		// (set) Token: 0x060017B8 RID: 6072
		public extern float frameRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060017B9 RID: 6073
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCurve(string relativePath, Type type, string propertyName, AnimationCurve curve);

		// Token: 0x060017BA RID: 6074 RVA: 0x00023D28 File Offset: 0x00021F28
		public void EnsureQuaternionContinuity()
		{
			AnimationClip.INTERNAL_CALL_EnsureQuaternionContinuity(this);
		}

		// Token: 0x060017BB RID: 6075
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_EnsureQuaternionContinuity(AnimationClip self);

		// Token: 0x060017BC RID: 6076 RVA: 0x00023D30 File Offset: 0x00021F30
		public void ClearCurves()
		{
			AnimationClip.INTERNAL_CALL_ClearCurves(this);
		}

		// Token: 0x060017BD RID: 6077
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ClearCurves(AnimationClip self);

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060017BE RID: 6078
		// (set) Token: 0x060017BF RID: 6079
		public extern WrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060017C0 RID: 6080
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddEvent(AnimationEvent evt);

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00023D38 File Offset: 0x00021F38
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x00023D50 File Offset: 0x00021F50
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_localBounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060017C3 RID: 6083
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060017C4 RID: 6084
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);
	}
}
