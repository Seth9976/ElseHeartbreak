using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AD RID: 173
	internal class CollectionWrapper<T> : ICollection<T>, IEnumerable<T>, IWrappedCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0001C904 File Offset: 0x0001AB04
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			if (list is ICollection<T>)
			{
				this._genericCollection = (ICollection<T>)list;
				return;
			}
			this._list = list;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001C933 File Offset: 0x0001AB33
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001C94D File Offset: 0x0001AB4D
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001C976 File Offset: 0x0001AB76
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001C997 File Offset: 0x0001AB97
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001C9BF File Offset: 0x0001ABBF
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001C9E4 File Offset: 0x0001ABE4
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001CA71 File Offset: 0x0001AC71
		public virtual IEnumerator<T> GetEnumerator()
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.GetEnumerator();
			}
			return this._list.Cast<T>().GetEnumerator();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001CA97 File Offset: 0x0001AC97
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.GetEnumerator();
			}
			return this._list.GetEnumerator();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new Exception("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001CB21 File Offset: 0x0001AD21
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new Exception("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001CB42 File Offset: 0x0001AD42
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new Exception("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001CB74 File Offset: 0x0001AD74
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001CB95 File Offset: 0x0001AD95
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x1700018F RID: 399
		object IList.this[int index]
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new Exception("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new Exception("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001CBFF File Offset: 0x0001ADFF
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001CC0E File Offset: 0x0001AE0E
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001CC11 File Offset: 0x0001AE11
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

		// Token: 0x060007EE RID: 2030 RVA: 0x0001CC34 File Offset: 0x0001AE34
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					value,
					typeof(T)
				}), "value");
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001CCAE File Offset: 0x0001AEAE
		public object UnderlyingCollection
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection;
				}
				return this._list;
			}
		}

		// Token: 0x04000272 RID: 626
		private readonly IList _list;

		// Token: 0x04000273 RID: 627
		private readonly ICollection<T> _genericCollection;

		// Token: 0x04000274 RID: 628
		private object _syncRoot;
	}
}
