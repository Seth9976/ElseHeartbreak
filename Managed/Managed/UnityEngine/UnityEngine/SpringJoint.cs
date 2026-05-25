using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	public sealed class SpringJoint : Joint
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001263 RID: 4707
		// (set) Token: 0x06001264 RID: 4708
		public extern float spring
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001265 RID: 4709
		// (set) Token: 0x06001266 RID: 4710
		public extern float damper
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001267 RID: 4711
		// (set) Token: 0x06001268 RID: 4712
		public extern float minDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001269 RID: 4713
		// (set) Token: 0x0600126A RID: 4714
		public extern float maxDistance
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
