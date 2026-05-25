using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200016D RID: 365
	public sealed class LocationService
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F87 RID: 3975
		public extern bool isEnabledByUser
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F88 RID: 3976
		public extern LocationServiceStatus status
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F89 RID: 3977
		public extern LocationInfo lastData
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000F8A RID: 3978
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Start([DefaultValue("10f")] float desiredAccuracyInMeters, [DefaultValue("10f")] float updateDistanceInMeters);

		// Token: 0x06000F8B RID: 3979 RVA: 0x0001F15C File Offset: 0x0001D35C
		[ExcludeFromDocs]
		public void Start(float desiredAccuracyInMeters)
		{
			float num = 10f;
			this.Start(desiredAccuracyInMeters, num);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0001F178 File Offset: 0x0001D378
		[ExcludeFromDocs]
		public void Start()
		{
			float num = 10f;
			float num2 = 10f;
			this.Start(num2, num);
		}

		// Token: 0x06000F8D RID: 3981
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();
	}
}
