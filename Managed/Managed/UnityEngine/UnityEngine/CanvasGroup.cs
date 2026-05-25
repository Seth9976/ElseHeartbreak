using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000225 RID: 549
	public sealed class CanvasGroup : Component, ICanvasRaycastFilter
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001AA9 RID: 6825
		// (set) Token: 0x06001AAA RID: 6826
		public extern float alpha
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001AAB RID: 6827
		// (set) Token: 0x06001AAC RID: 6828
		public extern bool interactable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001AAD RID: 6829
		// (set) Token: 0x06001AAE RID: 6830
		public extern bool blocksRaycasts
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001AAF RID: 6831
		// (set) Token: 0x06001AB0 RID: 6832
		public extern bool ignoreParentGroups
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00026338 File Offset: 0x00024538
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}
	}
}
