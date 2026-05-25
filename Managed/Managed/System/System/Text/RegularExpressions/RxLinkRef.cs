using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000492 RID: 1170
	internal class RxLinkRef : LinkRef
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x00090330 File Offset: 0x0008E530
		public RxLinkRef()
		{
			this.offsets = new int[8];
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00090344 File Offset: 0x0008E544
		public void PushInstructionBase(int offset)
		{
			if ((this.current & 1) != 0)
			{
				throw new Exception();
			}
			if (this.current == this.offsets.Length)
			{
				int[] array = new int[this.offsets.Length * 2];
				Array.Copy(this.offsets, array, this.offsets.Length);
				this.offsets = array;
			}
			this.offsets[this.current++] = offset;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000903BC File Offset: 0x0008E5BC
		public void PushOffsetPosition(int offset)
		{
			if ((this.current & 1) == 0)
			{
				throw new Exception();
			}
			this.offsets[this.current++] = offset;
		}

		// Token: 0x04001A4F RID: 6735
		public int[] offsets;

		// Token: 0x04001A50 RID: 6736
		public int current;
	}
}
