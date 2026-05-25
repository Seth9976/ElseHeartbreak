using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000149 RID: 329
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0001D784 File Offset: 0x0001B984
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0001D78C File Offset: 0x0001B98C
		public Sprite sprite
		{
			get
			{
				return this.GetSprite_INTERNAL();
			}
			set
			{
				this.SetSprite_INTERNAL(value);
			}
		}

		// Token: 0x06000DD1 RID: 3537
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Sprite GetSprite_INTERNAL();

		// Token: 0x06000DD2 RID: 3538
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSprite_INTERNAL(Sprite sprite);

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0001D798 File Offset: 0x0001B998
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
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

		// Token: 0x06000DD5 RID: 3541
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000DD6 RID: 3542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
