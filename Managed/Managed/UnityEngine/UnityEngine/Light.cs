using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000172 RID: 370
	public sealed class Light : Behaviour
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001039 RID: 4153
		// (set) Token: 0x0600103A RID: 4154
		public extern LightType type
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0001F70C File Offset: 0x0001D90C
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x0001F724 File Offset: 0x0001D924
		public Color color
		{
			get
			{
				Color color;
				this.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x0600103D RID: 4157
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x0600103E RID: 4158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600103F RID: 4159
		// (set) Token: 0x06001040 RID: 4160
		public extern float intensity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001041 RID: 4161
		// (set) Token: 0x06001042 RID: 4162
		public extern LightShadows shadows
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001043 RID: 4163
		// (set) Token: 0x06001044 RID: 4164
		public extern float shadowStrength
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001045 RID: 4165
		// (set) Token: 0x06001046 RID: 4166
		public extern float shadowBias
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001047 RID: 4167
		// (set) Token: 0x06001048 RID: 4168
		public extern float shadowSoftness
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001049 RID: 4169
		// (set) Token: 0x0600104A RID: 4170
		public extern float shadowSoftnessFade
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600104B RID: 4171
		// (set) Token: 0x0600104C RID: 4172
		public extern float range
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600104D RID: 4173
		// (set) Token: 0x0600104E RID: 4174
		public extern float spotAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600104F RID: 4175
		// (set) Token: 0x06001050 RID: 4176
		public extern float cookieSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001051 RID: 4177
		// (set) Token: 0x06001052 RID: 4178
		public extern Texture cookie
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001053 RID: 4179
		// (set) Token: 0x06001054 RID: 4180
		public extern Flare flare
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001055 RID: 4181
		// (set) Token: 0x06001056 RID: 4182
		public extern LightRenderMode renderMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001057 RID: 4183
		// (set) Token: 0x06001058 RID: 4184
		public extern bool alreadyLightmapped
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001059 RID: 4185
		// (set) Token: 0x0600105A RID: 4186
		public extern int cullingMask
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600105B RID: 4187
		// (set) Token: 0x0600105C RID: 4188
		public static extern int pixelLightCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600105D RID: 4189
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Light[] GetLights(LightType type, int layer);

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0001F730 File Offset: 0x0001D930
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0001F738 File Offset: 0x0001D938
		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0001F73C File Offset: 0x0001D93C
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x0001F744 File Offset: 0x0001D944
		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0001F748 File Offset: 0x0001D948
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x0001F74C File Offset: 0x0001D94C
		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
	}
}
