using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200002E RID: 46
	internal class OrderedSequence<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		internal OrderedSequence(IEnumerable<TElement> source, Func<TElement, TKey> key_selector, IComparer<TKey> comparer, SortDirection direction)
			: base(source)
		{
			this.selector = key_selector;
			this.comparer = comparer ?? Comparer<TKey>.Default;
			this.direction = direction;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		internal OrderedSequence(OrderedEnumerable<TElement> parent, IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, SortDirection direction)
			: this(source, keySelector, comparer, direction)
		{
			this.parent = parent;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public override SortContext<TElement> CreateContext(SortContext<TElement> current)
		{
			SortContext<TElement> sortContext = new SortSequenceContext<TElement, TKey>(this.selector, this.comparer, this.direction, current);
			if (this.parent != null)
			{
				return this.parent.CreateContext(sortContext);
			}
			return sortContext;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C734 File Offset: 0x0000A934
		protected override IEnumerable<TElement> Sort(IEnumerable<TElement> source)
		{
			return QuickSort<TElement>.Sort(source, this.CreateContext(null));
		}

		// Token: 0x040000A4 RID: 164
		private OrderedEnumerable<TElement> parent;

		// Token: 0x040000A5 RID: 165
		private Func<TElement, TKey> selector;

		// Token: 0x040000A6 RID: 166
		private IComparer<TKey> comparer;

		// Token: 0x040000A7 RID: 167
		private SortDirection direction;
	}
}
