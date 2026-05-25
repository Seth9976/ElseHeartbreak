using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000127 RID: 295
	public sealed class Ping
	{
		// Token: 0x06000BF8 RID: 3064
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Ping(string address);

		// Token: 0x06000BF9 RID: 3065
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DestroyPing();

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001C760 File Offset: 0x0001A960
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BFB RID: 3067
		public extern bool isDone
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BFC RID: 3068
		public extern int time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BFD RID: 3069
		public extern string ip
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x04000553 RID: 1363
		private IntPtr pingWrapper;
	}
}
