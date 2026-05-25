using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Represents a collection of objects that have a common key.</summary>
	/// <typeparam name="TKey">The type of the key of the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the values in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000025 RID: 37
	public interface IGrouping<TKey, TElement> : IEnumerable, IEnumerable<TElement>
	{
		/// <summary>Gets the key of the <see cref="T:System.Linq.IGrouping`2" />.</summary>
		/// <returns>The key of the <see cref="T:System.Linq.IGrouping`2" />.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002CB RID: 715
		TKey Key { get; }
	}
}
