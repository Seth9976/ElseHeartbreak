using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a conditional operator.</summary>
	// Token: 0x02000036 RID: 54
	public sealed class ConditionalExpression : Expression
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00010C4C File Offset: 0x0000EE4C
		internal ConditionalExpression(Expression test, Expression if_true, Expression if_false)
			: base(ExpressionType.Conditional, if_true.Type)
		{
			this.test = test;
			this.if_true = if_true;
			this.if_false = if_false;
		}

		/// <summary>Gets the test of the conditional operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the test of the conditional operation.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public Expression Test
		{
			get
			{
				return this.test;
			}
		}

		/// <summary>Gets the expression to execute if the test evaluates to true.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is true.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00010C84 File Offset: 0x0000EE84
		public Expression IfTrue
		{
			get
			{
				return this.if_true;
			}
		}

		/// <summary>Gets the expression to execute if the test evaluates to false.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is false.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public Expression IfFalse
		{
			get
			{
				return this.if_false;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010C94 File Offset: 0x0000EE94
		internal override void Emit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			this.test.Emit(ec);
			ig.Emit(OpCodes.Brfalse, label);
			this.if_true.Emit(ec);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			this.if_false.Emit(ec);
			ig.MarkLabel(label2);
		}

		// Token: 0x040000B9 RID: 185
		private Expression test;

		// Token: 0x040000BA RID: 186
		private Expression if_true;

		// Token: 0x040000BB RID: 187
		private Expression if_false;
	}
}
