using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009F RID: 159
	public sealed class RenderSettings : Object
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600033F RID: 831
		// (set) Token: 0x06000340 RID: 832
		public static extern bool fog
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000341 RID: 833
		// (set) Token: 0x06000342 RID: 834
		public static extern FogMode fogMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000B5C8 File Offset: 0x000097C8
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public static Color fogColor
		{
			get
			{
				Color color;
				RenderSettings.INTERNAL_get_fogColor(out color);
				return color;
			}
			set
			{
				RenderSettings.INTERNAL_set_fogColor(ref value);
			}
		}

		// Token: 0x06000345 RID: 837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_fogColor(out Color value);

		// Token: 0x06000346 RID: 838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_fogColor(ref Color value);

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000347 RID: 839
		// (set) Token: 0x06000348 RID: 840
		public static extern float fogDensity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000349 RID: 841
		// (set) Token: 0x0600034A RID: 842
		public static extern float fogStartDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600034B RID: 843
		// (set) Token: 0x0600034C RID: 844
		public static extern float fogEndDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000B5EC File Offset: 0x000097EC
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000B604 File Offset: 0x00009804
		public static Color ambientLight
		{
			get
			{
				Color color;
				RenderSettings.INTERNAL_get_ambientLight(out color);
				return color;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientLight(ref value);
			}
		}

		// Token: 0x0600034F RID: 847
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_ambientLight(out Color value);

		// Token: 0x06000350 RID: 848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_ambientLight(ref Color value);

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000351 RID: 849
		// (set) Token: 0x06000352 RID: 850
		public static extern float haloStrength
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		public static extern float flareStrength
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000355 RID: 853
		// (set) Token: 0x06000356 RID: 854
		public static extern float flareFadeSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000357 RID: 855
		// (set) Token: 0x06000358 RID: 856
		public static extern Material skybox
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
