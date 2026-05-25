using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D6 RID: 214
	public sealed class SparseTexture : Texture
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		public SparseTexture(int width, int height, TextureFormat format, int mipCount)
		{
			SparseTexture.Internal_Create(this, width, height, format, mipCount, false);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public SparseTexture(int width, int height, TextureFormat format, int mipCount, bool linear)
		{
			SparseTexture.Internal_Create(this, width, height, format, mipCount, linear);
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600062D RID: 1581
		public extern int tileWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600062E RID: 1582
		public extern int tileHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600062F RID: 1583
		public extern bool isCreated
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000630 RID: 1584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, TextureFormat format, int mipCount, bool linear);

		// Token: 0x06000631 RID: 1585
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		// Token: 0x06000632 RID: 1586
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		// Token: 0x06000633 RID: 1587
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UnloadTile(int tileX, int tileY, int miplevel);
	}
}
