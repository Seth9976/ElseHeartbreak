using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001A7 RID: 423
	public sealed class ClothRenderer : Renderer
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600141A RID: 5146
		// (set) Token: 0x0600141B RID: 5147
		public extern bool pauseWhenNotVisible
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
