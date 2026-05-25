using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000DE RID: 222
	public sealed class GUIText : GUIElement
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000688 RID: 1672
		// (set) Token: 0x06000689 RID: 1673
		public extern string text
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600068A RID: 1674
		// (set) Token: 0x0600068B RID: 1675
		public extern Material material
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600068C RID: 1676
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPixelOffset(out Vector2 output);

		// Token: 0x0600068D RID: 1677 RVA: 0x0000CE7C File Offset: 0x0000B07C
		private void Internal_SetPixelOffset(Vector2 p)
		{
			GUIText.INTERNAL_CALL_Internal_SetPixelOffset(this, ref p);
		}

		// Token: 0x0600068E RID: 1678
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetPixelOffset(GUIText self, ref Vector2 p);

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000CE88 File Offset: 0x0000B088
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		public Vector2 pixelOffset
		{
			get
			{
				Vector2 vector;
				this.Internal_GetPixelOffset(out vector);
				return vector;
			}
			set
			{
				this.Internal_SetPixelOffset(value);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000691 RID: 1681
		// (set) Token: 0x06000692 RID: 1682
		public extern Font font
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000693 RID: 1683
		// (set) Token: 0x06000694 RID: 1684
		public extern TextAlignment alignment
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000695 RID: 1685
		// (set) Token: 0x06000696 RID: 1686
		public extern TextAnchor anchor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000697 RID: 1687
		// (set) Token: 0x06000698 RID: 1688
		public extern float lineSpacing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000699 RID: 1689
		// (set) Token: 0x0600069A RID: 1690
		public extern float tabSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600069B RID: 1691
		// (set) Token: 0x0600069C RID: 1692
		public extern int fontSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600069D RID: 1693
		// (set) Token: 0x0600069E RID: 1694
		public extern FontStyle fontStyle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600069F RID: 1695
		// (set) Token: 0x060006A0 RID: 1696
		public extern bool richText
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
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

		// Token: 0x060006A3 RID: 1699
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x060006A4 RID: 1700
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
