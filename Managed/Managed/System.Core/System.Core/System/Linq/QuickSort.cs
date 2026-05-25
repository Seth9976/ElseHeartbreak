using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000030 RID: 48
	internal class QuickSort<TElement>
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000F488 File Offset: 0x0000D688
		private QuickSort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			this.elements = source.ToArray<TElement>();
			this.indexes = QuickSort<TElement>.CreateIndexes(this.elements.Length);
			this.context = context;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		private static int[] CreateIndexes(int length)
		{
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void PerformSort()
		{
			if (this.elements.Length <= 1)
			{
				return;
			}
			this.context.Initialize(this.elements);
			this.Sort(0, this.indexes.Length - 1);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F524 File Offset: 0x0000D724
		private int CompareItems(int first_index, int second_index)
		{
			return this.context.Compare(first_index, second_index);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F534 File Offset: 0x0000D734
		private int MedianOfThree(int left, int right)
		{
			int num = (left + right) / 2;
			if (this.CompareItems(this.indexes[num], this.indexes[left]) < 0)
			{
				this.Swap(left, num);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[left]) < 0)
			{
				this.Swap(left, right);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[num]) < 0)
			{
				this.Swap(num, right);
			}
			this.Swap(num, right - 1);
			return this.indexes[right - 1];
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		private void Sort(int left, int right)
		{
			if (left + 3 <= right)
			{
				int num = left;
				int num2 = right - 1;
				int num3 = this.MedianOfThree(left, right);
				for (;;)
				{
					while (this.CompareItems(this.indexes[++num], num3) < 0)
					{
					}
					while (this.CompareItems(this.indexes[--num2], num3) > 0)
					{
					}
					if (num >= num2)
					{
						break;
					}
					this.Swap(num, num2);
				}
				this.Swap(num, right - 1);
				this.Sort(left, num - 1);
				this.Sort(num + 1, right);
			}
			else
			{
				this.InsertionSort(left, right);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F674 File Offset: 0x0000D874
		private void InsertionSort(int left, int right)
		{
			for (int i = left + 1; i <= right; i++)
			{
				int num = this.indexes[i];
				int num2 = i;
				while (num2 > left && this.CompareItems(num, this.indexes[num2 - 1]) < 0)
				{
					this.indexes[num2] = this.indexes[num2 - 1];
					num2--;
				}
				this.indexes[num2] = num;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		private void Swap(int left, int right)
		{
			int num = this.indexes[right];
			this.indexes[right] = this.indexes[left];
			this.indexes[left] = num;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F714 File Offset: 0x0000D914
		public static IEnumerable<TElement> Sort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			QuickSort<TElement> sorter = new QuickSort<TElement>(source, context);
			sorter.PerformSort();
			for (int i = 0; i < sorter.indexes.Length; i++)
			{
				yield return sorter.elements[sorter.indexes[i]];
			}
			yield break;
		}

		// Token: 0x040000A8 RID: 168
		private TElement[] elements;

		// Token: 0x040000A9 RID: 169
		private int[] indexes;

		// Token: 0x040000AA RID: 170
		private SortContext<TElement> context;
	}
}
