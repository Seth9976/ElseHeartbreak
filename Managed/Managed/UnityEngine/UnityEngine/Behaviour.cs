using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000156 RID: 342
	public class Behaviour : Component
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E71 RID: 3697
		// (set) Token: 0x06000E72 RID: 3698
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000E73 RID: 3699
		public extern bool isActiveAndEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
