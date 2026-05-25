using System;

namespace GameTypes
{
	// Token: 0x0200000D RID: 13
	public struct Float3
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000030A0 File Offset: 0x000012A0
		public Float3(float pX, float pY, float pZ)
		{
			this.x = pX;
			this.y = pY;
			this.z = pZ;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030B8 File Offset: 0x000012B8
		public override string ToString()
		{
			return string.Format("Float3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x04000029 RID: 41
		public float x;

		// Token: 0x0400002A RID: 42
		public float y;

		// Token: 0x0400002B RID: 43
		public float z;
	}
}
