using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class List<T> : IEnumerable, ICollection, IEnumerable<T>, IList<T>, ICollection<T>, IEquatable<List<T>>, IList
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002FD4 File Offset: 0x000011D4
		public List()
		{
			this._items = List<T>.EmptyArray;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FE8 File Offset: 0x000011E8
		public List(IEnumerable enumerable)
			: this()
		{
			this.Extend(enumerable);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FF8 File Offset: 0x000011F8
		public List(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity");
			}
			this._items = new T[initialCapacity];
			this._count = 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003028 File Offset: 0x00001228
		public List(T[] items, bool takeOwnership)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this._items = ((!takeOwnership) ? ((T[])items.Clone()) : items);
			this._count = items.Length;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003078 File Offset: 0x00001278
		void ICollection<T>.Add(T item)
		{
			this.Push(item);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003084 File Offset: 0x00001284
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000308C File Offset: 0x0000128C
		void IList<T>.Insert(int index, T item)
		{
			this.Insert(index, item);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003098 File Offset: 0x00001298
		void IList<T>.RemoveAt(int index)
		{
			this.InnerRemoveAt(this.CheckIndex(this.NormalizeIndex(index)));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030B0 File Offset: 0x000012B0
		bool ICollection<T>.Remove(T item)
		{
			return this.InnerRemove(item);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030BC File Offset: 0x000012BC
		int IList.Add(object value)
		{
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030D4 File Offset: 0x000012D4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, List<T>.Coerce(value));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030E4 File Offset: 0x000012E4
		void IList.Remove(object value)
		{
			this.Remove(List<T>.Coerce(value));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030F4 File Offset: 0x000012F4
		int IList.IndexOf(object value)
		{
			return this.IndexOf(List<T>.Coerce(value));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003104 File Offset: 0x00001304
		bool IList.Contains(object value)
		{
			return this.Contains(List<T>.Coerce(value));
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003124 File Offset: 0x00001324
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = List<T>.Coerce(value);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003134 File Offset: 0x00001334
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003140 File Offset: 0x00001340
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003144 File Offset: 0x00001344
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this._items, 0, array, index, this._count);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000315C File Offset: 0x0000135C
		public List<T> Multiply(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			T[] array = new T[this._count * count];
			for (int i = 0; i < count; i++)
			{
				Array.Copy(this._items, 0, array, i * this._count, this._count);
			}
			return this.NewConcreteList(array, true);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031C0 File Offset: 0x000013C0
		protected virtual List<T> NewConcreteList(T[] items, bool takeOwnership)
		{
			return new List<T>(items, takeOwnership);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000031CC File Offset: 0x000013CC
		public IEnumerable<T> Reversed
		{
			get
			{
				for (int i = this._count - 1; i >= 0; i--)
				{
					yield return this._items[i];
				}
				yield break;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000031F0 File Offset: 0x000013F0
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031F8 File Offset: 0x000013F8
		public IEnumerator<T> GetEnumerator()
		{
			int originalCount = this._count;
			T[] originalItems = this._items;
			for (int i = 0; i < this._count; i++)
			{
				if (originalCount != this._count || originalItems != this._items)
				{
					throw new InvalidOperationException("The list was modified.");
				}
				yield return this._items[i];
			}
			yield break;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003214 File Offset: 0x00001414
		public void CopyTo(T[] target, int index)
		{
			Array.Copy(this._items, 0, target, index, this._count);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000322C File Offset: 0x0000142C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003230 File Offset: 0x00001430
		public object SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003238 File Offset: 0x00001438
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		public T this[int index]
		{
			get
			{
				return this._items[this.CheckIndex(this.NormalizeIndex(index))];
			}
			set
			{
				this._items[this.CheckIndex(this.NormalizeIndex(index))] = value;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003274 File Offset: 0x00001474
		public T FastAt(int normalizedIndex)
		{
			return this._items[normalizedIndex];
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003284 File Offset: 0x00001484
		public List<T> Push(T item)
		{
			return this.Add(item);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003290 File Offset: 0x00001490
		public virtual List<T> Add(T item)
		{
			this.EnsureCapacity(this._count + 1);
			this._items[this._count] = item;
			this._count++;
			return this;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032CC File Offset: 0x000014CC
		public List<T> AddUnique(T item)
		{
			if (!this.Contains(item))
			{
				this.Add(item);
			}
			return this;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032E4 File Offset: 0x000014E4
		public List<T> Extend(IEnumerable enumerable)
		{
			this.AddRange(enumerable);
			return this;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000032F0 File Offset: 0x000014F0
		public void AddRange(IEnumerable enumerable)
		{
			foreach (object obj in enumerable)
			{
				T t = (T)((object)obj);
				this.Add(t);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000335C File Offset: 0x0000155C
		public List<T> ExtendUnique(IEnumerable enumerable)
		{
			foreach (object obj in enumerable)
			{
				T t = (T)((object)obj);
				this.AddUnique(t);
			}
			return this;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000033C8 File Offset: 0x000015C8
		public List<T> Collect(Predicate<T> condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			List<T> list = this.NewConcreteList(new T[0], true);
			this.InnerCollect(list, condition);
			return list;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003400 File Offset: 0x00001600
		public List<T> Collect(List<T> target, Predicate<T> condition)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.InnerCollect(target, condition);
			return target;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003430 File Offset: 0x00001630
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return List<T>.EmptyArray;
			}
			T[] array = new T[this._count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003464 File Offset: 0x00001664
		public T[] ToArray(T[] array)
		{
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003470 File Offset: 0x00001670
		public TOut[] ToArray<TOut>(Function<T, TOut> selector)
		{
			TOut[] array = new TOut[this._count];
			for (int i = 0; i < this._count; i++)
			{
				array[i] = selector(this._items[i]);
			}
			return array;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000034BC File Offset: 0x000016BC
		public List<T> Sort()
		{
			Array.Sort(this._items, 0, this._count, BooComparer.Default);
			return this;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034D8 File Offset: 0x000016D8
		public List<T> Sort(IComparer comparer)
		{
			Array.Sort(this._items, 0, this._count, comparer);
			return this;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000034F0 File Offset: 0x000016F0
		public List<T> Sort(Comparison<T> comparison)
		{
			return this.Sort(new List<T>.ComparisonComparer(comparison));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003500 File Offset: 0x00001700
		public List<T> Sort(IComparer<T> comparer)
		{
			Array.Sort<T>(this._items, 0, this._count, comparer);
			return this;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003518 File Offset: 0x00001718
		public List<T> Sort(Comparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			Array.Sort<T>(this._items, 0, this._count, comparer);
			return this;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003540 File Offset: 0x00001740
		public override string ToString()
		{
			return "[" + this.Join(", ") + "]";
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000355C File Offset: 0x0000175C
		public string Join(string separator)
		{
			return Builtins.join(this, separator);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003568 File Offset: 0x00001768
		public override int GetHashCode()
		{
			int num = this._count;
			for (int i = 0; i < this._count; i++)
			{
				T t = this._items[i];
				if (t != null)
				{
					num ^= t.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000035BC File Offset: 0x000017BC
		public override bool Equals(object other)
		{
			return this == other || this.Equals(other as List<T>);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000035D4 File Offset: 0x000017D4
		public bool Equals(List<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (this._count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (!RuntimeServices.EqualityOperator(this._items[i], other[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000364C File Offset: 0x0000184C
		public void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._items[i] = default(T);
			}
			this._count = 0;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000368C File Offset: 0x0000188C
		public List<T> GetRange(int begin)
		{
			return this.InnerGetRange(this.AdjustIndex(this.NormalizeIndex(begin)), this._count);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036A8 File Offset: 0x000018A8
		public List<T> GetRange(int begin, int end)
		{
			return this.InnerGetRange(this.AdjustIndex(this.NormalizeIndex(begin)), this.AdjustIndex(this.NormalizeIndex(end)));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000036D8 File Offset: 0x000018D8
		public bool Contains(T item)
		{
			return -1 != this.IndexOf(item);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000036E8 File Offset: 0x000018E8
		public bool Contains(Predicate<T> condition)
		{
			return -1 != this.IndexOf(condition);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000036F8 File Offset: 0x000018F8
		public bool Find(Predicate<T> condition, out T found)
		{
			int num = this.IndexOf(condition);
			if (num != -1)
			{
				found = this._items[num];
				return true;
			}
			found = default(T);
			return false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003738 File Offset: 0x00001938
		public List<T> FindAll(Predicate<T> condition)
		{
			List<T> list = this.NewConcreteList(new T[0], true);
			foreach (T t in this)
			{
				if (condition.Invoke(t))
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000037B4 File Offset: 0x000019B4
		public int IndexOf(Predicate<T> condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			for (int i = 0; i < this._count; i++)
			{
				if (condition.Invoke(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003804 File Offset: 0x00001A04
		public int IndexOf(T item)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (RuntimeServices.EqualityOperator(this._items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000384C File Offset: 0x00001A4C
		public List<T> Insert(int index, T item)
		{
			int num = this.NormalizeIndex(index);
			this.EnsureCapacity(Math.Max(this._count, num) + 1);
			if (num < this._count)
			{
				Array.Copy(this._items, num, this._items, num + 1, this._count - num);
			}
			this._items[num] = item;
			this._count++;
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000038BC File Offset: 0x00001ABC
		public T Pop()
		{
			return this.Pop(-1);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000038C8 File Offset: 0x00001AC8
		public T Pop(int index)
		{
			int num = this.CheckIndex(this.NormalizeIndex(index));
			T t = this._items[num];
			this.InnerRemoveAt(num);
			return t;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000038F8 File Offset: 0x00001AF8
		public List<T> PopRange(int begin)
		{
			int num = this.AdjustIndex(this.NormalizeIndex(begin));
			List<T> list = this.InnerGetRange(num, this.AdjustIndex(this.NormalizeIndex(this._count)));
			for (int i = num; i < this._count; i++)
			{
				this._items[i] = default(T);
			}
			this._count = num;
			return list;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003964 File Offset: 0x00001B64
		public List<T> RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < this._count; i++)
			{
				if (match.Invoke(this._items[i]))
				{
					this.InnerRemoveAt(i--);
				}
			}
			return this;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000039BC File Offset: 0x00001BBC
		public List<T> Remove(T item)
		{
			this.InnerRemove(item);
			return this;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000039C8 File Offset: 0x00001BC8
		public List<T> RemoveAt(int index)
		{
			this.InnerRemoveAt(this.CheckIndex(this.NormalizeIndex(index)));
			return this;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000039E0 File Offset: 0x00001BE0
		private void EnsureCapacity(int minCapacity)
		{
			if (minCapacity > this._items.Length)
			{
				T[] array = this.NewArray(minCapacity);
				Array.Copy(this._items, 0, array, 0, this._count);
				this._items = array;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A20 File Offset: 0x00001C20
		private T[] NewArray(int minCapacity)
		{
			int num = Math.Max(1, this._items.Length) * 2;
			return new T[Math.Max(num, minCapacity)];
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A4C File Offset: 0x00001C4C
		private void InnerRemoveAt(int index)
		{
			this._count--;
			this._items[index] = default(T);
			if (index != this._count)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._count - index);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003AA8 File Offset: 0x00001CA8
		private bool InnerRemove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.InnerRemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private void InnerCollect(List<T> target, Predicate<T> condition)
		{
			for (int i = 0; i < this._count; i++)
			{
				T t = this._items[i];
				if (condition.Invoke(t))
				{
					target.Add(t);
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B18 File Offset: 0x00001D18
		private List<T> InnerGetRange(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				T[] array = new T[num];
				Array.Copy(this._items, begin, array, 0, num);
				return this.NewConcreteList(array, true);
			}
			return this.NewConcreteList(new T[0], true);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003B5C File Offset: 0x00001D5C
		private int AdjustIndex(int index)
		{
			if (index > this._count)
			{
				return this._count;
			}
			if (index < 0)
			{
				return 0;
			}
			return index;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003B7C File Offset: 0x00001D7C
		private int CheckIndex(int index)
		{
			if (index >= this._count)
			{
				throw new IndexOutOfRangeException();
			}
			return index;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003B94 File Offset: 0x00001D94
		private int NormalizeIndex(int index)
		{
			return (index >= 0) ? index : (index + this._count);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003BAC File Offset: 0x00001DAC
		private static T Coerce(object value)
		{
			if (value is T)
			{
				return (T)((object)value);
			}
			return (T)((object)RuntimeServices.Coerce(value, typeof(T)));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public static List<T>operator *(List<T> lhs, int count)
		{
			return lhs.Multiply(count);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public static List<T>operator *(int count, List<T> rhs)
		{
			return rhs.Multiply(count);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public static List<T>operator +(List<T> lhs, IEnumerable rhs)
		{
			List<T> list = lhs.NewConcreteList(lhs.ToArray(), true);
			list.Extend(rhs);
			return list;
		}

		// Token: 0x04000014 RID: 20
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x04000015 RID: 21
		protected T[] _items;

		// Token: 0x04000016 RID: 22
		protected int _count;

		// Token: 0x0200001A RID: 26
		private sealed class ComparisonComparer : IComparer<T>
		{
			// Token: 0x060000B7 RID: 183 RVA: 0x00003C14 File Offset: 0x00001E14
			public ComparisonComparer(Comparison<T> comparison)
			{
				this._comparison = comparison;
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00003C24 File Offset: 0x00001E24
			public int Compare(T x, T y)
			{
				return this._comparison.Invoke(x, y);
			}

			// Token: 0x04000017 RID: 23
			private readonly Comparison<T> _comparison;
		}
	}
}
