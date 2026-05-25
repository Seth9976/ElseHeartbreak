using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200021B RID: 539
	internal sealed class WindZone : Component
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001A3C RID: 6716
		// (set) Token: 0x06001A3D RID: 6717
		public extern WindZoneMode mode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001A3E RID: 6718
		// (set) Token: 0x06001A3F RID: 6719
		public extern float radius
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001A40 RID: 6720
		// (set) Token: 0x06001A41 RID: 6721
		public extern float windMain
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001A42 RID: 6722
		// (set) Token: 0x06001A43 RID: 6723
		public extern float windTurbulence
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001A44 RID: 6724
		// (set) Token: 0x06001A45 RID: 6725
		public extern float windPulseMagnitude
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001A46 RID: 6726
		// (set) Token: 0x06001A47 RID: 6727
		public extern float windPulseFrequency
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
