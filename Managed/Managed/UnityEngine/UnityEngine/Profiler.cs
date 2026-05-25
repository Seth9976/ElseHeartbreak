using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000094 RID: 148
	public sealed class Profiler
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600030E RID: 782
		public static extern bool supported
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600030F RID: 783
		// (set) Token: 0x06000310 RID: 784
		public static extern string logFile
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000311 RID: 785
		// (set) Token: 0x06000312 RID: 786
		public static extern bool enableBinaryLog
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000313 RID: 787
		// (set) Token: 0x06000314 RID: 788
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000315 RID: 789
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddFramesFromFile(string file);

		// Token: 0x06000316 RID: 790 RVA: 0x0000B294 File Offset: 0x00009494
		[Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name)
		{
			Profiler.BeginSampleOnly(name);
		}

		// Token: 0x06000317 RID: 791
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(string name, Object targetObject);

		// Token: 0x06000318 RID: 792
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSampleOnly(string name);

		// Token: 0x06000319 RID: 793
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample();

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600031A RID: 794
		public static extern uint usedHeapSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600031B RID: 795
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRuntimeMemorySize(Object o);

		// Token: 0x0600031C RID: 796
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetMonoHeapSize();

		// Token: 0x0600031D RID: 797
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetMonoUsedSize();

		// Token: 0x0600031E RID: 798
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalAllocatedMemory();

		// Token: 0x0600031F RID: 799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalUnusedReservedMemory();

		// Token: 0x06000320 RID: 800
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalReservedMemory();
	}
}
