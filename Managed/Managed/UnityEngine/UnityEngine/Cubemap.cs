using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D4 RID: 212
	public sealed class Cubemap : Texture
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public Cubemap(int size, TextureFormat format, bool mipmap)
		{
			Cubemap.Internal_Create(this, size, format, mipmap);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			Cubemap.INTERNAL_CALL_SetPixel(this, face, x, y, ref color);
		}

		// Token: 0x06000614 RID: 1556
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPixel(Cubemap self, CubemapFace face, int x, int y, ref Color color);

		// Token: 0x06000615 RID: 1557
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetPixel(CubemapFace face, int x, int y);

		// Token: 0x06000616 RID: 1558
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, [DefaultValue("0")] int miplevel);

		// Token: 0x06000617 RID: 1559 RVA: 0x0000CACC File Offset: 0x0000ACCC
		[ExcludeFromDocs]
		public Color[] GetPixels(CubemapFace face)
		{
			int num = 0;
			return this.GetPixels(face, num);
		}

		// Token: 0x06000618 RID: 1560
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, CubemapFace face, [DefaultValue("0")] int miplevel);

		// Token: 0x06000619 RID: 1561 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors, CubemapFace face)
		{
			int num = 0;
			this.SetPixels(colors, face, num);
		}

		// Token: 0x0600061A RID: 1562
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x0600061B RID: 1563 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			bool flag = false;
			this.Apply(updateMipmaps, flag);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000CB14 File Offset: 0x0000AD14
		[ExcludeFromDocs]
		public void Apply()
		{
			bool flag = false;
			bool flag2 = true;
			this.Apply(flag2, flag);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600061D RID: 1565
		public extern TextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600061E RID: 1566
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Cubemap mono, int size, TextureFormat format, bool mipmap);

		// Token: 0x0600061F RID: 1567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		// Token: 0x06000620 RID: 1568 RVA: 0x0000CB30 File Offset: 0x0000AD30
		[ExcludeFromDocs]
		public void SmoothEdges()
		{
			int num = 1;
			this.SmoothEdges(num);
		}
	}
}
