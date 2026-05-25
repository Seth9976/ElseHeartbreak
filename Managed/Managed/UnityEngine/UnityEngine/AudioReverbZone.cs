using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001DA RID: 474
	public sealed class AudioReverbZone : Behaviour
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060016EC RID: 5868
		// (set) Token: 0x060016ED RID: 5869
		public extern float minDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060016EE RID: 5870
		// (set) Token: 0x060016EF RID: 5871
		public extern float maxDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016F0 RID: 5872
		// (set) Token: 0x060016F1 RID: 5873
		public extern AudioReverbPreset reverbPreset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016F2 RID: 5874
		// (set) Token: 0x060016F3 RID: 5875
		public extern int room
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016F4 RID: 5876
		// (set) Token: 0x060016F5 RID: 5877
		public extern int roomHF
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016F6 RID: 5878
		// (set) Token: 0x060016F7 RID: 5879
		public extern int roomLF
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016F8 RID: 5880
		// (set) Token: 0x060016F9 RID: 5881
		public extern float decayTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016FA RID: 5882
		// (set) Token: 0x060016FB RID: 5883
		public extern float decayHFRatio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016FC RID: 5884
		// (set) Token: 0x060016FD RID: 5885
		public extern int reflections
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016FE RID: 5886
		// (set) Token: 0x060016FF RID: 5887
		public extern float reflectionsDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001700 RID: 5888
		// (set) Token: 0x06001701 RID: 5889
		public extern int reverb
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001702 RID: 5890
		// (set) Token: 0x06001703 RID: 5891
		public extern float reverbDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001704 RID: 5892
		// (set) Token: 0x06001705 RID: 5893
		public extern float HFReference
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001706 RID: 5894
		// (set) Token: 0x06001707 RID: 5895
		public extern float LFReference
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001708 RID: 5896
		// (set) Token: 0x06001709 RID: 5897
		public extern float roomRolloffFactor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600170A RID: 5898
		// (set) Token: 0x0600170B RID: 5899
		public extern float diffusion
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600170C RID: 5900
		// (set) Token: 0x0600170D RID: 5901
		public extern float density
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
