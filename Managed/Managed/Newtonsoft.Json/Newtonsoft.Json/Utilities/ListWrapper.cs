using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BC RID: 188
	internal class ListWrapper<T> : CollectionWrapper<T>, IList<T>, ICollection<T>, IEnumerable<T>, IWrappedList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0001EB34 File Offset: 0x0001CD34
		public ListWrapper(IList list)
			: base(list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			if (list is IList<T>)
			{
				this._genericList = (IList<T>)list;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		public ListWrapper(IList<T> list)
			: base(list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericList = list;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001EB77 File Offset: 0x0001CD77
		public int IndexOf(T item)
		{
			if (this._genericList != null)
			{
				return this._genericList.IndexOf(item);
			}
			return ((IList)this).IndexOf(item);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001EB9A File Offset: 0x0001CD9A
		public void Insert(int index, T item)
		{
			if (this._genericList != null)
			{
				this._genericList.Insert(index, item);
				return;
			}
			((IList)this).Insert(index, item);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001EBBF File Offset: 0x0001CDBF
		public void RemoveAt(int index)
		{
			if (this._genericList != null)
			{
				this._genericList.RemoveAt(index);
				return;
			}
			((IList)this).RemoveAt(index);
		}

		// Token: 0x170001A8 RID: 424
		public T this[int index]
		{
			get
			{
				if (this._genericList != null)
				{
					return this._genericList[index];
				}
				return (T)((object)((IList)this)[index]);
			}
			set
			{
				if (this._genericList != null)
				{
					this._genericList[index] = value;
					return;
				}
				((IList)this)[index] = value;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001EC25 File Offset: 0x0001CE25
		public override void Add(T item)
		{
			if (this._genericList != null)
			{
				this._genericList.Add(item);
				return;
			}
			base.Add(item);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001EC43 File Offset: 0x0001CE43
		public override void Clear()
		{
			if (this._genericList != null)
			{
				this._genericList.Clear();
				return;
			}
			base.Clear();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001EC5F File Offset: 0x0001CE5F
		public override bool Contains(T item)
		{
			if (this._genericList != null)
			{
				return this._genericList.Contains(item);
			}
			return base.Contains(item);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001EC7D File Offset: 0x0001CE7D
		public override void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericList != null)
			{
				this._genericList.CopyTo(array, arrayIndex);
				return;
			}
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001EC9D File Offset: 0x0001CE9D
		public override int Count
		{
			get
			{
				if (this._genericList != null)
				{
					return this._genericList.Count;
				}
				return base.Count;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001ECB9 File Offset: 0x0001CEB9
		public override bool IsReadOnly
		{
			get
			{
				if (this._genericList != null)
				{
					return this._genericList.IsReadOnly;
				}
				return base.IsReadOnly;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public override bool Remove(T item)
		{
			if (this._genericList != null)
			{
				return this._genericList.Remove(item);
			}
			bool flag = base.Contains(item);
			if (flag)
			{
				base.Remove(item);
			}
			return flag;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001ED0E File Offset: 0x0001CF0E
		public override IEnumerator<T> GetEnumerator()
		{
			if (this._genericList != null)
			{
				return this._genericList.GetEnumerator();
			}
			return base.GetEnumerator();
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001ED2A File Offset: 0x0001CF2A
		public object UnderlyingList
		{
			get
			{
				if (this._genericList != null)
				{
					return this._genericList;
				}
				return base.UnderlyingCollection;
			}
		}

		// Token: 0x0400029F RID: 671
		private readonly IList<T> _genericList;
	}
}
