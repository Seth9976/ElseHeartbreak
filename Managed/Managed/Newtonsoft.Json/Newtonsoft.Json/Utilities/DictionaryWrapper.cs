using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B0 RID: 176
	internal class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IWrappedDictionary, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x0001CD66 File Offset: 0x0001AF66
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001CD9A File Offset: 0x0001AF9A
		public void Add(TKey key, TValue value)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			this._dictionary.Add(key, value);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001CDC9 File Offset: 0x0001AFC9
		public bool ContainsKey(TKey key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey(key);
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001CDF1 File Offset: 0x0001AFF1
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys;
				}
				return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001CE1C File Offset: 0x0001B01C
		public bool Remove(TKey key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Remove(key);
			}
			if (this._dictionary.Contains(key))
			{
				this._dictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.TryGetValue(key, out value);
			}
			if (!this._dictionary.Contains(key))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)this._dictionary[key]);
			return true;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		public ICollection<TValue> Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values;
				}
				return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
			}
		}

		// Token: 0x17000196 RID: 406
		public TValue this[TKey key]
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary[key];
				}
				return (TValue)((object)this._dictionary[key]);
			}
			set
			{
				if (this._genericDictionary != null)
				{
					this._genericDictionary[key] = value;
					return;
				}
				this._dictionary[key] = value;
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001CF3F File Offset: 0x0001B13F
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(item);
				return;
			}
			((IList)this._dictionary).Add(item);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001CF6D File Offset: 0x0001B16D
		public void Clear()
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Clear();
				return;
			}
			this._dictionary.Clear();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001CF8E File Offset: 0x0001B18E
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Contains(item);
			}
			return ((IList)this._dictionary).Contains(item);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001CFBC File Offset: 0x0001B1BC
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.CopyTo(array, arrayIndex);
				return;
			}
			foreach (object obj in this._dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001D054 File Offset: 0x0001B254
		public int Count
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Count;
				}
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0001D075 File Offset: 0x0001B275
		public bool IsReadOnly
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.IsReadOnly;
				}
				return this._dictionary.IsReadOnly;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001D098 File Offset: 0x0001B298
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Remove(item);
			}
			if (!this._dictionary.Contains(item.Key))
			{
				return true;
			}
			object obj = this._dictionary[item.Key];
			if (object.Equals(obj, item.Value))
			{
				this._dictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001D13C File Offset: 0x0001B33C
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.GetEnumerator();
			}
			return (from DictionaryEntry de in this._dictionary
				select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001D18F File Offset: 0x0001B38F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001D197 File Offset: 0x0001B397
		void IDictionary.Add(object key, object value)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add((TKey)((object)key), (TValue)((object)value));
				return;
			}
			this._dictionary.Add(key, value);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001D1C6 File Offset: 0x0001B3C6
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001D1EE File Offset: 0x0001B3EE
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._genericDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._genericDictionary.GetEnumerator());
			}
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001D219 File Offset: 0x0001B419
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsFixedSize;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001D230 File Offset: 0x0001B430
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001D256 File Offset: 0x0001B456
		public void Remove(object key)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Remove((TKey)((object)key));
				return;
			}
			this._dictionary.Remove(key);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001D27F File Offset: 0x0001B47F
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x1700019C RID: 412
		object IDictionary.this[object key]
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary[(TKey)((object)key)];
				}
				return this._dictionary[key];
			}
			set
			{
				if (this._genericDictionary != null)
				{
					this._genericDictionary[(TKey)((object)key)] = (TValue)((object)value);
					return;
				}
				this._dictionary[key] = value;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001D301 File Offset: 0x0001B501
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
				return;
			}
			this._dictionary.CopyTo(array, index);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001D32B File Offset: 0x0001B52B
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001D342 File Offset: 0x0001B542
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001D364 File Offset: 0x0001B564
		public object UnderlyingDictionary
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary;
				}
				return this._dictionary;
			}
		}

		// Token: 0x04000275 RID: 629
		private readonly IDictionary _dictionary;

		// Token: 0x04000276 RID: 630
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x04000277 RID: 631
		private object _syncRoot;

		// Token: 0x020000B1 RID: 177
		private struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000816 RID: 2070 RVA: 0x0001D37B File Offset: 0x0001B57B
			public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001D38F File Offset: 0x0001B58F
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001D39C File Offset: 0x0001B59C
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object obj = keyValuePair.Key;
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair2 = this._e.Current;
					return new DictionaryEntry(obj, keyValuePair2.Value);
				}
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x0001D41B File Offset: 0x0001B61B
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0001D428 File Offset: 0x0001B628
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x04000279 RID: 633
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}
