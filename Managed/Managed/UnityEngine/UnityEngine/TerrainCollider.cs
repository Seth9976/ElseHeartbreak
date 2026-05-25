using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001A8 RID: 424
	public sealed class TerrainCollider : Collider
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600141D RID: 5149
		// (set) Token: 0x0600141E RID: 5150
		public extern TerrainData terrainData
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
