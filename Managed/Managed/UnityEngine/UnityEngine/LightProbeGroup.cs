using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010F RID: 271
	public sealed class LightProbeGroup : Component
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A0E RID: 2574
		// (set) Token: 0x06000A0F RID: 2575
		public extern Vector3[] probePositions
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
