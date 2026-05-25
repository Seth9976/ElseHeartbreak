using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000029 RID: 41
	public interface IQueryable : IEnumerable
	{
		/// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002CD RID: 717
		Type ElementType { get; }

		/// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002CE RID: 718
		Expression Expression { get; }

		/// <summary>Gets the query provider that is associated with this data source.</summary>
		/// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002CF RID: 719
		IQueryProvider Provider { get; }
	}
}
