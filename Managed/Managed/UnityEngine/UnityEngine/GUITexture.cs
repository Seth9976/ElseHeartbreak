using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D9 RID: 217
	public sealed class GUITexture : GUIElement
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000CE2C File Offset: 0x0000B02C
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000CE44 File Offset: 0x0000B044
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

		// Token: 0x0600067D RID: 1661
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x0600067E RID: 1662
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600067F RID: 1663
		// (set) Token: 0x06000680 RID: 1664
		public extern Texture texture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000CE50 File Offset: 0x0000B050
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000CE68 File Offset: 0x0000B068
		public Rect pixelInset
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_pixelInset(out rect);
				return rect;
			}
			set
			{
				this.INTERNAL_set_pixelInset(ref value);
			}
		}

		// Token: 0x06000683 RID: 1667
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelInset(out Rect value);

		// Token: 0x06000684 RID: 1668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pixelInset(ref Rect value);

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000685 RID: 1669
		// (set) Token: 0x06000686 RID: 1670
		public extern RectOffset border
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
