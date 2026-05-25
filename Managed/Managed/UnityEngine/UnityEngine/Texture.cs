using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D2 RID: 210
	public class Texture : Object
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005CD RID: 1485
		// (set) Token: 0x060005CE RID: 1486
		public static extern int masterTextureLimit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005CF RID: 1487
		// (set) Token: 0x060005D0 RID: 1488
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060005D1 RID: 1489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		// Token: 0x060005D2 RID: 1490
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth(Texture mono);

		// Token: 0x060005D3 RID: 1491
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight(Texture mono);

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public virtual int width
		{
			get
			{
				return Texture.Internal_GetWidth(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public virtual int height
		{
			get
			{
				return Texture.Internal_GetHeight(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005D8 RID: 1496
		// (set) Token: 0x060005D9 RID: 1497
		public extern FilterMode filterMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005DA RID: 1498
		// (set) Token: 0x060005DB RID: 1499
		public extern int anisoLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005DC RID: 1500
		// (set) Token: 0x060005DD RID: 1501
		public extern TextureWrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005DE RID: 1502
		// (set) Token: 0x060005DF RID: 1503
		public extern float mipMapBias
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060005E0 RID: 1504
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTexelSize(Texture tex, out Vector2 output);

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public Vector2 texelSize
		{
			get
			{
				Vector2 vector;
				Texture.Internal_GetTexelSize(this, out vector);
				return vector;
			}
		}

		// Token: 0x060005E2 RID: 1506
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeTexturePtr();

		// Token: 0x060005E3 RID: 1507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetNativeTextureID();
	}
}
