using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Represents the result of a sorting operation.</summary>
	/// <typeparam name="T">The type of the content of the data source.</typeparam>
	// Token: 0x02000027 RID: 39
	public interface IOrderedQueryable<T> : IEnumerable, IOrderedQueryable, IQueryable, IQueryable<T>, IEnumerable<T>
	{
	}
}
