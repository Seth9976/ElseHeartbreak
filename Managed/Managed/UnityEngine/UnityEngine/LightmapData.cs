using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightmapData
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public Texture2D lightmapFar
		{
			get
			{
				return this.m_Lightmap;
			}
			set
			{
				this.m_Lightmap = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000C604 File Offset: 0x0000A804
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000C60C File Offset: 0x0000A80C
		[Obsolete("Use lightmapFar instead")]
		public Texture2D lightmap
		{
			get
			{
				return this.m_Lightmap;
			}
			set
			{
				this.m_Lightmap = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000C618 File Offset: 0x0000A818
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000C620 File Offset: 0x0000A820
		public Texture2D lightmapNear
		{
			get
			{
				return this.m_IndirectLightmap;
			}
			set
			{
				this.m_IndirectLightmap = value;
			}
		}

		// Token: 0x040002A9 RID: 681
		internal Texture2D m_Lightmap;

		// Token: 0x040002AA RID: 682
		internal Texture2D m_IndirectLightmap;
	}
}
