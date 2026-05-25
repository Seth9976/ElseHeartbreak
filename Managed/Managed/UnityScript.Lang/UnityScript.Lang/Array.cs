using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;

namespace UnityScript.Lang
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class Array : CollectionBase, ICoercible
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public Array()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		public Array(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentException("Expected: (capacity >= 0)", "capacity");
			}
			this.EnsureCapacity(capacity);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
		public Array(IEnumerable collection)
		{
			if (collection is string)
			{
				this.Add(collection);
			}
			else
			{
				this.AddRange(collection);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		public Array(params object[] items)
		{
			if (items.Length == 1 && items[0] is IEnumerable)
			{
				object obj2;
				object obj = (obj2 = items[0]);
				if (!(obj is IEnumerable))
				{
					obj2 = RuntimeServices.Coerce(obj, typeof(IEnumerable));
				}
				this.AddRange((IEnumerable)obj2);
			}
			else
			{
				this.AddRange(items);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021BC File Offset: 0x000003BC
		public static implicit operator Array(IEnumerable e)
		{
			return (e != null) ? new Array(e) : null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D0 File Offset: 0x000003D0
		public static implicit operator Array(Array a)
		{
			return (a != null) ? new Array(a as IEnumerable) : null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
		public int length
		{
			get
			{
				return this.Count;
			}
			set
			{
				this.EnsureCapacity(value);
				if (value < this.Count)
				{
					this.InnerList.RemoveRange(value, checked(this.InnerList.Count - value));
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000430
		public void clear()
		{
			this.Clear();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002238 File Offset: 0x00000438
		public override object Coerce(Type toType)
		{
			return (!toType.IsArray) ? this : this.ToBuiltin(toType.GetElementType());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
		public Array ToBuiltin(Type type)
		{
			return this.InnerList.ToArray(type);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002268 File Offset: 0x00000468
		public void Add(object value, params object[] items)
		{
			this.AddImpl(value, items);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002274 File Offset: 0x00000474
		public void Add(object value)
		{
			this.InnerList.Add(value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002284 File Offset: 0x00000484
		public int Push(object value, params object[] items)
		{
			return this.AddImpl(value, items);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002290 File Offset: 0x00000490
		public int push(object value, params object[] items)
		{
			return this.AddImpl(value, items);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229C File Offset: 0x0000049C
		public int push(object value)
		{
			this.InnerList.Add(value);
			return this.InnerList.Count;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public int Push(object value)
		{
			return this.push(value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
		public int Unshift(object value, params object[] items)
		{
			return this.UnshiftImpl(value, items);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022DC File Offset: 0x000004DC
		public int unshift(object value, params object[] items)
		{
			return this.UnshiftImpl(value, items);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Splice(int index, int howmany, params object[] items)
		{
			this.SpliceImpl(index, howmany, items);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		public void splice(int index, int howmany, params object[] items)
		{
			this.SpliceImpl(index, howmany, items);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002300 File Offset: 0x00000500
		public Array Concat(ICollection value, params object[] items)
		{
			return this.ConcatImpl(value, items);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000230C File Offset: 0x0000050C
		public Array concat(ICollection value, params object[] items)
		{
			return this.ConcatImpl(value, items);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002318 File Offset: 0x00000518
		public object Pop()
		{
			int num = checked(this.InnerList.Count - 1);
			object obj = this.InnerList[num];
			this.InnerList.RemoveAt(num);
			return obj;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002350 File Offset: 0x00000550
		public object pop()
		{
			return this.Pop();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002358 File Offset: 0x00000558
		public object Shift()
		{
			int num = 0;
			object obj = this.InnerList[num];
			this.InnerList.RemoveAt(0);
			return obj;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002384 File Offset: 0x00000584
		public object shift()
		{
			return this.Shift();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000238C File Offset: 0x0000058C
		public override string ToString()
		{
			return this.Join(",");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239C File Offset: 0x0000059C
		public string toString()
		{
			return this.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A4 File Offset: 0x000005A4
		public Array Slice(int start, int end)
		{
			int num = this.NormalizeIndex(start);
			int num2 = this.NormalizeIndex(end);
			return new Array(this.InnerList.GetRange(num, checked(num2 - num)));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023D8 File Offset: 0x000005D8
		public Array Slice(int start)
		{
			return this.Slice(start, this.InnerList.Count);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023EC File Offset: 0x000005EC
		public Array slice(int start, int end)
		{
			return this.Slice(start, end);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023F8 File Offset: 0x000005F8
		public Array slice(int start)
		{
			return this.Slice(start);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public Array Reverse()
		{
			this.InnerList.Reverse();
			return this;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002414 File Offset: 0x00000614
		public Array reverse()
		{
			this.Reverse();
			return this;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002420 File Offset: 0x00000620
		public void Sort(Array.Comparison comparison)
		{
			this.InnerList.Sort(new Array.ComparisonComparer(comparison));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002434 File Offset: 0x00000634
		public void sort(Array.Comparison comparison)
		{
			this.Sort(comparison);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002440 File Offset: 0x00000640
		public Array Sort()
		{
			this.InnerList.Sort();
			return this;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002450 File Offset: 0x00000650
		public Array sort()
		{
			this.Sort();
			return this;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000245C File Offset: 0x0000065C
		public string Join(string seperator)
		{
			return Builtins.join(this.InnerList, seperator);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000246C File Offset: 0x0000066C
		public string join(string seperator)
		{
			return Builtins.join(this.InnerList, seperator);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000247C File Offset: 0x0000067C
		public void Remove(object obj)
		{
			this.InnerList.Remove(obj);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000248C File Offset: 0x0000068C
		public void remove(object obj)
		{
			this.Remove(obj);
		}

		// Token: 0x17000002 RID: 2
		public object this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				this.EnsureCapacity(checked(index + 1));
				this.InnerList[index] = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C0 File Offset: 0x000006C0
		public void AddRange(IEnumerable collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (object obj in collection)
			{
				this.InnerList.Add(obj);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000250C File Offset: 0x0000070C
		protected override void OnValidate(object newValue)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002510 File Offset: 0x00000710
		private int AddImpl(object value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.InnerList.Add(value);
			foreach (object obj in items)
			{
				this.InnerList.Add(obj);
			}
			return this.InnerList.Count;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002574 File Offset: 0x00000774
		private int UnshiftImpl(object value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.InnerList.InsertRange(0, (ICollection)items);
			this.InnerList.Insert(0, value);
			return this.InnerList.Count;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025BC File Offset: 0x000007BC
		private void SpliceImpl(int index, int howmany, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (howmany != 0)
			{
				this.InnerList.RemoveRange(index, howmany);
			}
			this.InnerList.InsertRange(index, (ICollection)items);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002600 File Offset: 0x00000800
		private Array ConcatImpl(ICollection value, IEnumerable items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			Array array = new Array(this.InnerList);
			array.InnerList.AddRange(value);
			foreach (object obj in items)
			{
				object obj3;
				object obj2 = (obj3 = obj);
				if (!(obj2 is ICollection))
				{
					obj3 = RuntimeServices.Coerce(obj2, typeof(ICollection));
				}
				ICollection collection = (ICollection)obj3;
				array.InnerList.AddRange(collection);
			}
			return array;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002680 File Offset: 0x00000880
		private void EnsureCapacity(int capacity)
		{
			if (capacity >= this.Count)
			{
				int i = 0;
				int num = checked(capacity - this.Count);
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("max");
				}
				while (i < num)
				{
					i++;
					this.InnerList.Add(null);
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026DC File Offset: 0x000008DC
		private int NormalizeIndex(int index)
		{
			return (index < 0) ? checked(index + this.InnerList.Count) : index;
		}

		// Token: 0x02000003 RID: 3
		[CompilerGenerated]
		[Serializable]
		public sealed class Comparison : MulticastDelegate, ICallable
		{
			// Token: 0x06000036 RID: 54
			public extern Comparison(object instance, IntPtr method);

			// Token: 0x06000037 RID: 55 RVA: 0x000026F8 File Offset: 0x000008F8
			public override object Call(object[] args)
			{
				return this(args[0], args[1]);
			}

			// Token: 0x06000038 RID: 56
			public override extern int Invoke(object lhs, object rhs);

			// Token: 0x06000039 RID: 57
			public override extern IAsyncResult BeginInvoke(object lhs, object rhs, AsyncCallback callback, object asyncState);

			// Token: 0x0600003A RID: 58
			public override extern int EndInvoke(IAsyncResult asyncResult);
		}

		// Token: 0x02000004 RID: 4
		[Serializable]
		private class ComparisonComparer : IComparer
		{
			// Token: 0x0600003B RID: 59 RVA: 0x0000270C File Offset: 0x0000090C
			public ComparisonComparer(Array.Comparison comparison)
			{
				this._comparison = comparison;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x0000271C File Offset: 0x0000091C
			public override int Compare(object lhs, object rhs)
			{
				return this._comparison(lhs, rhs);
			}

			// Token: 0x04000001 RID: 1
			protected Array.Comparison _comparison;
		}
	}
}
