using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Provides the base functionality for creating collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000056 RID: 86
	public class InternalDataCollectionBase : IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InternalDataCollectionBase" /> class.</summary>
		// Token: 0x060005E8 RID: 1512 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public InternalDataCollectionBase()
		{
			this.list = new ArrayList();
		}

		/// <summary>Gets the total number of elements in a collection.</summary>
		/// <returns>The total number of elements in a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001E400 File Offset: 0x0001C600
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001E410 File Offset: 0x0001C610
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is synchonized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001E418 File Offset: 0x0001C618
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.synchronized;
			}
		}

		/// <summary>Gets the items of the collection as a list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the collection.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001E420 File Offset: 0x0001C620
		protected virtual ArrayList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets an object that can be used to synchronize the collection.</summary>
		/// <returns>The <see cref="T:System.object" /> used to synchronize the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001E428 File Offset: 0x0001C628
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.InternalDataCollectionBase" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Data.InternalDataCollectionBase" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.InternalDataCollectionBase" /> object's elements into. </param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005EE RID: 1518 RVA: 0x0001E42C File Offset: 0x0001C62C
		public virtual void CopyTo(Array ar, int index)
		{
			this.list.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005EF RID: 1519 RVA: 0x0001E43C File Offset: 0x0001C63C
		public virtual IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001E44C File Offset: 0x0001C64C
		internal Array ToArray(Type type)
		{
			return this.list.ToArray(type);
		}

		// Token: 0x040001CC RID: 460
		private ArrayList list;

		// Token: 0x040001CD RID: 461
		private bool readOnly;

		// Token: 0x040001CE RID: 462
		private bool synchronized;
	}
}
