using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary using a singly linked list. Recommended for collections that typically contain 10 items or less.</summary>
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the default comparer.</summary>
		// Token: 0x06000796 RID: 1942 RVA: 0x00017264 File Offset: 0x00015464
		public ListDictionary()
		{
			this.count = 0;
			this.version = 0;
			this.comparer = null;
			this.head = null;
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x06000797 RID: 1943 RVA: 0x00017294 File Offset: 0x00015494
		public ListDictionary(IComparer comparer)
			: this()
		{
			this.comparer = comparer;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06000798 RID: 1944 RVA: 0x000172A4 File Offset: 0x000154A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.DictionaryNodeEnumerator(this);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000172AC File Offset: 0x000154AC
		private ListDictionary.DictionaryNode FindEntry(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Attempted lookup for a null key.");
			}
			ListDictionary.DictionaryNode dictionaryNode = this.head;
			if (this.comparer == null)
			{
				while (dictionaryNode != null)
				{
					if (key.Equals(dictionaryNode.key))
					{
						break;
					}
					dictionaryNode = dictionaryNode.next;
				}
			}
			else
			{
				while (dictionaryNode != null)
				{
					if (this.comparer.Compare(key, dictionaryNode.key) == 0)
					{
						break;
					}
					dictionaryNode = dictionaryNode.next;
				}
			}
			return dictionaryNode;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00017340 File Offset: 0x00015540
		private ListDictionary.DictionaryNode FindEntry(object key, out ListDictionary.DictionaryNode prev)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Attempted lookup for a null key.");
			}
			ListDictionary.DictionaryNode dictionaryNode = this.head;
			prev = null;
			if (this.comparer == null)
			{
				while (dictionaryNode != null)
				{
					if (key.Equals(dictionaryNode.key))
					{
						break;
					}
					prev = dictionaryNode;
					dictionaryNode = dictionaryNode.next;
				}
			}
			else
			{
				while (dictionaryNode != null)
				{
					if (this.comparer.Compare(key, dictionaryNode.key) == 0)
					{
						break;
					}
					prev = dictionaryNode;
					dictionaryNode = dictionaryNode.next;
				}
			}
			return dictionaryNode;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000173DC File Offset: 0x000155DC
		private void AddImpl(object key, object value, ListDictionary.DictionaryNode prev)
		{
			if (prev == null)
			{
				this.head = new ListDictionary.DictionaryNode(key, value, this.head);
			}
			else
			{
				prev.next = new ListDictionary.DictionaryNode(key, value, prev.next);
			}
			this.count++;
			this.version++;
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00017438 File Offset: 0x00015638
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00017440 File Offset: 0x00015640
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00017444 File Offset: 0x00015644
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.ListDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.ListDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.ListDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.ListDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x0600079F RID: 1951 RVA: 0x00017448 File Offset: 0x00015648
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "index is less than 0");
			}
			if (index > array.Length)
			{
				throw new IndexOutOfRangeException("index is too large");
			}
			if (this.Count > array.Length - index)
			{
				throw new ArgumentException("Not enough room in the array");
			}
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array.SetValue(dictionaryEntry, index++);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00017520 File Offset: 0x00015720
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00017524 File Offset: 0x00015724
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x17000199 RID: 409
		public object this[object key]
		{
			get
			{
				ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key);
				return (dictionaryNode != null) ? dictionaryNode.value : null;
			}
			set
			{
				ListDictionary.DictionaryNode dictionaryNode2;
				ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
				if (dictionaryNode != null)
				{
					dictionaryNode.value = value;
				}
				else
				{
					this.AddImpl(key, value, dictionaryNode2);
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00017584 File Offset: 0x00015784
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.DictionaryNodeCollection(this, true);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00017590 File Offset: 0x00015790
		public ICollection Values
		{
			get
			{
				return new ListDictionary.DictionaryNodeCollection(this, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </exception>
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001759C File Offset: 0x0001579C
		public void Add(object key, object value)
		{
			ListDictionary.DictionaryNode dictionaryNode2;
			ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
			if (dictionaryNode != null)
			{
				throw new ArgumentException("key", "Duplicate key in add.");
			}
			this.AddImpl(key, value, dictionaryNode2);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		// Token: 0x060007A7 RID: 1959 RVA: 0x000175D4 File Offset: 0x000157D4
		public void Clear()
		{
			this.head = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x060007A8 RID: 1960 RVA: 0x000175F4 File Offset: 0x000157F4
		public bool Contains(object key)
		{
			return this.FindEntry(key) != null;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x060007A9 RID: 1961 RVA: 0x00017604 File Offset: 0x00015804
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.DictionaryNodeEnumerator(this);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x060007AA RID: 1962 RVA: 0x0001760C File Offset: 0x0001580C
		public void Remove(object key)
		{
			ListDictionary.DictionaryNode dictionaryNode2;
			ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
			if (dictionaryNode == null)
			{
				return;
			}
			if (dictionaryNode2 == null)
			{
				this.head = dictionaryNode.next;
			}
			else
			{
				dictionaryNode2.next = dictionaryNode.next;
			}
			dictionaryNode.value = null;
			this.count--;
			this.version++;
		}

		// Token: 0x04000207 RID: 519
		private int count;

		// Token: 0x04000208 RID: 520
		private int version;

		// Token: 0x04000209 RID: 521
		private ListDictionary.DictionaryNode head;

		// Token: 0x0400020A RID: 522
		private IComparer comparer;

		// Token: 0x020000B2 RID: 178
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x060007AB RID: 1963 RVA: 0x00017670 File Offset: 0x00015870
			public DictionaryNode(object key, object value, ListDictionary.DictionaryNode next)
			{
				this.key = key;
				this.value = value;
				this.next = next;
			}

			// Token: 0x0400020B RID: 523
			public object key;

			// Token: 0x0400020C RID: 524
			public object value;

			// Token: 0x0400020D RID: 525
			public ListDictionary.DictionaryNode next;
		}

		// Token: 0x020000B3 RID: 179
		private class DictionaryNodeEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x060007AC RID: 1964 RVA: 0x00017690 File Offset: 0x00015890
			public DictionaryNodeEnumerator(ListDictionary dict)
			{
				this.dict = dict;
				this.version = dict.version;
				this.Reset();
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x000176B4 File Offset: 0x000158B4
			private void FailFast()
			{
				if (this.version != this.dict.version)
				{
					throw new InvalidOperationException("The ListDictionary's contents changed after this enumerator was instantiated.");
				}
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x000176D8 File Offset: 0x000158D8
			public bool MoveNext()
			{
				this.FailFast();
				if (this.current == null && !this.isAtStart)
				{
					return false;
				}
				this.current = ((!this.isAtStart) ? this.current.next : this.dict.head);
				this.isAtStart = false;
				return this.current != null;
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x00017744 File Offset: 0x00015944
			public void Reset()
			{
				this.FailFast();
				this.isAtStart = true;
				this.current = null;
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001775C File Offset: 0x0001595C
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001776C File Offset: 0x0001596C
			private ListDictionary.DictionaryNode DictionaryNode
			{
				get
				{
					this.FailFast();
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumerator is positioned before the collection's first element or after the last element.");
					}
					return this.current;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001779C File Offset: 0x0001599C
			public DictionaryEntry Entry
			{
				get
				{
					object key = this.DictionaryNode.key;
					return new DictionaryEntry(key, this.current.value);
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000177C8 File Offset: 0x000159C8
			public object Key
			{
				get
				{
					return this.DictionaryNode.key;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000177D8 File Offset: 0x000159D8
			public object Value
			{
				get
				{
					return this.DictionaryNode.value;
				}
			}

			// Token: 0x0400020E RID: 526
			private ListDictionary dict;

			// Token: 0x0400020F RID: 527
			private bool isAtStart;

			// Token: 0x04000210 RID: 528
			private ListDictionary.DictionaryNode current;

			// Token: 0x04000211 RID: 529
			private int version;
		}

		// Token: 0x020000B4 RID: 180
		private class DictionaryNodeCollection : ICollection, IEnumerable
		{
			// Token: 0x060007B5 RID: 1973 RVA: 0x000177E8 File Offset: 0x000159E8
			public DictionaryNodeCollection(ListDictionary dict, bool isKeyList)
			{
				this.dict = dict;
				this.isKeyList = isKeyList;
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00017800 File Offset: 0x00015A00
			public int Count
			{
				get
				{
					return this.dict.Count;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00017810 File Offset: 0x00015A10
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00017814 File Offset: 0x00015A14
			public object SyncRoot
			{
				get
				{
					return this.dict.SyncRoot;
				}
			}

			// Token: 0x060007B9 RID: 1977 RVA: 0x00017824 File Offset: 0x00015A24
			public void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array", "Array cannot be null.");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "index is less than 0");
				}
				if (index > array.Length)
				{
					throw new IndexOutOfRangeException("index is too large");
				}
				if (this.Count > array.Length - index)
				{
					throw new ArgumentException("Not enough room in the array");
				}
				foreach (object obj in this)
				{
					array.SetValue(obj, index++);
				}
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x000178F4 File Offset: 0x00015AF4
			public IEnumerator GetEnumerator()
			{
				return new ListDictionary.DictionaryNodeCollection.DictionaryNodeCollectionEnumerator(this.dict.GetEnumerator(), this.isKeyList);
			}

			// Token: 0x04000212 RID: 530
			private ListDictionary dict;

			// Token: 0x04000213 RID: 531
			private bool isKeyList;

			// Token: 0x020000B5 RID: 181
			private class DictionaryNodeCollectionEnumerator : IEnumerator
			{
				// Token: 0x060007BB RID: 1979 RVA: 0x0001790C File Offset: 0x00015B0C
				public DictionaryNodeCollectionEnumerator(IDictionaryEnumerator inner, bool isKeyList)
				{
					this.inner = inner;
					this.isKeyList = isKeyList;
				}

				// Token: 0x170001A4 RID: 420
				// (get) Token: 0x060007BC RID: 1980 RVA: 0x00017924 File Offset: 0x00015B24
				public object Current
				{
					get
					{
						return (!this.isKeyList) ? this.inner.Value : this.inner.Key;
					}
				}

				// Token: 0x060007BD RID: 1981 RVA: 0x00017958 File Offset: 0x00015B58
				public bool MoveNext()
				{
					return this.inner.MoveNext();
				}

				// Token: 0x060007BE RID: 1982 RVA: 0x00017968 File Offset: 0x00015B68
				public void Reset()
				{
					this.inner.Reset();
				}

				// Token: 0x04000214 RID: 532
				private IDictionaryEnumerator inner;

				// Token: 0x04000215 RID: 533
				private bool isKeyList;
			}
		}
	}
}
