using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001BD RID: 445
	public class Joint2D : Behaviour
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600157A RID: 5498
		// (set) Token: 0x0600157B RID: 5499
		public extern Rigidbody2D connectedBody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600157C RID: 5500
		// (set) Token: 0x0600157D RID: 5501
		public extern bool collideConnected
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
