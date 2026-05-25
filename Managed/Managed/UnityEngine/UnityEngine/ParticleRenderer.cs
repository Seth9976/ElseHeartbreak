using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000BB RID: 187
	public sealed class ParticleRenderer : Renderer
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004C0 RID: 1216
		// (set) Token: 0x060004C1 RID: 1217
		public extern ParticleRenderMode particleRenderMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004C2 RID: 1218
		// (set) Token: 0x060004C3 RID: 1219
		public extern float lengthScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004C4 RID: 1220
		// (set) Token: 0x060004C5 RID: 1221
		public extern float velocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C6 RID: 1222
		// (set) Token: 0x060004C7 RID: 1223
		public extern float cameraVelocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C8 RID: 1224
		// (set) Token: 0x060004C9 RID: 1225
		public extern float maxParticleSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004CA RID: 1226
		// (set) Token: 0x060004CB RID: 1227
		public extern int uvAnimationXTile
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004CC RID: 1228
		// (set) Token: 0x060004CD RID: 1229
		public extern int uvAnimationYTile
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CE RID: 1230
		// (set) Token: 0x060004CF RID: 1231
		public extern float uvAnimationCycles
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000BE00 File Offset: 0x0000A000
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000BE08 File Offset: 0x0000A008
		[Obsolete("animatedTextureCount has been replaced by uvAnimationXTile and uvAnimationYTile.")]
		public int animatedTextureCount
		{
			get
			{
				return this.uvAnimationXTile;
			}
			set
			{
				this.uvAnimationXTile = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000BE14 File Offset: 0x0000A014
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public float maxPartileSize
		{
			get
			{
				return this.maxParticleSize;
			}
			set
			{
				this.maxParticleSize = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D4 RID: 1236
		// (set) Token: 0x060004D5 RID: 1237
		public extern Rect[] uvTiles
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000BE28 File Offset: 0x0000A028
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000BE2C File Offset: 0x0000A02C
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve widthCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000BE30 File Offset: 0x0000A030
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000BE34 File Offset: 0x0000A034
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve heightCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000BE38 File Offset: 0x0000A038
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000BE3C File Offset: 0x0000A03C
		[Obsolete("This function has been removed.", true)]
		public AnimationCurve rotationCurve
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
