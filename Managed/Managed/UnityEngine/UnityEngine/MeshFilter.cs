using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A8 RID: 168
	public sealed class MeshFilter : Component
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000383 RID: 899
		// (set) Token: 0x06000384 RID: 900
		public extern Mesh mesh
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000385 RID: 901
		// (set) Token: 0x06000386 RID: 902
		public extern Mesh sharedMesh
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
