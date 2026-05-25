using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200000F RID: 15
	public class TdsBigDecimal
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000B670 File Offset: 0x00009870
		public TdsBigDecimal(byte precision, byte scale, bool isNegative, int[] data)
		{
			this.isNegative = isNegative;
			this.precision = precision;
			this.scale = scale;
			this.data = data;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000B698 File Offset: 0x00009898
		public int[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public bool IsNegative
		{
			get
			{
				return this.isNegative;
			}
		}

		// Token: 0x0400007B RID: 123
		private bool isNegative;

		// Token: 0x0400007C RID: 124
		private byte precision;

		// Token: 0x0400007D RID: 125
		private byte scale;

		// Token: 0x0400007E RID: 126
		private int[] data;
	}
}
