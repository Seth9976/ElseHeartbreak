using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000176 RID: 374
	public sealed class Time
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001125 RID: 4389
		public static extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001126 RID: 4390
		public static extern float timeSinceLevelLoad
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001127 RID: 4391
		public static extern float deltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001128 RID: 4392
		public static extern float fixedTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001129 RID: 4393
		public static extern float unscaledTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600112A RID: 4394
		public static extern float unscaledDeltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600112B RID: 4395
		// (set) Token: 0x0600112C RID: 4396
		public static extern float fixedDeltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600112D RID: 4397
		// (set) Token: 0x0600112E RID: 4398
		public static extern float maximumDeltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600112F RID: 4399
		public static extern float smoothDeltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001130 RID: 4400
		// (set) Token: 0x06001131 RID: 4401
		public static extern float timeScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001132 RID: 4402
		public static extern int frameCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001133 RID: 4403
		public static extern int renderedFrameCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001134 RID: 4404
		public static extern float realtimeSinceStartup
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001135 RID: 4405
		// (set) Token: 0x06001136 RID: 4406
		public static extern int captureFramerate
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
