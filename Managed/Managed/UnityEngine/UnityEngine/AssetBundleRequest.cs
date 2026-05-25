using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000079 RID: 121
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssetBundleRequest : AsyncOperation
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AF1C File Offset: 0x0000911C
		public Object asset
		{
			get
			{
				return this.m_AssetBundle.Load(this.m_Path, this.m_Type);
			}
		}

		// Token: 0x040001AE RID: 430
		internal AssetBundle m_AssetBundle;

		// Token: 0x040001AF RID: 431
		internal string m_Path;

		// Token: 0x040001B0 RID: 432
		internal Type m_Type;
	}
}
