using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001E9 RID: 489
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationEvent
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x00023CB8 File Offset: 0x00021EB8
		public AnimationEvent()
		{
			this.m_OwnsData = 1;
			this.Create();
		}

		// Token: 0x0600179E RID: 6046
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Create();

		// Token: 0x0600179F RID: 6047 RVA: 0x00023CD0 File Offset: 0x00021ED0
		~AnimationEvent()
		{
			if (this.m_OwnsData != 0)
			{
				this.Destroy();
			}
		}

		// Token: 0x060017A0 RID: 6048
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Destroy();

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060017A1 RID: 6049
		// (set) Token: 0x060017A2 RID: 6050
		[Obsolete("Use stringParameter instead")]
		public extern string data
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060017A3 RID: 6051
		// (set) Token: 0x060017A4 RID: 6052
		public extern string stringParameter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060017A5 RID: 6053
		// (set) Token: 0x060017A6 RID: 6054
		public extern float floatParameter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060017A7 RID: 6055
		// (set) Token: 0x060017A8 RID: 6056
		public extern int intParameter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060017A9 RID: 6057
		// (set) Token: 0x060017AA RID: 6058
		public extern Object objectReferenceParameter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060017AB RID: 6059
		// (set) Token: 0x060017AC RID: 6060
		public extern string functionName
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060017AD RID: 6061
		// (set) Token: 0x060017AE RID: 6062
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060017AF RID: 6063
		// (set) Token: 0x060017B0 RID: 6064
		public extern SendMessageOptions messageOptions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060017B1 RID: 6065
		public extern AnimationState animationState
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x04000730 RID: 1840
		[NotRenamed]
		internal IntPtr m_Ptr;

		// Token: 0x04000731 RID: 1841
		private int m_OwnsData;
	}
}
