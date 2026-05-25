using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000091 RID: 145
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ResourceRequest : AsyncOperation
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B164 File Offset: 0x00009364
		public Object asset
		{
			get
			{
				return Resources.Load(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x04000221 RID: 545
		internal string m_Path;

		// Token: 0x04000222 RID: 546
		internal Type m_Type;
	}
}
