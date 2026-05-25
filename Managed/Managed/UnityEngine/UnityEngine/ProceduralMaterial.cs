using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000142 RID: 322
	public sealed class ProceduralMaterial : Material
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x0001D618 File Offset: 0x0001B818
		public ProceduralMaterial()
			: base(string.Empty)
		{
		}

		// Token: 0x06000D89 RID: 3465
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralPropertyDescription[] GetProceduralPropertyDescriptions();

		// Token: 0x06000D8A RID: 3466
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProceduralProperty(string inputName);

		// Token: 0x06000D8B RID: 3467
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetProceduralBoolean(string inputName);

		// Token: 0x06000D8C RID: 3468
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralBoolean(string inputName, bool value);

		// Token: 0x06000D8D RID: 3469
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetProceduralFloat(string inputName);

		// Token: 0x06000D8E RID: 3470
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralFloat(string inputName, float value);

		// Token: 0x06000D8F RID: 3471
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector4 GetProceduralVector(string inputName);

		// Token: 0x06000D90 RID: 3472 RVA: 0x0001D628 File Offset: 0x0001B828
		public void SetProceduralVector(string inputName, Vector4 value)
		{
			ProceduralMaterial.INTERNAL_CALL_SetProceduralVector(this, inputName, ref value);
		}

		// Token: 0x06000D91 RID: 3473
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetProceduralVector(ProceduralMaterial self, string inputName, ref Vector4 value);

		// Token: 0x06000D92 RID: 3474
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetProceduralColor(string inputName);

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001D634 File Offset: 0x0001B834
		public void SetProceduralColor(string inputName, Color value)
		{
			ProceduralMaterial.INTERNAL_CALL_SetProceduralColor(this, inputName, ref value);
		}

		// Token: 0x06000D94 RID: 3476
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetProceduralColor(ProceduralMaterial self, string inputName, ref Color value);

		// Token: 0x06000D95 RID: 3477
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetProceduralEnum(string inputName);

		// Token: 0x06000D96 RID: 3478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralEnum(string inputName, int value);

		// Token: 0x06000D97 RID: 3479
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture2D GetProceduralTexture(string inputName);

		// Token: 0x06000D98 RID: 3480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetProceduralTexture(string inputName, Texture2D value);

		// Token: 0x06000D99 RID: 3481
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsProceduralPropertyCached(string inputName);

		// Token: 0x06000D9A RID: 3482
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CacheProceduralProperty(string inputName, bool value);

		// Token: 0x06000D9B RID: 3483
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearCache();

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D9C RID: 3484
		// (set) Token: 0x06000D9D RID: 3485
		public extern ProceduralCacheSize cacheSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D9E RID: 3486
		// (set) Token: 0x06000D9F RID: 3487
		public extern int animationUpdateRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000DA0 RID: 3488
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildTextures();

		// Token: 0x06000DA1 RID: 3489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildTexturesImmediately();

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DA2 RID: 3490
		public extern bool isProcessing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000DA3 RID: 3491
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopRebuilds();

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DA4 RID: 3492
		public extern bool isCachedDataAvailable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DA5 RID: 3493
		// (set) Token: 0x06000DA6 RID: 3494
		public extern bool isLoadTimeGenerated
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DA7 RID: 3495
		public extern ProceduralLoadingBehavior loadingBehavior
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DA8 RID: 3496
		public static extern bool isSupported
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DA9 RID: 3497
		// (set) Token: 0x06000DAA RID: 3498
		public static extern ProceduralProcessorUsage substanceProcessorUsage
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DAB RID: 3499
		// (set) Token: 0x06000DAC RID: 3500
		public extern string preset
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000DAD RID: 3501
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture[] GetGeneratedTextures();

		// Token: 0x06000DAE RID: 3502
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProceduralTexture GetGeneratedTexture(string textureName);

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DAF RID: 3503
		// (set) Token: 0x06000DB0 RID: 3504
		public extern bool isReadable
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
