using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000213 RID: 531
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SplatPrototype
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00024B3C File Offset: 0x00022D3C
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x00024B44 File Offset: 0x00022D44
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00024B50 File Offset: 0x00022D50
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00024B58 File Offset: 0x00022D58
		public Texture2D normalMap
		{
			get
			{
				return this.m_NormalMap;
			}
			set
			{
				this.m_NormalMap = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00024B64 File Offset: 0x00022D64
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00024B6C File Offset: 0x00022D6C
		public Vector2 tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00024B78 File Offset: 0x00022D78
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x00024B80 File Offset: 0x00022D80
		public Vector2 tileOffset
		{
			get
			{
				return this.m_TileOffset;
			}
			set
			{
				this.m_TileOffset = value;
			}
		}

		// Token: 0x0400081B RID: 2075
		private Texture2D m_Texture;

		// Token: 0x0400081C RID: 2076
		private Texture2D m_NormalMap;

		// Token: 0x0400081D RID: 2077
		private Vector2 m_TileSize = new Vector2(15f, 15f);

		// Token: 0x0400081E RID: 2078
		private Vector2 m_TileOffset = new Vector2(0f, 0f);
	}
}
