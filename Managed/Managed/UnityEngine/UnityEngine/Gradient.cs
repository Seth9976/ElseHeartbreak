using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Gradient
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0000D08C File Offset: 0x0000B28C
		public Gradient()
		{
			this.Init();
		}

		// Token: 0x060006D3 RID: 1747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x060006D4 RID: 1748
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000D09C File Offset: 0x0000B29C
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x060006D6 RID: 1750
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color Evaluate(float time);

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006D7 RID: 1751
		// (set) Token: 0x060006D8 RID: 1752
		public extern GradientColorKey[] colorKeys
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006D9 RID: 1753
		// (set) Token: 0x060006DA RID: 1754
		public extern GradientAlphaKey[] alphaKeys
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060006DB RID: 1755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x040002E8 RID: 744
		internal IntPtr m_Ptr;
	}
}
