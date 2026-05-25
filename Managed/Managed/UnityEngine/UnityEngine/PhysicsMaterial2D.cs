using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C4 RID: 452
	public sealed class PhysicsMaterial2D : Object
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x0002339C File Offset: 0x0002159C
		public PhysicsMaterial2D()
		{
			PhysicsMaterial2D.Internal_Create(this, null);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000233AC File Offset: 0x000215AC
		public PhysicsMaterial2D(string name)
		{
			PhysicsMaterial2D.Internal_Create(this, name);
		}

		// Token: 0x060015D8 RID: 5592
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] PhysicsMaterial2D mat, string name);

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060015D9 RID: 5593
		// (set) Token: 0x060015DA RID: 5594
		public extern float bounciness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060015DB RID: 5595
		// (set) Token: 0x060015DC RID: 5596
		public extern float friction
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
