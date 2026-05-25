using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B3 RID: 179
	public sealed class Skybox : Behaviour
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600043B RID: 1083
		// (set) Token: 0x0600043C RID: 1084
		public extern Material material
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
