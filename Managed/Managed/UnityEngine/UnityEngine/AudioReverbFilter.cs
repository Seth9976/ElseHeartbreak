using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E0 RID: 480
	public sealed class AudioReverbFilter : Behaviour
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001736 RID: 5942
		// (set) Token: 0x06001737 RID: 5943
		public extern AudioReverbPreset reverbPreset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001738 RID: 5944
		// (set) Token: 0x06001739 RID: 5945
		public extern float dryLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600173A RID: 5946
		// (set) Token: 0x0600173B RID: 5947
		public extern float room
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600173C RID: 5948
		// (set) Token: 0x0600173D RID: 5949
		public extern float roomHF
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600173E RID: 5950
		// (set) Token: 0x0600173F RID: 5951
		public extern float roomRolloff
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001740 RID: 5952
		// (set) Token: 0x06001741 RID: 5953
		public extern float decayTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001742 RID: 5954
		// (set) Token: 0x06001743 RID: 5955
		public extern float decayHFRatio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001744 RID: 5956
		// (set) Token: 0x06001745 RID: 5957
		public extern float reflectionsLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001746 RID: 5958
		// (set) Token: 0x06001747 RID: 5959
		public extern float reflectionsDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001748 RID: 5960
		// (set) Token: 0x06001749 RID: 5961
		public extern float reverbLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600174A RID: 5962
		// (set) Token: 0x0600174B RID: 5963
		public extern float reverbDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600174C RID: 5964
		// (set) Token: 0x0600174D RID: 5965
		public extern float diffusion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600174E RID: 5966
		// (set) Token: 0x0600174F RID: 5967
		public extern float density
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001750 RID: 5968
		// (set) Token: 0x06001751 RID: 5969
		public extern float hfReference
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001752 RID: 5970
		// (set) Token: 0x06001753 RID: 5971
		public extern float roomLF
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001754 RID: 5972
		// (set) Token: 0x06001755 RID: 5973
		public extern float lFReference
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
