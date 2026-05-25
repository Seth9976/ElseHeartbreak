using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000022 RID: 34
	internal class QueryableEnumerable<TElement> : IQueryableEnumerable, IEnumerable, IOrderedQueryable, IQueryable, IQueryProvider, IEnumerable<TElement>, IQueryableEnumerable<TElement>, IQueryable<TElement>, IOrderedQueryable<TElement>
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000BF40 File Offset: 0x0000A140
		public QueryableEnumerable(IEnumerable<TElement> enumerable)
		{
			this.expression = Expression.Constant(this);
			this.enumerable = enumerable;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public QueryableEnumerable(Expression expression)
		{
			this.expression = expression;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000BF6C File Offset: 0x0000A16C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000BF74 File Offset: 0x0000A174
		public Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BF80 File Offset: 0x0000A180
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000BF88 File Offset: 0x0000A188
		public IQueryProvider Provider
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public IEnumerable GetEnumerable()
		{
			return this.enumerable;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BF94 File Offset: 0x0000A194
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.Execute<IEnumerable<TElement>>(this.expression).GetEnumerator();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public IQueryable CreateQuery(Expression expression)
		{
			return (IQueryable)Activator.CreateInstance(typeof(QueryableEnumerable<>).MakeGenericType(new Type[] { expression.Type.GetFirstGenericArgument() }), new object[] { expression });
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		public object Execute(Expression expression)
		{
			LambdaExpression lambdaExpression = Expression.Lambda(QueryableEnumerable<TElement>.TransformQueryable(expression), new ParameterExpression[0]);
			return lambdaExpression.Compile().DynamicInvoke(new object[0]);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C01C File Offset: 0x0000A21C
		private static Expression TransformQueryable(Expression expression)
		{
			return new QueryableTransformer().Transform(expression);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C02C File Offset: 0x0000A22C
		public IQueryable<TElem> CreateQuery<TElem>(Expression expression)
		{
			return new QueryableEnumerable<TElem>(expression);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C034 File Offset: 0x0000A234
		public TResult Execute<TResult>(Expression expression)
		{
			Expression<Func<TResult>> expression2 = Expression.Lambda<Func<TResult>>(QueryableEnumerable<TElement>.TransformQueryable(expression), new ParameterExpression[0]);
			return expression2.Compile()();
		}

		// Token: 0x0400009D RID: 157
		private Expression expression;

		// Token: 0x0400009E RID: 158
		private IEnumerable<TElement> enumerable;
	}
}
