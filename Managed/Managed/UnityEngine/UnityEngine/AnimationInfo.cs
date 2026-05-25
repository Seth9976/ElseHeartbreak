using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001F7 RID: 503
	public struct AnimationInfo
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00024214 File Offset: 0x00022414
		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID == 0) ? null : AnimationInfo.ClipInstanceToScriptingObject(this.m_ClipInstanceID);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00024234 File Offset: 0x00022434
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x0600184C RID: 6220
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip ClipInstanceToScriptingObject(int instanceID);

		// Token: 0x04000757 RID: 1879
		private int m_ClipInstanceID;

		// Token: 0x04000758 RID: 1880
		private float m_Weight;
	}
}
