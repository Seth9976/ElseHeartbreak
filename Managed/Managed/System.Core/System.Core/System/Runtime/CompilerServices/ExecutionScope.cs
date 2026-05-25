using System;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000017 RID: 23
	public class ExecutionScope
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00008314 File Offset: 0x00006514
		private ExecutionScope(CompilationContext context, int compilation_unit)
		{
			this.context = context;
			this.compilation_unit = compilation_unit;
			this.Globals = context.GetGlobals();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008344 File Offset: 0x00006544
		internal ExecutionScope(CompilationContext context)
			: this(context, 0)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008350 File Offset: 0x00006550
		internal ExecutionScope(CompilationContext context, int compilation_unit, ExecutionScope parent, object[] locals)
			: this(context, compilation_unit)
		{
			this.Parent = parent;
			this.Locals = locals;
		}

		/// <summary>Creates a delegate that can be used to execute a dynamically generated method.</summary>
		/// <returns>A <see cref="T:System.Delegate" /> that can execute a dynamically generated method.</returns>
		/// <param name="indexLambda">The index of the object that stores information about associated lambda expression of the dynamic method.</param>
		/// <param name="locals">An array that contains the hoisted local variables from the parent context.</param>
		// Token: 0x0600015F RID: 351 RVA: 0x0000836C File Offset: 0x0000656C
		public Delegate CreateDelegate(int indexLambda, object[] locals)
		{
			return this.context.CreateDelegate(indexLambda, new ExecutionScope(this.context, indexLambda, this, locals));
		}

		/// <summary>Creates an array to store the hoisted local variables.</summary>
		/// <returns>An array to store hoisted local variables.</returns>
		// Token: 0x06000160 RID: 352 RVA: 0x00008388 File Offset: 0x00006588
		public object[] CreateHoistedLocals()
		{
			return this.context.CreateHoistedLocals(this.compilation_unit);
		}

		/// <summary>Frees a specified expression tree of external parameter references by replacing the parameter with its current value.</summary>
		/// <returns>An expression tree that does not contain external parameter references.</returns>
		/// <param name="expression">An expression tree to free of external parameter references.</param>
		/// <param name="locals">An array that contains the hoisted local variables.</param>
		// Token: 0x06000161 RID: 353 RVA: 0x0000839C File Offset: 0x0000659C
		public Expression IsolateExpression(Expression expression, object[] locals)
		{
			return this.context.IsolateExpression(this, locals, expression);
		}

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x0400005D RID: 93
		public object[] Globals;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x0400005E RID: 94
		public object[] Locals;

		/// <summary>Represents the execution scope of the calling delegate.</summary>
		// Token: 0x0400005F RID: 95
		public ExecutionScope Parent;

		// Token: 0x04000060 RID: 96
		internal CompilationContext context;

		// Token: 0x04000061 RID: 97
		internal int compilation_unit;
	}
}
