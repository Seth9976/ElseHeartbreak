using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call that has a collection initializer.</summary>
	// Token: 0x02000043 RID: 67
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000138DC File Offset: 0x00011ADC
		internal ListInitExpression(NewExpression new_expression, ReadOnlyCollection<ElementInit> initializers)
			: base(ExpressionType.ListInit, new_expression.Type)
		{
			this.new_expression = new_expression;
			this.initializers = initializers;
		}

		/// <summary>Gets the expression that contains a call to the constructor of a collection type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the call to the constructor of a collection type.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000138FC File Offset: 0x00011AFC
		public NewExpression NewExpression
		{
			get
			{
				return this.new_expression;
			}
		}

		/// <summary>Gets the element initializers that are used to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects which represent the elements that are used to initialize the collection.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00013904 File Offset: 0x00011B04
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001390C File Offset: 0x00011B0C
		internal override void Emit(EmitContext ec)
		{
			LocalBuilder localBuilder = ec.EmitStored(this.new_expression);
			ec.EmitCollection(this.initializers, localBuilder);
			ec.EmitLoad(localBuilder);
		}

		// Token: 0x04000102 RID: 258
		private NewExpression new_expression;

		// Token: 0x04000103 RID: 259
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
