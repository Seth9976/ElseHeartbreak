using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200014E RID: 334
	public sealed class Caching
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public static bool Authorize(string name, string domain, long size, string signature)
		{
			return Caching.Authorize(name, domain, size, -1, signature);
		}

		// Token: 0x06000DFE RID: 3582
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Authorize(string name, string domain, long size, int expiration, string signature);

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001E3BC File Offset: 0x0001C5BC
		[Obsolete("Size is now specified as a long")]
		public static bool Authorize(string name, string domain, int size, int expiration, string signature)
		{
			return Caching.Authorize(name, domain, (long)size, expiration, signature);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001E3CC File Offset: 0x0001C5CC
		[Obsolete("Size is now specified as a long")]
		public static bool Authorize(string name, string domain, int size, string signature)
		{
			return Caching.Authorize(name, domain, (long)size, signature);
		}

		// Token: 0x06000E01 RID: 3585
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CleanCache();

		// Token: 0x06000E02 RID: 3586
		[Obsolete("this API is not for public use.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CleanNamedCache(string name);

		// Token: 0x06000E03 RID: 3587
		[WrapperlessIcall]
		[Obsolete("This function is obsolete and has no effect.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DeleteFromCache(string url);

		// Token: 0x06000E04 RID: 3588
		[Obsolete("This function is obsolete and will always return -1. Use IsVersionCached instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersionFromCache(string url);

		// Token: 0x06000E05 RID: 3589
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsVersionCached(string url, int version);

		// Token: 0x06000E06 RID: 3590
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool MarkAsUsed(string url, int version);

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E07 RID: 3591
		[Obsolete("this API is not for public use.")]
		public static extern CacheIndex[] index
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E08 RID: 3592
		public static extern long spaceFree
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E09 RID: 3593
		// (set) Token: 0x06000E0A RID: 3594
		public static extern long maximumAvailableDiskSpace
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E0B RID: 3595
		public static extern long spaceOccupied
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E0C RID: 3596
		[Obsolete("Please use Caching.spaceFree instead")]
		public static extern int spaceAvailable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E0D RID: 3597
		[Obsolete("Please use Caching.spaceOccupied instead")]
		public static extern int spaceUsed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E0E RID: 3598
		// (set) Token: 0x06000E0F RID: 3599
		public static extern int expirationDelay
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E10 RID: 3600
		// (set) Token: 0x06000E11 RID: 3601
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E12 RID: 3602
		public static extern bool ready
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
