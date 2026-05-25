using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000089 RID: 137
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000B028 File Offset: 0x00009228
		private Coroutine()
		{
		}

		// Token: 0x060002E2 RID: 738
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseCoroutine();

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B030 File Offset: 0x00009230
		~Coroutine()
		{
			this.ReleaseCoroutine();
		}

		// Token: 0x0400021A RID: 538
		internal IntPtr m_Ptr;
	}
}
