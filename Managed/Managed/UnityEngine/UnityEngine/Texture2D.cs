using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D3 RID: 211
	public sealed class Texture2D : Texture
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x0000C814 File Offset: 0x0000AA14
		public Texture2D(int width, int height)
		{
			Texture2D.Internal_Create(this, width, height, TextureFormat.ARGB32, true, false, IntPtr.Zero);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000C838 File Offset: 0x0000AA38
		public Texture2D(int width, int height, TextureFormat format, bool mipmap)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, false, IntPtr.Zero);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, IntPtr.Zero);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000C884 File Offset: 0x0000AA84
		internal Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, nativeTex);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005E8 RID: 1512
		public extern int mipmapCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060005E9 RID: 1513
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Texture2D mono, int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex);

		// Token: 0x060005EA RID: 1514 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			return new Texture2D(width, height, format, mipmap, linear, nativeTex);
		}

		// Token: 0x060005EB RID: 1515
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005EC RID: 1516
		public extern TextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005ED RID: 1517
		public static extern Texture2D whiteTexture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005EE RID: 1518
		public static extern Texture2D blackTexture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public void SetPixel(int x, int y, Color color)
		{
			Texture2D.INTERNAL_CALL_SetPixel(this, x, y, ref color);
		}

		// Token: 0x060005F0 RID: 1520
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPixel(Texture2D self, int x, int y, ref Color color);

		// Token: 0x060005F1 RID: 1521
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixel(int x, int y);

		// Token: 0x060005F2 RID: 1522
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixelBilinear(float u, float v);

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int num = 0;
			this.SetPixels(colors, num);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x060005F5 RID: 1525
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000C920 File Offset: 0x0000AB20
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			int num = 0;
			this.SetPixels(x, y, blockWidth, blockHeight, colors, num);
		}

		// Token: 0x060005F7 RID: 1527
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000C940 File Offset: 0x0000AB40
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			int num = 0;
			this.SetPixels32(colors, num);
		}

		// Token: 0x060005F9 RID: 1529
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadImage(byte[] data);

		// Token: 0x060005FA RID: 1530
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadRawTextureData(byte[] data);

		// Token: 0x060005FB RID: 1531 RVA: 0x0000C958 File Offset: 0x0000AB58
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			int num = 0;
			return this.GetPixels(num);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000C970 File Offset: 0x0000AB70
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x060005FD RID: 1533
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x060005FE RID: 1534 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			int num = 0;
			return this.GetPixels(x, y, blockWidth, blockHeight, num);
		}

		// Token: 0x060005FF RID: 1535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x06000600 RID: 1536 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			int num = 0;
			return this.GetPixels32(num);
		}

		// Token: 0x06000601 RID: 1537
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x06000602 RID: 1538 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			bool flag = false;
			this.Apply(updateMipmaps, flag);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000CA00 File Offset: 0x0000AC00
		[ExcludeFromDocs]
		public void Apply()
		{
			bool flag = false;
			bool flag2 = true;
			this.Apply(flag2, flag);
		}

		// Token: 0x06000604 RID: 1540
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Resize(int width, int height, TextureFormat format, bool hasMipMap);

		// Token: 0x06000605 RID: 1541 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public bool Resize(int width, int height)
		{
			return this.Internal_ResizeWH(width, height);
		}

		// Token: 0x06000606 RID: 1542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_ResizeWH(int width, int height);

		// Token: 0x06000607 RID: 1543 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public void Compress(bool highQuality)
		{
			Texture2D.INTERNAL_CALL_Compress(this, highQuality);
		}

		// Token: 0x06000608 RID: 1544
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Compress(Texture2D self, bool highQuality);

		// Token: 0x06000609 RID: 1545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, [DefaultValue("2048")] int maximumAtlasSize, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x0600060A RID: 1546 RVA: 0x0000CA34 File Offset: 0x0000AC34
		[ExcludeFromDocs]
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			bool flag = false;
			return this.PackTextures(textures, padding, maximumAtlasSize, flag);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000CA50 File Offset: 0x0000AC50
		[ExcludeFromDocs]
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			bool flag = false;
			int num = 2048;
			return this.PackTextures(textures, padding, num, flag);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			Texture2D.INTERNAL_CALL_ReadPixels(this, ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000CA80 File Offset: 0x0000AC80
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			bool flag = true;
			Texture2D.INTERNAL_CALL_ReadPixels(this, ref source, destX, destY, flag);
		}

		// Token: 0x0600060E RID: 1550
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ReadPixels(Texture2D self, ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x0600060F RID: 1551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] EncodeToPNG();

		// Token: 0x06000610 RID: 1552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] EncodeToJPG(int quality);

		// Token: 0x06000611 RID: 1553 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public byte[] EncodeToJPG()
		{
			return this.EncodeToJPG(75);
		}
	}
}
