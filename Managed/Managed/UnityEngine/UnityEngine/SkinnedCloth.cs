using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A6 RID: 422
	public sealed class SkinnedCloth : Cloth
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001411 RID: 5137
		// (set) Token: 0x06001412 RID: 5138
		public extern ClothSkinningCoefficient[] coefficients
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001413 RID: 5139
		// (set) Token: 0x06001414 RID: 5140
		public extern float worldVelocityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001415 RID: 5141
		// (set) Token: 0x06001416 RID: 5142
		public extern float worldAccelerationScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001417 RID: 5143
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetEnabledFading(bool enabled, [DefaultValue("0.5f")] float interpolationTime);

		// Token: 0x06001418 RID: 5144 RVA: 0x00021C08 File Offset: 0x0001FE08
		[ExcludeFromDocs]
		public void SetEnabledFading(bool enabled)
		{
			float num = 0.5f;
			this.SetEnabledFading(enabled, num);
		}
	}
}
