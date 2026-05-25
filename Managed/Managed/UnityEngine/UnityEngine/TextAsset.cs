using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000136 RID: 310
	public class TextAsset : Object
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D20 RID: 3360
		public extern string text
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D21 RID: 3361
		public extern byte[] bytes
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001D28C File Offset: 0x0001B48C
		public override string ToString()
		{
			return this.text;
		}
	}
}
