using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000219 RID: 537
	public sealed class Tree : Component
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001A39 RID: 6713
		// (set) Token: 0x06001A3A RID: 6714
		public extern ScriptableObject data
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
