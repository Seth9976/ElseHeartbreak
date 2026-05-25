using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000481 RID: 1153
	internal struct Interval : IComparable
	{
		// Token: 0x0600292C RID: 10540 RVA: 0x00087DD0 File Offset: 0x00085FD0
		public Interval(int low, int high)
		{
			if (low > high)
			{
				int num = low;
				low = high;
				high = num;
			}
			this.low = low;
			this.high = high;
			this.contiguous = true;
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00087E04 File Offset: 0x00086004
		public static Interval Empty
		{
			get
			{
				Interval interval;
				interval.low = 0;
				interval.high = interval.low - 1;
				interval.contiguous = true;
				return interval;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x00087E34 File Offset: 0x00086034
		public static Interval Entire
		{
			get
			{
				return new Interval(int.MinValue, int.MaxValue);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x00087E48 File Offset: 0x00086048
		public bool IsDiscontiguous
		{
			get
			{
				return !this.contiguous;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x00087E54 File Offset: 0x00086054
		public bool IsSingleton
		{
			get
			{
				return this.contiguous && this.low == this.high;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x00087E74 File Offset: 0x00086074
		public bool IsRange
		{
			get
			{
				return !this.IsSingleton && !this.IsEmpty;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x00087E90 File Offset: 0x00086090
		public bool IsEmpty
		{
			get
			{
				return this.low > this.high;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x00087EA0 File Offset: 0x000860A0
		public int Size
		{
			get
			{
				if (this.IsEmpty)
				{
					return 0;
				}
				return this.high - this.low + 1;
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x00087EC0 File Offset: 0x000860C0
		public bool IsDisjoint(Interval i)
		{
			return this.IsEmpty || i.IsEmpty || (this.low > i.high || i.low > this.high);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00087F10 File Offset: 0x00086110
		public bool IsAdjacent(Interval i)
		{
			return !this.IsEmpty && !i.IsEmpty && (this.low == i.high + 1 || this.high == i.low - 1);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x00087F60 File Offset: 0x00086160
		public bool Contains(Interval i)
		{
			return (!this.IsEmpty && i.IsEmpty) || (!this.IsEmpty && this.low <= i.low && i.high <= this.high);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x00087FBC File Offset: 0x000861BC
		public bool Contains(int i)
		{
			return this.low <= i && i <= this.high;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00087FDC File Offset: 0x000861DC
		public bool Intersects(Interval i)
		{
			return !this.IsEmpty && !i.IsEmpty && ((this.Contains(i.low) && !this.Contains(i.high)) || (this.Contains(i.high) && !this.Contains(i.low)));
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00088050 File Offset: 0x00086250
		public void Merge(Interval i)
		{
			if (i.IsEmpty)
			{
				return;
			}
			if (this.IsEmpty)
			{
				this.low = i.low;
				this.high = i.high;
			}
			if (i.low < this.low)
			{
				this.low = i.low;
			}
			if (i.high > this.high)
			{
				this.high = i.high;
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000880D0 File Offset: 0x000862D0
		public void Intersect(Interval i)
		{
			if (this.IsDisjoint(i))
			{
				this.low = 0;
				this.high = this.low - 1;
				return;
			}
			if (i.low > this.low)
			{
				this.low = i.low;
			}
			if (i.high > this.high)
			{
				this.high = i.high;
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x00088140 File Offset: 0x00086340
		public int CompareTo(object o)
		{
			return this.low - ((Interval)o).low;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00088164 File Offset: 0x00086364
		public new string ToString()
		{
			if (this.IsEmpty)
			{
				return "(EMPTY)";
			}
			if (!this.contiguous)
			{
				return string.Concat(new object[] { "{", this.low, ", ", this.high, "}" });
			}
			if (this.IsSingleton)
			{
				return "(" + this.low + ")";
			}
			return string.Concat(new object[] { "(", this.low, ", ", this.high, ")" });
		}

		// Token: 0x040019ED RID: 6637
		public int low;

		// Token: 0x040019EE RID: 6638
		public int high;

		// Token: 0x040019EF RID: 6639
		public bool contiguous;
	}
}
