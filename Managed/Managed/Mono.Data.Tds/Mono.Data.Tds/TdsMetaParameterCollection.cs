using System;
using System.Collections;

namespace Mono.Data.Tds
{
	// Token: 0x02000008 RID: 8
	public class TdsMetaParameterCollection : IEnumerable, ICollection
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000045E0 File Offset: 0x000027E0
		public TdsMetaParameterCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000045F4 File Offset: 0x000027F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00004604 File Offset: 0x00002804
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00004614 File Offset: 0x00002814
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x1700000D RID: 13
		public TdsMetaParameter this[int index]
		{
			get
			{
				return (TdsMetaParameter)this.list[index];
			}
		}

		// Token: 0x1700000E RID: 14
		public TdsMetaParameter this[string name]
		{
			get
			{
				return this[this.IndexOf(name)];
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00004648 File Offset: 0x00002848
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004658 File Offset: 0x00002858
		public int Add(TdsMetaParameter value)
		{
			return this.list.Add(value);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004668 File Offset: 0x00002868
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004678 File Offset: 0x00002878
		public bool Contains(TdsMetaParameter value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004688 File Offset: 0x00002888
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004698 File Offset: 0x00002898
		public int IndexOf(TdsMetaParameter value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000046A8 File Offset: 0x000028A8
		public int IndexOf(string name)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].ParameterName.Equals(name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000046E8 File Offset: 0x000028E8
		public void Insert(int index, TdsMetaParameter value)
		{
			this.list.Insert(index, value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000046F8 File Offset: 0x000028F8
		public void Remove(TdsMetaParameter value)
		{
			this.list.Remove(value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004708 File Offset: 0x00002908
		public void Remove(string name)
		{
			this.RemoveAt(this.IndexOf(name));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004718 File Offset: 0x00002918
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04000042 RID: 66
		private ArrayList list;
	}
}
