using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001D RID: 29
	public sealed class TdsInternalErrorCollection : IEnumerable
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public TdsInternalErrorCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700005D RID: 93
		public TdsInternalError this[int index]
		{
			get
			{
				return (TdsInternalError)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public int Add(TdsInternalError error)
		{
			return this.list.Add(error);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x04000103 RID: 259
		private ArrayList list;
	}
}
