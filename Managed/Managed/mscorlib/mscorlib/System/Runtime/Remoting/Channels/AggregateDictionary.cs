using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000444 RID: 1092
	[ComVisible(true)]
	internal class AggregateDictionary : IEnumerable, ICollection, IDictionary
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x00099610 File Offset: 0x00097810
		public AggregateDictionary(IDictionary[] dics)
		{
			this.dictionaries = dics;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00099620 File Offset: 0x00097820
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x00099630 File Offset: 0x00097830
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00099634 File Offset: 0x00097834
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700082F RID: 2095
		public object this[object key]
		{
			get
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x00099684 File Offset: 0x00097884
		public ICollection Keys
		{
			get
			{
				if (this._keys != null)
				{
					return this._keys;
				}
				this._keys = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._keys.AddRange(dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000996E4 File Offset: 0x000978E4
		public ICollection Values
		{
			get
			{
				if (this._values != null)
				{
					return this._values;
				}
				this._values = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._values.AddRange(dictionary.Values);
				}
				return this._values;
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x00099744 File Offset: 0x00097944
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0009974C File Offset: 0x0009794C
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x00099754 File Offset: 0x00097954
		public bool Contains(object ob)
		{
			foreach (IDictionary dictionary in this.dictionaries)
			{
				if (dictionary.Contains(ob))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00099790 File Offset: 0x00097990
		public IDictionaryEnumerator GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000997A0 File Offset: 0x000979A0
		public void Remove(object ob)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000997A8 File Offset: 0x000979A8
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x00099814 File Offset: 0x00097A14
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IDictionary dictionary in this.dictionaries)
				{
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0009984C File Offset: 0x00097A4C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00099850 File Offset: 0x00097A50
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040013C5 RID: 5061
		private IDictionary[] dictionaries;

		// Token: 0x040013C6 RID: 5062
		private ArrayList _values;

		// Token: 0x040013C7 RID: 5063
		private ArrayList _keys;
	}
}
