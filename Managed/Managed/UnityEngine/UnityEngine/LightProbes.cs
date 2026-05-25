using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C7 RID: 199
	public sealed class LightProbes : Object
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0000C634 File Offset: 0x0000A834
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
			LightProbes.INTERNAL_CALL_GetInterpolatedLightProbe(this, ref position, renderer, coefficients);
		}

		// Token: 0x06000563 RID: 1379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetInterpolatedLightProbe(LightProbes self, ref Vector3 position, Renderer renderer, float[] coefficients);

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000564 RID: 1380
		public extern Vector3[] positions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000565 RID: 1381
		// (set) Token: 0x06000566 RID: 1382
		public extern float[] coefficients
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000567 RID: 1383
		public extern int count
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000568 RID: 1384
		public extern int cellCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
