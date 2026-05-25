using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Collections
{
	// Token: 0x0200008A RID: 138
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00013970 File Offset: 0x00011B70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00013978 File Offset: 0x00011B78
		public void Add(T item)
		{
			if (this.m_Dictionary.ContainsKey(item))
			{
				return;
			}
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000139BC File Offset: 0x00011BBC
		public bool Remove(T item)
		{
			int num = -1;
			if (!this.m_Dictionary.TryGetValue(item, out num))
			{
				return false;
			}
			this.RemoveAt(num);
			return true;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000139E8 File Offset: 0x00011BE8
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000139F0 File Offset: 0x00011BF0
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Dictionary.Clear();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013A08 File Offset: 0x00011C08
		public bool Contains(T item)
		{
			return this.m_Dictionary.ContainsKey(item);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013A18 File Offset: 0x00011C18
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00013A28 File Offset: 0x00011C28
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00013A38 File Offset: 0x00011C38
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013A3C File Offset: 0x00011C3C
		public int IndexOf(T item)
		{
			int num = -1;
			this.m_Dictionary.TryGetValue(item, out num);
			return num;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013A5C File Offset: 0x00011C5C
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013A68 File Offset: 0x00011C68
		public void RemoveAt(int index)
		{
			T t = this.m_List[index];
			this.m_Dictionary.Remove(t);
			if (index == this.m_List.Count - 1)
			{
				this.m_List.RemoveAt(index);
			}
			else
			{
				int num = this.m_List.Count - 1;
				T t2 = this.m_List[num];
				this.m_List[index] = t2;
				this.m_Dictionary[t2] = index;
				this.m_List.RemoveAt(num);
			}
		}

		// Token: 0x17000143 RID: 323
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				T t = this.m_List[index];
				this.m_Dictionary.Remove(t);
				this.m_List[index] = value;
				this.m_Dictionary.Add(t, index);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00013B48 File Offset: 0x00011D48
		public void RemoveAll(Predicate<T> match)
		{
			int i = 0;
			while (i < this.m_List.Count)
			{
				T t = this.m_List[i];
				if (match(t))
				{
					this.Remove(t);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013B98 File Offset: 0x00011D98
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.m_List.Sort(sortLayoutFunction);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				T t = this.m_List[i];
				this.m_Dictionary[t] = i;
			}
		}

		// Token: 0x04000245 RID: 581
		private readonly List<T> m_List = new List<T>();

		// Token: 0x04000246 RID: 582
		private Dictionary<T, int> m_Dictionary = new Dictionary<T, int>();
	}
}
