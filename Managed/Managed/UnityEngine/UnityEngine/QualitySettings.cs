using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000A2 RID: 162
	public sealed class QualitySettings : Object
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600035A RID: 858
		public static extern string[] names
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600035B RID: 859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetQualityLevel();

		// Token: 0x0600035C RID: 860
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x0600035D RID: 861 RVA: 0x0000B618 File Offset: 0x00009818
		[ExcludeFromDocs]
		public static void SetQualityLevel(int index)
		{
			bool flag = true;
			QualitySettings.SetQualityLevel(index, flag);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600035E RID: 862
		// (set) Token: 0x0600035F RID: 863
		[Obsolete("Use GetQualityLevel and SetQualityLevel")]
		public static extern QualityLevel currentLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000360 RID: 864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges);

		// Token: 0x06000361 RID: 865 RVA: 0x0000B630 File Offset: 0x00009830
		[ExcludeFromDocs]
		public static void IncreaseLevel()
		{
			bool flag = false;
			QualitySettings.IncreaseLevel(flag);
		}

		// Token: 0x06000362 RID: 866
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges);

		// Token: 0x06000363 RID: 867 RVA: 0x0000B648 File Offset: 0x00009848
		[ExcludeFromDocs]
		public static void DecreaseLevel()
		{
			bool flag = false;
			QualitySettings.DecreaseLevel(flag);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000364 RID: 868
		// (set) Token: 0x06000365 RID: 869
		public static extern int pixelLightCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000366 RID: 870
		// (set) Token: 0x06000367 RID: 871
		public static extern ShadowProjection shadowProjection
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000368 RID: 872
		// (set) Token: 0x06000369 RID: 873
		public static extern int shadowCascades
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600036A RID: 874
		// (set) Token: 0x0600036B RID: 875
		public static extern float shadowDistance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600036C RID: 876
		// (set) Token: 0x0600036D RID: 877
		public static extern int masterTextureLimit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036E RID: 878
		// (set) Token: 0x0600036F RID: 879
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000370 RID: 880
		// (set) Token: 0x06000371 RID: 881
		public static extern float lodBias
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000372 RID: 882
		// (set) Token: 0x06000373 RID: 883
		public static extern int maximumLODLevel
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000374 RID: 884
		// (set) Token: 0x06000375 RID: 885
		public static extern int particleRaycastBudget
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000376 RID: 886
		// (set) Token: 0x06000377 RID: 887
		public static extern bool softVegetation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000378 RID: 888
		// (set) Token: 0x06000379 RID: 889
		public static extern int maxQueuedFrames
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600037A RID: 890
		// (set) Token: 0x0600037B RID: 891
		public static extern int vSyncCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600037C RID: 892
		// (set) Token: 0x0600037D RID: 893
		public static extern int antiAliasing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600037E RID: 894
		public static extern ColorSpace desiredColorSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600037F RID: 895
		public static extern ColorSpace activeColorSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x06000381 RID: 897
		public static extern BlendWeights blendWeights
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
