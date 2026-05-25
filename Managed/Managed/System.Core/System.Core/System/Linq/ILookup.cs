using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Defines an indexer, size property, and Boolean search method for data structures that map keys to <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences of values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences that make up the values in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002C RID: 44
	public interface ILookup<TKey, TElement> : IEnumerable, IEnumerable<IGrouping<TKey, TElement>>
	{
		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002D7 RID: 727
		int Count { get; }

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by a specified key.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by the specified key.</returns>
		/// <param name="key">The key of the desired sequence of values.</param>
		// Token: 0x1700002D RID: 45
		IEnumerable<TElement> this[TKey key] { get; }

		/// <summary>Determines whether a specified key exists in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <returns>true if <paramref name="key" /> is in the <see cref="T:System.Linq.ILookup`2" />; otherwise, false.</returns>
		/// <param name="key">The key to search for in the <see cref="T:System.Linq.ILookup`2" />.</param>
		// Token: 0x060002D9 RID: 729
		bool Contains(TKey key);
	}
}
