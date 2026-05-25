using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D5 RID: 213
	public sealed class Texture3D : Texture
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public Texture3D(int width, int height, int depth, TextureFormat format, bool mipmap)
		{
			Texture3D.Internal_Create(this, width, height, depth, format, mipmap);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000622 RID: 1570
		public extern int depth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000623 RID: 1571
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels([DefaultValue("0")] int miplevel);

		// Token: 0x06000624 RID: 1572 RVA: 0x0000CB68 File Offset: 0x0000AD68
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			int num = 0;
			return this.GetPixels(num);
		}

		// Token: 0x06000625 RID: 1573
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x06000626 RID: 1574 RVA: 0x0000CB80 File Offset: 0x0000AD80
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int num = 0;
			this.SetPixels(colors, num);
		}

		// Token: 0x06000627 RID: 1575
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps);

		// Token: 0x06000628 RID: 1576 RVA: 0x0000CB98 File Offset: 0x0000AD98
		[ExcludeFromDocs]
		public void Apply()
		{
			bool flag = true;
			this.Apply(flag);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000629 RID: 1577
		public extern TextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600062A RID: 1578
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Texture3D mono, int width, int height, int depth, TextureFormat format, bool mipmap);
	}
}
