using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200002D RID: 45
	internal abstract class OrderedEnumerable<TElement> : IEnumerable, IEnumerable<TElement>, IOrderedEnumerable<TElement>
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000C664 File Offset: 0x0000A864
		protected OrderedEnumerable(IEnumerable<TElement> source)
		{
			this.source = source;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C674 File Offset: 0x0000A874
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C67C File Offset: 0x0000A87C
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.Sort(this.source).GetEnumerator();
		}

		// Token: 0x060002DD RID: 733
		public abstract SortContext<TElement> CreateContext(SortContext<TElement> current);

		// Token: 0x060002DE RID: 734
		protected abstract IEnumerable<TElement> Sort(IEnumerable<TElement> source);

		// Token: 0x060002DF RID: 735 RVA: 0x0000C690 File Offset: 0x0000A890
		public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> selector, IComparer<TKey> comparer, bool descending)
		{
			return new OrderedSequence<TElement, TKey>(this, this.source, selector, comparer, (!descending) ? SortDirection.Ascending : SortDirection.Descending);
		}

		// Token: 0x040000A3 RID: 163
		private IEnumerable<TElement> source;
	}
}
