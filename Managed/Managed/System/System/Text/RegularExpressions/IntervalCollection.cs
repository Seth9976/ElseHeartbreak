using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000482 RID: 1154
	internal class IntervalCollection : ICollection, IEnumerable
	{
		// Token: 0x0600293D RID: 10557 RVA: 0x00088234 File Offset: 0x00086434
		public IntervalCollection()
		{
			this.intervals = new ArrayList();
		}

		// Token: 0x17000B78 RID: 2936
		public Interval this[int i]
		{
			get
			{
				return (Interval)this.intervals[i];
			}
			set
			{
				this.intervals[i] = value;
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x00088270 File Offset: 0x00086470
		public void Add(Interval i)
		{
			this.intervals.Add(i);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00088284 File Offset: 0x00086484
		public void Clear()
		{
			this.intervals.Clear();
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00088294 File Offset: 0x00086494
		public void Sort()
		{
			this.intervals.Sort();
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000882A4 File Offset: 0x000864A4
		public void Normalize()
		{
			this.intervals.Sort();
			int i = 0;
			while (i < this.intervals.Count - 1)
			{
				Interval interval = (Interval)this.intervals[i];
				Interval interval2 = (Interval)this.intervals[i + 1];
				if (!interval.IsDisjoint(interval2) || interval.IsAdjacent(interval2))
				{
					interval.Merge(interval2);
					this.intervals[i] = interval;
					this.intervals.RemoveAt(i + 1);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00088348 File Offset: 0x00086548
		public IntervalCollection GetMetaCollection(IntervalCollection.CostDelegate cost_del)
		{
			IntervalCollection intervalCollection = new IntervalCollection();
			this.Normalize();
			this.Optimize(0, this.Count - 1, intervalCollection, cost_del);
			intervalCollection.intervals.Sort();
			return intervalCollection;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x00088380 File Offset: 0x00086580
		private void Optimize(int begin, int end, IntervalCollection meta, IntervalCollection.CostDelegate cost_del)
		{
			Interval interval;
			interval.contiguous = false;
			int num = -1;
			int num2 = -1;
			double num3 = 0.0;
			for (int i = begin; i <= end; i++)
			{
				interval.low = this[i].low;
				double num4 = 0.0;
				for (int j = i; j <= end; j++)
				{
					interval.high = this[j].high;
					num4 += cost_del(this[j]);
					double num5 = cost_del(interval);
					if (num5 < num4 && num4 > num3)
					{
						num = i;
						num2 = j;
						num3 = num4;
					}
				}
			}
			if (num < 0)
			{
				for (int k = begin; k <= end; k++)
				{
					meta.Add(this[k]);
				}
			}
			else
			{
				interval.low = this[num].low;
				interval.high = this[num2].high;
				meta.Add(interval);
				if (num > begin)
				{
					this.Optimize(begin, num - 1, meta, cost_del);
				}
				if (num2 < end)
				{
					this.Optimize(num2 + 1, end, meta, cost_del);
				}
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000884D0 File Offset: 0x000866D0
		public int Count
		{
			get
			{
				return this.intervals.Count;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000884E0 File Offset: 0x000866E0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000884E4 File Offset: 0x000866E4
		public object SyncRoot
		{
			get
			{
				return this.intervals;
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000884EC File Offset: 0x000866EC
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this.intervals)
			{
				Interval interval = (Interval)obj;
				if (index > array.Length)
				{
					break;
				}
				array.SetValue(interval, index++);
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00088578 File Offset: 0x00086778
		public IEnumerator GetEnumerator()
		{
			return new IntervalCollection.Enumerator(this.intervals);
		}

		// Token: 0x040019F0 RID: 6640
		private ArrayList intervals;

		// Token: 0x02000483 RID: 1155
		private class Enumerator : IEnumerator
		{
			// Token: 0x0600294B RID: 10571 RVA: 0x00088588 File Offset: 0x00086788
			public Enumerator(IList list)
			{
				this.list = list;
				this.Reset();
			}

			// Token: 0x17000B7C RID: 2940
			// (get) Token: 0x0600294C RID: 10572 RVA: 0x000885A0 File Offset: 0x000867A0
			public object Current
			{
				get
				{
					if (this.ptr >= this.list.Count)
					{
						throw new InvalidOperationException();
					}
					return this.list[this.ptr];
				}
			}

			// Token: 0x0600294D RID: 10573 RVA: 0x000885D0 File Offset: 0x000867D0
			public bool MoveNext()
			{
				if (this.ptr > this.list.Count)
				{
					throw new InvalidOperationException();
				}
				return ++this.ptr < this.list.Count;
			}

			// Token: 0x0600294E RID: 10574 RVA: 0x00088618 File Offset: 0x00086818
			public void Reset()
			{
				this.ptr = -1;
			}

			// Token: 0x040019F1 RID: 6641
			private IList list;

			// Token: 0x040019F2 RID: 6642
			private int ptr;
		}

		// Token: 0x020004ED RID: 1261
		// (Invoke) Token: 0x06002C60 RID: 11360
		public delegate double CostDelegate(Interval i);
	}
}
