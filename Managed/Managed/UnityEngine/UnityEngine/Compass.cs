using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016E RID: 366
	public sealed class Compass
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F8F RID: 3983
		public extern float magneticHeading
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F90 RID: 3984
		public extern float trueHeading
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F91 RID: 3985
		public extern float headingAccuracy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		public Vector3 rawVector
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_rawVector(out vector);
				return vector;
			}
		}

		// Token: 0x06000F93 RID: 3987
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rawVector(out Vector3 value);

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F94 RID: 3988
		public extern double timestamp
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F95 RID: 3989
		// (set) Token: 0x06000F96 RID: 3990
		public extern bool enabled
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
