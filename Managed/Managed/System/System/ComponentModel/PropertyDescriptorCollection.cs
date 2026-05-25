using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</summary>
	// Token: 0x02000196 RID: 406
	public class PropertyDescriptorCollection : IDictionary, IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection. </param>
		// Token: 0x06000E1F RID: 3615 RVA: 0x000245C0 File Offset: 0x000227C0
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			this.properties = new ArrayList();
			if (properties == null)
			{
				return;
			}
			this.properties.AddRange(properties);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class, which is optionally read-only.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		/// <param name="readOnly">If true, specifies that the collection cannot be modified.</param>
		// Token: 0x06000E20 RID: 3616 RVA: 0x000245F4 File Offset: 0x000227F4
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly)
			: this(properties)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00024604 File Offset: 0x00022804
		private PropertyDescriptorCollection()
		{
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		// Token: 0x06000E23 RID: 3619 RVA: 0x0002461C File Offset: 0x0002281C
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0002462C File Offset: 0x0002282C
		void IDictionary.Add(object key, object value)
		{
			if (!(value is PropertyDescriptor))
			{
				throw new ArgumentException("value");
			}
			this.Add((PropertyDescriptor)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E25 RID: 3621 RVA: 0x00024654 File Offset: 0x00022854
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" />. </summary>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0002465C File Offset: 0x0002285C
		void IDictionary.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the item is found in the collection; otherwise, false.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06000E27 RID: 3623 RVA: 0x00024664 File Offset: 0x00022864
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		// Token: 0x06000E28 RID: 3624 RVA: 0x00024674 File Offset: 0x00022874
		bool IDictionary.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06000E29 RID: 3625 RVA: 0x00024684 File Offset: 0x00022884
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0002468C File Offset: 0x0002288C
		[global::System.MonoTODO]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines the index of a specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list, otherwise -1.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06000E2B RID: 3627 RVA: 0x00024694 File Offset: 0x00022894
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E2C RID: 3628 RVA: 0x000246A4 File Offset: 0x000228A4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x06000E2D RID: 3629 RVA: 0x000246B4 File Offset: 0x000228B4
		void IDictionary.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specified value from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E2E RID: 3630 RVA: 0x000246C4 File Offset: 0x000228C4
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E2F RID: 3631 RVA: 0x000246D4 File Offset: 0x000228D4
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000246E0 File Offset: 0x000228E0
		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IList)this).IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000246E8 File Offset: 0x000228E8
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000246F0 File Offset: 0x000228F0
		bool IDictionary.IsReadOnly
		{
			get
			{
				return ((IList)this).IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x000246F8 File Offset: 0x000228F8
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00024700 File Offset: 0x00022900
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00024704 File Offset: 0x00022904
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0002470C File Offset: 0x0002290C
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00024710 File Offset: 0x00022910
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.properties.Count];
				int num = 0;
				foreach (object obj in this.properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					array[num++] = propertyDescriptor.Name;
				}
				return array;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0002479C File Offset: 0x0002299C
		ICollection IDictionary.Values
		{
			get
			{
				return (ICollection)this.properties.Clone();
			}
		}

		/// <summary>Gets or sets the element with the specified key. </summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		// Token: 0x1700034B RID: 843
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					return null;
				}
				return this[(string)key];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (!(key is string) || !(value is PropertyDescriptor))
				{
					throw new ArgumentException();
				}
				int num = this.properties.IndexOf(value);
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
				}
				else
				{
					this.properties[num] = value;
				}
			}
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.ComponentModel.PropertyDescriptor" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x1700034C RID: 844
		object IList.this[int index]
		{
			get
			{
				return this.properties[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				this.properties[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection.</summary>
		/// <returns>The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added to the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E3D RID: 3645 RVA: 0x0002486C File Offset: 0x00022A6C
		public int Add(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.properties.Add(value);
			return this.properties.Count - 1;
		}

		/// <summary>Removes all <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E3E RID: 3646 RVA: 0x0002489C File Offset: 0x00022A9C
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.properties.Clear();
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>true if the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to find in the collection. </param>
		// Token: 0x06000E3F RID: 3647 RVA: 0x000248BC File Offset: 0x00022ABC
		public bool Contains(PropertyDescriptor value)
		{
			return this.properties.Contains(value);
		}

		/// <summary>Copies the entire collection to an array, starting at the specified index number.</summary>
		/// <param name="array">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to copy elements of the collection to. </param>
		/// <param name="index">The index of the <paramref name="array" /> parameter at which copying begins. </param>
		// Token: 0x06000E40 RID: 3648 RVA: 0x000248CC File Offset: 0x00022ACC
		public void CopyTo(Array array, int index)
		{
			this.properties.CopyTo(array, index);
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, using a Boolean to indicate whether to ignore case.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the property name; otherwise, false. </param>
		// Token: 0x06000E41 RID: 3649 RVA: 0x000248DC File Offset: 0x00022ADC
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			for (int i = 0; i < this.properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this.properties[i];
				if (ignoreCase)
				{
					if (string.Compare(name, propertyDescriptor.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return propertyDescriptor;
					}
				}
				else if (string.Compare(name, propertyDescriptor.Name, StringComparison.Ordinal) == 0)
				{
					return propertyDescriptor;
				}
			}
			return null;
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0002495C File Offset: 0x00022B5C
		public virtual IEnumerator GetEnumerator()
		{
			return this.properties.GetEnumerator();
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return the index of. </param>
		// Token: 0x06000E43 RID: 3651 RVA: 0x0002496C File Offset: 0x00022B6C
		public int IndexOf(PropertyDescriptor value)
		{
			return this.properties.IndexOf(value);
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection at the specified index number.</summary>
		/// <param name="index">The index at which to add the <paramref name="value" /> parameter to the collection. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E44 RID: 3652 RVA: 0x0002497C File Offset: 0x00022B7C
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.properties.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E45 RID: 3653 RVA: 0x0002499C File Offset: 0x00022B9C
		public void Remove(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.properties.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000E46 RID: 3654 RVA: 0x000249BC File Offset: 0x00022BBC
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.properties.RemoveAt(index);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000249DC File Offset: 0x00022BDC
		private PropertyDescriptorCollection CloneCollection()
		{
			return new PropertyDescriptorCollection
			{
				properties = (ArrayList)this.properties.Clone()
			};
		}

		/// <summary>Sorts the members of this collection, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x06000E48 RID: 3656 RVA: 0x00024A08 File Offset: 0x00022C08
		public virtual PropertyDescriptorCollection Sort()
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.CloneCollection();
			propertyDescriptorCollection.InternalSort(null);
			return propertyDescriptorCollection;
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000E49 RID: 3657 RVA: 0x00024A24 File Offset: 0x00022C24
		public virtual PropertyDescriptorCollection Sort(IComparer comparer)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.CloneCollection();
			propertyDescriptorCollection.InternalSort(comparer);
			return propertyDescriptorCollection;
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000E4A RID: 3658 RVA: 0x00024A40 File Offset: 0x00022C40
		public virtual PropertyDescriptorCollection Sort(string[] order)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.CloneCollection();
			propertyDescriptorCollection.InternalSort(order);
			return propertyDescriptorCollection;
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the sort using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000E4B RID: 3659 RVA: 0x00024A5C File Offset: 0x00022C5C
		public virtual PropertyDescriptorCollection Sort(string[] order, IComparer comparer)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.CloneCollection();
			if (order != null)
			{
				ArrayList arrayList = propertyDescriptorCollection.ExtractItems(order);
				propertyDescriptorCollection.InternalSort(comparer);
				arrayList.AddRange(propertyDescriptorCollection.properties);
				propertyDescriptorCollection.properties = arrayList;
			}
			else
			{
				propertyDescriptorCollection.InternalSort(comparer);
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000E4C RID: 3660 RVA: 0x00024AA8 File Offset: 0x00022CA8
		protected void InternalSort(IComparer ic)
		{
			if (ic == null)
			{
				ic = MemberDescriptor.DefaultComparer;
			}
			this.properties.Sort(ic);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000E4D RID: 3661 RVA: 0x00024AC4 File Offset: 0x00022CC4
		protected void InternalSort(string[] order)
		{
			if (order != null)
			{
				ArrayList arrayList = this.ExtractItems(order);
				this.InternalSort(null);
				arrayList.AddRange(this.properties);
				this.properties = arrayList;
			}
			else
			{
				this.InternalSort(null);
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00024B08 File Offset: 0x00022D08
		private ArrayList ExtractItems(string[] names)
		{
			ArrayList arrayList = new ArrayList(this.properties.Count);
			object[] array = new object[names.Length];
			for (int i = 0; i < this.properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this.properties[i];
				int num = Array.IndexOf<string>(names, propertyDescriptor.Name);
				if (num != -1)
				{
					array[num] = propertyDescriptor;
					this.properties.RemoveAt(i);
					i--;
				}
			}
			foreach (object obj in array)
			{
				if (obj != null)
				{
					arrayList.Add(obj);
				}
			}
			return arrayList;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00024BBC File Offset: 0x00022DBC
		internal PropertyDescriptorCollection Filter(Attribute[] attributes)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Attributes.Contains(attributes))
				{
					arrayList.Add(propertyDescriptor);
				}
			}
			PropertyDescriptor[] array = new PropertyDescriptor[arrayList.Count];
			arrayList.CopyTo(array);
			return new PropertyDescriptorCollection(array, true);
		}

		/// <summary>Gets the number of property descriptors in the collection.</summary>
		/// <returns>The number of property descriptors in the collection.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00024C60 File Offset: 0x00022E60
		public int Count
		{
			get
			{
				return this.properties.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get from the collection. </param>
		// Token: 0x1700034E RID: 846
		public virtual PropertyDescriptor this[string s]
		{
			get
			{
				return this.Find(s, false);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is not a valid index for <see cref="P:System.ComponentModel.PropertyDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x1700034F RID: 847
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				return (PropertyDescriptor)this.properties[index];
			}
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x04000402 RID: 1026
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x04000403 RID: 1027
		private ArrayList properties;

		// Token: 0x04000404 RID: 1028
		private bool readOnly;
	}
}
