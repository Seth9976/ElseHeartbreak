using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x020000D2 RID: 210
	internal sealed class FieldNameLookup : IEnumerable, ICollection
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
		public FieldNameLookup()
		{
			this.list = new ArrayList();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		public FieldNameLookup(DataTable schemaTable)
			: this()
		{
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				this.list.Add((string)dataRow["ColumnName"]);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002F870 File Offset: 0x0002DA70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002F880 File Offset: 0x0002DA80
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002F890 File Offset: 0x0002DA90
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002F894 File Offset: 0x0002DA94
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002F898 File Offset: 0x0002DA98
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x170001D6 RID: 470
		public string this[int index]
		{
			get
			{
				return (string)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002F8CC File Offset: 0x0002DACC
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002F8DC File Offset: 0x0002DADC
		public int Add(object value)
		{
			return this.list.Add(value);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002F8EC File Offset: 0x0002DAEC
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002F8FC File Offset: 0x0002DAFC
		public bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002F91C File Offset: 0x0002DB1C
		public int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002F93C File Offset: 0x0002DB3C
		public void Remove(object value)
		{
			this.list.Remove(value);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002F94C File Offset: 0x0002DB4C
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04000386 RID: 902
		private ArrayList list;
	}
}
