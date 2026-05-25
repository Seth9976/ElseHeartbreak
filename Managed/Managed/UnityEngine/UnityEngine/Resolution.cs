using System;

namespace UnityEngine
{
	// Token: 0x020000C3 RID: 195
	public struct Resolution
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		public int refreshRate
		{
			get
			{
				return this.m_RefreshRate;
			}
			set
			{
				this.m_RefreshRate = value;
			}
		}

		// Token: 0x040002A6 RID: 678
		private int m_Width;

		// Token: 0x040002A7 RID: 679
		private int m_Height;

		// Token: 0x040002A8 RID: 680
		private int m_RefreshRate;
	}
}
