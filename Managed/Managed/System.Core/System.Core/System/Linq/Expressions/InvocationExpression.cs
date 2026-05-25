using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that applies a delegate or lambda expression to a list of argument expressions.</summary>
	// Token: 0x02000042 RID: 66
	public sealed class InvocationExpression : Expression
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00013880 File Offset: 0x00011A80
		internal InvocationExpression(Expression expression, Type type, ReadOnlyCollection<Expression> arguments)
			: base(ExpressionType.Invoke, type)
		{
			this.expression = expression;
			this.arguments = arguments;
		}

		/// <summary>Gets the delegate or lambda expression to be applied.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate to be applied.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001389C File Offset: 0x00011A9C
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		/// <summary>Gets the arguments that the delegate is applied to.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments that the delegate is applied to.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000138A4 File Offset: 0x00011AA4
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000138AC File Offset: 0x00011AAC
		internal override void Emit(EmitContext ec)
		{
			ec.EmitCall(this.expression, this.arguments, this.expression.Type.GetInvokeMethod());
		}

		// Token: 0x04000100 RID: 256
		private Expression expression;

		// Token: 0x04000101 RID: 257
		private ReadOnlyCollection<Expression> arguments;
	}
}
