using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B0 RID: 176
	public sealed class LensFlare : Behaviour
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003ED RID: 1005
		// (set) Token: 0x060003EE RID: 1006
		public extern Flare flare
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003EF RID: 1007
		// (set) Token: 0x060003F0 RID: 1008
		public extern float brightness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003F1 RID: 1009
		// (set) Token: 0x060003F2 RID: 1010
		public extern float fadeSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000BA5C File Offset: 0x00009C5C
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000BA74 File Offset: 0x00009C74
		public Color color
		{
			get
			{
				Color color;
				this.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060003F5 RID: 1013
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x060003F6 RID: 1014
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
