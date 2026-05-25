using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001A RID: 26
	public class TdsDataRow : IEnumerable, ICollection, IList
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public TdsDataRow()
		{
			this.list = new ArrayList();
			this.bigDecimalIndex = -1;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000DA70 File Offset: 0x0000BC70
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public int BigDecimalIndex
		{
			get
			{
				return this.bigDecimalIndex;
			}
			set
			{
				this.bigDecimalIndex = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x17000053 RID: 83
		public object this[int index]
		{
			get
			{
				if (index >= this.list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public int Add(object value)
		{
			return this.list.Add(value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			this.list.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public void Remove(object value)
		{
			this.list.Remove(value);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000DB88 File Offset: 0x0000BD88
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x040000F3 RID: 243
		private ArrayList list;

		// Token: 0x040000F4 RID: 244
		private int bigDecimalIndex;
	}
}
