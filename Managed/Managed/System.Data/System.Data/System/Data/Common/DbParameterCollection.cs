using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>The base class for a collection of parameters relevant to a <see cref="T:System.Data.Common.DbCommand" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C8 RID: 200
	public abstract class DbParameterCollection : MarshalByRefObject, IList, IDataParameterCollection, IEnumerable, ICollection
	{
		// Token: 0x170001B5 RID: 437
		object IDataParameterCollection.this[string parameterName]
		{
			get
			{
				return this[parameterName];
			}
			set
			{
				this[parameterName] = (DbParameter)value;
			}
		}

		// Token: 0x170001B6 RID: 438
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (DbParameter)value;
			}
		}

		/// <summary>Specifies the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060009B1 RID: 2481
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract int Count { get; }

		/// <summary>Specifies whether the collection is a fixed size.</summary>
		/// <returns>true if the collection is a fixed size; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060009B2 RID: 2482
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool IsFixedSize { get; }

		/// <summary>Specifies whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060009B3 RID: 2483
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract bool IsReadOnly { get; }

		/// <summary>Specifies whether the collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060009B4 RID: 2484
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract bool IsSynchronized { get; }

		/// <summary>Gets and sets the <see cref="T:System.Data.Common.DbParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BB RID: 443
		public DbParameter this[string parameterName]
		{
			get
			{
				int num = this.IndexOf(parameterName);
				return this[num];
			}
			set
			{
				int num = this.IndexOf(parameterName);
				this[num] = value;
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BC RID: 444
		public DbParameter this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		/// <summary>Specifies the <see cref="T:System.Object" /> to be used to synchronize access to the collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> to be used to synchronize access to the <see cref="T:System.Data.Common.DbParameterCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009B9 RID: 2489
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public abstract object SyncRoot { get; }

		/// <summary>Adds the specified <see cref="T:System.Data.Common.DbParameter" /> object to the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009BA RID: 2490
		public abstract int Add(object value);

		/// <summary>Adds an array of items with the specified values to the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <param name="values">An array of values of type <see cref="T:System.Data.Common.DbParameter" /> to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009BB RID: 2491
		public abstract void AddRange(Array values);

		/// <summary>Returns <see cref="T:System.Data.Common.DbParameter" /> the object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> the object with the specified name.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> in the collection.</param>
		// Token: 0x060009BC RID: 2492
		protected abstract DbParameter GetParameter(string parameterName);

		/// <summary>Sets the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name to a new value.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <param name="value">The new <see cref="T:System.Data.Common.DbParameter" /> value.</param>
		// Token: 0x060009BD RID: 2493
		protected abstract void SetParameter(string parameterName, DbParameter value);

		/// <summary>Removes all <see cref="T:System.Data.Common.DbParameter" /> values from the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009BE RID: 2494
		public abstract void Clear();

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DbParameter" /> with the specified <see cref="P:System.Data.Common.DbParameter.Value" /> is contained in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to look for in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009BF RID: 2495
		public abstract bool Contains(object value);

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DbParameter" /> with the specified name exists in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The name of the <see cref="T:System.Data.Common.DbParameter" /> to look for in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C0 RID: 2496
		public abstract bool Contains(string value);

		/// <summary>Copies an array of items to the collection starting at the specified index.</summary>
		/// <param name="array">The array of items to copy to the collection.</param>
		/// <param name="index">The index in the collection to copy the items.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C1 RID: 2497
		public abstract void CopyTo(Array array, int index);

		/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C2 RID: 2498
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Returns the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Data.Common.DbParameter" /> in the collection.</param>
		// Token: 0x060009C3 RID: 2499
		protected abstract DbParameter GetParameter(int index);

		/// <summary>Returns the index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C4 RID: 2500
		public abstract int IndexOf(object value);

		/// <summary>Returns the index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C5 RID: 2501
		public abstract int IndexOf(string parameterName);

		/// <summary>Inserts the specified index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name into the collection at the specified index.</summary>
		/// <param name="index">The index at which to insert the <see cref="T:System.Data.Common.DbParameter" /> object.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to insert into the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C6 RID: 2502
		public abstract void Insert(int index, object value);

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DbParameter" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009C7 RID: 2503
		public abstract void Remove(object value);

		/// <summary>Removes the <see cref="T:System.Data.Common.DbParameter" /> object at the specified from the collection.</summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C8 RID: 2504
		public abstract void RemoveAt(int index);

		/// <summary>Removes the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name from the collection.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object to remove.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009C9 RID: 2505
		public abstract void RemoveAt(string parameterName);

		/// <summary>Sets the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index to a new value. </summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <param name="value">The new <see cref="T:System.Data.Common.DbParameter" /> value.</param>
		// Token: 0x060009CA RID: 2506
		protected abstract void SetParameter(int index, DbParameter value);
	}
}
