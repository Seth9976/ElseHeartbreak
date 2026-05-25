using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000086 RID: 134
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000B008 File Offset: 0x00009208
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x04000219 RID: 537
		internal float m_Seconds;
	}
}
