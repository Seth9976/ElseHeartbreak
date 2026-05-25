using System;

namespace UnityEngine
{
	// Token: 0x020000BE RID: 190
	public struct RenderBuffer
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		public IntPtr GetNativeRenderBufferPtr()
		{
			return this.m_BufferPtr;
		}

		// Token: 0x04000290 RID: 656
		internal int m_RenderTextureInstanceID;

		// Token: 0x04000291 RID: 657
		internal IntPtr m_BufferPtr;
	}
}
