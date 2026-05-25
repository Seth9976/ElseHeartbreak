using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x020000D5 RID: 213
	internal class Index
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002F95C File Offset: 0x0002DB5C
		internal Index(Key key)
		{
			this._key = key;
			this.Reset();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002F984 File Offset: 0x0002DB84
		internal Key Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002F98C File Offset: 0x0002DB8C
		internal int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002F994 File Offset: 0x0002DB94
		internal int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002F99C File Offset: 0x0002DB9C
		internal int IndexToRecord(int index)
		{
			return (index >= 0) ? this._array[index] : index;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		internal bool HasDuplicates
		{
			get
			{
				if (!this.know_have_duplicates && !this.know_no_duplicates)
				{
					for (int i = 0; i < this._size - 1; i++)
					{
						if (this.Key.CompareRecords(this._array[i], this._array[i + 1]) == 0)
						{
							this.know_have_duplicates = true;
							break;
						}
					}
					this.know_no_duplicates = !this.know_have_duplicates;
				}
				return this.know_have_duplicates;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002FA34 File Offset: 0x0002DC34
		internal int[] Duplicates
		{
			get
			{
				if (!this.HasDuplicates)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				bool flag = false;
				for (int i = 0; i < this._size - 1; i++)
				{
					if (this.Key.CompareRecords(this._array[i], this._array[i + 1]) == 0)
					{
						if (!flag)
						{
							arrayList.Add(this._array[i]);
							flag = true;
						}
						arrayList.Add(this._array[i + 1]);
					}
					else
					{
						flag = false;
					}
				}
				return (int[])arrayList.ToArray(typeof(int));
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		internal int[] GetAll()
		{
			return this._array;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
		internal DataRow[] GetAllRows()
		{
			DataRow[] array = new DataRow[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this.Key.Table.RecordCache[this._array[i]];
			}
			return array;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002FB3C File Offset: 0x0002DD3C
		internal DataRow[] GetDistinctRows()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.Key.Table.RecordCache[this._array[0]]);
			int num = this._array[0];
			for (int i = 1; i < this._size; i++)
			{
				if (this.Key.CompareRecords(num, this._array[i]) != 0)
				{
					arrayList.Add(this.Key.Table.RecordCache[this._array[i]]);
					num = this._array[i];
				}
			}
			return (DataRow[])arrayList.ToArray(typeof(DataRow));
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002FBF4 File Offset: 0x0002DDF4
		internal void Reset()
		{
			this._array = Index.empty;
			this._size = 0;
			this.RebuildIndex();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002FC10 File Offset: 0x0002DE10
		private void RebuildIndex()
		{
			int currentCapacity = this.Key.Table.RecordCache.CurrentCapacity;
			if (currentCapacity == 0)
			{
				return;
			}
			this._array = new int[currentCapacity];
			this._size = 0;
			foreach (object obj in this.Key.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int record = this.Key.GetRecord(dataRow);
				if (record != -1)
				{
					this._array[this._size++] = record;
				}
			}
			this.know_have_duplicates = (this.know_no_duplicates = false);
			this.Sort();
			this.know_no_duplicates = !this.know_have_duplicates;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002FD0C File Offset: 0x0002DF0C
		private void Sort()
		{
			this.MergeSort(this._array, this._size);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002FD20 File Offset: 0x0002DF20
		internal int Find(object[] keys)
		{
			int num = this.FindIndex(keys);
			return this.IndexToRecord(num);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		internal int FindIndex(object[] keys)
		{
			if (keys == null || keys.Length != this.Key.Columns.Length)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Expecting ",
					this.Key.Columns.Length,
					" value(s) for the key being indexed, but received ",
					(keys != null) ? keys.Length : 0,
					" value(s)."
				}));
			}
			int num = this.Key.Table.RecordCache.NewRecord();
			int num2;
			try
			{
				for (int i = 0; i < this.Key.Columns.Length; i++)
				{
					this.Key.Columns[i].DataContainer[num] = keys[i];
				}
				num2 = this.FindIndex(num);
			}
			finally
			{
				this.Key.Table.RecordCache.DisposeRecord(num);
			}
			return num2;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002FE50 File Offset: 0x0002E050
		internal int Find(int record)
		{
			int num = this.FindIndex(record);
			return this.IndexToRecord(num);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002FE6C File Offset: 0x0002E06C
		internal int[] FindAll(object[] keys)
		{
			int[] array = this.FindAllIndexes(keys);
			this.IndexesToRecords(array);
			return array;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002FE8C File Offset: 0x0002E08C
		internal int[] FindAllIndexes(object[] keys)
		{
			if (keys == null || keys.Length != this.Key.Columns.Length)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Expecting ",
					this.Key.Columns.Length,
					" value(s) for the key being indexed,but received ",
					(keys != null) ? keys.Length : 0,
					" value(s)."
				}));
			}
			int num = this.Key.Table.RecordCache.NewRecord();
			int[] array;
			try
			{
				for (int i = 0; i < this.Key.Columns.Length; i++)
				{
					this.Key.Columns[i].DataContainer[num] = keys[i];
				}
				array = this.FindAllIndexes(num);
			}
			catch (FormatException)
			{
				array = Index.empty;
			}
			catch (InvalidCastException)
			{
				array = Index.empty;
			}
			finally
			{
				this.Key.Table.RecordCache.DisposeRecord(num);
			}
			return array;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
		internal int[] FindAll(int record)
		{
			int[] array = this.FindAllIndexes(record);
			this.IndexesToRecords(array);
			return array;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00030010 File Offset: 0x0002E210
		internal int[] FindAllIndexes(int record)
		{
			int num = this.FindIndex(record);
			if (num == -1)
			{
				return Index.empty;
			}
			int num2 = num++;
			int num3 = num;
			while (num2 >= 0 && this.Key.CompareRecords(this._array[num2], record) == 0)
			{
				num2--;
			}
			while (num3 < this._size && this.Key.CompareRecords(this._array[num3], record) == 0)
			{
				num3++;
			}
			int num4 = num3 - num2 - 1;
			int[] array = new int[num4];
			for (int i = 0; i < num4; i++)
			{
				num2 = (array[i] = num2 + 1);
			}
			return array;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000300C4 File Offset: 0x0002E2C4
		private int FindIndex(int record)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.BinarySearch(this._array, 0, this._size - 1, record);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000300EC File Offset: 0x0002E2EC
		private int FindIndexExact(int record)
		{
			int i = 0;
			int size = this._size;
			while (i < size)
			{
				if (this._array[i] == record)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00030124 File Offset: 0x0002E324
		private void IndexesToRecords(int[] indexes)
		{
			for (int i = 0; i < indexes.Length; i++)
			{
				indexes[i] = this._array[indexes[i]];
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00030154 File Offset: 0x0002E354
		internal void Delete(DataRow row)
		{
			int record = this.Key.GetRecord(row);
			this.Delete(record);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00030178 File Offset: 0x0002E378
		internal void Delete(int oldRecord)
		{
			if (oldRecord == -1)
			{
				return;
			}
			int num = this.FindIndexExact(oldRecord);
			if (num != -1)
			{
				if (this.know_have_duplicates)
				{
					int num2 = 1;
					int num3 = 1;
					if (num > 0)
					{
						num2 = this.Key.CompareRecords(this._array[num - 1], oldRecord);
					}
					if (num < this._size - 1)
					{
						num3 = this.Key.CompareRecords(this._array[num + 1], oldRecord);
					}
					if ((num2 == 0) ^ (num3 == 0))
					{
						this.know_have_duplicates = (this.know_no_duplicates = false);
					}
				}
				this.Remove(num);
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00030214 File Offset: 0x0002E414
		private void Remove(int index)
		{
			if (this._size > 1)
			{
				Array.Copy(this._array, index + 1, this._array, index, this._size - index - 1);
			}
			this._size--;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00030250 File Offset: 0x0002E450
		internal void Update(DataRow row, int oldRecord, DataRowVersion oldVersion, DataRowState oldState)
		{
			bool flag = this.Key.ContainsVersion(oldState, oldVersion);
			int record = this.Key.GetRecord(row);
			if (oldRecord == -1 || this._size == 0 || !flag)
			{
				if (record >= 0 && this.FindIndexExact(record) < 0)
				{
					this.Add(row, record);
				}
				return;
			}
			if (record < 0 || !this.Key.CanContain(record))
			{
				this.Delete(oldRecord);
				return;
			}
			int num = this.FindIndexExact(oldRecord);
			if (num == -1)
			{
				this.Add(row, record);
				return;
			}
			int num2 = this.Key.CompareRecords(this._array[num], record);
			int num3 = 1;
			int num4 = 1;
			if (num2 == 0)
			{
				if (this._array[num] == record)
				{
					return;
				}
			}
			else if (this.know_have_duplicates)
			{
				if (num > 0)
				{
					num3 = this.Key.CompareRecords(this._array[num - 1], record);
				}
				if (num < this._size - 1)
				{
					num4 = this.Key.CompareRecords(this._array[num + 1], record);
				}
				if (((num3 == 0) ^ (num4 == 0)) && num2 != 0)
				{
					this.know_have_duplicates = (this.know_no_duplicates = false);
				}
			}
			int num5;
			if ((num == 0 && num2 > 0) || (num == this._size - 1 && num2 < 0) || num2 == 0)
			{
				num5 = num;
			}
			else
			{
				int num6;
				int num7;
				if (num2 < 0)
				{
					num6 = num + 1;
					num7 = this._size - 1;
				}
				else
				{
					num6 = 0;
					num7 = num - 1;
				}
				num5 = this.LazyBinarySearch(this._array, num6, num7, record);
				if (num < num5)
				{
					Array.Copy(this._array, num + 1, this._array, num, num5 - num);
					if (this.Key.CompareRecords(this._array[num5], record) > 0)
					{
						num5--;
					}
				}
				else if (num > num5)
				{
					Array.Copy(this._array, num5, this._array, num5 + 1, num - num5);
					if (this.Key.CompareRecords(this._array[num5], record) < 0)
					{
						num5++;
					}
				}
			}
			this._array[num5] = record;
			if (num2 != 0 && !this.know_have_duplicates)
			{
				if (num5 > 0)
				{
					num3 = this.Key.CompareRecords(this._array[num5 - 1], record);
				}
				if (num5 < this._size - 1)
				{
					num4 = this.Key.CompareRecords(this._array[num5 + 1], record);
				}
				if (num3 == 0 || num4 == 0)
				{
					this.know_have_duplicates = true;
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000304F4 File Offset: 0x0002E6F4
		internal void Add(DataRow row)
		{
			this.Add(row, this.Key.GetRecord(row));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003050C File Offset: 0x0002E70C
		private void Add(DataRow row, int newRecord)
		{
			if (newRecord < 0 || !this.Key.CanContain(newRecord))
			{
				return;
			}
			int num;
			if (this._size == 0)
			{
				num = 0;
			}
			else
			{
				num = this.LazyBinarySearch(this._array, 0, this._size - 1, newRecord);
				if (this.Key.CompareRecords(this._array[num], newRecord) < 0)
				{
					num++;
				}
			}
			this.Insert(num, newRecord);
			int num2 = 1;
			int num3 = 1;
			if (!this.know_have_duplicates)
			{
				if (num > 0)
				{
					num2 = this.Key.CompareRecords(this._array[num - 1], newRecord);
				}
				if (num < this._size - 1)
				{
					num3 = this.Key.CompareRecords(this._array[num + 1], newRecord);
				}
				if (num2 == 0 || num3 == 0)
				{
					this.know_have_duplicates = true;
				}
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000305E8 File Offset: 0x0002E7E8
		private void Insert(int index, int r)
		{
			if (this._array.Length == this._size)
			{
				int[] array = ((this._size != 0) ? new int[this._size << 1] : new int[16]);
				Array.Copy(this._array, 0, array, 0, index);
				array[index] = r;
				Array.Copy(this._array, index, array, index + 1, this._size - index);
				this._array = array;
			}
			else
			{
				Array.Copy(this._array, index, this._array, index + 1, this._size - index);
				this._array[index] = r;
			}
			this._size++;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00030698 File Offset: 0x0002E898
		private void MergeSort(int[] to, int length)
		{
			int[] array = new int[length];
			Array.Copy(to, 0, array, 0, array.Length);
			this.MergeSort(array, to, 0, array.Length);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000306C4 File Offset: 0x0002E8C4
		private void MergeSort(int[] from, int[] to, int p, int r)
		{
			int i = p + r >> 1;
			if (i == p)
			{
				return;
			}
			this.MergeSort(to, from, p, i);
			this.MergeSort(to, from, i, r);
			int num = i;
			int num2 = p;
			for (;;)
			{
				int num3 = this.Key.CompareRecords(from[p], from[i]);
				if (num3 > 0)
				{
					to[num2++] = from[i++];
					if (i == r)
					{
						break;
					}
				}
				else
				{
					if (num3 == 0)
					{
						this.know_have_duplicates = true;
					}
					to[num2++] = from[p++];
					if (p == num)
					{
						goto Block_6;
					}
				}
			}
			while (p < num)
			{
				to[num2++] = from[p++];
			}
			return;
			Block_6:
			while (i < r)
			{
				to[num2++] = from[i++];
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003079C File Offset: 0x0002E99C
		private void QuickSort(int[] a, int p, int r)
		{
			if (p < r)
			{
				int num = this.Partition(a, p, r);
				this.QuickSort(a, p, num);
				this.QuickSort(a, num + 1, r);
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000307D0 File Offset: 0x0002E9D0
		private int Partition(int[] a, int p, int r)
		{
			int num = a[p];
			int num2 = p - 1;
			int num3 = r + 1;
			for (;;)
			{
				num3--;
				if (this.Key.CompareRecords(a[num3], num) <= 0)
				{
					do
					{
						num2++;
					}
					while (this.Key.CompareRecords(a[num2], num) < 0);
					if (num2 >= num3)
					{
						break;
					}
					int num4 = a[num3];
					a[num3] = a[num2];
					a[num2] = num4;
				}
			}
			return num3;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0003083C File Offset: 0x0002EA3C
		private int BinarySearch(int[] a, int p, int r, int b)
		{
			int num = this.LazyBinarySearch(a, p, r, b);
			return (this.Key.CompareRecords(a[num], b) != 0) ? (-1) : num;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00030874 File Offset: 0x0002EA74
		private int LazyBinarySearch(int[] a, int p, int r, int b)
		{
			if (p == r)
			{
				return p;
			}
			int num = p + r >> 1;
			int num2 = this.Key.CompareRecords(a[num], b);
			if (num2 < 0)
			{
				return this.LazyBinarySearch(a, num + 1, r, b);
			}
			if (num2 > 0)
			{
				return this.LazyBinarySearch(a, p, num, b);
			}
			return num;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000308CC File Offset: 0x0002EACC
		internal void AddRef()
		{
			this._refCount++;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000308DC File Offset: 0x0002EADC
		internal void RemoveRef()
		{
			this._refCount--;
		}

		// Token: 0x04000391 RID: 913
		private static readonly int[] empty = new int[0];

		// Token: 0x04000392 RID: 914
		private int[] _array;

		// Token: 0x04000393 RID: 915
		private int _size;

		// Token: 0x04000394 RID: 916
		private Key _key;

		// Token: 0x04000395 RID: 917
		private int _refCount;

		// Token: 0x04000396 RID: 918
		private bool know_have_duplicates;

		// Token: 0x04000397 RID: 919
		private bool know_no_duplicates;
	}
}
