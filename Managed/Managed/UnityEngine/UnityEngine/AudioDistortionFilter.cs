using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DD RID: 477
	public sealed class AudioDistortionFilter : Behaviour
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001719 RID: 5913
		// (set) Token: 0x0600171A RID: 5914
		public extern float distortionLevel
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
