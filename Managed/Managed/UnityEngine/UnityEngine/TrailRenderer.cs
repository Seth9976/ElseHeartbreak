using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B9 RID: 185
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004B7 RID: 1207
		// (set) Token: 0x060004B8 RID: 1208
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004B9 RID: 1209
		// (set) Token: 0x060004BA RID: 1210
		public extern float startWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004BB RID: 1211
		// (set) Token: 0x060004BC RID: 1212
		public extern float endWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004BD RID: 1213
		// (set) Token: 0x060004BE RID: 1214
		public extern bool autodestruct
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
