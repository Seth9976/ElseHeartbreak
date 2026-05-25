using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	public sealed class ParticleSystemRenderer : Renderer
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D0F RID: 3343
		// (set) Token: 0x06000D10 RID: 3344
		public extern ParticleSystemRenderMode renderMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D11 RID: 3345
		// (set) Token: 0x06000D12 RID: 3346
		public extern float lengthScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D13 RID: 3347
		// (set) Token: 0x06000D14 RID: 3348
		public extern float velocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D15 RID: 3349
		// (set) Token: 0x06000D16 RID: 3350
		public extern float cameraVelocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D17 RID: 3351
		// (set) Token: 0x06000D18 RID: 3352
		public extern float maxParticleSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D19 RID: 3353
		// (set) Token: 0x06000D1A RID: 3354
		public extern Mesh mesh
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
