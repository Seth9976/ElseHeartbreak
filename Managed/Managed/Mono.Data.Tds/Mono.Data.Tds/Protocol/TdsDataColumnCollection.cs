using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000019 RID: 25
	public class TdsDataColumnCollection : IEnumerable
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000D958 File Offset: 0x0000BB58
		public TdsDataColumnCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x1700004B RID: 75
		public TdsDataColumn this[int index]
		{
			get
			{
				return (TdsDataColumn)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000D990 File Offset: 0x0000BB90
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		public int Add(TdsDataColumn schema)
		{
			int num = this.list.Add(schema);
			schema.ColumnOrdinal = new int?(num);
			return num;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public void Add(TdsDataColumnCollection columns)
		{
			foreach (object obj in columns)
			{
				TdsDataColumn tdsDataColumn = (TdsDataColumn)obj;
				this.Add(tdsDataColumn);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000DA34 File Offset: 0x0000BC34
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x040000F2 RID: 242
		private ArrayList list;
	}
}
