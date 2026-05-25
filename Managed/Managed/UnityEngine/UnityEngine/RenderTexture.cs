using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D7 RID: 215
	public sealed class RenderTexture : Texture
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			bool flag = readWrite == RenderTextureReadWrite.sRGB;
			if (readWrite == RenderTextureReadWrite.Default)
			{
				flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
			RenderTexture.Internal_SetSRGBReadWrite(this, flag);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public RenderTexture(int width, int height, int depth)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = RenderTextureFormat.Default;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000637 RID: 1591
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateRenderTexture([Writable] RenderTexture rt);

		// Token: 0x06000638 RID: 1592
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing);

		// Token: 0x06000639 RID: 1593 RVA: 0x0000CCCC File Offset: 0x0000AECC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			int num = 1;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, num);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, renderTextureReadWrite, num);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000CD04 File Offset: 0x0000AF04
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, renderTextureFormat, renderTextureReadWrite, num);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000CD24 File Offset: 0x0000AF24
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.Default;
			int num2 = 0;
			return RenderTexture.GetTemporary(width, height, num2, renderTextureFormat, renderTextureReadWrite, num);
		}

		// Token: 0x0600063D RID: 1597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x0600063E RID: 1598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(RenderTexture mono);

		// Token: 0x0600063F RID: 1599
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetWidth(RenderTexture mono, int width);

		// Token: 0x06000640 RID: 1600
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(RenderTexture mono);

		// Token: 0x06000641 RID: 1601
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHeight(RenderTexture mono, int width);

		// Token: 0x06000642 RID: 1602
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetSRGBReadWrite(RenderTexture mono, bool sRGB);

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000CD44 File Offset: 0x0000AF44
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public override int width
		{
			get
			{
				return RenderTexture.Internal_GetWidth(this);
			}
			set
			{
				RenderTexture.Internal_SetWidth(this, value);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000CD58 File Offset: 0x0000AF58
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public override int height
		{
			get
			{
				return RenderTexture.Internal_GetHeight(this);
			}
			set
			{
				RenderTexture.Internal_SetHeight(this, value);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000647 RID: 1607
		// (set) Token: 0x06000648 RID: 1608
		public extern int depth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000649 RID: 1609
		// (set) Token: 0x0600064A RID: 1610
		public extern bool isPowerOfTwo
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600064B RID: 1611
		public extern bool sRGB
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600064C RID: 1612
		// (set) Token: 0x0600064D RID: 1613
		public extern RenderTextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600064E RID: 1614
		// (set) Token: 0x0600064F RID: 1615
		public extern bool useMipMap
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000650 RID: 1616
		// (set) Token: 0x06000651 RID: 1617
		public extern bool generateMips
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000652 RID: 1618
		// (set) Token: 0x06000653 RID: 1619
		public extern bool isCubemap
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000654 RID: 1620
		// (set) Token: 0x06000655 RID: 1621
		public extern bool isVolume
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000656 RID: 1622
		// (set) Token: 0x06000657 RID: 1623
		public extern int volumeDepth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000658 RID: 1624
		// (set) Token: 0x06000659 RID: 1625
		public extern int antiAliasing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600065A RID: 1626
		// (set) Token: 0x0600065B RID: 1627
		public extern bool enableRandomWrite
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public bool Create()
		{
			return RenderTexture.INTERNAL_CALL_Create(this);
		}

		// Token: 0x0600065D RID: 1629
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Create(RenderTexture self);

		// Token: 0x0600065E RID: 1630 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public void Release()
		{
			RenderTexture.INTERNAL_CALL_Release(this);
		}

		// Token: 0x0600065F RID: 1631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Release(RenderTexture self);

		// Token: 0x06000660 RID: 1632 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public bool IsCreated()
		{
			return RenderTexture.INTERNAL_CALL_IsCreated(this);
		}

		// Token: 0x06000661 RID: 1633
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_IsCreated(RenderTexture self);

		// Token: 0x06000662 RID: 1634 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void DiscardContents()
		{
			RenderTexture.INTERNAL_CALL_DiscardContents(this);
		}

		// Token: 0x06000663 RID: 1635
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DiscardContents(RenderTexture self);

		// Token: 0x06000664 RID: 1636
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		// Token: 0x06000665 RID: 1637 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		public void MarkRestoreExpected()
		{
			RenderTexture.INTERNAL_CALL_MarkRestoreExpected(this);
		}

		// Token: 0x06000666 RID: 1638
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MarkRestoreExpected(RenderTexture self);

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000CD94 File Offset: 0x0000AF94
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				this.GetColorBuffer(out renderBuffer);
				return renderBuffer;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				this.GetDepthBuffer(out renderBuffer);
				return renderBuffer;
			}
		}

		// Token: 0x06000669 RID: 1641
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorBuffer(out RenderBuffer res);

		// Token: 0x0600066A RID: 1642
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDepthBuffer(out RenderBuffer res);

		// Token: 0x0600066B RID: 1643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGlobalShaderProperty(string propertyName);

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600066C RID: 1644
		// (set) Token: 0x0600066D RID: 1645
		public static extern RenderTexture active
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600066E RID: 1646
		// (set) Token: 0x0600066F RID: 1647
		[Obsolete("Use SystemInfo.supportsRenderTextures instead.")]
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000670 RID: 1648
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexelOffset(RenderTexture tex, out Vector2 output);

		// Token: 0x06000671 RID: 1649 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public Vector2 GetTexelOffset()
		{
			Vector2 vector;
			RenderTexture.Internal_GetTexelOffset(this, out vector);
			return vector;
		}

		// Token: 0x06000672 RID: 1650
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsStencil(RenderTexture rt);

		// Token: 0x06000673 RID: 1651 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		[Obsolete("RenderTexture.SetBorderColor was removed", true)]
		public void SetBorderColor(Color color)
		{
		}
	}
}
