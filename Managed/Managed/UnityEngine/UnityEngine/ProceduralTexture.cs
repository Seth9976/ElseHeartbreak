using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000143 RID: 323
	public sealed class ProceduralTexture : Texture
	{
		// Token: 0x06000DB2 RID: 3506
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralOutputType GetProceduralOutputType();

		// Token: 0x06000DB3 RID: 3507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ProceduralMaterial GetProceduralMaterial();

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DB4 RID: 3508
		public extern bool hasAlpha
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000DB5 RID: 3509
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasBeenGenerated();

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DB6 RID: 3510
		public extern TextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000DB7 RID: 3511
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(int x, int y, int blockWidth, int blockHeight);
	}
}
